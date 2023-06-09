using CoL.Service.Importer;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoL.Service.Tests.Mappers;

public class MemberMapperTests
{
    [Fact]
    public void CreateAndUpdateEntity_ValidRequest_ReturnsDBMember()
    {
        // Arrange
        var timeStamp = DateTime.Now;
        var league = new League
        {
            Id = 9001,
            Name = "Test league",
            IconUrls = new IconUrls
            {
                Small = "https://fake-url.com/small.png",
                Medium = "https://fake-url.com/medium.png",
                Tiny = "https://fake-url.com/tiny.png",
            }
        };
        DBLeague dbLeague = new()
        {
            Id = 9001,
            Name = "Test league",
            IconUrls = new DB.Entities.IconUrls
            {
                Small = "https://fake-url.com/small.png",
                Medium = "https://fake-url.com/medium.png",
                Tiny = "https://fake-url.com/tiny.png",
            }
        };

        var member = new Member
        {
            Tag = "@1234ASDF",
            Name = "Test member",
            Role = "Elder",
            ExpLevel = 5,
            League = league,
            Trophies = 1234,
            VersusTrophies = 2345,
            ClanRank = 5,
            PreviousClanRank = 7,
            Donations = 1000,
            DonationsReceived = 1200
        };

        var leagueMapperMock = new Mock<IMapper<DBLeague, League>>();
        var leagueRepositoryMock = new Mock<IRepository<DBLeague>>();
        var leagueImporterLoggerMock = new Mock<ILogger<EntityImporter<DBLeague, League>>>();
        var leagueImporterMock = new Mock<EntityImporter<DBLeague, League>>(
            () => new LeagueImporter(leagueMapperMock.Object, leagueRepositoryMock.Object,
                leagueImporterLoggerMock.Object)
        );

        leagueImporterMock.Setup(importer => importer.ImportAsync(
                It.IsAny<League>(), It.IsAny<DateTime>(), false))
            .Returns(() => Task.FromResult(dbLeague)!);

        var mapper = new MemberMapper(leagueImporterMock.Object);

        // Act
        var dbMember = mapper.CreateAndUpdateEntity(member, DateTime.Now);

        // Assert
        Assert.Equal(member.Tag, dbMember.Tag);
        Assert.Equal(member.Name, dbMember.Name);
        Assert.Equal(member.Role, dbMember.Role);
        Assert.Equal(member.ExpLevel, dbMember.ExpLevel);
        Assert.Equal(member.Trophies, dbMember.Trophies);
        Assert.Equal(member.VersusTrophies, dbMember.VersusTrophies);
        Assert.Equal(member.ClanRank, dbMember.ClanRank);
        Assert.Equal(member.PreviousClanRank, dbMember.PreviousClanRank);
        Assert.Equal(member.Donations, dbMember.Donations);
        Assert.Equal(member.DonationsReceived, dbMember.DonationsReceived);

        leagueImporterMock.Verify(leagueImporter =>
            leagueImporter.ImportAsync(It.IsAny<League>(), It.IsAny<DateTime>(), false), Times.Once);
        Assert.NotNull(dbMember.League);
        if (dbMember.League != null)
        {
            Assert.Equal(dbMember.League.Id, league.Id);
            Assert.Equal(dbMember.League.Name, league.Name);
            Assert.NotNull(dbMember.League.IconUrls);
            if (dbMember.League.IconUrls != null)
            {
                Assert.Equal(dbMember.League.IconUrls.Small, league.IconUrls.Small);
                Assert.Equal(dbMember.League.IconUrls.Medium, league.IconUrls.Medium);
                Assert.Equal(dbMember.League.IconUrls.Tiny, league.IconUrls.Tiny);
            }
        }
    }

    [Fact]
    public void UpdateEntity_ValidRequest_ReturnsUpdateddbLeague()
    {
        // Arrange
        var timeStamp = DateTime.Now;
        var dbLeague = new DBLeague
        {
            Id = 9001,
            Name = "Test league",
            IconUrls = new CoL.DB.Entities.IconUrls
            {
                Small = "https://fake-url.com/small.png",
                Medium = "https://fake-url.com/medium.png",
                Tiny = "https://fake-url.com/tiny.png",
            }
        };
        var league = new League
        {
            Id = 9002,
            Name = "Test league updated",
            IconUrls = new IconUrls
            {
                Small = "https://fake-url.com/updated-small.png",
                Medium = "https://fake-url.com/updated-medium.png",
                Tiny = "https://fake-url.com/updated-tiny.png",
            }
        };
        var dbMember = new DBMember
        {
            Tag = "@234ASDFG",
            Name = "Test member 1",
            Role = "Peasant",
            ExpLevel = 3,
            League = dbLeague,
            Trophies = 234,
            VersusTrophies = 345,
            ClanRank = 9,
            PreviousClanRank = 10,
            Donations = 10,
            DonationsReceived = 12
        };
        var member = new Member
        {
            Tag = "@1234ASDF",
            Name = "Test member",
            Role = "Elder",
            ExpLevel = 5,
            League = league,
            Trophies = 1234,
            VersusTrophies = 2345,
            ClanRank = 5,
            PreviousClanRank = 7,
            Donations = 1000,
            DonationsReceived = 1200
        };


        var leagueMapperMock = new Mock<IMapper<DBLeague, League>>();
        var leagueRepositoryMock = new Mock<IRepository<DBLeague>>();
        var leagueImporterLoggerMock = new Mock<ILogger<EntityImporter<DBLeague, League>>>();
        var leagueImporterMock = new Mock<EntityImporter<DBLeague, League>>(
            () => new LeagueImporter(leagueMapperMock.Object, leagueRepositoryMock.Object,
                leagueImporterLoggerMock.Object)
        );

        leagueImporterMock.Setup(importer => importer.ImportAsync(
                It.IsAny<League>(), It.IsAny<DateTime>(), false))
            .Returns(() => Task.FromResult(dbLeague)!);

        var mapper = new MemberMapper(leagueImporterMock.Object);


        // Act
        var changed = mapper.UpdateEntity(dbMember, member, timeStamp);

        // Assert
        Assert.True(changed);


        // Assert
        Assert.Equal(member.Tag, dbMember.Tag);
        Assert.Equal(member.Name, dbMember.Name);
        Assert.Equal(member.Role, dbMember.Role);
        Assert.Equal(member.ExpLevel, dbMember.ExpLevel);
        Assert.Equal(member.Trophies, dbMember.Trophies);
        Assert.Equal(member.VersusTrophies, dbMember.VersusTrophies);
        Assert.Equal(member.ClanRank, dbMember.ClanRank);
        Assert.Equal(member.PreviousClanRank, dbMember.PreviousClanRank);
        Assert.Equal(member.Donations, dbMember.Donations);
        Assert.Equal(member.DonationsReceived, dbMember.DonationsReceived);

        leagueImporterMock.Verify(leagueImporter =>
            leagueImporter.ImportAsync(It.IsAny<League>(), It.IsAny<DateTime>(), false), Times.Once);
        Assert.NotNull(dbMember.League);
        if (dbMember.League != null)
        {
            Assert.Equal(dbMember.League.Id, dbLeague.Id);
            Assert.Equal(dbMember.League.Name, dbLeague.Name);
            Assert.NotNull(dbMember.League.IconUrls);
            if (dbMember.League.IconUrls != null)
            {
                Assert.Equal(dbMember.League.IconUrls.Small, dbLeague.IconUrls.Small);
                Assert.Equal(dbMember.League.IconUrls.Medium, dbLeague.IconUrls.Medium);
                Assert.Equal(dbMember.League.IconUrls.Tiny, dbLeague.IconUrls.Tiny);
            }
        }
    }
}