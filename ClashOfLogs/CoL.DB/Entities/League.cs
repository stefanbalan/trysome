namespace CoL.DB.Entities;

public record League : BaseEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public IconUrls? IconUrls { get; set; }
}