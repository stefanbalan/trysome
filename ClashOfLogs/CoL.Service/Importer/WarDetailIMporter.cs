using ClashOfLogs.Shared;
using CoL.DB;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importer;
internal class WarDetailIMporter : EntityImporter<DBWar, WarDetail>
{
    public WarDetailIMporter(CoLContext context, ILogger<EntityImporter<DBWar, WarDetail>> logger) : base(context, logger)
    {
    }

    protected override Task<DBWar?> FindExistingAsync(WarDetail entity) => throw new NotImplementedException();

    protected override Task<DBWar> CreateNewAsync(WarDetail entity, DateTime dateTime) => throw new NotImplementedException();

    protected override Task UpdateProperties(DBWar tDbEntity, WarDetail entity, DateTime dateTime) => throw new NotImplementedException();

    protected override Task UpdateChildrenAsync(DBWar tDbEntity, WarDetail entity, DateTime dateTime) => throw new NotImplementedException();
}
