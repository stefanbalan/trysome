using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoL.DB
{
    public class ModelConvertible
    {
        private static Dictionary<string, IModelConvertible> ClassMappings { get; } 
            = new Dictionary<string, IModelConvertible>();

        public static void MapClasses<TEntity, TModel>(Action<ConvertibleBuilder> builder) where TModel : new()
        {
            var classMapping = new ModelConvertibleBase<TEntity, TModel>();
            ClassMappings.Add(""/*classMapping.Key*/, classMapping);
        }
    }

    public class ConvertibleBuilder { }
}
