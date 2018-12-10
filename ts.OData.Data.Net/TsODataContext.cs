using System.Data.Entity;
using ts.Domain.Entities;

namespace ts.OData.Data.Net
{
    public class TsODataContext : DbContext
    {

        public TsODataContext() : base("ODataDbConnection")
        {
            Database.SetInitializer(new TsODataContextInitializer());
        }

        public DbSet<Site> SiteSet { get; set; }
        public DbSet<Server> ServerSet { get; set; }

        public DbSet<Policy> PolicySet { get; set; }
    }

    public class TsODataContextInitializer : CreateDatabaseIfNotExists<TsODataContext>
    {

    }
}
