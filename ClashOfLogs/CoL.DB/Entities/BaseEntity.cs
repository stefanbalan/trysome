using System;

namespace CoL.DB.Entities;

public record BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public record BaseEntityWithTag : BaseEntity
{
    public string Tag { get; set; }
}