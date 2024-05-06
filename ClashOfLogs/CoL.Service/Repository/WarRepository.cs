using System.Linq;
using CoL.DB;
using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Repository;

internal class WarRepository : BaseEFRepository<CoLContext, DBWar>
{
    public WarRepository(CoLContext context) : base(context)
    {
    }

    protected override DbSet<DBWar> EntitySet => Context.Wars;

    public async override ValueTask<DBWar?> GetByIdAsync(params object?[] keyValues)
        => await EntitySet
            .Include(w => w.ClanMembers)
            .Include(w => w.OpponentMembers)
            .Where(war => war.EndTime.Equals(keyValues[0])
                           //&& war.Clan != null
                           && war.Clan.Tag == keyValues[1] as string
                           // && war.Opponent != null
                           && war.Opponent.Tag == keyValues[2] as string)
            .SingleOrDefaultAsync();
}