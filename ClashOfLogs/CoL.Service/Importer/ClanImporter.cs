using ClashOfLogs.Shared;

using CoL.DB.mssql;
using CoL.Service.Mappers;

using Microsoft.EntityFrameworkCore;

using System.Linq;



namespace CoL.Service.Importer
{
    internal class ClanImporter : EntityImporter<DBClan, Clan, string>
    {
        private readonly IMapper<DBClan, Clan> clanMapper;

        public ClanImporter(
            CoLContext context,
            IMapper<DBClan, Clan> clanMapper
            ) : base(context)
        {
            this.clanMapper = clanMapper;
        }

        public override Func<Clan, string> GetKey => (clan) => clan.Tag;


        public override async Task<DBClan> FindExistingAsync(Clan entity)
        {
            var dbEntity = await context.Clans
                .Include(clan => clan.Members.Where(member => member.IsMember))
                .FirstOrDefaultAsync(clan => clan.Tag == entity.Tag);

            return dbEntity;
        }

        public override DbSet<DBClan> GetDbSet() => context.Clans;

        public override void UpdateChildren(DBClan tDBEntity, Clan entity, DateTime dateTime)
        {
            foreach (var member in tDBEntity.Members)
            {
                if (!entity.Members.Any(m => string.Equals(m.Tag, member.Tag, StringComparison.OrdinalIgnoreCase)))
                {
                    member.LastLeft = dateTime;
                    member.IsMember = false;
                }


            }
        }


        public override async Task<DBClan> CreateNewAsync(Clan entity, DateTime timeStamp)
        {
            var dbclan = clanMapper.CreateEntity(entity, timeStamp);
            await context.Clans.AddAsync(dbclan);
            return dbclan;
        }

        public override void UpdateProperties(DBClan entity, Clan model, DateTime timeStamp)
        {
            clanMapper.UpdateEntity(entity, model, timeStamp);
        }
    }
}
