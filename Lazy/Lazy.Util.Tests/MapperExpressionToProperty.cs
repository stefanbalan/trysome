namespace Lazy.Util.Tests;

public class MapperExpressionToProperty
{
    private class TestClass
    {
        public int ExampleField;
        public int ExampleProperty { get; set; }
        public int ExampleMethod() => 1;
    }

    private class TestClass2
    {
        public int ExampleProperty { get; set; }
    }
#region destination member tests
    [Fact]
    public void DestinationMember_ThrowsNullWhenExpressionIsNull() =>
        Assert.Throws<NullReferenceException>(() =>
            EntityModelMapper.Internal.MapperExpressionToProperty
                .DestinationMember<TestClass, int>(null!));

    [Fact]
    public void DestinationMember_AcceptsProperty()
    {
        var pi = EntityModelMapper.Internal.MapperExpressionToProperty
            .DestinationMember<TestClass, int>(ex => ex.ExampleProperty);

        Assert.Equal(nameof(TestClass.ExampleProperty), pi.Name);
    }

    [Fact]
    public void DestinationMember_AcceptsField()
    {
        var pi = EntityModelMapper.Internal.MapperExpressionToProperty
            .DestinationMember<TestClass, int>(ex => ex.ExampleField);

        Assert.Equal(nameof(TestClass.ExampleField), pi.Name);
    }

    [Fact]
    public void DestinationMember_DoesntAcceptMethod_Throws() =>
        Assert.Throws<ArgumentException>(() =>
            EntityModelMapper.Internal.MapperExpressionToProperty
                .DestinationMember<TestClass, int>(ex => ex.ExampleMethod())
        );

    [Fact]
    public void DestinationMember_DoesntAcceptPropertyFromOtherTypes_Throws()
    {
        var t2 = new TestClass2();
        Assert.Throws<ArgumentException>(() =>
            EntityModelMapper.Internal.MapperExpressionToProperty
                .DestinationMember<TestClass, int>(ex => t2.ExampleProperty)
        );
    }
}
#endregion
