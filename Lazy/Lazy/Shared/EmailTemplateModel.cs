namespace Lazy.Model;

public record EmailTemplateModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public bool Html { get; set; }
}