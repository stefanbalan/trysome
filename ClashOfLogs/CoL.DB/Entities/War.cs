using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;

namespace CoL.DB.Entities
{
    public class War
    {
        public int Id { get; set; }

        public string Result { get; set; }

        public DateTime? PreparationStartTime { get; set; }
        public DateTime? StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int TeamSize { get; set; }
        public int AttacksPerMember { get; set; }

        public string ClanTag { get; set; }
        public WarClan Clan { get; set; }
        public List<ClanWarMember> ClanMembers { get; set; }

        public string OpponentTag { get; set; }
        public WarClan Opponent { get; set; }
        public List<OpponentWarMember> OpponentMembers { get; set; }
    }

    public class WarConfiguration : IEntityTypeConfiguration<War>
    {
        public void Configure(EntityTypeBuilder<War> builder)
        {
            builder.HasKey(x => x.Id);

            //builder.HasAlternateKey(nameof(War.EndTime), nameof(War.ClanTag), nameof(War.OpponentTag));

            builder.OwnsOne<WarClan>(nameof(War.Clan));
            builder.OwnsOne<WarClan>(nameof(War.Opponent));

            //builder.OwnsMany<OpponentWarMember>(wc => wc.ClanMembers).WithOwner(wm => wm.War)
            //    //.Navigation(wm => wm.War).HasField(nameof(WarMember.WarId))
            //    ;
            //builder.OwnsMany<OpponentWarMember>(wc => wc.OpponentMembers).WithOwner(wm => wm.War)
            //    //.Navigation(wm => wm.War).HasField(nameof(WarMember.WarId))
            //    ;


            builder.HasMany<ClanWarMember>(wc => wc.ClanMembers).WithOne(wm => wm.War).HasForeignKey(wm => wm.WarId);
            builder.HasMany<OpponentWarMember>(wc => wc.OpponentMembers).WithOne(wm => wm.War).HasForeignKey(wm => wm.WarId);

            //builder.HasOne<WarClan>(nameof(War.Clan));
            //builder.HasOne<WarClan>(nameof(War.Opponent));
        }
    }
}
