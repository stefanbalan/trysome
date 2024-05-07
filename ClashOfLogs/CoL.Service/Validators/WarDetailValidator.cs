using ClashOfLogs.Shared;

namespace CoL.Service.Validators;

public class WarDetailValidator : IValidator<WarDetail>
{
    public bool IsValid(WarDetail entity) =>
        !string.Equals(entity.State, "notInWar") &&
        !string.IsNullOrWhiteSpace(entity.Clan.Tag) &&
        !string.IsNullOrWhiteSpace(entity.Clan.Name) &&
        !string.IsNullOrWhiteSpace(entity.Opponent.Tag) &&
        !string.IsNullOrWhiteSpace(entity.Opponent.Name);
}