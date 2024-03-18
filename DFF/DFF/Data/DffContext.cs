using Microsoft.EntityFrameworkCore;


namespace DuplicateFileFind
{
    public class DffContext : DbContext
    {
        //EXPLAIN QUERY PLAN SELECT* FROM 'Files' where length = 387850

        public DbSet<IndexedFolder> Directories { get; set; }
        public DbSet<IndexedFile> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IndexedFolder>()
                .HasIndex(dir => dir.Path);

            modelBuilder.Entity<IndexedFile>()
                .HasIndex(f => f.DirectoryId);

            modelBuilder.Entity<IndexedFile>()
                .HasIndex(f => f.Name);

            modelBuilder.Entity<IndexedFile>()
                .HasIndex(f => f.Length);
            
            modelBuilder.Entity<IndexedFile>()
                .HasIndex(f => f.CreationTime);

            modelBuilder.Entity<IndexedFile>()
                .HasIndex(f => f.Hash);

            base.OnModelCreating(modelBuilder);
        }
    }
}
