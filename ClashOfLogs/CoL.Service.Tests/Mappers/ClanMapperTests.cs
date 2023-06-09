using CoL.Service.Mappers;

namespace CoL.Service.Tests.Mappers;

public class ClanMapperTests
{
    [Fact]
    public void CreateAndUpdateEntity_ValidRequest_ReturnsDBClan()
    {
        // Arrange
        var dbClan = new DBClan();
        var clan = new Clan
        {
            Tag = "#9JYRJGQY",
            Name = "Test Clan",
            Type = "inviteOnly",
            Description = "Testing purposes",
            ClanLevel = 15,
            ClanPoints = 5000,
            ClanVersusPoints = 6000,
            RequiredTrophies = 2500,
            WarFrequency = "always",
            WarWinStreak = 3,
            WarWins = 10,
            WarTies = 2,
            WarLosses = 4,
            IsWarLogPublic = true,
            RequiredVersusTrophies = 4000,
            RequiredTownhallLevel = 8,
            BadgeUrls = new BadgeUrls
            {
                Small = "https://fake-url.com/small.png",
                Medium = "https://fake-url.com/medium.png",
                Large = "https://fake-url.com/large.png",
            }
        };

        var mapper = new ClanMapper();

        // Act
        dbClan = mapper.CreateAndUpdateEntity(clan, DateTime.Now);

        // Assert
        Assert.Equal(clan.Tag, dbClan.Tag);
        Assert.Equal(clan.Name, dbClan.Name);
        Assert.Equal(clan.Type, dbClan.Type);
        Assert.Equal(clan.Description, dbClan.Description);
        Assert.Equal(clan.ClanLevel, dbClan.ClanLevel);
        Assert.Equal(clan.ClanPoints, dbClan.ClanPoints);
        Assert.Equal(clan.ClanVersusPoints, dbClan.ClanVersusPoints);
        Assert.Equal(clan.RequiredTrophies, dbClan.RequiredTrophies);
        Assert.Equal(clan.WarFrequency, dbClan.WarFrequency);
        Assert.Equal(clan.WarWinStreak, dbClan.WarWinStreak);
        Assert.Equal(clan.WarWins, dbClan.WarWins);
        Assert.Equal(clan.WarTies, dbClan.WarTies);
        Assert.Equal(clan.WarLosses, dbClan.WarLosses);
        Assert.Equal(clan.IsWarLogPublic, dbClan.IsWarLogPublic);
        Assert.Equal(clan.RequiredVersusTrophies, dbClan.RequiredVersusTrophies);
        Assert.Equal(clan.RequiredTownhallLevel, dbClan.RequiredTownhallLevel);
        Assert.NotNull(dbClan.BadgeUrls);
        if (dbClan.BadgeUrls != null)
        {
            Assert.Equal(clan.BadgeUrls.Small, dbClan.BadgeUrls.Small);
            Assert.Equal(clan.BadgeUrls.Medium, dbClan.BadgeUrls.Medium);
            Assert.Equal(clan.BadgeUrls.Large, dbClan.BadgeUrls.Large);
        }
    }

    [Fact]
    public void UpdateEntity_ValidRequest_ReturnsUpdatedDBClan()
    {
        // Arrange
        var dbClan = new DBClan
        {
            Tag = "#9JYRJGQY",
            Name = "Test Clan",
            Type = "inviteOnly",
            Description = "Testing purposes",
            ClanLevel = 15,
            ClanPoints = 5000,
            ClanVersusPoints = 6000,
            RequiredTrophies = 2500,
            WarFrequency = "always",
            WarWinStreak = 3,
            WarWins = 10,
            WarTies = 2,
            WarLosses = 4,
            IsWarLogPublic = true,
            RequiredVersusTrophies = 4000,
            RequiredTownhallLevel = 8,
            BadgeUrls = new DBBadgeUrls
            {
                Small = "https://fake-url.com/small.png",
                Medium = "https://fake-url.com/medium.png",
                Large = "https://fake-url.com/large.png",
            }
        };
        var clan = new Clan
        {
            Tag = "#9JYRJGQY",
            Name = "Updated Clan Name",
            Type = "closed",
            Description = "Testing purposes - updated",
            ClanLevel = 17,
            ClanPoints = 5500,
            ClanVersusPoints = 6500,
            RequiredTrophies = 3000,
            WarFrequency = "never",
            WarWinStreak = 0,
            WarWins = 20,
            WarTies = 5,
            WarLosses = 9,
            IsWarLogPublic = false,
            RequiredVersusTrophies = 4500,
            RequiredTownhallLevel = 9,
            BadgeUrls = new BadgeUrls
            {
                Small = "https://fake-url.com/updated-small.png",
                Medium = "https://fake-url.com/updated-medium.png",
                Large = "https://fake-url.com/updated-large.png",
            }
        };

        var mapper = new ClanMapper();

        // Act
        var changed = mapper.UpdateEntity(dbClan, clan, DateTime.Now);

        // Assert
        Assert.True(changed);
        Assert.Equal(clan.Name, dbClan.Name);
        Assert.Equal(clan.Type, dbClan.Type);
        Assert.Equal(clan.Description, dbClan.Description);
        Assert.Equal(clan.ClanLevel, dbClan.ClanLevel);
        Assert.Equal(clan.ClanPoints, dbClan.ClanPoints);
        Assert.Equal(clan.ClanVersusPoints, dbClan.ClanVersusPoints);
        Assert.Equal(clan.RequiredTrophies, dbClan.RequiredTrophies);
        Assert.Equal(clan.WarFrequency, dbClan.WarFrequency);
        Assert.Equal(clan.WarWinStreak, dbClan.WarWinStreak);
        Assert.Equal(clan.WarWins, dbClan.WarWins);
        Assert.Equal(clan.WarTies, dbClan.WarTies);
        Assert.Equal(clan.WarLosses, dbClan.WarLosses);
        Assert.Equal(clan.IsWarLogPublic, dbClan.IsWarLogPublic);
        Assert.Equal(clan.RequiredVersusTrophies, dbClan.RequiredVersusTrophies);
        Assert.Equal(clan.RequiredTownhallLevel, dbClan.RequiredTownhallLevel);
        Assert.NotNull(dbClan.BadgeUrls);
        if (dbClan.BadgeUrls != null)
        {
            Assert.Equal(clan.BadgeUrls.Small, dbClan.BadgeUrls.Small);
            Assert.Equal(clan.BadgeUrls.Medium, dbClan.BadgeUrls.Medium);
            Assert.Equal(clan.BadgeUrls.Large, dbClan.BadgeUrls.Large);
        }
    }
}