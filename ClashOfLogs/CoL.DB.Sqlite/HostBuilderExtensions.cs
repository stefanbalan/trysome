using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoL.DB.Sqlite;

public static class HostBuilderExtensions
{
    public static IServiceCollection UseSqliteCoLContext(this IServiceCollection services, string connectionString)
        =>
            services.AddDbContext<CoLContextSqlite>(
                options =>
                options.UseSqlite(connectionString, sqlOptions => sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)),
                ServiceLifetime.Singleton
                );
}