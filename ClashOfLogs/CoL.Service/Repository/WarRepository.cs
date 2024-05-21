using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Repository;

internal class WarRepository(CoLContext context) : BaseEFRepository<CoLContext, DBWar>(context)
{
    protected override DbSet<DBWar> EntitySet => Context.Wars;

    public async override ValueTask<DBWar?> GetByIdAsync(params object?[] keyValues)
    {
        if (keyValues.Length != 3)
        {
            throw new ArgumentException("Invalid number of key values");
        }

        if (keyValues[0] is not DateTime || keyValues[1] == null || keyValues[2] == null)
        {
            throw new ArgumentException("Null key values");
        }

        var dt = ((DateTime)keyValues[0]!).Date; // ! is safe because of the check above

        return await EntitySet
            .Include(w => w.ClanMembers)
            .Include(w => w.OpponentMembers)
            .Where(war => // war.EndTime.Equals(keyValues[0])
                war.EndTime > dt && war.EndTime < dt.AddDays(1)
                                 && war.Clan.Tag == keyValues[1] as string
                                 && war.Opponent.Tag == keyValues[2] as string)
            .SingleOrDefaultAsync();
    }
}