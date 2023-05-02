namespace CoL.DB.Entities;

public record WarClan
{
    public string Tag { get; set; } = null!;
    public string? Name { get; set; }
    public BadgeUrls? BadgeUrls { get; set; }
    public int ClanLevel { get; set; }
    public int Attacks { get; set; }
    public int Stars { get; set; }
    public double DestructionPercentage { get; set; }
}