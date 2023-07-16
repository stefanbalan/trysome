namespace CoL.Service.Validators;

public class WarValidator : IValidator<DBWar>
{
    public bool IsValid(DBWar entity) => !string.IsNullOrWhiteSpace(entity.Clan.Name) &&
                                         !string.IsNullOrWhiteSpace(entity.Opponent.Name);
}