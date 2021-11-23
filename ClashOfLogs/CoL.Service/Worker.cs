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
using DBClan = CoL.DB.Entities.Clan;
using DBClanMember = CoL.DB.Entities.ClanMember;
using DBWarSummary = CoL.DB.Entities.WarSummary;

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
            }
        }



        private async Task ImportClan(FileInfo file)
        {

            if (!file.Exists) return;
            var stream = file.OpenRead();
            var clan = await JsonSerializer.DeserializeAsync<Clan>(stream);
            stream.Close();

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

            if (!file.Exists) return;
            var stream = file.OpenRead();
            var warlog = await JsonSerializer.DeserializeAsync<Warlog>(stream);
            stream.Close();

            foreach (var warSummary in warlog.Items)
            {

                await AddOrUpdateWarSummary(warSummary);
            }
        }

        private async Task AddOrUpdateWarSummary(WarSummary warSummary)
        {
            var exdbWarSummary = await context.WarSummaries
               .FindAsync(warSummary.EndTime, warSummary.Clan.Tag, warSummary.Opponent.Tag);

            if (exdbWarSummary != null)
                return;

            var dbWarSummary = new DBWarSummary
            {
                Result = warSummary.Result,
                EndTime = warSummary.EndTime,
                TeamSize = warSummary.TeamSize,
                AttacksPerMember = warSummary.AttacksPerMember,

                ClanTag = warSummary.Clan.Tag,
                ClanName = warSummary.Clan.Name,
                ClanBadgeUrlSmall = warSummary.Clan.BadgeUrls.Small,
                ClanBadgeUrlLarge = warSummary.Clan.BadgeUrls.Large,
                ClanBadgeUrlMedium = warSummary.Clan.BadgeUrls.Medium,
                ClanClanLevel = warSummary.Clan.ClanLevel,
                ClanAttacks = warSummary.Clan.Attacks,
                ClanStars = warSummary.Clan.Stars,
                ClanDestructionPercentage = warSummary.Clan.DestructionPercentage,

                OpponentTag = warSummary.Opponent.Tag,
                OpponentName = warSummary.Opponent.Name,
                OpponentBadgeUrlSmall = warSummary.Opponent.BadgeUrls.Small,
                OpponentBadgeUrlLarge = warSummary.Opponent.BadgeUrls.Large,
                OpponentBadgeUrlMedium = warSummary.Opponent.BadgeUrls.Medium,
                OpponentClanLevel = warSummary.Opponent.ClanLevel,
                OpponentAttacks = warSummary.Opponent.Attacks,
                OpponentStars = warSummary.Opponent.Stars,
                OpponentDestructionPercentage = warSummary.Opponent.DestructionPercentage
            };

            context.WarSummaries.Add(dbWarSummary);
            var saved = await context.SaveChangesAsync();
        }

    }
}
