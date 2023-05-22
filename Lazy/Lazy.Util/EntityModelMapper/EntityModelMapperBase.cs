using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
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

    public static void MapTwoWay<TValue>(
        Expression<Func<TEntity, TValue>> entityExp,
        Expression<Func<TModel, TValue>> modelExp)
    {
        var etmMapping = new Mapping<TEntity, TModel, TValue>(entityExp, modelExp);
        var mteMapping = new Mapping<TModel, TEntity, TValue>(modelExp, entityExp);
        // dictionary[] -> if it exists: Add throws, TryAdd does nothing, indexer overwrites
        EntityToModelMappings[etmMapping.Key] = etmMapping;
        ModelToEntityMappings[mteMapping.Key] = mteMapping;
    }

    public static void MapEntityToModel<TValue>(
        Expression<Func<TEntity, TValue>> entityExp,
        Expression<Func<TModel, TValue>> modelExp)
    {
        var etmMapping = new Mapping<TEntity, TModel, TValue>(entityExp, modelExp);
        EntityToModelMappings[etmMapping.Key] = etmMapping;
    }

    public static void MapModelToEntity<TValue>(
        Expression<Func<TModel, TValue>> modelExp,
        Expression<Func<TEntity, TValue>> entityExp)
    {
        var mteMapping = new Mapping<TModel, TEntity, TValue>(modelExp, entityExp);
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
    }

    public class Mapping<TSrc, TDst, TValue> : IMapping<TSrc, TDst>
    {
        private readonly Expression<Func<TSrc, TValue>> srcExp;
        private readonly MemberInfo srcMember;
        private readonly MemberInfo dstMember;

        private readonly Func<TSrc, TValue> srcGetter;
        private Action<TDst, TValue> dstSetter;

        public Mapping(Expression<Func<TSrc, TValue>> srcExp, Expression<Func<TDst, TValue>> dstExp)
        {
            this.srcExp = srcExp;
            srcMember = MapperExpressionToMemberBuilder.SourceExpression(srcExp);
            dstMember = MapperExpressionToMemberBuilder.DestinationMember(dstExp);
            if (dstMember is PropertyInfo { CanWrite: false }) throw new ArgumentException("Destination is read-only");

            srcGetter = srcExp.Compile();
            switch (dstMember)
            {
                case FieldInfo fi:

                    var dynamicMethod = new DynamicMethod(string.Empty, typeof(void),
                        new[] { typeof(TDst), typeof(TValue) }, true);
                    var ilGenerator = dynamicMethod.GetILGenerator();
                    ilGenerator.Emit(OpCodes.Ldarg_0);
                    ilGenerator.Emit(OpCodes.Ldarg_1);
                    ilGenerator.Emit(OpCodes.Stfld, fi);
                    ilGenerator.Emit(OpCodes.Ret);
                    dstSetter = (Action<TDst, TValue>)dynamicMethod.CreateDelegate(typeof(Action<TDst, TValue>));
                    break;
                case PropertyInfo pi:
                    var setter = pi.GetSetMethod(true)!;

                    dstSetter = (Action<TDst, TValue>)Delegate.CreateDelegate(typeof(Action<TDst, TValue>), setter);

                    break;
            }
        }

        public string Key => $"{srcMember.Name}_{dstMember.Name}";


        public void Apply(TSrc src, TDst dst)
        {
            var c = srcExp.Compile();
            switch (dstMember)
            {
                case FieldInfo fi:
                    fi.SetValue(dst, c(src));
                    break;
                case PropertyInfo pi:
                    pi.SetValue(dst, c(src));
                    break;
            }
        }

        public void Apply2(TSrc src, TDst dst) => dstSetter(dst, srcGetter(src));
    }

    private class MappingWithChangeTracking<TSrc, TPropSrc, TDst, TPropDst> : IMapping<TSrc, TDst>
    {
        private readonly Expression<Func<TSrc, TPropSrc>> srcExp;
        private readonly MemberInfo srcMember;
        private readonly MemberInfo dstMember;

        public MappingWithChangeTracking(Expression<Func<TSrc, TPropSrc>> srcExp,
            Expression<Func<TDst, TPropDst>> dstExp)
        {
            this.srcExp = srcExp;
            srcMember = MapperExpressionToMemberBuilder.SourceExpression(srcExp);
            dstMember = MapperExpressionToMemberBuilder.DestinationMember(dstExp);
            if (dstMember is PropertyInfo { CanWrite: false }) throw new ArgumentException("Destination is read-only");
        }

        public string Key => $"{srcMember.Name}_{dstMember.Name}";


        public void Apply(TSrc src, TDst dst)
        {
            // _dstProp.SetValue(dst, _srcProp.GetValue(src));
            var c = srcExp.Compile();
            switch (dstMember)
            {
                case FieldInfo fi:
                    fi.SetValue(dst, c(src));
                    break;
                case PropertyInfo pi:
                    pi.SetValue(dst, c(src));
                    break;
            }
        }
    }
}
