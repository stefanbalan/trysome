using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoL.DB.Entities.Configuration;

public class ClanBaseEntityWithTagConfiguration : BaseEntityWithTagConfiguration<Clan>
{
    public override void Configure(EntityTypeBuilder<Clan> builder)
    {
        base.Configure(builder);

        builder.Property(clan => clan.Name).IsRequired().HasColumnType("nvarchar(50)");
        builder.Property(clan => clan.Type).IsRequired().HasColumnType("varchar(10)");
        builder.Property(clan => clan.Description).HasColumnType("nvarchar(500)");

        builder.HasMany(clan => clan.Members);

        builder.Property(clan => clan.WarFrequency).HasColumnType("varchar(50)");

        builder.OwnsOne(wc => wc.BadgeUrls,
            b =>
            {
                b.Property(urls => urls.Small).IsRequired().HasColumnType("varchar(150)");
                b.Property(urls => urls.Medium).IsRequired().HasColumnType("varchar(150)");
                b.Property(urls => urls.Large).IsRequired().HasColumnType("varchar(150)");
            });
    }
}