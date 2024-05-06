using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoL.DB.Entities.Configuration;

public class BaseEntityWithTagConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntityWithTag
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(clan => clan.Tag);
        builder.Property(clan => clan.Tag)
            .ConfigureTag();
    }
}

public static class ConfigurationExtensions
{
    public static PropertyBuilder ConfigureTag(this PropertyBuilder<string> builder)
        => builder
            .IsRequired()
            .HasColumnType("varchar(12)")
            .HasConversion(tag => tag.Replace("#", string.Empty), db => $"#{db}");
}