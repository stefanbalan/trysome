using ClashOfLogs.Shared;
using WarClan = ClashOfLogs.Shared.WarClan;

namespace CoL.Service.Mappers;

internal class WarSummaryMapper : BaseMapper<DBWar, WarSummary>
{
    public override DBWar CreateEntity(WarSummary entity, DateTime timeStamp) =>
        base.CreateEntity(entity, timeStamp) with
        {
            Clan = new DBWarClan { Tag = entity.Clan.Tag },
            Opponent = new DBWarClan { Tag = entity.Opponent.Tag },
            EndTime = entity.EndTime
        };


    public override void UpdateEntity(DBWar entity, WarSummary model, DateTime timeStamp)
    {
        base.UpdateEntity(entity, model, timeStamp);

        entity.Result = model.Result;
        entity.TeamSize = model.TeamSize;
        entity.AttacksPerMember = model.AttacksPerMember;

        UpdateWarClan(entity.Clan, model.Clan);
        UpdateWarClan(entity.Opponent, model.Opponent);

        static void UpdateWarClan(DBWarClan warClan, WarClan clan)
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
