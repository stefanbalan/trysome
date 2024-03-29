﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoL.DB.Entities
{
    public class Clan : BaseEntityWithTag
    {
        public Clan()
        {
            Members = new List<Member>();
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        //public Location Location { get; set; }

        [Required]
        public BadgeUrls BadgeUrls { get; set; }

        public int ClanLevel { get; set; }
        public int ClanPoints { get; set; }
        public int ClanVersusPoints { get; set; }
        public int RequiredTrophies { get; set; }
        public string WarFrequency { get; set; }
        public int WarWinStreak { get; set; }
        public int WarWins { get; set; }
        public int WarTies { get; set; }
        public int WarLosses { get; set; }
        public bool IsWarLogPublic { get; set; }

        //public WarLeague WarLeague { get; set; }

        public int MembersCount { get; set; }

        public List<Member> Members { get; set; }
        //public List<Label> Labels { get; set; } //= new List<Label>();

        public int RequiredVersusTrophies { get; set; }
        public int RequiredTownhallLevel { get; set; }
    }

    public class ClanConfiguration : BaseEntityWithTag.Configuration<Clan>
    {
        public new void Configure(EntityTypeBuilder<Clan> builder)
        {
            base.Configure(builder);

            builder.HasMany(clan => clan.Members);
        }
    }
}