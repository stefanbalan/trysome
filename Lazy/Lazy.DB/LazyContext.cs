using Lazy.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lazy.DB
{
    public class LazyContext : DbContext
    {
        public LazyContext()
        {
        }

        public LazyContext(DbContextOptions<LazyContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http: //go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Lazy;" +
                                            "Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;" +
                                            "ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "1.0.1");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LazyContext).Assembly);
        }

        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; } = null!;
    }
}