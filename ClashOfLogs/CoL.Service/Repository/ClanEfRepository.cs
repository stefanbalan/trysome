using CoL.DB;
using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Repository;

public class ClanEfRepository : BaseEFRepository<CoLContext, DBClan>
{
    public ClanEfRepository(CoLContext context) : base(context)
    {
    }

    protected override DbSet<DBClan> DbSet => Context.Clans;
}