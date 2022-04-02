using ClashOfLogs.Shared;
using CoL.DB.mssql;
using Microsoft.EntityFrameworkCore;
using DBClan = CoL.DB.Entities.Clan;

namespace CoL.Service.Importer
{
    internal class ClanImporter : EntityImporter<DBClan, Clan, string>
    {
        public ClanImporter(CoLContext context) : base(context)
        {
        }

        public override Func<Clan, string> GetKey => (clan) => clan.Tag;

        public override async Task<DBClan> CreateNewAsync(Clan entity, DateTime dateTime)
        {
            var dbclan = new DBClan
            {
                Tag = entity.Tag,
            };

            await context.Clans.AddAsync(dbclan);
            return dbclan;
        }

        public override async Task<DBClan> FindExistingAsync(Clan entity)
        {
            var dbEntity = await context.Clans.Include(clan => clan.Members)
                .FirstOrDefaultAsync(clan => clan.Tag == entity.Tag);

            return dbEntity;
        }

        public override DbSet<DBClan> GetDbSet() => context.Clans;

        public override void UpdateChildren(DBClan tDBEntity, Clan entity, DateTime dateTime)
        {
            foreach (var m in tDBEntity.Members)
            {

            }
        }

        public override void UpdateProperties(DBClan tDBEntity, Clan entity, DateTime dateTime)
        {
            tDBEntity.Name = entity.Name;
            tDBEntity.Type = entity.Type;
            tDBEntity.Description = entity.Description;
            tDBEntity.ClanLevel = entity.ClanLevel;
            tDBEntity.ClanPoints = entity.ClanPoints;
            tDBEntity.ClanVersusPoints = entity.ClanVersusPoints;
            tDBEntity.RequiredTrophies = entity.RequiredTrophies;
            tDBEntity.WarFrequency = entity.WarFrequency;
            tDBEntity.WarWinStreak = entity.WarWinStreak;
            tDBEntity.WarWins = entity.WarWins;
            tDBEntity.WarTies = entity.WarTies;
            tDBEntity.WarLosses = entity.WarLosses;
            tDBEntity.IsWarLogPublic = entity.IsWarLogPublic;
            tDBEntity.RequiredVersusTrophies = entity.RequiredVersusTrophies;
            tDBEntity.RequiredTownhallLevel = entity.RequiredTownhallLevel;

            tDBEntity.UpdatedAt = dateTime;
        }
    }
}
