using System.ComponentModel.DataAnnotations;

namespace CoL.DB.Entities
{
    //[Owned]
    public class WarClan
    {

        public string Tag { get; set; }
        public string Name { get; set; }
        [Required]
        public BadgeUrls BadgeUrls { get; set; }
        public int ClanLevel { get; set; }
        public int Attacks { get; set; }
        public int Stars { get; set; }
        public double DestructionPercentage { get; set; }
        //public List<WarMember> Members { get; set; }
    }

    //public class WarClanConfiguration : IEntityTypeConfiguration<WarClan>
    //{
    //    public void Configure(EntityTypeBuilder<WarClan> builder)
    //    {
    //        //builder.OwnsMany<WarMember>(wc => wc.Members);


    //    }
    //}
}
