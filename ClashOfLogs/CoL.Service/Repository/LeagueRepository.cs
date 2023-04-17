using CoL.DB;
using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Repository;

public class LeagueEfRepository : BaseEFRepository<CoLContext, DBLeague, int>
{
    public LeagueEfRepository(CoLContext context) : base(context)
    {
    }

    protected override DbSet<DBLeague> DbSet => Context.Leagues;
}