using Lazy.Util.EntityModelMapper.Internal;
using Lazy.Util.Tests.Model;

namespace Lazy.Util.Tests;

public class MapperExpressionToPropertyTests
{
    #region source member tests

    [Fact]
    public void SourceMember_DoesntAcceptNull_Throws() =>
        Assert.Throws<NullReferenceException>(() =>
            MapperExpressionToMemberBuilder.SourceExpression<Source, int>(null!));

    [Fact]
    public void SourceExpression_AcceptsProperty()
    {
        var pi = MapperExpressionToMemberBuilder
            .SourceExpression<Source, int>(ex => ex.IntProperty);

        Assert.Equal(nameof(Source.IntProperty), pi.Name);
    }

    [Fact]
    public void SourceExpression_AcceptsField()
    {
        var pi = MapperExpressionToMemberBuilder
            .SourceExpression<Source, int>(ex => ex.IntField);

        Assert.Equal(nameof(Source.IntField), pi.Name);
    }

    [Fact]
    public void SourceExpression_AcceptsUnnaryExpression()
    {
        var pi = MapperExpressionToMemberBuilder
            .SourceExpression<Source, bool>(ex => !ex.BoolProperty);

        Assert.Equal(nameof(Source.BoolProperty), pi.Name);
    }

    [Fact]
    public void SourceExpression_AcceptsBinaryExpression()
    {
        var pi = MapperExpressionToMemberBuilder
            .SourceExpression<Source, int>(ex => ex.IntProperty * 2);

        Assert.Equal(nameof(Source.IntProperty), pi.Name);
    }

    [Fact]
    public void SourceExpression_AcceptsMethodCall()
    {
        var pi = MapperExpressionToMemberBuilder
            .SourceExpression<Source, int>(ex => ex.TestMethod(ex.IntProperty, 2));

        Assert.Equal(nameof(Source.IntProperty), pi.Name);
    }

    [Fact]
    public void SourceExpression_DoesntAcceptPropertyFromOtherTypesThrows()
    {
        var d = new Destination();
        Assert.Throws<ArgumentException>(() => MapperExpressionToMemberBuilder
            .SourceExpression<Source, int>(ex => ex.TestMethod(d.IntProperty, 2)));
    }

    #endregion

    #region destination member tests

    [Fact]
    public void DestinationMember_DoesntAcceptNull_Throws() =>
        Assert.Throws<NullReferenceException>(() =>
            MapperExpressionToMemberBuilder.DestinationMember<Source, int>(null!));

    [Fact]
    public void DestinationMember_AcceptsProperty()
    {
        var pi = MapperExpressionToMemberBuilder
            .DestinationMember<Source, int>(ex => ex.IntProperty);

        Assert.Equal(nameof(Source.IntProperty), pi.Name);
    }

    [Fact]
    public void DestinationMember_AcceptsField()
    {
        var pi = MapperExpressionToMemberBuilder
            .DestinationMember<Source, int>(ex => ex.IntField);

        Assert.Equal(nameof(Source.IntField), pi.Name);
    }

    [Fact]
    public void DestinationMember_DoesntAcceptMethod_Throws() =>
        Assert.Throws<ArgumentException>(() => MapperExpressionToMemberBuilder
            .DestinationMember<Source, int>(ex => ex.TestMethod(ex.TestProperty1, 2))
        );

    [Fact]
    public void DestinationMember_DoesntAcceptPropertyFromOtherTypes_Throws()
    {
        var s = new Source();
        Assert.Throws<ArgumentException>(() =>
            MapperExpressionToMemberBuilder.DestinationMember<Destination, int>(ex => s.IntProperty)
        );
    }
}

#endregion