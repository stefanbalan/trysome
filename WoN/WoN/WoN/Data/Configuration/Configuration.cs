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

        builder.HasData(
            new Employee {
                Id = 1,
                Name = "Ion Test",
                CountryCode = "RO",
            }, new Employee {
                Id = 2,
                Name = "Jacques Tést",
                CountryCode = "FR"
            });
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

        builder.HasData(
            new PublicHoliday { Year = 2024, CountryCode = "RO", Date = new DateOnly(2024, 01, 01), Name = "New year" },
            new PublicHoliday
                { Year = 2024, CountryCode = "RO", Date = new DateOnly(2024, 01, 02), Name = "New year 2nd day" },
            new PublicHoliday { Year = 2024, CountryCode = "RO", Date = new DateOnly(2024, 01, 06), Name = "Epiphany" },
            new PublicHoliday {
                Year = 2024, CountryCode = "RO", Date = new DateOnly(2024, 01, 07),
                Name = "Synaxis of St.John the Baptist"
            },
            new PublicHoliday {
                Year = 2024, CountryCode = "RO", Date = new DateOnly(2024, 01, 24),
                Name = "Union of the Romanian Principalities"
            },
            new PublicHoliday
                { Year = 2024, CountryCode = "RO", Date = new DateOnly(2024, 05, 01), Name = "Labour Day" },
            new PublicHoliday
                { Year = 2024, CountryCode = "RO", Date = new DateOnly(2024, 05, 03), Name = "Orthodox Good Friday" },
            new PublicHoliday
                { Year = 2024, CountryCode = "RO", Date = new DateOnly(2024, 05, 05), Name = "Orthodox Easter Sunday" },
            new PublicHoliday
                { Year = 2024, CountryCode = "RO", Date = new DateOnly(2024, 05, 06), Name = "Orthodox Easter Monday" },
            new PublicHoliday
                { Year = 2024, CountryCode = "RO", Date = new DateOnly(2024, 06, 01), Name = "Children's Day" },
            new PublicHoliday
                { Year = 2024, CountryCode = "RO", Date = new DateOnly(2024, 06, 23), Name = "Orthodox Whit Sunday" },
            new PublicHoliday
                { Year = 2024, CountryCode = "RO", Date = new DateOnly(2024, 06, 24), Name = "Orthodox Whit Monday" },
            new PublicHoliday
                { Year = 2024, CountryCode = "RO", Date = new DateOnly(2024, 08, 15), Name = "Assumption Day" },
            new PublicHoliday
                { Year = 2024, CountryCode = "RO", Date = new DateOnly(2024, 11, 30), Name = "Feast of Saint Andrew" },
            new PublicHoliday
                { Year = 2024, CountryCode = "RO", Date = new DateOnly(2024, 12, 01), Name = "Great Union Day" },
            new PublicHoliday
                { Year = 2024, CountryCode = "RO", Date = new DateOnly(2024, 12, 25), Name = "Christmas Day" },
            new PublicHoliday
                { Year = 2024, CountryCode = "RO", Date = new DateOnly(2024, 12, 26), Name = "2nd Day of Christmas" },

            new PublicHoliday { Year = 2024, CountryCode = "FR", Date = new DateOnly(2024, 01, 01), Name = "New year" },
            new PublicHoliday
                { Year = 2024, CountryCode = "FR", Date = new DateOnly(2024, 03, 29), Name = "Good Friday *" },
            new PublicHoliday
                { Year = 2024, CountryCode = "FR", Date = new DateOnly(2024, 04, 01), Name = "Easter Monday" },
            new PublicHoliday
                { Year = 2024, CountryCode = "FR", Date = new DateOnly(2024, 05, 01), Name = "Labour Day" },
            new PublicHoliday
                { Year = 2024, CountryCode = "FR", Date = new DateOnly(2024, 05, 08), Name = "Victory Day" },
            new PublicHoliday
                { Year = 2024, CountryCode = "FR", Date = new DateOnly(2024, 05, 09), Name = "Ascension Day" },
            new PublicHoliday
                { Year = 2024, CountryCode = "FR", Date = new DateOnly(2024, 05, 19), Name = "Whit Sunday" },
            new PublicHoliday
                { Year = 2024, CountryCode = "FR", Date = new DateOnly(2024, 05, 20), Name = "Whit Monday" },
            new PublicHoliday
                { Year = 2024, CountryCode = "FR", Date = new DateOnly(2024, 07, 14), Name = "Bastille Day" },
            new PublicHoliday
                { Year = 2024, CountryCode = "FR", Date = new DateOnly(2024, 08, 15), Name = "Assumption Day" },
            new PublicHoliday
                { Year = 2024, CountryCode = "FR", Date = new DateOnly(2024, 11, 01), Name = "All Saints' Day" },
            new PublicHoliday
                { Year = 2024, CountryCode = "FR", Date = new DateOnly(2024, 11, 11), Name = "Armistice Day" },
            new PublicHoliday
                { Year = 2024, CountryCode = "FR", Date = new DateOnly(2024, 12, 25), Name = "Christmas Day" },
            new PublicHoliday
                { Year = 2024, CountryCode = "FR", Date = new DateOnly(2024, 12, 26), Name = "St Stephen's Day *" }
        );
    }
}