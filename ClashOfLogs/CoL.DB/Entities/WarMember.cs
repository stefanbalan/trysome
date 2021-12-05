﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoL.DB.Entities
{
    public class ClanWarMember : WarMember
    {

    }


    public class OpponentWarMember : WarMember
    {

    }

    

    public class WarMember
    {
        public int WarId { get; set; }
        public War War { get; set; }

        [Key]
        public string Tag { get; set; }
        public string Name { get; set; }
        public int TownhallLevel { get; set; }
        public int MapPosition { get; set; }
        public WarAttack Attack1 { get; set; }
        public WarAttack Attack2 { get; set; }
        public int OpponentAttacks { get; set; }
        public WarAttack BestOpponentAttack { get; set; }
    }


}
