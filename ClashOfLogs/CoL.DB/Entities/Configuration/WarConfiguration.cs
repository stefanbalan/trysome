using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoL.DB.Entities.Configuration;

public class WarConfiguration : IEntityTypeConfiguration<War>
{
    public void Configure(EntityTypeBuilder<War> builder)
    {
        builder.HasKey(x => x.Id);

        //builder.HasAlternateKey(nameof(War.EndTime), nameof(War.ClanTag), nameof(War.OpponentTag));

        builder.Property(w => w.Result).IsRequired().HasColumnType("varchar(10)");

        builder.OwnsOne(w => w.Clan,
            bwc =>
            {
                bwc.Property(wc => wc.Tag).ConfigureTag();
                bwc.Property(wc => wc.Name).IsRequired()
                    .HasColumnType("varchar(50)");

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
                bwc.Property(wc => wc.Tag).ConfigureTag();
                bwc.Property(wc => wc.Name).IsRequired()
                    .HasColumnType("varchar(50)");

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
            .HasForeignKey(wm => wm.WarId)
            ;

        builder
            .HasMany(wc => wc.OpponentMembers)
            .WithOne()
            .HasForeignKey(wm => wm.WarId);
    }
}