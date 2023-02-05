using System.Linq.Expressions;
using System.Reflection;

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


    public abstract void BuildMappings();


    internal interface IMapping<in TSrc, in TDst>
    {
        string Key { get; }
        void Apply(TSrc src, TDst dst);
    }

    internal class Mapping<TSrc, TPropSrc, TDst, TPropDst> : IMapping<TSrc, TDst>
    {
        private readonly Expression<Func<TSrc, TPropSrc>> _srcExp;
        private readonly PropertyInfo _srcProp;
        private readonly PropertyInfo _dstProp ;
        
        public Mapping(Expression<Func<TSrc, TPropSrc>> srcExp, Expression<Func<TDst, TPropDst>> dstExp)
        {
            _srcExp = srcExp;
            _srcProp = SourcePropertyInfo(srcExp);
            _dstProp = DestinationPropertyInfo(dstExp);
            if (!_dstProp.CanWrite) throw new ArgumentException("Destination is read-only");
        }

        public string Key => $"{_srcProp.Name}_{_dstProp.Name}";

        private static PropertyInfo SourcePropertyInfo<T, TP>(Expression<Func<T, TP>> expression)
        {
            var p = TestExpression(expression.Body, typeof(T));

            if (p is null) throw new ArgumentException("No property found.");

            return p;

            PropertyInfo? TestExpression(Expression ex, Type type)
            {
                switch (ex)
                {
                    case LambdaExpression le:
                        foreach (var exParameter in le.Parameters)
                        {
                            var pi = TestExpression(exParameter, type);
                            if (pi is not null) return pi;
                        }
                        break;
                    case MethodCallExpression mce:
                        foreach (var exParameter in mce.Arguments)
                        {
                            var pi = TestExpression(exParameter, type);
                            if (pi is not null) return pi;
                        }
                        break;
                    case MemberExpression me:
                        var p = me.Member as PropertyInfo;
                        if (p is null)
                            return null; //throw new ArgumentException($"Expression '{memberExpression1.Member.Name}' refers to a field, not a property.");
                        if (p.ReflectedType == null ||
                            type != p.ReflectedType && !type.IsSubclassOf(p.ReflectedType))
                            return null;// throw new ArgumentException($"Expresion '{memberExpression1.Member.Name}' refers to a property that is not from type {type}.");
                        return p;
                    default:
                        return null;
                }

                return null;
            }
            
            // this is suitable for only one case, property expression `obj => obj.Property`
            PropertyInfo? GetPropInfo(MemberExpression? memberExpression1, Type type)
            {
                if (memberExpression1 == null)
                    throw new ArgumentException($"Expression '{expression.Name}' refers to a method, not a property.");

                var propInfo = memberExpression1.Member as PropertyInfo;
                if (propInfo == null)
                    throw new ArgumentException($"Expression '{memberExpression1.Member.Name}' refers to a field, not a property.");

                if (propInfo.ReflectedType == null ||
                    type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
                    throw new ArgumentException(
                        $"Expresion '{memberExpression1.Member.Name}' refers to a property that is not from type {type}.");
                return propInfo;
            }
        }

        private static PropertyInfo DestinationPropertyInfo<T, TP>(Expression<Func<T, TP>> expression)
        {
            var memberExpression = expression.Body as MemberExpression ??
                                   ((UnaryExpression)expression.Body).Operand as MemberExpression;

            var type = typeof(T);
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
            // _dstProp.SetValue(dst, _srcProp.GetValue(src));
            var c = _srcExp.Compile();
            _dstProp.SetValue(dst, c(src));
        }
    }
}