using Lazy.Util.EntityModelMapper;
using Lazy.Util.Tests.Model;

namespace Lazy.Util.Tests;

public class MapperTests
{
    [Fact]
    public void Mapper_Constructor_ThrowsPropertyWithoutSetter()
        => Assert.Throws<ArgumentException>(() =>
            new EntityModelMapper<Source, Destination>.Mapping<Source, Destination, int>(s => s.TestProperty3,
                d => d.TestProperty3));


    [Fact]
    public void Mapper_AppliesProperty()
    {
        var m = new EntityModelMapper<Source, Destination>.Mapping<Source, Destination, int>(s => s.TestProperty1,
            d => d.TestProperty1);

        var s = new Source { TestProperty1 = 1 };
        var d = new Destination();

        m.Apply(s, d);
        Assert.Equal(1, d.TestProperty1);
    }


    [Fact]
    public void Mapper_AppliesPropertyWithPrivateSetter()
    {
        var m = new EntityModelMapper<Source, Destination>.Mapping<Source, Destination, int>(s => s.TestProperty2,
            d => d.TestProperty2);

        var s = new Source(1);
        var d = new Destination();

        m.Apply(s, d);
        Assert.Equal(1, d.TestProperty2);
    }


    [Fact]
    public void Mapper_AppliesField()
    {
        var m = new EntityModelMapper<Source, Destination>.Mapping<Source, Destination, int>(s => s.TestField,
            d => d.TestField);

        var s = new Source { TestField = 1 };
        var d = new Destination();

        m.Apply(s, d);
        Assert.Equal(1, d.TestField);
    }


    [Fact]
    public void Mapper_ApplyConditional_ReturnsTrueWhenChanged()
    {
        var m = new EntityModelMapper<Source, Destination>.Mapping<Source, Destination, int>(s => s.TestProperty1,
            d => d.TestProperty1);

        var s = new Source { TestProperty1 = 1 };
        var d = new Destination();

        var changed = m.ApplyConditional(s, d);
        Assert.True(changed);
    }


    [Fact]
    public void Mapper_ApplyConditional_ReturnsFalseWhenNotChanged()
    {
        var m = new EntityModelMapper<Source, Destination>.Mapping<Source, Destination, int>(s => s.TestProperty1,
            d => d.TestProperty1);

        var s = new Source { TestProperty1 = 1 };
        var d = new Destination { TestProperty1 = 1 };

        var changed = m.ApplyConditional(s, d);
        Assert.False(changed);
    }

    [Fact]
    public void Mapper_AppliesProperty_ValueType()
    {
        var m = new EntityModelMapper<Source, Destination>.Mapping<Source, Destination, string>(
            s => s.TestReferenceType,
            d => d.TestReferenceType);

        var s = new Source { TestReferenceType = "1" };
        var d = new Destination();

        m.Apply(s, d);
        Assert.Equal("1", d.TestReferenceType);
    }


    [Fact]
    public void Mapper_ApplyConditional_ReferenceType_ReturnsTrueWhenChanged()
    {
        var m = new EntityModelMapper<Source, Destination>.Mapping<Source, Destination, string>(
            s => s.TestReferenceType,
            d => d.TestReferenceType);

        var s = new Source { TestReferenceType = "1" };
        var d = new Destination();

        var changed = m.ApplyConditional(s, d);
        Assert.True(changed);
    }


    [Fact]
    public void Mapper_ApplyConditional_ReferenceType_ReturnsFalseWhenNotChanged()
    {
        var m = new EntityModelMapper<Source, Destination>.Mapping<Source, Destination, string>(
            s => s.TestReferenceType,
            d => d.TestReferenceType);

        var s = new Source { TestReferenceType = "1" };
        var d = new Destination { TestReferenceType = "1" };

        var changed = m.ApplyConditional(s, d);
        Assert.False(changed);
    }
}