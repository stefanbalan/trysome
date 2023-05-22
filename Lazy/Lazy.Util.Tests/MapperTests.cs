using Lazy.Util.EntityModelMapper;

namespace Lazy.Util.Tests;

public class MapperTests
{
    private class Source
    {
        public int TestProperty1 { get; set; }
        public int TestProperty2 { get; private set; }
        public int TestProperty3 { get; } = 1;
        public int TestField;

        public Source()
        {
        }

        public Source(int testProperty2)
        {
            TestProperty2 = testProperty2;
        }
    }

    private class Destination
    {
        public int TestProperty1 { get; set; }
        public int TestProperty2 { get; private set; }
        public int TestProperty3 { get; }
        public int TestField;
    }

    [Fact]
    public void Mapper_Constructor_ThrowsPropertyWithoutSetter()
        => Assert.Throws<ArgumentException>(() =>
            new EntityModelMapperBase<Source, Destination>.Mapping<Source, Destination, int>(s => s.TestProperty3,
                d => d.TestProperty3));

    [Fact]
    public void Mapper_AppliesProperty()
    {
        var m = new EntityModelMapperBase<Source, Destination>.Mapping<Source, Destination, int>(s => s.TestProperty1,
            d => d.TestProperty1);

        var s = new Source { TestProperty1 = 1 };
        var d = new Destination();

        m.Apply2(s, d);
        Assert.Equal(1, d.TestProperty1);
    }

    [Fact]
    public void Mapper_AppliesPropertyWithPrivateSetter()
    {
        var m = new EntityModelMapperBase<Source, Destination>.Mapping<Source, Destination, int>(s => s.TestProperty2,
            d => d.TestProperty2);

        var s = new Source(1);
        var d = new Destination();

        m.Apply2(s, d);
        Assert.Equal(1, d.TestProperty2);
    }


    [Fact]
    public void Mapper_AppliesField()
    {
        var m = new EntityModelMapperBase<Source, Destination>.Mapping<Source, Destination, int>(s => s.TestField,
            d => d.TestField);

        var s = new Source() { TestField = 1 };
        var d = new Destination();

        m.Apply2(s, d);
        Assert.Equal(1, d.TestField);
    }
}
