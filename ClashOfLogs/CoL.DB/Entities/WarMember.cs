using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoL.DB.Entities
{
    public class WarMemberClan : WarMember
    {

    }


    public class WarMemberOpponent : WarMember
    {

    }

    

    public class WarMember : BaseEntityWithTag
    {
        public int WarId { get; set; }

        public string Name { get; set; }
        public int TownhallLevel { get; set; }
        public int MapPosition { get; set; }
        public WarAttack Attack1 { get; set; }
        public WarAttack Attack2 { get; set; }
        public int OpponentAttacks { get; set; }
        public WarAttack BestOpponentAttack { get; set; }
    }


}
