using System.Linq;
using CoL.DB;
using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Repository;

internal class WarRepository : IRepository<DBWar>
{
    private readonly CoLContext context;

    public WarRepository(CoLContext context)
    {
        this.context = context;
    }

    public async Task<DBWar?> GetByIdAsync(params object?[] keyValues)
        => await context.Wars
            .Include(w => w.ClanMembers)
            .Include(w => w.OpponentMembers)
            .Where(war => war.EndTime.Equals(keyValues[0])
                          && war.Clan.Tag == keyValues[1] as string
                          && war.Opponent.Tag == keyValues[2] as string)
            .SingleOrDefaultAsync();

    public async Task Add(DBWar entity)
        => await context.Wars.AddAsync(entity);
}