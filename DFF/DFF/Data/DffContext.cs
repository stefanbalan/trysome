using Microsoft.EntityFrameworkCore;


namespace DuplicateFileFind
{
    public class DffContext : DbContext
    {
        //EXPLAIN QUERY PLAN SELECT* FROM 'Files' where length = 387850

        public DbSet<Folder> Directories { get; set; }
        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Folder>()
                .HasIndex(dir => dir.Path);

            modelBuilder.Entity<File>()
                .HasIndex(f => f.DirectoryId);

            modelBuilder.Entity<File>()
                .HasIndex(f => f.Name);

            modelBuilder.Entity<File>()
                .HasIndex(f => f.Length);

            modelBuilder.Entity<File>()
                .HasIndex(f => f.Hash);

            base.OnModelCreating(modelBuilder);
        }
    }
}
