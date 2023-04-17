using System;
using System.Collections.Generic;

namespace CoL.DB.ModelConvertible
{
    public class ModelConvertible<TEntity, TModel>
        where TModel : new()
    {
        private static Dictionary<string, IModelConvertible<TEntity, TModel>> ClassMappings { get; } = new();

        public static void MapClasses(Action<ConvertibleBuilder> builder)
        {
            //var classMapping = new ModelConvertibleBase<TEntity, TModel>();
            //ClassMappings.Add(""/*classMapping.Key*/, (IModelConvertible<TEntity, TModel>)classMapping);
        }
    }

    public class ConvertibleBuilder
    {
    }
}
