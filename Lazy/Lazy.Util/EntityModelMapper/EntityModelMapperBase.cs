using System.Linq.Expressions;
using System.Reflection;
using Lazy.Util.EntityModelMapper.Internal;

// ReSharper disable UnusedMember.Global

namespace Lazy.Util.EntityModelMapper;

public abstract class EntityModelMapperBase<TEntity, TModel> : IEntityModelMapper<TEntity, TModel>
    where TEntity : new()
    where TModel : new()
{
    private static Dictionary<string, IMapping<TEntity, TModel>> EntityToModelMappings { get; } = new();
    private static Dictionary<string, IMapping<TModel, TEntity>> ModelToEntityMappings { get; } = new();

    protected EntityModelMapperBase()
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        BuildMappings();
    }

    public static void MapTwoWay<TPropEntity, TPropModel>(
        Expression<Func<TEntity, TPropEntity>> entityExp,
        Expression<Func<TModel, TPropModel>> modelExp)
    {
        var etmMapping = new Mapping<TEntity, TPropEntity, TModel, TPropModel>(entityExp, modelExp);
        var mteMapping = new Mapping<TModel, TPropModel, TEntity, TPropEntity>(modelExp, entityExp);
        // dictionary[] -> if it exists: Add throws, TryAdd does nothing, indexer overwrites
        EntityToModelMappings[etmMapping.Key] = etmMapping;
        ModelToEntityMappings[mteMapping.Key] = mteMapping;
    }

    public static void MapEntityToModel<TPropEntity, TPropModel>(
        Expression<Func<TEntity, TPropEntity>> entityExp,
        Expression<Func<TModel, TPropModel>> modelExp)
    {
        var etmMapping = new Mapping<TEntity, TPropEntity, TModel, TPropModel>(entityExp, modelExp);
        EntityToModelMappings[etmMapping.Key] = etmMapping;
    }

    public static void MapModelToEntity<TPropModel, TPropEntity>(
        Expression<Func<TModel, TPropModel>> modelExp,
        Expression<Func<TEntity, TPropEntity>> entityExp)
    {
        var mteMapping = new Mapping<TModel, TPropModel, TEntity, TPropEntity>(modelExp, entityExp);
        ModelToEntityMappings[mteMapping.Key] = mteMapping;
    }

    public static TModel ToModel(TEntity entity)
    {
        TModel result = new();
        foreach (var keyValuePair in EntityToModelMappings) keyValuePair.Value.Apply(entity, result);
        return result;
    }

    public static TEntity ToEntity(TModel model)
    {
        TEntity result = new();
        foreach (var keyValuePair in ModelToEntityMappings) keyValuePair.Value.Apply(model, result);
        return result;
    }

    public TModel GetModelFrom(TEntity entity) => ToModel(entity);

    public TEntity GetEntityFrom(TModel model) => ToEntity(model);

    public bool UpdateEntityFromModel(TEntity entity, TModel model) => throw new NotImplementedException();


    protected abstract void BuildMappings();


    private interface IMapping<in TSrc, in TDst>
    {
        string Key { get; }
        void Apply(TSrc src, TDst dst);

        bool ApplyToExisting(TSrc src, TDst dst);
    }

    private class Mapping<TSrc, TPropSrc, TDst, TPropDst> : IMapping<TSrc, TDst>
    {
        private readonly Expression<Func<TSrc, TPropSrc>> srcExp;
        private readonly PropertyInfo srcProp;
        private readonly PropertyInfo dstProp;

        public Mapping(Expression<Func<TSrc, TPropSrc>> srcExp, Expression<Func<TDst, TPropDst>> dstExp)
        {
            this.srcExp = srcExp;
            srcProp = MapperExpressionToProperty.SourcePropertyInfo(srcExp);
            dstProp = MapperExpressionToProperty.DestinationPropertyInfo(dstExp);
            if (!dstProp.CanWrite) throw new ArgumentException("Destination is read-only");
        }

        public string Key => $"{srcProp.Name}_{dstProp.Name}";


        public void Apply(TSrc src, TDst dst)
        {
            // _dstProp.SetValue(dst, _srcProp.GetValue(src));
            var c = srcExp.Compile();
            dstProp.SetValue(dst, c(src));
        }

        public bool ApplyToExisting(TSrc src, TDst dst)
        {
            return false;
        }
    }
}