using System;
using System.Collections.Generic;

namespace CoL.DB.Entities;

public record War : BaseEntity
{
    public int Id { get; set; }

    public string Result { get; set; }

    public DateTime? PreparationStartTime { get; set; }
    public DateTime? StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int TeamSize { get; set; }
    public int AttacksPerMember { get; set; }

    public WarClan Clan { get; set; }
    public string ClanTag => Clan.Tag;
    public List<WarMemberClan> ClanMembers { get; set; }

    public WarClan Opponent { get; set; }
    public string OpponentTag => Opponent.Tag;
    public List<WarMemberOpponent> OpponentMembers { get; set; }
}