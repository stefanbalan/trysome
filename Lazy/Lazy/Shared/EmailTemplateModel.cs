namespace Lazy.Model;

public record EmailTemplateModel : IValidator

{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public bool Html { get; set; }


    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Name);
    }
}

interface IValidator
{
    bool IsValid( /*out ValidationResult validationErrors*/);
}