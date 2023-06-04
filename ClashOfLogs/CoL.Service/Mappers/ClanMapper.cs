using ClashOfLogs.Shared;

namespace CoL.Service.Mappers;

internal class ClanMapper : BaseMapper<DBClan, Clan>
{
    public override DBClan CreateEntity(Clan entity, DateTime timeStamp) =>
        base.CreateEntity(entity, timeStamp) with
        {
            Tag = entity.Tag,
        };

    public override bool UpdateEntity(DBClan entity, Clan model, DateTime timeStamp)
    {
        base.UpdateEntity(entity, model, timeStamp);
        entity.Name = model.Name;
        entity.Type = model.Type;
        entity.Description = model.Description;
        entity.ClanLevel = model.ClanLevel;
        entity.ClanPoints = model.ClanPoints;
        entity.ClanVersusPoints = model.ClanVersusPoints;
        entity.RequiredTrophies = model.RequiredTrophies;
        entity.WarFrequency = model.WarFrequency;
        entity.WarWinStreak = model.WarWinStreak;
        entity.WarWins = model.WarWins;
        entity.WarTies = model.WarTies;
        entity.WarLosses = model.WarLosses;
        entity.IsWarLogPublic = model.IsWarLogPublic;
        entity.RequiredVersusTrophies = model.RequiredVersusTrophies;
        entity.RequiredTownhallLevel = model.RequiredTownhallLevel;

        entity.BadgeUrls = new DBBadgeUrls
        {
            Small = model.BadgeUrls.Small,
            Medium = model.BadgeUrls.Medium,
            Large = model.BadgeUrls.Large
        };

        return true;
    }
}