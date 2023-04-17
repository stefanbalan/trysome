namespace CoL.DB.Entities;

public record WarMember : BaseEntityWithTag
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