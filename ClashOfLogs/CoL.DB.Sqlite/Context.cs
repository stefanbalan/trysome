using Microsoft.EntityFrameworkCore;

namespace CoL.DB.Sqlite;

public class CoLContextSqlite : CoLContext
{
    public CoLContextSqlite()
    {
        
    }
    public CoLContextSqlite(DbContextOptions options)
        : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http: //go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer("Data Source=data\\col.db");
        }
    }
}
