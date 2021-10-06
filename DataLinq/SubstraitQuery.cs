using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataLinq
{
    public interface ISubstraitQuery
    {
        public List<string> Paths { get; }
        public Substrait.Protobuf.Type.Types.NamedStruct Schema { get; }
    }
    
    public class SubstraitQuery<T> : IOrderedQueryable<T>, ISubstraitQuery
    {
        
        private readonly IQueryProvider _provider;
        public List<string> Paths { get; init; }
        public Substrait.Protobuf.Type.Types.NamedStruct Schema { get; init; }

        public SubstraitQuery(IQueryProvider provider, List<string> paths)
        {
            this._provider = provider;
            Paths = paths;
            Schema = TypeParser.SchemaFromType(typeof(T));
            Expression = Expression.Constant(this);
        }

        public SubstraitQuery(IQueryProvider provider, Expression expression, List<string> paths)
        {
            this._provider = provider;
            Paths = paths;
            Schema = TypeParser.SchemaFromType(typeof(T));
            Expression = expression;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var queryResult = _provider.Execute(Expression);
            if (queryResult is IEnumerator<T> result)
            {
                return result;
            }
            throw new InvalidOperationException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType { get; } = typeof(T);
        public Expression Expression { get; init; }

        public IQueryProvider Provider => _provider;
    }
}