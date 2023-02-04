namespace Lazy.Data.Entities
{
    public record EmailTemplate
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool Html { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}