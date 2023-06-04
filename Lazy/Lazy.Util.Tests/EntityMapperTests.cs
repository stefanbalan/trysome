using Lazy.Util.EntityModelMapper;
using Lazy.Util.Tests.Model;

namespace Lazy.Util.Tests;

public class EntityMapperTests
{
    [Fact]
    public void Get2From1()
    {
        var mapper = new EntityModelMapper<Source, Destination>();
        mapper.T1ToT2(t1 => t1.TestProperty1, t2 => t2.TestProperty1);
        mapper.T1ToT2(t1 => t1.TestReferenceType, t2 => t2.TestReferenceType);

        var s = new Source
        {
            TestProperty1 = 1,
            TestReferenceType = "Test"
        };
        var d = mapper.Get2From(s);
        Assert.Equal(s.TestProperty1, d.TestProperty1);
        Assert.Equal(s.TestReferenceType, d.TestReferenceType);
    }

    [Fact]
    public void Get1From2()
    {
        var mapper = new EntityModelMapper<Destination, Source>();
        mapper.T2ToT1(t1 => t1.TestProperty1, t2 => t2.TestProperty1);
        mapper.T2ToT1(t1 => t1.TestReferenceType, t2 => t2.TestReferenceType);

        var s = new Source
        {
            TestProperty1 = 1,
            TestReferenceType = "Test"
        };
        var d = mapper.Get1From(s);
        Assert.Equal(s.TestProperty1, d.TestProperty1);
        Assert.Equal(s.TestReferenceType, d.TestReferenceType);
    }

    [Fact]
    public void GetBothWays()
    {
        var mapper = new EntityModelMapper<Destination, Source>();
        mapper.TwoWay(t1 => t1.TestProperty1, t2 => t2.TestProperty1);
        mapper.TwoWay(t1 => t1.TestReferenceType, t2 => t2.TestReferenceType);

        var s = new Source
        {
            TestProperty1 = 1,
            TestReferenceType = "Test"
        };
        var d = mapper.Get1From(s);
        Assert.Equal(s.TestProperty1, d.TestProperty1);
        Assert.Equal(s.TestReferenceType, d.TestReferenceType);

        var s2 = mapper.Get2From(d);

        Assert.False(ReferenceEquals(s, s2));
        Assert.Equal(s.TestProperty1, s2.TestProperty1);
        Assert.Equal(s.TestReferenceType, s2.TestReferenceType);
    }

    [Fact]
    public void Update2From1()
    {
        var mapper = new EntityModelMapper<Source, Destination>();
        mapper.T1ToT2(t1 => t1.TestProperty1, t2 => t2.TestProperty1);
        mapper.T1ToT2(t1 => t1.TestReferenceType, t2 => t2.TestReferenceType);

        var s = new Source
        {
            TestProperty1 = 1,
            TestReferenceType = "Test"
        };
        var d = new Destination();

        var changed = mapper.UpdateT2FromT1(d, s);
        Assert.Equal(s.TestProperty1, d.TestProperty1);
        Assert.Equal(s.TestReferenceType, d.TestReferenceType);
        Assert.True(changed);

        changed = mapper.UpdateT2FromT1(d, s);
        Assert.Equal(s.TestProperty1, d.TestProperty1);
        Assert.Equal(s.TestReferenceType, d.TestReferenceType); //refernces are equal
        Assert.False(changed);

        var ns = new Span<char>(new char[s.TestReferenceType.Length]);
        s.TestReferenceType.CopyTo(ns);
        d.TestReferenceType = ns.ToString();
        changed = mapper.UpdateT2FromT1(d, s);
        Assert.Equal(s.TestProperty1, d.TestProperty1); // different refences but equal
        Assert.False(changed);

        s.TestReferenceType = null!;
        d.TestReferenceType = null!;
        changed = mapper.UpdateT2FromT1(d, s);
        Assert.Equal(s.TestProperty1, d.TestProperty1);
        Assert.False(changed);
    }

    [Fact]
    public void Update1From2()
    {
        var mapper = new EntityModelMapper<Destination, Source>();
        mapper.T2ToT1(t1 => t1.TestProperty1, t2 => t2.TestProperty1);
        mapper.T2ToT1(t1 => t1.TestReferenceType, t2 => t2.TestReferenceType);

        var s = new Source
        {
            TestProperty1 = 1,
            TestReferenceType = "Test"
        };
        var d = new Destination();
        var changed = mapper.UpdateT1FromT2(d, s);
        Assert.Equal(s.TestProperty1, d.TestProperty1);
        Assert.Equal(s.TestReferenceType, d.TestReferenceType);
        Assert.True(changed);

        changed = mapper.UpdateT1FromT2(d, s);
        Assert.Equal(s.TestProperty1, d.TestProperty1);
        Assert.Equal(s.TestReferenceType, d.TestReferenceType);
        Assert.False(changed);


        var ns = new Span<char>(new char[s.TestReferenceType.Length]);
        s.TestReferenceType.CopyTo(ns);
        d.TestReferenceType = ns.ToString();
        changed = mapper.UpdateT1FromT2(d, s);
        Assert.Equal(s.TestProperty1, d.TestProperty1); // different refences but equal
        Assert.False(changed);

        s.TestReferenceType = null!;
        d.TestReferenceType = null!;
        changed = mapper.UpdateT1FromT2(d, s);
        Assert.Equal(s.TestProperty1, d.TestProperty1);
        Assert.False(changed);
    }
}