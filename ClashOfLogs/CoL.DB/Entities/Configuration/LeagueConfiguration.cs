using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoL.DB.Entities.Configuration;

public class LeagueConfiguration : IEntityTypeConfiguration<League>
{
    public void Configure(EntityTypeBuilder<League> builder)
    {
        builder.HasKey(league => league.Id);

        builder.Property(league => league.Id).ValueGeneratedNever();

        builder.Property(league => league.Name).IsRequired().HasColumnType("nvarchar(30)");

        builder.OwnsOne(league => league.IconUrls,
            b =>
            {
                b.Property(urls => urls.Tiny).IsRequired().HasColumnType("varchar(150)");
                b.Property(urls => urls.Small).IsRequired().HasColumnType("varchar(150)");
                b.Property(urls => urls.Medium).IsRequired().HasColumnType("varchar(150)");
            });
    }
}