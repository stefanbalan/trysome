using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoL.DB.Entities
{
    public class League : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IconUrls IconUrls { get; set; }

        public class LeagueConfiguration : IEntityTypeConfiguration<League>
        {
            public void Configure(EntityTypeBuilder<League> builder)
            {
                builder.HasKey(league => league.Id);

                builder.Property(league => league.Id).ValueGeneratedNever();

                builder.OwnsOne(league => league.IconUrls,
                    nav => nav.Property(urls => urls.Small).IsRequired());
            }
        }
    }
}
