using System.Globalization;
using ClashOfLogs.Shared;

namespace CoL.Service.Mappers;

internal class WarMapper : IMapper<DBWar, WarSummary>
{
    public DBWar CreateEntity(WarSummary entity, DateTime timeStamp)
    {
        // "endTime": "20210524T130613.000Z",
        if (!DateTime.TryParseExact(entity.EndTime, "yyyyMMddTHHmmss.000Z",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal, out var endTime))
            throw new FormatException($"{nameof(entity.EndTime)} cannot be parsed");

        return new DBWar
        {
            Clan = new DBWarClan
            {
                Tag = entity.Clan.Tag
            },
            Opponent = new DBWarClan
            {
                Tag = entity.Opponent.Tag
            },
            EndTime = endTime,
            CreatedAt = timeStamp
        };
    }


    public ValueTask UpdateEntityAsync(DBWar entity, WarSummary model, DateTime timeStamp)
    {
        entity.Result = model.Result;
        entity.TeamSize = model.TeamSize;
        entity.AttacksPerMember = model.AttacksPerMember;

        UpdateWarClan(entity.Clan, model.Clan);
        UpdateWarClan(entity.Opponent, model.Opponent);

        entity.UpdatedAt = timeStamp;
        return ValueTask.CompletedTask;

        void UpdateWarClan(DBWarClan warClan, WarClan clan)
        {
            warClan.Name = clan.Name;
            warClan.ClanLevel = clan.ClanLevel;
            warClan.Attacks = clan.Attacks;
            warClan.Stars = clan.Stars;
            warClan.DestructionPercentage = clan.DestructionPercentage;
            warClan.BadgeUrls = new DBBadgeUrls
            {
                Small = clan.BadgeUrls.Small,
                Medium = clan.BadgeUrls.Medium,
                Large = clan.BadgeUrls.Large
            };
        }
    }
}