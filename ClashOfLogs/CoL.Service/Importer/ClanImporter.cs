using DBClan = CoL.DB.Entities.Clan;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClashOfLogs.Shared;
using CoL.DB.mssql;

namespace CoL.Service.Importer
{
    internal class ClanImporter : EntityImporter<CoLContext, DBClan, Clan>
    {
        public ClanImporter(CoLContext context) : base(context)
        {
        }

        public override DBClan CreateNew(Clan entity, DateTime dateTime)
        {
            var dbclan = new DBClan
            {
                Tag = entity.Tag,
            };

            context.Clans.Add(dbclan);
            return dbclan;
        }

        public override void UpdateChildren(DBClan tDBEntity, Clan entity, DateTime dateTime)
        {
            throw new NotImplementedException();
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
