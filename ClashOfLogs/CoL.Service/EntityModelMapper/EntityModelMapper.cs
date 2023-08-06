using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using CoL.Service.EntityModelMapper.Internal;

namespace CoL.Service.EntityModelMapper;

public class EntityModelMapper<T1, T2> : IEntityModelMapper<T1, T2>
    where T1 : new()
    where T2 : new()
{
    private static Dictionary<string, IMapping<T1, T2>> T1ToT2Mappings { get; } = new();
    private static Dictionary<string, IMapping<T2, T1>> T2ToT1Mappings { get; } = new();

    #region protected builders

    protected static void MapTwoWay<TValue>(
        Expression<Func<T1, TValue>> entityExp,
        Expression<Func<T2, TValue>> modelExp)
    {
        var etmMapping = new Mapping<T1, T2, TValue>(entityExp, modelExp);
        var mteMapping = new Mapping<T2, T1, TValue>(modelExp, entityExp);
        // dictionary[] -> if it exists: Add throws, TryAdd does nothing, indexer overwrites
        T1ToT2Mappings[etmMapping.Key] = etmMapping;
        T2ToT1Mappings[mteMapping.Key] = mteMapping;
    }

    protected static void MapT1ToT2<TValue>(
        Expression<Func<T1, TValue>> t1Exp,
        Expression<Func<T2, TValue>> t2Exp
        , Action<T1, T2>? onChangeAction = null)
    {
        var etmMapping = new Mapping<T1, T2, TValue>(t1Exp, t2Exp, onChangeAction);
        T1ToT2Mappings[etmMapping.Key] = etmMapping;
    }

    protected static void MapT2ToT1<TValue>(
        Expression<Func<T2, TValue>> t2Exp,
        Expression<Func<T1, TValue>> t1Exp,
        Action<T2, T1>? onChangeAction = null)
    {
        var t2ToT1Mapping = new Mapping<T2, T1, TValue>(t2Exp, t1Exp, onChangeAction);
        T2ToT1Mappings[t2ToT1Mapping.Key] = t2ToT1Mapping;
    }

    #endregion

    #region public builders

    public void T1ToT2<TValue>(Expression<Func<T1, TValue>> t1Exp, Expression<Func<T2, TValue>> t2Exp,
        Action<T1, T2>? onChangeAction = null)
        => MapT1ToT2(t1Exp, t2Exp, onChangeAction);

    public void T2ToT1<TValue>(Expression<Func<T2, TValue>> t2Exp, Expression<Func<T1, TValue>> t1Exp,
        Action? onChangeAction = null)
        => MapT2ToT1(t2Exp, t1Exp);

    public void TwoWay<TValue>(Expression<Func<T1, TValue>> t1Exp, Expression<Func<T2, TValue>> t2Exp)
        => MapTwoWay(t1Exp, t2Exp);

    #endregion


    #region main function

    public T2 Get2From(T1 o1)
    {
        T2 result = new();
        foreach (var keyValuePair in T1ToT2Mappings) keyValuePair.Value.Apply(o1, result);
        return result;
    }

    public T1 Get1From(T2 o2)
    {
        T1 result = new();
        foreach (var keyValuePair in T2ToT1Mappings) keyValuePair.Value.Apply(o2, result);
        return result;
    }

    public bool UpdateT1FromT2(T1 o1, T2 o2)
        => T2ToT1Mappings.Aggregate(false, (b, pair) => pair.Value.ApplyConditional(o2, o1) || b);

    public bool UpdateT2FromT1(T2 o2, T1 o1)
        => T1ToT2Mappings.Aggregate(false, (b, pair) => pair.Value.ApplyConditional(o1, o2) || b);

    #endregion

    #region internal

    private interface IMapping<in TSrc, in TDst>
    {
        string Key { get; }
        void Apply(TSrc src, TDst dst);
        public bool ApplyConditional(TSrc src, TDst dst);
    }

    public class Mapping<TSrc, TDst, TValue> : IMapping<TSrc, TDst>
    {
        private readonly MemberInfo srcMember;
        private readonly MemberInfo dstMember;

        protected readonly Func<TSrc, TValue> SrcGetter;
        protected readonly Func<TDst, TValue> DstGetter;
        protected readonly Action<TDst, TValue> DstSetter = null!;
        private readonly Action<TSrc, TDst>? changeAction;

        public Mapping(Expression<Func<TSrc, TValue>> srcExp, Expression<Func<TDst, TValue>> dstExp,
            Action<TSrc, TDst>? onChangeAction = null)
        {
            changeAction = onChangeAction;
            srcMember = MapperExpressionToMemberBuilder.SourceExpression(srcExp);
            dstMember = MapperExpressionToMemberBuilder.DestinationMember(dstExp);
            if (dstMember is PropertyInfo { CanWrite: false }) throw new ArgumentException("Destination is read-only");

            SrcGetter = srcExp.Compile();
            DstGetter = dstExp.Compile();
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
                    DstSetter = (Action<TDst, TValue>)dynamicMethod.CreateDelegate(typeof(Action<TDst, TValue>));
                    break;
                case PropertyInfo pi:
                    var setter = pi.GetSetMethod(true)!;
                    DstSetter = (Action<TDst, TValue>)Delegate.CreateDelegate(typeof(Action<TDst, TValue>), setter);
                    break;
            }
        }

        public string Key => $"{srcMember.Name}_{dstMember.Name}";

        public void Apply(TSrc src, TDst dst) => DstSetter(dst, SrcGetter(src));

        public bool ApplyConditional(TSrc src, TDst dst)
        {
            var oldValue = DstGetter(dst);
            var newValue = SrcGetter(src);

            // compare the two values without boxing
            if (typeof(TValue).IsValueType)
            {
                if (EqualityComparer<TValue>.Default.Equals(oldValue, newValue)) return false;
            }
            else
            {
                if (ReferenceEquals(
                        // ReSharper disable once HeapView.PossibleBoxingAllocation
                        oldValue,
                        // ReSharper disable once HeapView.PossibleBoxingAllocation
                        newValue
                    )) return false;
                if (oldValue is null && newValue is null)
                    return false;

                if (oldValue is not null &&
                    // ReSharper disable once HeapView.PossibleBoxingAllocation
                    oldValue.Equals(
                        // ReSharper disable once HeapView.PossibleBoxingAllocation
                        newValue))
                    return false;
            }

            DstSetter(dst, newValue);
            changeAction?.Invoke(src, dst);
            return true;
        }

        //// // kept for benchmark and refernce, not used (causes boxing)
        //// /// <obsolete>causes boxing</obsolete>
        //// public void Apply2(TSrc src, TDst dst)
        //// {
        ////     var c = srcExp.Compile();
        ////     switch (dstMember)
        ////     {
        ////         case FieldInfo fi:
        ////             fi.SetValue(dst, c(src));
        ////             break;
        ////         case PropertyInfo pi:
        ////             pi.SetValue(dst, c(src));
        ////             break;
        ////     }
        //// }
    }

    #endregion
}