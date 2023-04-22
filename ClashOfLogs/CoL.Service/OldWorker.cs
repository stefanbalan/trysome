#nullable disable
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Threading;
using ClashOfLogs.Shared;
using CoL.DB;
using CoL.Service.DataProvider;
using CoL.Service.Importer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoL.Service;

public class OldWorker : BackgroundService
{
    private readonly IHostApplicationLifetime hostApplicationLifetime;
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _config;
    private readonly CoLContext context;
    private readonly IJsonDataProvider importDataProvider;
    private readonly EntityImporter<DBClan, Clan> clanDataImporter;

    public OldWorker(
        IHostApplicationLifetime hostApplicationLifetime,
        ILogger<Worker> logger,
        IConfiguration config,
        CoLContext context,
        IJsonDataProvider importDataProvider,
        EntityImporter<DBClan, Clan> clanDataImporter)
    {
        this.hostApplicationLifetime = hostApplicationLifetime;
        _logger = logger;
        _config = config;
        this.context = context;
        this.importDataProvider = importDataProvider;
        this.clanDataImporter = clanDataImporter;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }

    private async Task AddOrUpdateWarSummary(WarSummary warSummary)
    {
        if (!DateTime.TryParse(warSummary.EndTime, out var endTime))
        {
            _logger.LogError($"Cannot import warsummary, invalid end time {warSummary.EndTime} " +
                             $"clan:{warSummary.Clan.Tag} opponent:{warSummary.Opponent.Tag}");
            return;
        }

        var exdbWarSummary = await context.Wars
            .FindAsync(endTime, warSummary.Clan.Tag, warSummary.Opponent.Tag);

        if (exdbWarSummary != null)
            return;

        var dbWarSummary = new DBWar
        {
            Result = warSummary.Result,
            EndTime = endTime,
            TeamSize = warSummary.TeamSize,
            AttacksPerMember = warSummary.AttacksPerMember,

            //ClanTag = warSummary.Clan.Tag,
            Clan = new DBWarClan
            {
                Name = warSummary.Clan.Name,
                BadgeUrls =
                    new DBBadgeUrls
                    {
                        Small = warSummary.Clan.BadgeUrls.Small,
                        Large = warSummary.Clan.BadgeUrls.Large,
                        Medium = warSummary.Clan.BadgeUrls.Medium,
                    },
                ClanLevel = warSummary.Clan.ClanLevel,
                Attacks = warSummary.Clan.Attacks,
                Stars = warSummary.Clan.Stars,
                DestructionPercentage = warSummary.Clan.DestructionPercentage,
            },

            //OpponentTag = warSummary.Opponent.Tag,
            Opponent = new DBWarClan
            {
                Tag = warSummary.Opponent.Tag,
                Name = warSummary.Opponent.Name,
                BadgeUrls =
                    new DBBadgeUrls
                    {
                        Small = warSummary.Opponent.BadgeUrls.Small,
                        Large = warSummary.Opponent.BadgeUrls.Large,
                        Medium = warSummary.Opponent.BadgeUrls.Medium,
                    },
                ClanLevel = warSummary.Opponent.ClanLevel,
                Attacks = warSummary.Opponent.Attacks,
                Stars = warSummary.Opponent.Stars,
                DestructionPercentage = warSummary.Opponent.DestructionPercentage
            }
        };

        context.Wars.Add(dbWarSummary);
        var saved = await context.SaveChangesAsync();
    }


    private async Task ImportCurrentwar(FileInfo file, DateTime date)
    {
        WarDetail wardetail = null;
        FileStream stream = null;

        if (!file.Exists) return;
        try
        {
            stream = file.OpenRead();
            wardetail = await JsonSerializer.DeserializeAsync<WarDetail>(stream);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to deserialize {file.FullName}");
            return;
        }
        finally { stream.Close(); }

        await AddOrUpdateWarDetail(wardetail);
    }

    private async Task AddOrUpdateWarDetail(WarDetail war)
    {
        if (!DateTime.TryParseExact(war.EndTime, @"yyyyMMdd\THHmmss.fff\Z", CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal, out var endTime))
        {
            _logger.LogError($"Cannot import warsummary, invalid end time {war.EndTime} " +
                             $"clan:{war.Clan.Tag} opponent:{war.Opponent.Tag}");
            return;
        }

        var exdbWarSummary = await context.Wars
            .FindAsync(endTime, war.Clan.Tag, war.Opponent.Tag);

        if (exdbWarSummary != null)
            return;

        var dbWarSummary = new DBWar
        {
            EndTime = endTime,
            TeamSize = war.TeamSize,
            AttacksPerMember = war.AttacksPerMember,

            //ClanTag = war.Clan.Tag,
            Clan = new DBWarClan
            {
                Name = war.Clan.Name,
                BadgeUrls =
                    new DBBadgeUrls
                    {
                        Small = war.Clan.BadgeUrls.Small,
                        Large = war.Clan.BadgeUrls.Large,
                        Medium = war.Clan.BadgeUrls.Medium,
                    },
                ClanLevel = war.Clan.ClanLevel,
                Attacks = war.Clan.Attacks,
                Stars = war.Clan.Stars,
                DestructionPercentage = war.Clan.DestructionPercentage,
            },

            //OpponentTag = war.Opponent.Tag,
            Opponent = new DBWarClan
            {
                Tag = war.Opponent.Tag,
                Name = war.Opponent.Name,
                BadgeUrls =
                    new DBBadgeUrls
                    {
                        Small = war.Opponent.BadgeUrls.Small,
                        Large = war.Opponent.BadgeUrls.Large,
                        Medium = war.Opponent.BadgeUrls.Medium,
                    },
                ClanLevel = war.Opponent.ClanLevel,
                Attacks = war.Opponent.Attacks,
                Stars = war.Opponent.Stars,
                DestructionPercentage = war.Opponent.DestructionPercentage
            }
        };

        context.Wars.Add(dbWarSummary);
        var saved = await context.SaveChangesAsync();
    }
}