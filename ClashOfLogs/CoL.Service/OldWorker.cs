#nullable disable
using ClashOfLogs.Shared;

using CoL.DB.mssql;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Threading;


namespace CoL.Service
{
    public class OldWorker : BackgroundService
    {
        private readonly IHostApplicationLifetime hostApplicationLifetime;
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly CoLContext context;
        private readonly IJsonDataProvider importDataProvider;
        private readonly EntityImporter<DBClan, Clan, string> clanDataImporter;

        public OldWorker(
            IHostApplicationLifetime hostApplicationLifetime,
            ILogger<Worker> logger,
            IConfiguration config,
            CoLContext context,
            IJsonDataProvider importDataProvider,
            EntityImporter<DBClan, Clan, string> clanDataImporter)
        {
            this.hostApplicationLifetime = hostApplicationLifetime;
            _logger = logger;
            _config = config;
            this.context = context;
            this.importDataProvider = importDataProvider;
            this.clanDataImporter = clanDataImporter;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var dbOk = context.Database.CanConnect();
            if (!dbOk)
            {
                _logger.LogError("database not available");
                hostApplicationLifetime.StopApplication();
                return;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                if (importDataProvider.HasImportData())
                {
                    var jsonData = await importDataProvider.GetImportDataAsync();

                    await clanDataImporter.ImportAsync(jsonData.Clan, jsonData.Date);
                }

                await Task.Delay(120_000, stoppingToken);
            }
        }


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        async Task ImportFiles(DirectoryInfo dir, DateTime date)
        {
            var jsonFiles = dir.EnumerateFiles("*.json");

            foreach (var jsonFile in jsonFiles)
            {
                if (!jsonFile.Exists) continue;
                if (jsonFile.Name.StartsWith("clan")) await ImportClan(jsonFile, date);
                if (jsonFile.Name.StartsWith("warlog")) await ImportWarlog(jsonFile, date);
                if (jsonFile.Name.StartsWith("currentwar")) await ImportCurrentwar(jsonFile, date);
            }
        }

        private async Task ImportClan(FileInfo file, DateTime date)
        {
            Clan clan = null;
            FileStream stream = null;
            try
            {
                if (!file.Exists) return;
                stream = file.OpenRead();
                clan = await JsonSerializer.DeserializeAsync<Clan>(stream);
            }
            catch (Exception ex) { _logger.LogError(ex, $"Failed to deserialize {file.FullName}"); }
            finally
            {
                stream.Close();
            }

            var dbclan = await context.Clans.FindAsync(clan.Tag);


            if (dbclan == null)
            {
                dbclan = new DBClan
                {
                    Tag = clan.Tag,
                };

                context.Clans.Add(dbclan);
            }

            dbclan.Name = clan.Name;
            dbclan.Type = clan.Type;
            dbclan.Description = clan.Description;
            dbclan.ClanLevel = clan.ClanLevel;
            dbclan.ClanPoints = clan.ClanPoints;
            dbclan.ClanVersusPoints = clan.ClanVersusPoints;
            dbclan.RequiredTrophies = clan.RequiredTrophies;
            dbclan.WarFrequency = clan.WarFrequency;
            dbclan.WarWinStreak = clan.WarWinStreak;
            dbclan.WarWins = clan.WarWins;
            dbclan.WarTies = clan.WarTies;
            dbclan.WarLosses = clan.WarLosses;
            dbclan.IsWarLogPublic = clan.IsWarLogPublic;
            dbclan.RequiredVersusTrophies = clan.RequiredVersusTrophies;
            dbclan.RequiredTownhallLevel = clan.RequiredTownhallLevel;

            dbclan.BadgeUrls = new DBBadgeUrls()
            {
                Large = clan.BadgeUrls.Large,
                Medium = clan.BadgeUrls.Medium,
                Small = clan.BadgeUrls.Small
            };

            dbclan.UpdatedAt = date;

            var saved = await context.SaveChangesAsync();
            if (saved > 0) _logger.LogInformation($"Clan {dbclan.Tag} updated");

            foreach (var member in clan.Members)
            {
                await AddOrUpdateClanMember(dbclan, member, date);
            }
        }

        private async Task AddOrUpdateClanMember(DBClan dbclan, Member member, DateTime date)
        {
            var dbMember = await context.ClanMembers.FindAsync(member.Tag);
            if (dbMember == null)
            {
                dbMember = new DBMember
                {
                    Tag = member.Tag,
                    ClanTag = dbclan.Tag,
                    CreatedAt = date
                };
            }


            dbMember.Name = member.Name;
            dbMember.Role = member.Role;
            dbMember.ExpLevel = member.ExpLevel;
            dbMember.Trophies = member.Trophies;
            dbMember.VersusTrophies = member.VersusTrophies;
            dbMember.ClanRank = member.ClanRank;
            dbMember.PreviousClanRank = member.PreviousClanRank;
            dbMember.CreatedAt = date;

            if (member.Donations < dbMember.Donations)
            {
                //new season
                dbMember.DonationsPreviousSeason = dbMember.Donations;
                dbMember.DonationsReceivedPreviousSeason = dbMember.DonationsReceived;
            }
            else
            {
                dbMember.Donations = member.Donations;
                dbMember.DonationsReceived = member.DonationsReceived;
            }
            dbclan.Members.Add(dbMember);

            var saved = await context.SaveChangesAsync();
            if (saved > 0) _logger.LogInformation($"Clan member {dbMember.Name}({dbMember.Tag}) updated");
        }

        private async Task ImportWarlog(FileInfo file, DateTime date)
        {
            Warlog warlog = null;
            FileStream stream = null;

            if (!file.Exists) return;
            try
            {
                stream = file.OpenRead();
                warlog = await JsonSerializer.DeserializeAsync<Warlog>(stream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to deserialize {file.FullName}");
                return;
            }
            finally { stream.Close(); }

            foreach (var warSummary in warlog.Items)
            {
                await AddOrUpdateWarSummary(warSummary);
            }
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
                    BadgeUrls = new DBBadgeUrls
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
                    BadgeUrls = new DBBadgeUrls
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
            if (!DateTime.TryParseExact(war.EndTime, @"yyyyMMdd\THHmmss.fff\Z", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var endTime))
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
                    BadgeUrls = new DBBadgeUrls
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
                    BadgeUrls = new DBBadgeUrls
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
}
