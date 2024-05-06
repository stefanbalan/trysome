using ClashOfLogs.Shared;

namespace CoL.Service.Validators;

public class WarSummaryValidator : IValidator<WarSummary>
{
    public bool IsValid(WarSummary entity) =>
        !string.IsNullOrWhiteSpace(entity.Clan.Name) &&
        !string.IsNullOrWhiteSpace(entity.Opponent.Name);
}