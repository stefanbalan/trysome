using ClashOfLogs.Shared;
using WarClan = ClashOfLogs.Shared.WarClan;

namespace CoL.Service.Mappers;

internal class WarDetailMapper : BaseMapper<DBWar, WarDetail>
{
    public override DBWar CreateEntity(WarDetail entity, DateTime timeStamp)
    {
        var result = base.CreateEntity(entity, timeStamp);
        return result with
        {
            Clan = new DBWarClan { Tag = entity.Clan.Tag },
            Opponent = new DBWarClan { Tag = entity.Opponent.Tag },
            EndTime = entity.EndTime
        };
    }

    public override void UpdateEntity(DBWar entity, WarDetail model, DateTime timeStamp)
    {
        entity.State = model.State;
        entity.TeamSize = model.TeamSize;
        entity.AttacksPerMember = model.AttacksPerMember;

        entity.PreparationStartTime = model.PreparationStartTime;
        entity.StartTime = model.StartTime;
        entity.EndTime = model.EndTime;

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