using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Repository;

public class LeagueEfRepository(CoLContext context) : BaseEFRepository<CoLContext, DBLeague>(context)
{
    protected override DbSet<DBLeague> EntitySet => Context.Leagues;
}