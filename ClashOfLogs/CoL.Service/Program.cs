global using System;
global using System.Threading.Tasks;
global using DBBadgeUrls = CoL.DB.Entities.BadgeUrls;
global using DBClan = CoL.DB.Entities.Clan;
global using DBLeague = CoL.DB.Entities.League;
global using DBMember = CoL.DB.Entities.Member;
global using DBWar = CoL.DB.Entities.War;
global using DBWarClan = CoL.DB.Entities.WarClan;

using ClashOfLogs.Shared;
using CoL.DB;
using CoL.Service.DataProvider;
using CoL.Service.Importer;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace CoL.Service;

public static class Program
{
    public static int Main(string[] args)
    {
        var config =
            BuildConfiguration(); // access as var setting = config["Setting"]; var setting = config["Category:Setting"]; or config.GetSection<T>("section");

        Log.Logger =
            new LoggerConfiguration() //todo: study if it is better to use a separate logger for the bootstrapper part
                .ReadFrom.Configuration(config)
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                //.WriteTo.Console()
                //.WriteTo.File("col.log")
                .CreateLogger();

        try
        {
            Log.Information("Starting host");
            CreateHostBuilder(config, args).Build().Run();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var env = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{env}.json", true, true)
            .AddEnvironmentVariables();

        return builder.Build();
    }


    private static IHostBuilder CreateHostBuilder(IConfigurationRoot configuration, string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureHostConfiguration(builder =>
            {
                /* Host configuration */
            })
            .ConfigureAppConfiguration(builder =>
            {
                /* App configuration */
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddLogging(lb =>
                {
                    lb.AddConsole();
                    lb.AddConfiguration(configuration);
                });


                services.AddDbContext<CoLContext>(ServiceLifetime.Singleton);

                // data providers
                services.AddTransient<IJsonDataProvider, FileJsonDataProvider>();
                // services.AddTransient<IJsonDataProvider, ApiJsonDataProvider>();

                // importers
                services.AddTransient<EntityImporter<DBClan, Clan>, ClanImporter>();
                services.AddSingleton<EntityImporter<DBMember, Member>, MemberProvider>();
                services.AddSingleton<EntityImporter<DBLeague, League>, LeagueCatalogProvider>();
                services.AddTransient<EntityImporter<DBWar, WarSummary>, WarLogImporter>();


                // mappers
                services.AddSingleton<IMapper<DBClan, Clan>, ClanMapper>();
                services.AddSingleton<IMapper<DBMember, Member>, MemberMapper>();
                services.AddSingleton<IMapper<DBLeague, League>, LeagueMapper>();
                services.AddSingleton<IMapper<DBWar, WarSummary>, WarMapper>();

                // repositories
                services.AddSingleton<IRepository<DBClan>, ClanEfRepository>();
                services.AddSingleton<IRepository<DBMember>, MemberEfRepository>();
                services.AddSingleton<IRepository<DBLeague>, LeagueEfRepository>();
                services.AddSingleton<IRepository<DBWar>, WarRepository>();

                //the actual service to run
                services.AddHostedService<Worker>();
            })
            .UseSerilog();
}