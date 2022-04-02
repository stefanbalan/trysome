using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace CoL.DB
{
    //todo implement as separate, injectable service?

    public interface IModelConvertible<TEntity, TModel>
    {
        string Key { get; }

        TModel FromEntityToThisModel(TEntity entity);

        TEntity ToEntity();
    }

    public class ModelConvertibleBase<TEntity, TModel> : IModelConvertible<TEntity, TModel>
        where TModel : /*ModelConvertibleBase<TEntity, TModel>,*/ new()
    {
        public TModel FromEntity(TEntity entity)
        {
            var model = new TModel();
            model = FromEntityToThisModel(entity);
            return model;
        }


        private static Dictionary<string, IMapping<TEntity, TModel>> Mappings { get; } = new Dictionary<string, IMapping<TEntity, TModel>>();

        public string Key => typeof(TEntity).FullName + "_" + typeof(TModel).FullName;

        public static void Map<TPropEntity, TPropModel>(Expression<Func<TEntity, TPropEntity>> entityExp, Expression<Func<TModel, TPropModel>> modelExp)
        {
            var mapping = new Mapping<TEntity, TPropEntity, TModel, TPropModel>(entityExp, modelExp);
            Mappings.Add(mapping.Key, mapping);
        }

        public TModel FromEntityToThisModel(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity ToEntity()
        {
            throw new NotImplementedException();
        }
    }



    internal interface IMapping<in TSrc, in TDst>
    {
        string Key { get; }
        void Map(TSrc src, TDst dst);
    }



    internal class Mapping<TSrc, TPropSrc, TDst, TPropDst> : IMapping<TSrc, TDst>
    {
        private PropertyInfo SrcProp { get; }
        private PropertyInfo DstProp { get; }

        public Mapping(Expression<Func<TSrc, TPropSrc>> srcExp, Expression<Func<TDst, TPropDst>> dstExp)
        {
            SrcProp = PropertyInfo(srcExp);
            DstProp = PropertyInfo(dstExp);
            if (!DstProp.CanWrite) throw new ArgumentException("Destination is read-only");
        }

        public string Key => $"{SrcProp.Name}_{DstProp.Name}";

        private static PropertyInfo PropertyInfo<T, TP>(Expression<Func<T, TP>> expression)
        {
            var memberExpression = expression.Body as MemberExpression ?? ((UnaryExpression)expression.Body).Operand as MemberExpression;

            var type = typeof(TP);
            if (memberExpression == null)
                throw new ArgumentException($"Expression '{expression.Name}' refers to a method, not a property.");

            var propInfo = memberExpression.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException($"Expression '{memberExpression.Member.Name}' refers to a field, not a property.");

            if (propInfo.ReflectedType == null ||
                type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException($"Expresion '{memberExpression.Member.Name}' refers to a property that is not from type {type}.");
            return propInfo;
        }

        public void Map(TSrc src, TDst dst)
        {
            DstProp.SetValue(dst, SrcProp.GetValue(src));
        }
    }

}
