using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ts.Domain;

namespace ts.OData.Core.WithModel.Infrastructure
{
    public interface IQueryableODataProjection
    {
        IQueryable Source { get; set; }
        Type Type { get; }
    }

    public class QueryableODataProjection<TEntity, TModel> :
        IQueryable<TModel>,
        IQueryableODataProjection 
        where TModel : ModelConvertibleBase<TEntity, TModel>, new()
    {
        private IQueryable<TEntity> _source;
        public IQueryable Source
        {
            get => _source;
            set => _source = (IQueryable<TEntity>)value;
        }

        public Type Type => typeof(TEntity);


        public QueryableODataProjection(IQueryable<TEntity> source) => _source = source;

        public IEnumerator<TModel> GetEnumerator()
        {
            return new ModelProjectionEnumerator<TEntity, TModel>(_source.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression => Source.Expression;
        public Type ElementType => Source.ElementType;
        public IQueryProvider Provider => Source.Provider;

        public class ModelProjectionEnumerator<TSrcEnum, TDstEnum> :
            IEnumerator<TDstEnum> where TDstEnum : ModelConvertibleBase<TSrcEnum, TDstEnum>, new()
        {
            private readonly IEnumerator<TSrcEnum> _sourceEnumerator;

            public ModelProjectionEnumerator(IEnumerator<TSrcEnum> sourceEnumerator)
            {
                _sourceEnumerator = sourceEnumerator;
            }

            public bool MoveNext()
            {
                return _sourceEnumerator.MoveNext();
            }

            public void Reset()
            {
                _sourceEnumerator.Reset();
            }

            public TDstEnum Current => ModelConvertibleBase<TSrcEnum, TDstEnum>.FromEntity(_sourceEnumerator.Current);

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                _sourceEnumerator.Dispose();
            }
        }
    }


}