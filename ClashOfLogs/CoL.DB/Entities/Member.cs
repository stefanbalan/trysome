using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace CoL.DB.Entities
{
    public class Member : BaseEntityWithTag
    {

        public string ClanTag { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int ExpLevel { get; set; }
        //public League League { get; set; }
        public int Trophies { get; set; }
        public int VersusTrophies { get; set; }
        public int ClanRank { get; set; }
        public int PreviousClanRank { get; set; }
        public int Donations { get; set; }
        public int DonationsReceived { get; set; }
        public int DonationsPreviousSeason { get; set; }
        public int DonationsReceivedPreviousSeason { get; set; }
        public DateTime? LastLeft { get; set; }
        public DateTime? LastJoined { get; set; }
        public bool IsMember { get; set; }
    }


    public class ClanMemberConfiguration : BaseEntityWithTag.Configuration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
            base.Configure(builder);

            builder.HasKey(clan => clan.Tag);
        }
    }
}
