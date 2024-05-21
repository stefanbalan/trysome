using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Repository;

internal class ClanRepository(CoLContext context) : BaseEFRepository<CoLContext, DBClan>(context)
{
    protected override DbSet<DBClan> EntitySet => Context.Clans;

    public async override ValueTask<DBClan?> GetByIdAsync(params object?[] keyValues)
    {
        if (keyValues.Length == 0 || keyValues[0] is null)
            return null;

        var dbEntity = await EntitySet
            .Include(clan => clan.Members.Where(member => member.IsMember))
            .FirstOrDefaultAsync(clan => clan.Tag == (string?)keyValues[0]);

        return dbEntity;
    }
}