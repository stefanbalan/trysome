using System.ComponentModel.DataAnnotations;

namespace CoL.DB.Entities
{
    public class ClanMember
    {
        [Key]
        public string Tag { get; set; }

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
    }
}
