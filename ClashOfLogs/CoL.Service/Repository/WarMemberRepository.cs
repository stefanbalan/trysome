using CoL.DB;
using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Repository;

internal class WarMemberRepository : BaseEFRepository<CoLContext, DBWarMember>
{
    public WarMemberRepository(CoLContext context) : base(context)
    {
    }

    protected override DbSet<DBWarMember> EntitySet => Context.Set<DBWarMember>();
}
