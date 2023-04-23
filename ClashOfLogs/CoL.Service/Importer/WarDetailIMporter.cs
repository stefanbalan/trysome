using ClashOfLogs.Shared;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importer;

internal class WarDetailIMporter : EntityImporter<DBWar, WarDetail>
{
    public WarDetailIMporter(IMapper<DBWar, WarDetail> mapper, IRepository<DBWar> repository, ILogger<EntityImporter<DBWar, WarDetail>> logger) : base(mapper, repository, logger)
    {
    }

    protected override object?[] EntityKey(WarDetail entity) => throw new NotImplementedException();

    protected override Task UpdateChildrenAsync(DBWar tDbEntity, WarDetail entity, DateTime timestamp) => throw new NotImplementedException();
}