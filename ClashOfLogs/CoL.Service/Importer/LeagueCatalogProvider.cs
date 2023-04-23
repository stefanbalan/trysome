using ClashOfLogs.Shared;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importer;

internal class LeagueCatalogProvider : EntityImporter<DBLeague, League>
{
    public LeagueCatalogProvider(
        IMapper<DBLeague, League> mapper,
        IRepository<DBLeague> repository,
        ILogger<EntityImporter<DBLeague, League>> logger)
        : base(mapper, repository, logger)
    {
    }

    protected override object?[] EntityKey(League entity) => new object?[] { entity.Id };

    protected override Task UpdateChildrenAsync(DBLeague tDbEntity, League entity, DateTime timestamp)
        => Task.CompletedTask;
}