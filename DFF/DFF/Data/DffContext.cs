using Microsoft.EntityFrameworkCore;


namespace DuplicateFileFind
{
    public class DffContext : DbContext
    {

        public DbSet<IndexedFolder> Folders { get; set; }
        public DbSet<IndexedFile> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IndexedFolder>()
                .Property(fld => fld.Path).HasMaxLength(256);
            modelBuilder.Entity<IndexedFolder>()
                .HasIndex(dir => dir.Path);


            modelBuilder.Entity<IndexedFile>()
                .HasIndex(f => f.FolderId);

            modelBuilder.Entity<IndexedFile>()
                .HasIndex(f => f.Name);

            modelBuilder.Entity<IndexedFile>()
                .HasIndex(f => f.Length);
            

            modelBuilder.Entity<IndexedFile>()
                .HasIndex(f => f.Hash128);

            base.OnModelCreating(modelBuilder);
        }
    }
}
