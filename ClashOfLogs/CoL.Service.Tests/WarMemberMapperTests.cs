using WarMember = ClashOfLogs.Shared.WarMember;

namespace CoL.Service.Mappers.Tests;

public class WarMemberMapperTests
{
    [Fact]
    public void UpdateEntity_WithPopulatedModel_PropertiesUpdated_WarMember()
        => UpdateEntity_WithPopulatedModel_PropertiesUpdated<WarMemberMapper>();

    [Fact]
    public void UpdateEntity_WithPopulatedModel_PropertiesUpdated_WarMemberClan()
        => UpdateEntity_WithPopulatedModel_PropertiesUpdated<WarMemberMapperForClan>();

    [Fact]
    public void UpdateEntity_WithPopulatedModel_PropertiesUpdated_WarMemberForOpponent()
        => UpdateEntity_WithPopulatedModel_PropertiesUpdated<WarMemberMapperForOpponent>();


    private void UpdateEntity_WithPopulatedModel_PropertiesUpdated<TMapper>()
        where TMapper : WarMemberMapper, new()
    {
        // Arrange
        var model = new WarMember
        {
            Tag = "123",
            Name = "Test",
            TownHallLevel = 13,
            MapPosition = 5,
            Attacks = new List<Attack> {
                new Attack {
                    AttackerTag = "456",
                    DefenderTag = "789",
                    Stars = 2,
                    DestructionPercentage = 80,
                    Order = 1,
                    Duration = 180
                },
                new Attack {
                    AttackerTag = "789",
                    DefenderTag = "9ab",
                    Stars = 3,
                    DestructionPercentage = 90,
                    Order = 2,
                    Duration = 120
                }
            },
            OpponentAttacks = 3,
            BestOpponentAttack = new Attack
            {
                AttackerTag = "012",
                DefenderTag = "345",
                Stars = 3,
                DestructionPercentage = 95,
                Order = 1,
                Duration = 120
            }
        };
        var mapper = new TMapper();


        var now = DateTime.Now;
        var entity = mapper.CreateEntity(model, now);

        // Act
        var result = mapper.UpdateEntity(entity, model, now);

        Assert.True(result);
        // Assert
        Assert.Equal(model.Tag, entity.Tag);
        Assert.Equal(model.Name, entity.Name);
        Assert.Equal(model.TownHallLevel, entity.TownHallLevel);
        Assert.Equal(model.MapPosition, entity.MapPosition);
        Assert.Equal(model.Attacks[0].AttackerTag, entity.Attack1?.AttackerTag);
        Assert.Equal(model.Attacks[0].DefenderTag, entity.Attack1?.DefenderTag);
        Assert.Equal(model.Attacks[0].Stars, entity.Attack1?.Stars);
        Assert.Equal(model.Attacks[0].DestructionPercentage, entity.Attack1?.DestructionPercentage);
        Assert.Equal(model.Attacks[0].Order, entity.Attack1?.Order);
        Assert.Equal(model.Attacks[0].Duration, entity.Attack1?.Duration);

        Assert.Equal(model.Attacks[1].AttackerTag, entity.Attack2?.AttackerTag);
        Assert.Equal(model.Attacks[1].DefenderTag, entity.Attack2?.DefenderTag);
        Assert.Equal(model.Attacks[1].Stars, entity.Attack2?.Stars);
        Assert.Equal(model.Attacks[1].DestructionPercentage, entity.Attack2?.DestructionPercentage);
        Assert.Equal(model.Attacks[1].Order, entity.Attack2?.Order);
        Assert.Equal(model.Attacks[1].Duration, entity.Attack2?.Duration);

        Assert.Equal(model.OpponentAttacks, entity.OpponentAttacks);

        Assert.Equal(model.BestOpponentAttack?.AttackerTag, entity.BestOpponentAttack?.AttackerTag);
        Assert.Equal(model.BestOpponentAttack?.DefenderTag, entity.BestOpponentAttack?.DefenderTag);
        Assert.Equal(model.BestOpponentAttack?.Stars, entity.BestOpponentAttack?.Stars);
        Assert.Equal(model.BestOpponentAttack?.DestructionPercentage, entity.BestOpponentAttack?.DestructionPercentage);
        Assert.Equal(model.BestOpponentAttack?.Order, entity.BestOpponentAttack?.Order);
        Assert.Equal(model.BestOpponentAttack?.Duration, entity.BestOpponentAttack?.Duration);
    }
}