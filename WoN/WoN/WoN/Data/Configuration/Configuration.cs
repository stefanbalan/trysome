using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WoN.Data.Configuration;

public class Configuration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employee");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsUnicode().IsRequired().HasMaxLength(50);
        builder.Property(e => e.CountryCode).IsRequired().HasMaxLength(2);

        builder.HasOne(e => e.Country)
            .WithMany()
            .HasForeignKey(e => e.CountryCode)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Country");
        builder.HasKey(e => e.Code);
        builder.Property(e => e.Code).HasMaxLength(2);
        builder.Property(e => e.Name).IsUnicode().IsRequired().HasMaxLength(50);

        builder.HasData(
            new Country { Code = "FR", Name = "France" },
            new Country { Code = "RO", Name = "Romania" });
    }
}

public class CalendarConfiguration : IEntityTypeConfiguration<Calendar>
{
    public void Configure(EntityTypeBuilder<Calendar> builder)
    {
        builder.ToTable("Calendar");
        builder.HasKey(e => new { e.Year, e.CountryCode });
        builder.Property(e => e.Year).ValueGeneratedNever();
        builder.Property(e => e.CountryCode).HasMaxLength(2);
        builder.Property(e => e.NonWorkingDays).IsRequired().HasMaxLength(12);
    }
}

public class EmployeeTimeTrackingConfiguration : IEntityTypeConfiguration<EmployeeTimeTracking>
{
    public void Configure(EntityTypeBuilder<EmployeeTimeTracking> builder)
    {
        builder.ToTable("EmployeeTimeTracking");
        builder.HasKey(e => new { e.EmployeeId, e.Year });
        builder.HasOne(e => e.Employee).WithMany().HasForeignKey(ett => ett.EmployeeId);
        builder.Property(e => e.Year).ValueGeneratedNever();
        builder.Property(e => e.Vacation).IsRequired().HasMaxLength(12);
    }
}

public class PublicHolidayConfiguration : IEntityTypeConfiguration<PublicHoliday>
{
    public void Configure(EntityTypeBuilder<PublicHoliday> builder)
    {
        builder.ToTable("PublicHoliday");
        builder.HasKey(e => new { e.Year, e.CountryCode, e.Date });
        builder.Property(e => e.Year);
        builder.Property(e => e.CountryCode).IsRequired().HasMaxLength(2);
        builder.Property(e => e.Date).IsRequired();
        builder.Property(e => e.Name).IsUnicode().IsRequired().HasMaxLength(50);
    }
}