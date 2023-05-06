using CoL.DB;
using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Repository;

internal class WarClanMemberRepository : BaseEFRepository<CoLContext, DBWarClanMember>
{
    public WarClanMemberRepository(CoLContext context) : base(context)
    {
    }

    protected override DbSet<DBWarClanMember> EntitySet => Context.Set<DBWarClanMember>();
}
