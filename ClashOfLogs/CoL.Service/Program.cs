global using System;
global using System.Threading.Tasks;
global using DBClan = CoL.DB.Entities.Clan;
global using DBMember = CoL.DB.Entities.Member;
global using DBWar = CoL.DB.Entities.War;
global using DBWarClan = CoL.DB.Entities.WarClan;
global using DBBadgeUrls = CoL.DB.Entities.BadgeUrls;

using ClashOfLogs.Shared;

using CoL.DB.mssql;
using CoL.Service.Mappers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoL.Service
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var config = BuildConfiguration(); // access as var setting = config["Setting"]; var setting = config["Category:Setting"]; or config.GetSection<T>("section");

            CreateHostBuilder(config, args).Build().Run();

            //ClanMapper.Map(dbclan => dbclan.Name, clan => clan.Name);
        }




        private static IConfigurationRoot BuildConfiguration()
        {

            var env = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true)
                                                    .AddJsonFile($"appsettings.{env}.json", true, true)
                                                    .AddEnvironmentVariables();

            return builder.Build();
        }



        public static IHostBuilder CreateHostBuilder(IConfigurationRoot configuration, string[] args)
        {

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging(lb =>
                    {
                        lb.AddConsole();
                        lb.AddConfiguration(configuration);
                    });


                    services.AddDbContext<CoLContext>(ServiceLifetime.Singleton);

                    //services.AddSingleton<IImportDataProvider, FileImportDataProvider>();
                    services.AddTransient<IJsonDataProvider, FileJsonDataProvider>();

                    services.AddSingleton<IMapper<DBClan, Clan>, ClanMapper>();

                    services.AddHostedService<Worker>();
                });
        }
    }
}
