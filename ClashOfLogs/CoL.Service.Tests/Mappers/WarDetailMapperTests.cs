using CoL.Service.Mappers;
using BadgeUrls = ClashOfLogs.Shared.BadgeUrls;
using WarClan = ClashOfLogs.Shared.WarClan;

public class WarDetailMapperTests
{
    [Fact]
    public void Map_CreatesWarDetail_ReturnsMappedWarDetail()
    {
        // Arrange
        WarDetailMapper mapper = new();

        var now = DateTime.UtcNow;
        WarDetail war = new()
        {
            State = "InWar",
            TeamSize = 5,
            AttacksPerMember = 2,
            PreparationStartTime = now,
            StartTime = now,
            EndTime = now.AddHours(24),
            Clan = new WarClan
            {
                Tag = "#123456",
                Name = "Test Clan",
                ClanLevel = 5,
                Attacks = 10,
                Stars = 15,
                DestructionPercentage = 95,
                BadgeUrls = new BadgeUrls
                {
                    Small = "https://smallbadgeurl",
                    Medium = "https://mediumbadgeurl",
                    Large = "https://largebadgeurl"
                }
            },
            Opponent = new WarClan
            {
                Tag = "#654321",
                Name = "Test Opponent",
                ClanLevel = 7,
                Attacks = 20,
                Stars = 25,
                DestructionPercentage = 85,
                BadgeUrls = new BadgeUrls
                {
                    Small = "https://smallbadgeurl2",
                    Medium = "https://mediumbadgeurl2",
                    Large = "https://largebadgeurl2"
                }
            }
        };

        // Act
        var dbwar = mapper.Get1From(war);

        // Assert
        Assert.NotNull(dbwar);
        Assert.Equal(war.State, dbwar.State);
        Assert.Equal(war.TeamSize, dbwar.TeamSize);
        Assert.Equal(war.AttacksPerMember, dbwar.AttacksPerMember);
        Assert.Equal(war.PreparationStartTime, dbwar.PreparationStartTime);
        Assert.Equal(war.StartTime, dbwar.StartTime);
        Assert.Equal(war.EndTime, dbwar.EndTime);
        Assert.Equal(war.Clan.Tag, dbwar.Clan.Tag);
        Assert.Equal(war.Clan.Name, dbwar.Clan.Name);
        Assert.Equal(war.Clan.ClanLevel, dbwar.Clan.ClanLevel);
        Assert.Equal(war.Clan.Attacks, dbwar.Clan.Attacks);
        Assert.Equal(war.Clan.Stars, dbwar.Clan.Stars);
        Assert.Equal(war.Clan.DestructionPercentage, dbwar.Clan.DestructionPercentage);

        Assert.NotNull(dbwar.Clan.BadgeUrls);
        if (dbwar.Clan.BadgeUrls != null)
        {
            Assert.Equal(war.Clan.BadgeUrls.Small, dbwar.Clan.BadgeUrls.Small);
            Assert.Equal(war.Clan.BadgeUrls.Medium, dbwar.Clan.BadgeUrls.Medium);
            Assert.Equal(war.Clan.BadgeUrls.Large, dbwar.Clan.BadgeUrls.Large);
        }

        Assert.Equal(war.Opponent.Tag, dbwar.Opponent.Tag);
        Assert.Equal(war.Opponent.Name, dbwar.Opponent.Name);
        Assert.Equal(war.Opponent.ClanLevel, dbwar.Opponent.ClanLevel);
        Assert.Equal(war.Opponent.Attacks, dbwar.Opponent.Attacks);
        Assert.Equal(war.Opponent.Stars, dbwar.Opponent.Stars);
        Assert.Equal(war.Opponent.DestructionPercentage, dbwar.Opponent.DestructionPercentage);

        Assert.NotNull(dbwar.Opponent.BadgeUrls);
        if (dbwar.Opponent.BadgeUrls != null)
        {
            Assert.Equal(war.Opponent.BadgeUrls.Small, dbwar.Opponent.BadgeUrls.Small);
            Assert.Equal(war.Opponent.BadgeUrls.Medium, dbwar.Opponent.BadgeUrls.Medium);
            Assert.Equal(war.Opponent.BadgeUrls.Large, dbwar.Opponent.BadgeUrls.Large);
        }
    }

    [Fact]
    public void Map_UpdatesWarDetail_ReturnsMappedWarDetail()
    {
        // Arrange
        WarDetailMapper mapper = new();

        var now = DateTime.UtcNow;
        DBWar dbwar = new()
        {
            State = "InWar",
            TeamSize = 5,
            AttacksPerMember = 2,
            PreparationStartTime = now,
            StartTime = now,
            EndTime = now.AddHours(24),
            Clan = new DBWarClan
            {
                Tag = "#123456",
                Name = "Test Clan",
                ClanLevel = 5,
                Attacks = 10,
                Stars = 15,
                DestructionPercentage = 95,
                BadgeUrls = new DBBadgeUrls
                {
                    Small = "https://smallbadgeurl",
                    Medium = "https://mediumbadgeurl",
                    Large = "https://largebadgeurl"
                }
            },
            Opponent = new DBWarClan
            {
                Tag = "#654321",
                Name = "Test Opponent",
                ClanLevel = 7,
                Attacks = 20,
                Stars = 25,
                DestructionPercentage = 85,
                BadgeUrls = new DBBadgeUrls
                {
                    Small = "https://smallbadgeurl2",
                    Medium = "https://mediumbadgeurl2",
                    Large = "https://largebadgeurl2"
                }
            }
        };

        WarDetail war = new()
        {
            State = "Ended",
            TeamSize = 7,
            AttacksPerMember = 1,
            PreparationStartTime = now.AddHours(2),
            StartTime = now.AddHours(2),
            EndTime = now.AddHours(26),
            Clan = new WarClan
            {
                Tag = "#123456a",
                Name = "Test Clan 2",
                ClanLevel = 7,
                Attacks = 20,
                Stars = 25,
                DestructionPercentage = 75,
                BadgeUrls = new BadgeUrls
                {
                    Small = "https://smallbadgeurl/2",
                    Medium = "https://mediumbadgeurl/2",
                    Large = "https://largebadgeurl/2"
                }
            },
            Opponent = new WarClan
            {
                Tag = "#654321a",
                Name = "Test Opponent2",
                ClanLevel = 9,
                Attacks = 30,
                Stars = 35,
                DestructionPercentage = 65,
                BadgeUrls = new BadgeUrls
                {
                    Small = "https://smallbadgeurl2/3",
                    Medium = "https://mediumbadgeurl2/3",
                    Large = "https://largebadgeurl2/3"
                }
            }
        };


        // Act
        var changed = mapper.UpdateT1FromT2(dbwar, war);

        // Assert
        Assert.True(changed);
        Assert.Equal(war.State, dbwar.State);
        Assert.Equal(war.TeamSize, dbwar.TeamSize);
        Assert.Equal(war.AttacksPerMember, dbwar.AttacksPerMember);
        Assert.Equal(war.PreparationStartTime, dbwar.PreparationStartTime);
        Assert.Equal(war.StartTime, dbwar.StartTime);
        Assert.Equal(war.EndTime, dbwar.EndTime);
        Assert.Equal(war.Clan.Tag, dbwar.Clan.Tag);
        Assert.Equal(war.Clan.Name, dbwar.Clan.Name);
        Assert.Equal(war.Clan.ClanLevel, dbwar.Clan.ClanLevel);
        Assert.Equal(war.Clan.Attacks, dbwar.Clan.Attacks);
        Assert.Equal(war.Clan.Stars, dbwar.Clan.Stars);
        Assert.Equal(war.Clan.DestructionPercentage, dbwar.Clan.DestructionPercentage);

        Assert.NotNull(dbwar.Clan.BadgeUrls);
        if (dbwar.Clan.BadgeUrls != null)
        {
            Assert.Equal(war.Clan.BadgeUrls.Small, dbwar.Clan.BadgeUrls.Small);
            Assert.Equal(war.Clan.BadgeUrls.Medium, dbwar.Clan.BadgeUrls.Medium);
            Assert.Equal(war.Clan.BadgeUrls.Large, dbwar.Clan.BadgeUrls.Large);
        }

        Assert.Equal(war.Opponent.Tag, dbwar.Opponent.Tag);
        Assert.Equal(war.Opponent.Name, dbwar.Opponent.Name);
        Assert.Equal(war.Opponent.ClanLevel, dbwar.Opponent.ClanLevel);
        Assert.Equal(war.Opponent.Attacks, dbwar.Opponent.Attacks);
        Assert.Equal(war.Opponent.Stars, dbwar.Opponent.Stars);
        Assert.Equal(war.Opponent.DestructionPercentage, dbwar.Opponent.DestructionPercentage);

        Assert.NotNull(dbwar.Opponent.BadgeUrls);
        if (dbwar.Opponent.BadgeUrls != null)
        {
            Assert.Equal(war.Opponent.BadgeUrls.Small, dbwar.Opponent.BadgeUrls.Small);
            Assert.Equal(war.Opponent.BadgeUrls.Medium, dbwar.Opponent.BadgeUrls.Medium);
            Assert.Equal(war.Opponent.BadgeUrls.Large, dbwar.Opponent.BadgeUrls.Large);
        }
    }
}