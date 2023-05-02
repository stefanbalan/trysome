using ClashOfLogs.Shared;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importer;

internal class WarLogImporter : EntityImporter<DBWar, WarSummary>
{
    public WarLogImporter(
        IMapper<DBWar, WarSummary> mapper,
        IRepository<DBWar> repository,
        ILogger<EntityImporter<DBWar, WarSummary>> logger)
        : base(mapper, repository, logger)
    {
        PersistChangesAfterImport = true;
    }

    protected override object?[] EntityKey(WarSummary entity)
        // => throw new Exception($"Don't use {nameof(EntityKey)} to get the exisitng War");
        => new object?[] { entity.EndTime, entity.Clan.Tag, entity.Opponent.Tag };

    protected async override Task UpdateChildrenAsync(DBWar dbEntity, WarSummary entity, DateTime dateTime) =>
        await Task.CompletedTask;
}