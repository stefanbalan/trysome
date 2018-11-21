using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ts.Domain
{
    public abstract class ModelConvertibleBase<TEntity, TModel> where TModel : ModelConvertibleBase<TEntity, TModel>, new()
    {
        public static TModel FromEntity(TEntity entity)
        {
            var model = new TModel();
            model.FromEntityToThisModel(entity);
            return model;
        }

        protected abstract void FromEntityToThisModel(TEntity entity);

        public abstract TEntity ToEntity();

        public static Dictionary<string, Mapping> Mappings { get; protected set; } = new Dictionary<string, Mapping>();
        public static MappingBuilder MapBuilder { get; protected set; } = new MappingBuilder(Mappings);


        protected static MappingBuilder AddMapping(Expression<Func<TEntity, object>> entityExp, Expression<Func<TModel, object>> modelExp)
        {
            var mapping = new Mapping(entityExp, modelExp);
            Mappings.Add(mapping.GetEntityProperty().Name, mapping);
            return MapBuilder;
        }

        public class MappingBuilder
        {
            private readonly Dictionary<string, Mapping> _mappings;
            public MappingBuilder(Dictionary<string, Mapping> mappings)
            {
                _mappings = mappings;
            }
            public MappingBuilder Map(Expression<Func<TEntity, object>> entityExp, Expression<Func<TModel, object>> modelExp)
            {
                var mapping = new Mapping(entityExp, modelExp);
                _mappings.Add(mapping.GetEntityProperty().Name, mapping);
                return this;
            }
        }
        public class Mapping
        {
            private Expression<Func<TEntity, object>> EntityExp { get; }
            private Expression<Func<TModel, object>> ModelExp { get; }

            public Mapping(Expression<Func<TEntity, object>> entityExp, Expression<Func<TModel, object>> modelExp)
            {
                EntityExp = entityExp;
                ModelExp = modelExp;
            }

            public PropertyInfo GetEntityProperty()
            {
                return PropertyInfo(EntityExp, typeof(TEntity));
            }

            public PropertyInfo GetModelProperty()
            {
                return PropertyInfo(ModelExp, typeof(TModel));
            }

            private static PropertyInfo PropertyInfo<T>(Expression<Func<T, object>> expression, Type type)
            {
                var memberExpression = expression.Body as MemberExpression ?? ((UnaryExpression)expression.Body).Operand as MemberExpression;

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
        }
    }
}
