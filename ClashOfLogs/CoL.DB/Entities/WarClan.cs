using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System.ComponentModel.DataAnnotations;

namespace CoL.DB.Entities
{

    public class WarClan
    {
        [Required]
        public string Tag { get; set; }
        public string Name { get; set; }
        public BadgeUrls BadgeUrls { get; set; }
        public int ClanLevel { get; set; }
        public int Attacks { get; set; }
        public int Stars { get; set; }
        public double DestructionPercentage { get; set; }
    }

    //public class WarClanConfiguration : IEntityTypeConfiguration<WarClan>
    //{
    //    public void Configure(EntityTypeBuilder<WarClan> builder)
    //    {
    //        builder.Property(wc => wc.Tag).IsRequired();
    //        builder.Property(wc => wc.Name).IsRequired();
    //    }
    //}
}
