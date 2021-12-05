using CoL.DB.Entities;

using Microsoft.EntityFrameworkCore;

namespace CoL.DB.mssql
{
    public class CoLContext : DbContext
    {
        public CoLContext()
        {
        }

        public CoLContext(DbContextOptions<CoLContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CoL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoLContext).Assembly);



        }

        public virtual DbSet<Clan> Clans { get; set; }
        public virtual DbSet<Member> ClanMembers { get; set; }
        public virtual DbSet<War> WarSummaries { get; set; }
    }
}
