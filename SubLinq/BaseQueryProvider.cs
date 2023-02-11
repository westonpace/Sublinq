using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SubLinq
{
    public abstract class BaseQueryProvider : IQueryProvider
    {
        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
            /*Type elementType = TypeSystem.GetElementType(expression.Type);
            try
            {
                return (IQueryable)Activator.CreateInstance(typeof(SubstraitQuery<>).MakeGenericType(elementType), new object[] { this, expression })!;
            }
            catch (TargetInvocationException tie)
            {
                throw tie.InnerException!;
            }*/
        }

        IQueryable<T> IQueryProvider.CreateQuery<T>(Expression expression)
        {
            throw new NotImplementedException();
            /*return new SubstraitQuery<T>(this, expression);*/
        }

        public abstract object? Execute(Expression expression);

        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult)Execute(expression)!;
        }
    }
}