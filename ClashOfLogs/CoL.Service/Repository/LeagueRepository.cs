using CoL.DB;
using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Repository;

public class LeagueEfRepository : BaseEFRepository<CoLContext, DBLeague>
{
    public LeagueEfRepository(CoLContext context) : base(context)
    {
    }

    protected override DbSet<DBLeague> EntitySet => Context.Leagues;
}