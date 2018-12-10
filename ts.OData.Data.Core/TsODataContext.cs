using Microsoft.EntityFrameworkCore;
using ts.Domain.Entities;

namespace ts.OData.Data.Core
{
    public class TsODataContext : DbContext
    {

        public DbSet<Site> SiteSet { get; set; }
        public DbSet<Server> ServerSet { get; set; }

        public DbSet<Policy> PolicySet { get; set; }
    }
}
