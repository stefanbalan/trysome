using ClashOfLogs.Shared;

namespace CoL.Service.Mappers;

public class ClanMapper : BaseMapper<DBClan, Clan>
{
    public ClanMapper()
    {
        MapT2ToT1(c => c.Tag, dbc => dbc.Tag);
        MapT2ToT1(c => c.Name, dbc => dbc.Name);
        MapT2ToT1(c => c.Type, dbc => dbc.Type);
        MapT2ToT1(c => c.Description, dbc => dbc.Description);
        MapT2ToT1(c => c.ClanLevel, dbc => dbc.ClanLevel);

        MapT2ToT1(c => c.ClanPoints, dbc => dbc.ClanPoints);
        MapT2ToT1(c => c.ClanVersusPoints, dbc => dbc.ClanVersusPoints);
        MapT2ToT1(c => c.RequiredTrophies, dbc => dbc.RequiredTrophies);
        MapT2ToT1(c => c.WarFrequency, dbc => dbc.WarFrequency);
        MapT2ToT1(c => c.WarWinStreak, dbc => dbc.WarWinStreak);
        MapT2ToT1(c => c.WarWins, dbc => dbc.WarWins);
        MapT2ToT1(c => c.WarTies, dbc => dbc.WarTies);
        MapT2ToT1(c => c.WarLosses, dbc => dbc.WarLosses);
        MapT2ToT1(c => c.IsWarLogPublic, dbc => dbc.IsWarLogPublic);

        MapT2ToT1(c => c.RequiredVersusTrophies, dbc => dbc.RequiredVersusTrophies);
        MapT2ToT1(c => c.RequiredTownhallLevel, dbc => dbc.RequiredTownhallLevel);

        MapT2ToT1(c => GetBadgeUrls(c.BadgeUrls), dbc => dbc.BadgeUrls);
    }

    private DBBadgeUrls GetBadgeUrls(BadgeUrls badgeUrls)
        => new()
        {
            Small = badgeUrls.Small,
            Medium = badgeUrls.Medium,
            Large = badgeUrls.Large
        };

    // public override DBClan CreateAndUpdateEntity(Clan entity, DateTime timeStamp) =>
    //     base.CreateAndUpdateEntity(entity, timeStamp) with
    //     {
    //         Tag = entity.Tag,
    //     };
    //
    // public override bool UpdateEntity(DBClan entity, Clan model, DateTime timeStamp)
    // {
    //     base.UpdateEntity(entity, model, timeStamp);
    //     entity.Name = model.Name;
    //     entity.Type = model.Type;
    //     entity.Description = model.Description;
    //     entity.ClanLevel = model.ClanLevel;
    //     entity.ClanPoints = model.ClanPoints;
    //     entity.ClanVersusPoints = model.ClanVersusPoints;
    //     entity.RequiredTrophies = model.RequiredTrophies;
    //     entity.WarFrequency = model.WarFrequency;
    //     entity.WarWinStreak = model.WarWinStreak;
    //     entity.WarWins = model.WarWins;
    //     entity.WarTies = model.WarTies;
    //     entity.WarLosses = model.WarLosses;
    //     entity.IsWarLogPublic = model.IsWarLogPublic;
    //     entity.RequiredVersusTrophies = model.RequiredVersusTrophies;
    //     entity.RequiredTownhallLevel = model.RequiredTownhallLevel;
    //
    //     entity.BadgeUrls =
    //
    //     return true;
    // }
}