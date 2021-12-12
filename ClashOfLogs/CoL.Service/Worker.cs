using AutoMapper;

using ClashOfLogs.Shared;

using CoL.DB.mssql;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using DBBadgeUrls = CoL.DB.Entities.BadgeUrls;
using DBClan = CoL.DB.Entities.Clan;
using DBClanMember = CoL.DB.Entities.Member;
using DBWar = CoL.DB.Entities.War;
using DBWarClan = CoL.DB.Entities.WarClan;

namespace CoL.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly CoLContext context;
        private readonly IMapper mapper;
        private readonly string jsondirectory;

        public Worker(ILogger<Worker> logger, IConfiguration config, CoLContext context, IMapper mapper)
        {
            _logger = logger;
            _config = config;
            this.context = context;
            this.mapper = mapper;
            jsondirectory = config["JSONdirectory"];
            if (string.IsNullOrEmpty(jsondirectory))
            {
                jsondirectory = "JSON";
                config["JSONdirectory"] = jsondirectory;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var dbOk = await context.Database.EnsureCreatedAsync();
            if (!dbOk)
            {
                _logger.LogError("database not created");
                return;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);


                await ImportDirectories();

                await Task.Delay(120_000, stoppingToken);
            }
        }

        private async Task ImportDirectories()
        {
            try
            {
                if (!Directory.Exists(jsondirectory))
                {
                    Directory.CreateDirectory(jsondirectory);
                    var d = new DirectoryInfo(jsondirectory);
                }

                var importdates = Directory.EnumerateDirectories(jsondirectory);
                foreach (var importdate in importdates)
                {
                    var dir = new DirectoryInfo(importdate);
                    if (!dir.Exists) continue;

                    var dirDateString = dir.Name;
                    if (!DateTime.TryParse(dirDateString, out var dirDate)) continue;

                    await ImportFiles(dir, dirDate);
                }

            }
            catch (Exception ex) { }
        }

        async Task ImportFiles(DirectoryInfo dir, DateTime date)
        {
            var jsonFiles = dir.EnumerateFiles("*.json");

            foreach (var jsonFile in jsonFiles)
            {
                if (!jsonFile.Exists) continue;
                if (jsonFile.Name.StartsWith("clan")) await ImportClan(jsonFile);
                if (jsonFile.Name.StartsWith("warlog")) await ImportWarlog(jsonFile);
                if (jsonFile.Name.StartsWith("currentwar")) await ImportCurrentwar(jsonFile);
            }
        }

        private async Task ImportClan(FileInfo file)
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

            var exdbclan = await context.Clans.FindAsync(clan.Tag);
            if (exdbclan != null)
            {
                exdbclan.Name = clan.Name;
                exdbclan.Type = clan.Type;
                exdbclan.Description = clan.Description;
                exdbclan.ClanLevel = clan.ClanLevel;
                exdbclan.ClanPoints = clan.ClanPoints;
                exdbclan.ClanVersusPoints = clan.ClanVersusPoints;
                exdbclan.RequiredTrophies = clan.RequiredTrophies;
                exdbclan.WarFrequency = clan.WarFrequency;
                exdbclan.WarWinStreak = clan.WarWinStreak;
                exdbclan.WarWins = clan.WarWins;
                exdbclan.WarTies = clan.WarTies;
                exdbclan.WarLosses = clan.WarLosses;
                exdbclan.IsWarLogPublic = clan.IsWarLogPublic;
                exdbclan.RequiredVersusTrophies = clan.RequiredVersusTrophies;
                exdbclan.RequiredTownhallLevel = clan.RequiredTownhallLevel;

                var saved = await context.SaveChangesAsync();
            }
            else
            {
                var dbClan = mapper.Map<DBClan>(clan);
                context.Clans.Add(dbClan);
                var saved = await context.SaveChangesAsync();
            }

            foreach (var member in clan.MemberList)
            {
                await AddOrUpdateClanMember(member);
            }
        }

        private async Task AddOrUpdateClanMember(ClanMember member)
        {
            var exMember = await context.ClanMembers.FindAsync(member.Tag);
            if (exMember != null)
            {
                exMember.Name = member.Name;
                exMember.Role = member.Role;
                exMember.ExpLevel = member.ExpLevel;
                exMember.Trophies = member.Trophies;
                exMember.VersusTrophies = member.VersusTrophies;
                exMember.ClanRank = member.ClanRank;
                exMember.PreviousClanRank = member.PreviousClanRank;

                if (member.Donations < exMember.Donations)
                {
                    //new season
                    exMember.DonationsPreviousSeason = exMember.Donations;
                    exMember.DonationsReceivedPreviousSeason = exMember.DonationsReceived;
                }
                else
                {
                    exMember.Donations = member.Donations;
                    exMember.DonationsReceived = member.DonationsReceived;
                }

                var saved = await context.SaveChangesAsync();
            }
            else
            {
                var dbMember = mapper.Map<DBClanMember>(member);
                context.ClanMembers.Add(dbMember);
                var saved = await context.SaveChangesAsync();
            }
        }

        private async Task ImportWarlog(FileInfo file)
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


        private async Task ImportCurrentwar(FileInfo file)
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
            if (!DateTime.TryParse(war.EndTime, out var endTime))
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
