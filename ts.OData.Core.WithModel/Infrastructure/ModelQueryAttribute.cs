using System;
using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;

namespace ts.OData.Core.WithModel.Infrastructure
{
    public class ModelQueryAttribute : EnableQueryAttribute
    {
        public ModelQueryAttribute()
        {
            PageSize = //int.TryParse(ConfigurationManager.AppSettings["DefaultPageSize"], out var result) ? result : 
                100;
        }

        public override IQueryable ApplyQuery(IQueryable queryable, ODataQueryOptions queryOptions)
        {
            if (!(queryable is IQueryableODataProjection projection))
                throw new InvalidOperationException($"Action must return {nameof(IQueryableODataProjection)}");

            ODataModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.AddEntityType(projection.Type);

            var newOptions = new ODataQueryOptions(
                new ODataQueryContext(modelBuilder.GetEdmModel(), projection.Type, new ODataPath()),
                queryOptions.Request);

            projection.Source = base.ApplyQuery(projection.Source, newOptions);
            return (IQueryable)projection;
        }
    }
}