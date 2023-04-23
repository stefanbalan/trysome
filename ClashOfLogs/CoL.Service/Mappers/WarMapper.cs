using ClashOfLogs.Shared;

namespace CoL.Service.Mappers;

internal class WarMapper : IMapper<DBWar, WarSummary>
{
    public DBWar CreateEntity(WarSummary entity, DateTime timeStamp) =>
        new()
        {
            Clan = new DBWarClan
            {
                Tag = entity.Clan.Tag
            },
            Opponent = new DBWarClan
            {
                Tag = entity.Opponent.Tag
            },
            EndTime = entity.EndTime,
            CreatedAt = timeStamp
        };


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