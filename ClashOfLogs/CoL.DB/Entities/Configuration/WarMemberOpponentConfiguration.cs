using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoL.DB.Entities.Configuration;

public class WarMemberOpponentConfiguration : WarMemberBaseEntityWithTagConfiguration
{
    public override void Configure(EntityTypeBuilder<WarMember> builder)
    {
        builder.ToTable("WarMembers_Opponent");
        base.Configure(builder);
    }
}