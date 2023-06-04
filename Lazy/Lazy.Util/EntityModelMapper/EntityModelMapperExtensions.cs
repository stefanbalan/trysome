using Microsoft.Extensions.DependencyInjection;

namespace Lazy.Util.EntityModelMapper;

public static class EntityModelMapperExtensions
{
    // private static Dictionary<(Type, Type), IEntityModelMapper> EntityToModelMappings { get; } = new();
    //
    public static void AddMapper<T1, T2>(
        this IServiceCollection serviceCollection,
        Action<EntityModelMapper<T1, T2>> mapBuildingExpression) where T1 : new() where T2 : new()
    {
        var mm = new EntityModelMapper<T1, T2>();
        mapBuildingExpression(mm);
    }
}