using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoL.DB.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }


    public class BaseEntityWithTag : BaseEntity
    {
        public string Tag { get; set; }

        public class Configuration<T> :
            IEntityTypeConfiguration<T> where T : BaseEntityWithTag
        {
            public void Configure(EntityTypeBuilder<T> builder)
            {
                builder.HasKey(clan => clan.Tag);
                builder.Property(clan => clan.Tag)
                    .HasColumnType("varchar(12)")
                    .HasConversion(tag => tag.Replace("#", string.Empty), db => $"#{db}");
            }
        }
    }
}