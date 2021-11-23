using System.ComponentModel.DataAnnotations;

namespace CoL.DB.Entities
{
    public class WarSummary
    {
        public string Result { get; set; }
        [Key]
        public string EndTime { get; set; }
        public int TeamSize { get; set; }
        public int AttacksPerMember { get; set; }

        //public WarClan Clan { get; set; }
        [Key]
        public string ClanTag { get; set; }
        public string ClanName { get; set; }
        public string ClanBadgeUrlSmall { get; set; }
        public string ClanBadgeUrlLarge { get; set; }
        public string ClanBadgeUrlMedium { get; set; }
        public int ClanClanLevel { get; set; }
        public int ClanAttacks { get; set; }
        public int ClanStars { get; set; }
        public double ClanDestructionPercentage { get; set; }


        //public WarClan Opponent { get; set; }
        [Key]
        public string OpponentTag { get; set; }
        public string OpponentName { get; set; }
        public string OpponentBadgeUrlSmall { get; set; }
        public string OpponentBadgeUrlLarge { get; set; }
        public string OpponentBadgeUrlMedium { get; set; }
        public int OpponentClanLevel { get; set; }
        public int OpponentAttacks { get; set; }
        public int OpponentStars { get; set; }
        public double OpponentDestructionPercentage { get; set; }

    }
}
