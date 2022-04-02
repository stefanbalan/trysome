using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoL.DB
{
    public class ModelConvertible<TEntity, TModel>
    {
        private static Dictionary<string, IModelConvertible<TEntity,  TModel>> ClassMappings { get; } 
            = new Dictionary<string, IModelConvertible<TEntity, TModel>>();

        public static void MapClasses<TEntity, TModel>(Action<ConvertibleBuilder> builder) where TModel : new()
        {
            //var classMapping = new ModelConvertibleBase<TEntity, TModel>();
            //ClassMappings.Add(""/*classMapping.Key*/, (IModelConvertible<TEntity, TModel>)classMapping);
        }
    }

    public class ConvertibleBuilder { }
}
