using ClashOfLogs.Shared;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importers;

public class LeagueImporter : EntityImporter<DBLeague, League>
{
    public LeagueImporter(
        IMapper<DBLeague, League> mapper,
        IRepository<DBLeague> repository,
        ILogger<IEntityImporter<DBLeague, League>> logger)
        : base(mapper, repository, logger)
    {
        PersistChangesAfterImport = true;
    }

    public override object?[] EntityKey(League entity) => new object?[] { entity.Id };

    public async override Task UpdateChildrenAsync(DBLeague dbEntity, League entity, DateTime timestamp)
        => await Task.CompletedTask;
}