using System.Linq;
using ClashOfLogs.Shared;
using CoL.DB;
using CoL.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importer;

internal class ClanImporter : EntityImporter<DBClan, Clan>
{
    private readonly IMapper<DBClan, Clan> clanMapper;
    private readonly IMapper<DBMember, Member> memberMapper;
    private readonly EntityProviderBase<DBMember, string, Member> memberProvider;

    public ClanImporter(
        CoLContext context,
        ILogger<ClanImporter> logger,
        IMapper<DBClan, Clan> clanMapper,
        IMapper<DBMember, Member> memberMapper,
        EntityProviderBase<DBMember, string, Member> memberProvider) : base(context, logger)
    {
        this.clanMapper = clanMapper;
        this.memberMapper = memberMapper;
        this.memberProvider = memberProvider;
    }

    protected async override Task<DBClan?> FindExistingAsync(Clan entity)
    {
        var dbEntity = await Context.Clans
            .Include(clan => clan.Members.Where(member => member.IsMember))
            .FirstOrDefaultAsync(clan => clan.Tag == entity.Tag);

        return dbEntity;
    }

    protected async override Task<DBClan> CreateNewAsync(Clan entity, DateTime timeStamp)
    {
        var dbClan = clanMapper.CreateEntity(entity, timeStamp);
        await Context.Clans.AddAsync(dbClan);
        return dbClan;
    }

    protected async override Task UpdateProperties(DBClan entity, Clan model, DateTime timeStamp)
        => await clanMapper.UpdateEntityAsync(entity, model, timeStamp);

    protected async override Task UpdateChildrenAsync(DBClan dbClan, Clan clan, DateTime timeStamp)
    {
        var previousMembers = dbClan.Members.ToList();
        dbClan.Members.Clear();

        foreach (var member in clan.Members)
        {
            var dbMember = await memberProvider.GetOrCreateAsync(member, timeStamp);
            dbClan.Members.Add(dbMember);
            previousMembers.RemoveAll(m => m.Tag.Equals(dbMember.Tag));
        }

        foreach (var pm in previousMembers)
        {
            pm.LastLeft = timeStamp;
            pm.IsMember = false;
        }
    }
}