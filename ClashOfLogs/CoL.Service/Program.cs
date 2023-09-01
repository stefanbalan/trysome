global using System;
global using System.Threading.Tasks;
global using DBBadgeUrls = CoL.DB.Entities.BadgeUrls;
global using DBClan = CoL.DB.Entities.Clan;
global using DBLeague = CoL.DB.Entities.League;
global using DBMember = CoL.DB.Entities.Member;
global using DBWar = CoL.DB.Entities.War;
global using DBWarClan = CoL.DB.Entities.WarClan;
global using DBWarMember = CoL.DB.Entities.WarMember;
global using CoLContext = CoL.DB.Sqlite.CoLContextSqlite;
using System.IO;
using System.Linq;
using ClashOfLogs.Shared;
using CoL.DB.Sqlite;
using CoL.Service.DataProvider;
using CoL.Service.Importers;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using CoL.Service.Validators;
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

        var builder = new ConfigurationBuilder();

        if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
            builder.AddJsonFile($"/data/appsettings.json", true, true);
        else
        {
            builder.AddJsonFile("appsettings.json", true, true);
            if (!string.IsNullOrWhiteSpace(env))
                builder.AddJsonFile($"appsettings.{env}.json", true, true);
        }

        builder.AddEnvironmentVariables();
        return builder.Build();
    }


    private static IHostBuilder CreateHostBuilder(IConfigurationRoot config, string[] args) =>
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
                    lb.AddConfiguration(config);
                });


                //                 services.AddDbContext<CoLContext>(options =>
                //                     {
                //                         // options.UseSqlServer(config.GetConnectionString("CoLContext"),
                //                         //     sqlOptions => sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                //                         
                //                         // options.UseSqlite(config.GetConnectionString("CoLContext"),
                //                         //     sqlOptions => sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                //                         
                // #if DEBUG
                //                         // options.LogTo(Log.Debug, LogLevel.Information);
                //                         // options.ConfigureWarnings(warningOptions
                //                         //     => warningOptions.Log((RelationalEventId.CommandExecuting, LogLevel.Information)));
                //                         // options.EnableSensitiveDataLogging();
                // #endif
                //                     },
                //                     ServiceLifetime.Singleton);

                services.UseSqliteCoLContext(config.GetConnectionString("CoLContext")
                                             ?? throw new Exception("Missing db connection string configuration"));

                // data providers
                if (args.Contains("-files"))
                {
                    Log.Information("Using file data provider");
                    services.AddTransient<IJsonDataProvider, FileJsonDataProvider>();
                }
                else
                {
                    services.AddSingleton<IApiKeyProvider, AppSettingsApiKeyProvider>();
                    services.AddSingleton<ApiClient>();
                    services.AddHttpClient<ApiClient>(
                        client =>
                        {
                            client.BaseAddress = new Uri("https://api.clashofclans.com/v1/");
                            client.DefaultRequestHeaders.Add("Accept", "application/json");
                        });
                    services.AddSingleton<IJsonDataProvider, ApiJsonDataProvider>(
                        serviceProvider => new ApiJsonDataProvider(
                            serviceProvider.GetRequiredService<ILogger<ApiJsonDataProvider>>(),
                            serviceProvider.GetRequiredService<ApiClient>(),
                            config.GetValue<string>("ClanTag") ??
                            throw new Exception("Missing configuration ClanTag")));
                }

                services.AddSingleton(typeof(JsonBackup),
                    svcs =>
                        new JsonBackup(Path.Combine((config.GetValue<string>("JSONDirectory") ?? ""), "backup"),
                            svcs.GetService<ILogger<JsonBackup>>() ?? throw new Exception("Logger not configured")
                        ));

                //validators
                services.AddSingleton<IValidator<WarSummary>, WarSummaryValidator>();

                // importers
                services.AddSingleton<IEntityImporter<DBClan, Clan>, ClanImporter>();
                services.AddSingleton<IEntityImporter<DBMember, Member>, MemberImporter>();
                services.AddSingleton<IEntityImporter<DBLeague, League>, LeagueImporter>();
                services.AddSingleton<IEntityImporter<DBWar, WarSummary>, WarLogImporter>();
                services.AddSingleton<IEntityImporter<DBWar, WarDetail>, WarDetailImporter>();
                services.AddSingleton<IEntityImporter<DBWarMember, WarMember>, WarMemberImporter>();


                // mappers
                services.AddSingleton<IMapper<DBClan, Clan>, ClanMapper>();
                services.AddTransient<IMapper<DBMember, Member>, MemberMapper>();
                services.AddSingleton<IMapper<DBLeague, League>, LeagueMapper>();
                services.AddSingleton<IMapper<DBWar, WarSummary>, WarSummaryMapper>();
                services.AddSingleton<IMapper<DBWar, WarDetail>, WarDetailMapper>();

                services.AddSingleton<IMapper<DBWarMember, WarMember>, WarMemberMapper>();


                // repositories
                services.AddTransient<IRepository<DBClan>, ClanRepository>();
                services.AddTransient<IRepository<DBMember>, MemberEfRepository>();
                services.AddTransient<IRepository<DBLeague>, LeagueEfRepository>();
                services.AddTransient<IRepository<DBWar>, WarRepository>();
                services.AddTransient<IRepository<DBWarMember>, WarMemberRepository>();

                //the actual service to run
                services.AddHostedService<Worker>();
            })
            .UseSerilog();
}