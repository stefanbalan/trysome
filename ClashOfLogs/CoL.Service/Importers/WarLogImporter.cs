using ClashOfLogs.Shared;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using CoL.Service.Validators;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importers;

public class WarLogImporter : EntityImporter<DBWar, WarSummary>
{
    public WarLogImporter(
        IMapper<DBWar, WarSummary> mapper,
        IRepository<DBWar> repository,
        ILogger<IEntityImporter<DBWar, WarSummary>> logger,
        IValidator<WarSummary> validator)
        : base(mapper, repository, logger, validator)
    {
        PersistChangesAfterImport = true;
    }

    public override object?[] EntityKey(WarSummary entity)
        // => throw new Exception($"Don't use {nameof(EntityKey)} to get the exisitng War");
        => new object?[] { entity.EndTime, entity.Clan.Tag, entity.Opponent.Tag };

    public async override Task UpdateChildrenAsync(DBWar dbEntity, WarSummary entity, DateTime dateTime) =>
        await Task.CompletedTask;
}