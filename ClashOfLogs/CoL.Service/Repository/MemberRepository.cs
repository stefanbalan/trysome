using Microsoft.EntityFrameworkCore;

namespace CoL.Service.Repository;

public class MemberEfRepository(CoLContext context) : BaseEFRepository<CoLContext, DBMember>(context)
{
    protected override DbSet<DBMember> EntitySet => Context.ClanMembers;
}