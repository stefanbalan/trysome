using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoL.DB.Entities.Configuration;

public class WarMemberConfiguration : BaseEntityWithTagConfiguration<WarMember>
{
    public override void Configure(EntityTypeBuilder<WarMember> builder)
    {
        base.Configure(builder);
        builder.Property(member => member.Name).IsRequired().HasColumnType("nvarchar(50)");

        builder.OwnsOne(wm => wm.Attack1, WarAttackConfiguration);
        builder.OwnsOne(wm => wm.Attack2, WarAttackConfiguration);
        builder.OwnsOne(wm => wm.BestOpponentAttack, WarAttackConfiguration);
    }


    private static void WarAttackConfiguration(OwnedNavigationBuilder<WarMember, WarAttack> builder)
    {
        builder.Property(wa => wa.AttackerTag).ConfigureTag();
        builder.Property(wa => wa.DefenderTag).ConfigureTag();
    }
}