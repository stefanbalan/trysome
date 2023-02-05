using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lazy.EF
{
    public static class Config
    {
        public static void AddDb(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<LazyContext>(builder => builder.UseSqlServer(connectionString));
        }
    }
}