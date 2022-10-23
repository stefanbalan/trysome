using ClashOfLogs.Shared;

using CoL.DB.mssql;
using CoL.Service.Mappers;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System.Linq;

namespace CoL.Service.Importer
{
    internal class ClanImporter : EntityImporter<DBClan, Clan, string>
    {
        private readonly IMapper<DBClan, Clan> clanMapper;
        private readonly IMapper<DB.Entities.Member, Member> memberMapper;
        private readonly EntityProviderBase<DBMember, string, Member> memberProvider;

        public ClanImporter(
            CoLContext context,
            IMapper<DBClan, Clan> clanMapper,
            IMapper<DBMember, Member> memberMapper,
            ILogger<ClanImporter> logger,
            EntityProviderBase<DBMember, string, Member> memberProvider) : base(context, logger)
        {
            this.clanMapper = clanMapper;
            this.memberMapper = memberMapper;
            this.memberProvider = memberProvider;
        }

        protected override Func<Clan, string> GetKey => (clan) => clan.Tag;


        protected async override Task<DBClan> FindExistingAsync(Clan entity)
        {
            var dbEntity = await context.Clans
                .Include(clan => clan.Members.Where(member => member.IsMember))
                .FirstOrDefaultAsync(clan => clan.Tag == entity.Tag);

            return dbEntity;
        }

        protected override DbSet<DBClan> GetDbSet() => context.Clans;

        protected async override Task UpdateChildrenAsync(DBClan dbClan, Clan clan, DateTime timeStamp)
        {
            foreach (var member in clan.Members)
                await memberProvider.GetOrCreateAsync(member, timeStamp);
        }

        protected async override Task<DBClan> CreateNewAsync(Clan entity, DateTime timeStamp)
        {
            var dbClan = clanMapper.CreateEntity(entity, timeStamp);
            await context.Clans.AddAsync(dbClan);
            return dbClan;
        }

        protected override void UpdateProperties(DBClan entity, Clan model, DateTime timeStamp)
            => clanMapper.UpdateEntityAsync(entity, model, timeStamp);
    }
}
