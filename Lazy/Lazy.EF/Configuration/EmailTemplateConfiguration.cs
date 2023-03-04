using Lazy.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lazy.EF.Configuration
{
    internal class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            builder.HasKey(et => et.Id);

            builder.Property(et => et.Name).HasMaxLength(50);
            
            builder.Property(et => et.Title).HasMaxLength(50);

            builder.HasData(new List<EmailTemplate>
            {
                new() { Id = 1, Name = "Template 1 aaa",   Title = "Test email title",  Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 2, Name = "Template 2 aab",   Title = "Test email title",  Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 3, Name = "Template 3 aac",   Title = "Test email title",  Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 4, Name = "Template 4 aad",   Title = "Test email title",  Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 5, Name = "Template 5 aba",   Title = "Test email title",  Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 6, Name = "Template 6 abb",   Title = "Test email title",  Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 7, Name = "Template 7 abc",   Title = "Test email title",  Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 8, Name = "Template 8 abd",   Title = "Test email title",  Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 9, Name = "Template 9 aca",   Title = "Test email title",  Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 10, Name = "Template 10 acb", Title = "Test email title",   Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 11, Name = "Template 11 acc", Title = "Test email title",   Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 12, Name = "Template 12 acd", Title = "Test email title",   Html = true, Text = "An <em>example</em> of rich text." },
            });
        }
    }
}