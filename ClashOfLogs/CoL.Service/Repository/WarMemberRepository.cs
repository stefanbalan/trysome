using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Repository;

internal class WarMemberRepository(CoLContext context) : BaseEFRepository<CoLContext, DBWarMember>(context)
{
    protected override DbSet<DBWarMember> EntitySet => Context.Set<DBWarMember>();
}
