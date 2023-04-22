using ClashOfLogs.Shared;
using CoL.DB;
using CoL.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoL.Service.Importer;

internal class WarLogImporter : EntityImporter<DBWar, WarSummary>
{
    private readonly IMapper<DBWar, WarSummary> warSummaryMapper;

    public WarLogImporter(
        CoLContext context,
        ILogger<EntityImporter<DBWar, WarSummary>> logger,
        IMapper<DBWar, WarSummary> warSummaryMapper)
        : base(context, logger)
    {
        this.warSummaryMapper = warSummaryMapper;
    }

    protected async override Task<DBWar?> FindExistingAsync(WarSummary entity)
    {
        var dbEntity = await Context.Set<DBWar>()
            .Include(ws => ws.Clan)
            .Include(ws => ws.Opponent)
            .FirstOrDefaultAsync(w => w.Clan.Tag == entity.Clan.Tag && w.Opponent.Tag == entity.Clan.Tag);

        return dbEntity;
    }

    protected async override Task<DBWar> CreateNewAsync(WarSummary entity, DateTime dateTime)
    {
        var dbWar = warSummaryMapper.CreateEntity(entity, dateTime);
        await Context.Wars.AddAsync(dbWar);
        return dbWar;
    }

    protected async override Task UpdateProperties(DBWar tDbEntity, WarSummary entity, DateTime dateTime) =>
        await warSummaryMapper.UpdateEntityAsync(tDbEntity, entity, dateTime);

    protected override Task UpdateChildrenAsync(DBWar tDbEntity, WarSummary entity, DateTime dateTime) =>
        Task.CompletedTask;
}