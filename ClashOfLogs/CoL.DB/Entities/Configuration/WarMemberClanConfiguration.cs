using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoL.DB.Entities.Configuration;

public class WarMemberClanConfiguration : WarMemberBaseEntityWithTagConfiguration
{
    public override void Configure(EntityTypeBuilder<WarMember> builder)
    {
        builder.ToTable("WarMembers_Clan");
        base.Configure(builder);
    }
}