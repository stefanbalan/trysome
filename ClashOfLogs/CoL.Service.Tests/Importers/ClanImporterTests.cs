using CoL.Service.Importers;
using CoL.Service.Mappers;
using CoL.Service.Repository;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CoL.Service.Tests.Importers;

public class ClanImporterTests
{
    private readonly Mock<IMapper<DBClan, Clan>> mapper = new();
    private readonly Mock<IRepository<DBClan>> repositoryMock = new();
    private readonly Mock<IEntityImporter<DBLeague, League>> leagueImporterMock = new();
    private readonly Mock<IEntityImporter<DBMember, Member>> memberImporterMock = new();


    [Fact]
    public async Task ImportAsync_RepositoryGetByIdFails_ReturnsNull()
    {
        // Arrange
        var timeStamp = DateTime.UtcNow;
        var clan = new Clan {
            Tag = "#1234",
            Members = new List<Member> {
                new() {
                    Tag = "#0001",
                    League = new() { Id = 1 }
                },
                new() { Tag = "#0002" }
            }
        };

        var updatedDbClan = new DBClan {
            Tag = "#1234"
        };


        repositoryMock
            .Setup(r => r.GetByIdAsync("#1234"))
            .Throws(new Exception("Test repository error"));

        var importer = new ClanImporter(mapper.Object,
            repositoryMock.Object,
            NullLogger<ClanImporter>.Instance,
            leagueImporterMock.Object,
            memberImporterMock.Object);

        // Act
        var result = await importer.ImportAsync(clan, timeStamp);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ImportAsync_NoExistingDBEntity()
    {
        // Arrange
        var timeStamp = DateTime.UtcNow;
        var clan = new Clan {
            Tag = "#1234",
            Members = new List<Member> {
                new() {
                    Tag = "#0001",
                    League = new() { Id = 1 }
                },
                new() { Tag = "#0002" }
            }
        };

        var updatedDbClan = new DBClan {
            Tag = "#1234"
        };

        mapper
            .Setup(m => m.CreateAndUpdateEntity(It.IsAny<Clan>(), timeStamp))
            .Returns(updatedDbClan);

        repositoryMock
            .Setup(r => r.GetByIdAsync("#1234"))
            .ReturnsAsync((DBClan?)null);

        leagueImporterMock
            .Setup(l => l.ImportAsync(It.IsAny<League>(), timeStamp, false))
            .ReturnsAsync(new DBLeague { Id = 1 })
            .Verifiable();

        memberImporterMock
            .Setup(m => m.ImportAsync(It.IsAny<Member>(), timeStamp, false))
            .ReturnsAsync(new DBMember());

        var importer = new ClanImporter(mapper.Object,
            repositoryMock.Object,
            NullLogger<ClanImporter>.Instance,
            leagueImporterMock.Object,
            memberImporterMock.Object);


        // Act
        var result = await importer.ImportAsync(clan, timeStamp);

        // Assert
        Assert.Equal(updatedDbClan, result);
        repositoryMock.Verify(r => r.GetByIdAsync("#1234"), Times.Once);

        leagueImporterMock.Verify(l => l.ImportAsync(It.IsAny<League>(), timeStamp, false),
            Times.Exactly(1));
        memberImporterMock.Verify(m => m.ImportAsync(It.IsAny<Member>(), timeStamp, false),
            Times.Exactly(2));

        repositoryMock.Verify(r => r.Add(updatedDbClan), Times.Once);
        repositoryMock.Verify(r => r.PersistChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task ImportAsync_ExistingDBEntity()
    {
        // Arrange
        var timeStamp = DateTime.UtcNow;
        var dbClan = new DBClan { Tag = "#1234" };

        Mock<IMapper<DBClan, Clan>> mapper = new();

        Mock<IRepository<DBClan>> repositoryMock = new();
        repositoryMock.Setup(r => r.GetByIdAsync("#1234")).ReturnsAsync(dbClan);

        Mock<IEntityImporter<DBLeague, League>> leagueImporterMock = new();
        Mock<IEntityImporter<DBMember, Member>> memberImporterMock = new();

        var importer = new ClanImporter(mapper.Object, repositoryMock.Object, new NullLogger<ClanImporter>(),
            leagueImporterMock.Object, memberImporterMock.Object);

        var clan = new Clan {
            Tag = "#1234",
            Members = new List<Member> {
                new() { Tag = "#0001" },
                new() { Tag = "#0002" }
            }
        };

        // Act
        var result = await importer.ImportAsync(clan, timeStamp);

        // Assert
        Assert.Equal(dbClan, result);
    }
}