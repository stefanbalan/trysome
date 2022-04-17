using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoL.DB.Entities
{
    public class WarConfiguration : IEntityTypeConfiguration<War>
    {
        public void Configure(EntityTypeBuilder<War> builder)
        {
            builder.HasKey(x => x.Id);

            //builder.HasAlternateKey(nameof(War.EndTime), nameof(War.ClanTag), nameof(War.OpponentTag));

            builder.OwnsOne(w => w.Clan).WithOwner();
            builder.OwnsOne(w => w.Opponent).WithOwner();


            //builder.HasMany<ClanWarMember>(wc => wc.ClanMembers).WithOne(wm => wm.War).HasForeignKey(wm => wm.WarId);
            //builder.HasMany<OpponentWarMember>(wc => wc.OpponentMembers).WithOne(wm => wm.War).HasForeignKey(wm => wm.WarId);


            builder.OwnsMany(wc => wc.ClanMembers).ToTable("WarMembers_Clan").WithOwner().HasForeignKey(wm => wm.WarId);
            builder.OwnsMany(wc => wc.OpponentMembers).ToTable("WarMembers_Opponent").WithOwner().HasForeignKey(wm => wm.WarId);

        }
    }
}
