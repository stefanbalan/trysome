using ClashOfLogs.Shared;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importer;

internal class LeagueImporter : EntityImporter<DBLeague, League>
{
    public LeagueImporter(
        IMapper<DBLeague, League> mapper,
        IRepository<DBLeague> repository,
        ILogger<EntityImporter<DBLeague, League>> logger)
        : base(mapper, repository, logger)
    {
    }

    protected override object?[] EntityKey(League entity) => new object?[] { entity.Id };

    protected async override Task UpdateChildrenAsync(DBLeague dbEntity, League entity, DateTime timestamp)
        => await Task.CompletedTask;
}