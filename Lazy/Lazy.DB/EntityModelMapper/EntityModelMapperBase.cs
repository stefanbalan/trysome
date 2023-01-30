using System.Linq.Expressions;
using System.Reflection;

namespace Lazy.DB.EntityModelMapper;

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

    public static void MapTwoWay<TPropEntity, TPropModel>(Expression<Func<TEntity, TPropEntity>> entityExp,
        Expression<Func<TModel, TPropModel>> modelExp)
    {
        var etmMapping = new Mapping<TEntity, TPropEntity, TModel, TPropModel>(entityExp, modelExp);
        var mteMapping = new Mapping<TModel, TPropModel, TEntity, TPropEntity>(modelExp, entityExp);
        EntityToModelMappings[etmMapping.Key] =
            etmMapping; // if it exists: Add throws, TryAdd does nothing, indexer overwrites
        ModelToEntityMappings[etmMapping.Key] = mteMapping;
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

    public abstract void BuildMappings();


    internal interface IMapping<in TSrc, in TDst>
    {
        string Key { get; }
        void Apply(TSrc src, TDst dst);
    }

    internal class Mapping<TSrc, TPropSrc, TDst, TPropDst> : IMapping<TSrc, TDst>
    {
        public Mapping(Expression<Func<TSrc, TPropSrc>> srcExp, Expression<Func<TDst, TPropDst>> dstExp)
        {
            SrcProp = PropertyInfo(srcExp);
            DstProp = PropertyInfo(dstExp);
            if (!DstProp.CanWrite) throw new ArgumentException("Destination is read-only");
        }

        public string Key => $"{SrcProp.Name}_{DstProp.Name}";

        private PropertyInfo SrcProp { get; }
        private PropertyInfo DstProp { get; }

        private static PropertyInfo PropertyInfo<T, TP>(Expression<Func<T, TP>> expression)
        {
            var memberExpression = expression.Body as MemberExpression ??
                                   ((UnaryExpression)expression.Body).Operand as MemberExpression;

            var type = typeof(TP);
            if (memberExpression == null)
                throw new ArgumentException($"Expression '{expression.Name}' refers to a method, not a property.");

            var propInfo = memberExpression.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(
                    $"Expression '{memberExpression.Member.Name}' refers to a field, not a property.");

            if (propInfo.ReflectedType == null ||
                type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(
                    $"Expresion '{memberExpression.Member.Name}' refers to a property that is not from type {type}.");
            return propInfo;
        }

        public void Apply(TSrc src, TDst dst)
        {
            DstProp.SetValue(dst, SrcProp.GetValue(src));
        }
    }
}