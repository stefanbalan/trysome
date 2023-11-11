using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoL.DB.Entities.Configuration;

public class WarConfiguration : IEntityTypeConfiguration<War>
{
    public void Configure(EntityTypeBuilder<War> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(war => war.EndTime);

        builder.Property(w => w.Result).HasColumnType("varchar(10)");
        builder.Property(w => w.State).HasColumnType("varchar(10)");

        builder.OwnsOne(w => w.Clan,
            bwc =>
            {
                bwc.HasIndex(clan => clan.Tag);
                bwc.Property(wc => wc.Tag).ConfigureTag();
                bwc.Property(wc => wc.Name).IsRequired()
                    .HasColumnType("nvarchar(50)");

                bwc.OwnsOne(wc => wc.BadgeUrls,
                    bbu =>
                    {
                        bbu.Property(badgeUrls => badgeUrls.Small).IsRequired().HasColumnType("varchar(150)");
                        bbu.Property(badgeUrls => badgeUrls.Medium).IsRequired().HasColumnType("varchar(150)");
                        bbu.Property(badgeUrls => badgeUrls.Large).IsRequired().HasColumnType("varchar(150)");
                    });
            });

        builder.OwnsOne(w => w.Opponent,
            bwc =>
            {
                bwc.HasIndex(clan => clan.Tag);
                bwc.Property(wc => wc.Tag).ConfigureTag();
                bwc.Property(wc => wc.Name).IsRequired()
                    .HasColumnType("nvarchar(50)");

                bwc.OwnsOne(wc => wc.BadgeUrls,
                    bbu =>
                    {
                        bbu.Property(badgeUrls => badgeUrls.Small)
                            .IsRequired().HasColumnType("varchar(150)");
                        bbu.Property(badgeUrls => badgeUrls.Medium)
                            .IsRequired().HasColumnType("varchar(150)");
                        bbu.Property(badgeUrls => badgeUrls.Large)
                            .IsRequired().HasColumnType("varchar(150)");
                    });
            });


        builder
            .HasMany(wc => wc.ClanMembers)
            .WithOne()
            .HasForeignKey(wm => wm.WarIdC)
            .HasConstraintName("FK_WarMember_Wars_WarId_ClanMembers");

        builder
            .HasMany(wc => wc.OpponentMembers)
            .WithOne()
            .HasForeignKey(wm => wm.WarIdO)
            .HasConstraintName("FK_WarMember_Wars_WarId_OpponentMembers");
    }
}