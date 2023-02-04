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

            builder.HasData(new List<EmailTemplate>()
            {
                new() { Id = 1, Name = "Template 1", Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 2, Name = "Template 2", Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 3, Name = "Template 3", Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 4, Name = "Template 4", Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 5, Name = "Template 5", Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 6, Name = "Template 6", Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 7, Name = "Template 7", Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 8, Name = "Template 8", Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 9, Name = "Template 9", Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 10, Name = "Template 10", Html = true, Text = "An <em>example</em> of rich text." },
                new() { Id = 11, Name = "Template 11", Html = true, Text = "An <em>example</em> of rich text." },
            });
        }
    }
}