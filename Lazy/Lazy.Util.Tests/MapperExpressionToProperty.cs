using Lazy.Util.EntityModelMapper.Internal;

namespace Lazy.Util.Tests;

public class MapperExpressionToPropertyTests
{
    private class TestClass
    {
        public int IntField;
        public int IntProperty { get; set; }
        public bool BoolProperty { get; set; }
        public int ExampleMethod() => 1;
    }

    private class TestClass2
    {
        public int IntProperty { get; set; }
        public static int TestMethod(int num) => num + 2;
    }

    #region source member tests

    [Fact]
    public void SourceMember_DoesntAcceptNull_Throws() =>
        Assert.Throws<NullReferenceException>(() =>
            MapperExpressionToProperty.SourceExpression<TestClass, int>(null!));

    [Fact]
    public void SourceExpression_AcceptsProperty()
    {
        var pi = MapperExpressionToProperty
            .SourceExpression<TestClass, int>(ex => ex.IntProperty);

        Assert.Equal(nameof(TestClass.IntProperty), pi.Name);
    }

    [Fact]
    public void SourceExpression_AcceptsField()
    {
        var pi = MapperExpressionToProperty
            .SourceExpression<TestClass, int>(ex => ex.IntField);

        Assert.Equal(nameof(TestClass.IntField), pi.Name);
    }

    [Fact]
    public void SourceExpression_AcceptsUnnaryExpression()
    {
        var pi = MapperExpressionToProperty
            .SourceExpression<TestClass, bool>(ex => !ex.BoolProperty);

        Assert.Equal(nameof(TestClass.BoolProperty), pi.Name);
    }

    [Fact]
    public void SourceExpression_AcceptsBinaryExpression()
    {
        var pi = MapperExpressionToProperty
            .SourceExpression<TestClass, int>(ex => ex.IntProperty * 2);

        Assert.Equal(nameof(TestClass.IntProperty), pi.Name);
    }

    [Fact]
    public void SourceExpression_AcceptsMethodCall()
    {
        var pi = MapperExpressionToProperty
            .SourceExpression<TestClass, int>(ex => TestClass2.TestMethod(ex.IntProperty));

        Assert.Equal(nameof(TestClass.IntProperty), pi.Name);
    }

    #endregion

    #region destination member tests

    [Fact]
    public void DestinationMember_DoesntAcceptNull_Throws() =>
        Assert.Throws<NullReferenceException>(() =>
            MapperExpressionToProperty.DestinationMember<TestClass, int>(null!));

    [Fact]
    public void DestinationMember_AcceptsProperty()
    {
        var pi = MapperExpressionToProperty
            .DestinationMember<TestClass, int>(ex => ex.IntProperty);

        Assert.Equal(nameof(TestClass.IntProperty), pi.Name);
    }

    [Fact]
    public void DestinationMember_AcceptsField()
    {
        var pi = MapperExpressionToProperty
            .DestinationMember<TestClass, int>(ex => ex.IntField);

        Assert.Equal(nameof(TestClass.IntField), pi.Name);
    }

    [Fact]
    public void DestinationMember_DoesntAcceptMethod_Throws() =>
        Assert.Throws<ArgumentException>(() =>
            MapperExpressionToProperty.DestinationMember<TestClass, int>(ex => ex.ExampleMethod())
        );

    [Fact]
    public void DestinationMember_DoesntAcceptPropertyFromOtherTypes_Throws()
    {
        var t2 = new TestClass2();
        Assert.Throws<ArgumentException>(() =>
            MapperExpressionToProperty.DestinationMember<TestClass, int>(ex => t2.IntProperty)
        );
    }
}

#endregion
