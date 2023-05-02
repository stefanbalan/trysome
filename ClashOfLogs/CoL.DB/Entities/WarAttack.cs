namespace CoL.DB.Entities;

public class WarAttack
{
    public string AttackerTag { get; set; } = null!;
    public string DefenderTag { get; set; } = null!;
    public int Stars { get; set; }
    public int DestructionPercentage { get; set; }
    public int Order { get; set; }
    public int Duration { get; set; }
}