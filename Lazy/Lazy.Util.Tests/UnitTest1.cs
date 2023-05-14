using Lazy.Util.EntityModelMapper.Internal;

namespace Lazy.Util.Tests;

public class MapperExpressionToProperty
{
    private class ExampleClass
    {
        public int ExampleField;
        public int ExampleProperty { get; set; }
        public int ExampleMethod() => 1;
    }

    [Fact]
    public void DestinationProperty_ThrowsNullWhenExpressionIsNull() =>
        Assert.Throws<NullReferenceException>(() =>
            EntityModelMapper.Internal.MapperExpressionToProperty
                .DestinationPropertyInfo<ExampleClass, int>(null!));

    [Fact]
    public void DestinationProperty_AcceptsProperty()
    {
        var pi = EntityModelMapper.Internal.MapperExpressionToProperty
            .DestinationPropertyInfo<ExampleClass, int>(ex => ex.ExampleProperty);

        Assert.Equal(nameof(ExampleClass.ExampleProperty), pi.Name);
    }

    [Fact]
    public void DestinationProperty_AcceptsField()
    {
        var pi = EntityModelMapper.Internal.MapperExpressionToProperty
            .DestinationPropertyInfo<ExampleClass, int>(ex => ex.ExampleField);

        Assert.Equal(nameof(ExampleClass.ExampleField), pi.Name);
    }

    [Fact]
    public void DestinationProperty_DoesntAcceptMethod_ReturnsNull()
    {
        var pi = EntityModelMapper.Internal.MapperExpressionToProperty
            .DestinationPropertyInfo<ExampleClass, int>(ex => ex.ExampleMethod());

        Assert.Null(pi);
    }
}