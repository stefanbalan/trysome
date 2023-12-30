namespace WoN.Data.Repository;

public class PublicHolidayRepository(ApplicationDbContext context) 
    : AbstractRepository<PublicHoliday>(context);

public class CountryRepository(ApplicationDbContext context) 
    : AbstractRepository<Country>(context);