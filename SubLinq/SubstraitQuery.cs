using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SubLinq
{
    public class SubstraitQuery<T> : IOrderedQueryable<T>
    {

        private readonly IQueryProvider _provider;

        public SubstraitQuery(IQueryProvider provider, SubstraitSource source)
        {
            this._provider = provider;
            Expression = Expression.Constant(source);
        }

        // public SubstraitQuery(IQueryProvider provider, Expression expression)
        // {
        //     this._provider = provider;
        //     Schema = TypeParser.SchemaFromType(typeof(T));
        //     Expression = expression;
        // }

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
        public Expression Expression { get; private set; }

        public IQueryProvider Provider => _provider;
    }
}