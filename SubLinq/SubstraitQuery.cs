using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SubLinq
{
    public interface ISubstraitQuery
    {
        public string SourceTableName { get; }
        public Substrait.Protobuf.Type.Types.NamedStruct Schema { get; }
    }
    
    public class SubstraitQuery<T> : IOrderedQueryable<T>, ISubstraitQuery
    {
        
        private readonly IQueryProvider _provider;
        public string SourceTableName { get; }
        public Substrait.Protobuf.Type.Types.NamedStruct Schema { get; init; }

        public SubstraitQuery(IQueryProvider provider, string sourceTableName)
        {
            this._provider = provider;
            SourceTableName = sourceTableName;
            Schema = TypeParser.SchemaFromType(typeof(T));
            Expression = Expression.Constant(this);
        }

        public SubstraitQuery(IQueryProvider provider, Expression expression, string sourceTableName)
        {
            this._provider = provider;
            SourceTableName = sourceTableName;
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