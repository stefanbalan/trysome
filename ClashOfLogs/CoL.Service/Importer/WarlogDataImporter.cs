using ClashOfLogs.Shared;
using CoL.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importer;

internal class WarlogDataImporter : EntityImporter<DBWar, WarSummary, string>
{
    public WarlogDataImporter(CoLContext context, ILogger<EntityImporter<DBWar, WarSummary, string>> logger) : base(
        context, logger)
    {
    }

    protected async override Task<DBWar?> FindExistingAsync(WarSummary entity)
    {
        var dbEntity = await Context.Set<DBWar>()
            .Include(ws => ws.Clan)
            .Include(ws => ws.Opponent)
            .FirstOrDefaultAsync(w =>
                w.Clan.Tag == entity.Clan.Tag
                &&
                w.Opponent.Tag == entity.Clan.Tag);

        return dbEntity;
    }

    protected override Task<DBWar> CreateNewAsync(WarSummary entity, DateTime dateTime) =>
        throw new NotImplementedException();

    protected override void UpdateProperties(DBWar tDbEntity, WarSummary entity, DateTime dateTime) =>
        throw new NotImplementedException();

    protected override Task UpdateChildrenAsync(DBWar tDbEntity, WarSummary entity, DateTime dateTime) =>
        throw new NotImplementedException();
}