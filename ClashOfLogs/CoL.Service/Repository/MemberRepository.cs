using CoL.DB;
using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Repository;

public class MemberEfRepository : BaseEFRepository<CoLContext, DBMember, string>
{
    public MemberEfRepository(CoLContext context) : base(context)
    {
    }

    protected override DbSet<DBMember> DbSet => Context.ClanMembers;
}