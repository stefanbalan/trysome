using CoL.Service.Mappers;

namespace CoL.Service.Tests.Mappers;

public class LeagueMapperTests
{
    [Fact]
    public void CreateAndUpdateEntity_ValidRequest_ReturnsDBLeague()
    {
        // Arrange
        var league = new League {
            Id = 9001,
            Name = "Test league",
            IconUrls = new IconUrls {
                Small = "https://fake-url.com/small.png",
                Medium = "https://fake-url.com/medium.png",
                Tiny = "https://fake-url.com/tiny.png",
            }
        };

        var mapper = new LeagueMapper();

        // Act
        var dbLeague = mapper.CreateAndUpdateEntity(league, DateTime.Now);

        // Assert
        Assert.Equal(league.Id, dbLeague.Id);
        Assert.Equal(league.Name, dbLeague.Name);
        Assert.NotNull(dbLeague.IconUrls);
        if (dbLeague.IconUrls != null)
        {
            Assert.Equal(league.IconUrls.Small, dbLeague.IconUrls.Small);
            Assert.Equal(league.IconUrls.Medium, dbLeague.IconUrls.Medium);
            Assert.Equal(league.IconUrls.Tiny, dbLeague.IconUrls.Tiny);
        }
    }

    [Fact]
    public void UpdateEntity_ValidRequest_ReturnsUpdateddbLeague()
    {
        // Arrange
        var dbLeague = new DBLeague {
            Id = 9001,
            Name = "Test league",
            IconUrls = new CoL.DB.Entities.IconUrls {
                Small = "https://fake-url.com/small.png",
                Medium = "https://fake-url.com/medium.png",
                Tiny = "https://fake-url.com/tiny.png",
            }
        };
        var league = new League {
            Id = 9002,
            Name = "Test league updated",
            IconUrls = new IconUrls {
                Small = "https://fake-url.com/updated-small.png",
                Medium = "https://fake-url.com/updated-medium.png",
                Tiny = "https://fake-url.com/updated-tiny.png",
            }
        };

        var mapper = new LeagueMapper();

        // Act
        var changed = mapper.UpdateEntity(dbLeague, league, DateTime.Now);

        // Assert
        Assert.True(changed);

        Assert.Equal(league.Id, dbLeague.Id);
        Assert.Equal(league.Name, dbLeague.Name);
        Assert.NotNull(dbLeague.IconUrls);
        if (dbLeague.IconUrls != null)
        {
            Assert.Equal(league.IconUrls.Small, dbLeague.IconUrls.Small);
            Assert.Equal(league.IconUrls.Medium, dbLeague.IconUrls.Medium);
            Assert.Equal(league.IconUrls.Tiny, dbLeague.IconUrls.Tiny);
        }
    }
}