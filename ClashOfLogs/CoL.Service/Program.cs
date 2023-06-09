global using System;
global using System.Threading.Tasks;
global using DBBadgeUrls = CoL.DB.Entities.BadgeUrls;
global using DBClan = CoL.DB.Entities.Clan;
global using DBLeague = CoL.DB.Entities.League;
global using DBMember = CoL.DB.Entities.Member;
global using DBWar = CoL.DB.Entities.War;
global using DBWarClan = CoL.DB.Entities.WarClan;
global using DBWarMember = CoL.DB.Entities.WarMember;
// global using DBWarClanMember = CoL.DB.Entities.WarMemberOfClan;
// global using DBWarOpponentMember = CoL.DB.Entities.WarMemberOfOpponent;
using ClashOfLogs.Shared;
using CoL.DB;
using CoL.DB.Entities;
using CoL.Service.DataProvider;
using CoL.Service.Importer;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Lazy.Util.EntityModelMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Clan = ClashOfLogs.Shared.Clan;
using League = ClashOfLogs.Shared.League;
using Member = ClashOfLogs.Shared.Member;
using WarMember = ClashOfLogs.Shared.WarMember;

namespace CoL.Service;

public static class Program
{
    public static int Main(string[] args)
    {
        var config = BuildConfiguration();
        // access as var setting = config["Setting"]; var setting = config["Category:Setting"]; or config.GetSection<T>("section");

        var loggerConfiguration =
                new LoggerConfiguration() //todo: study if it is better to use a separate logger for the bootstrapper part
                    .ReadFrom.Configuration(config)
            // these are configured from appsettings.json
            // .MinimumLevel.Information()
            // .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            // .Enrich.FromLogContext()
            // .WriteTo.Console()
            // .WriteTo.File("col.log")
            ;

        Log.Logger = loggerConfiguration.CreateLogger();

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
        var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true);
        if (!string.IsNullOrWhiteSpace(env))
            builder.AddJsonFile($"appsettings.{env}.json", true, true);
        builder.AddEnvironmentVariables();
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

                services.AddDbContext<CoLContext>(options =>
                    {
                        options.UseSqlServer(configuration.GetConnectionString("CoLContext"),
                            sqlOptions => sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
#if DEBUG
                        options.LogTo(Log.Debug, LogLevel.Information);
                        options.ConfigureWarnings(warningOptions
                            => warningOptions.Log((RelationalEventId.CommandExecuting, LogLevel.Information)));
                        options.EnableSensitiveDataLogging();
#endif
                    },
                    ServiceLifetime.Singleton);

                // data providers
                services.AddTransient<IJsonDataProvider, FileJsonDataProvider>();
                // services.AddTransient<IJsonDataProvider, ApiJsonDataProvider>();

                // importers
                services.AddSingleton<EntityImporter<DBClan, Clan>, ClanImporter>();
                services.AddSingleton<EntityImporter<DBMember, Member>, MemberImporter>();
                services.AddSingleton<EntityImporter<DBLeague, League>, LeagueImporter>();
                services.AddSingleton<EntityImporter<DBWar, WarSummary>, WarLogImporter>();
                services.AddSingleton<EntityImporter<DBWar, WarDetail>, WarDetailImporter>();
                services.AddSingleton<EntityImporter<DBWarMember, WarMember>, WarMemberImporter>();
                // services.AddSingleton<EntityImporter<DBWarClanMember, WarMember>, WarMemberClanImporter>();
                // services.AddSingleton<EntityImporter<DBWarOpponentMember, WarMember>, WarMemberOpponentImporter>();

                // mappers
                services.AddSingleton<IMapper<DBClan, Clan>, ClanMapper>();
                services.AddTransient<IMapper<DBMember, Member>, MemberMapper>();
                services.AddSingleton<IMapper<DBLeague, League>, LeagueMapper>();
                services.AddSingleton<IMapper<DBWar, WarSummary>, WarSummaryMapper>();
                services.AddSingleton<IMapper<DBWar, WarDetail>, WarDetailMapper>();

                services.AddSingleton<IMapper<DBWarMember, WarMember>, WarMemberMapper>();
                // services.AddSingleton<IMapper<DBWarClanMember, WarMember>, WarMemberMapperForClan>();
                // services.AddSingleton<IMapper<DBWarOpponentMember, WarMember>, WarMemberMapperForOpponent>();

                // repositories
                services.AddTransient<IRepository<DBClan>, ClanRepository>();
                services.AddTransient<IRepository<DBMember>, MemberEfRepository>();
                services.AddTransient<IRepository<DBLeague>, LeagueEfRepository>();
                services.AddTransient<IRepository<DBWar>, WarRepository>();
                services.AddTransient<IRepository<DBWarMember>, WarMemberRepository>();
                // services.AddTransient<IRepository<DBWarClanMember>, WarClanMemberRepository>();
                // services.AddTransient<IRepository<DBWarOpponentMember>, WarClanOpponentMemberRepository>();

                //the actual service to run
                services.AddHostedService<Worker>();
            })
            .UseSerilog();
}