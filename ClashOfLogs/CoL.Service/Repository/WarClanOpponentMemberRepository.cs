using CoL.DB;
using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Repository;

internal class WarClanOpponentMemberRepository : BaseEFRepository<CoLContext, DBWarOpponentMember>
{
    public WarClanOpponentMemberRepository(CoLContext context) : base(context)
    {
    }

    protected override DbSet<DBWarOpponentMember> EntitySet => Context.Set<DBWarOpponentMember>();
}
