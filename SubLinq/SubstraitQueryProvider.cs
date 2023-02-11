using System;
using System.IO;
using Substrait.Protobuf;
using Expression = System.Linq.Expressions.Expression;
using Type = Substrait.Protobuf.Type;

namespace SubLinq
{
    public class SubstraitQueryProvider : BaseQueryProvider
    {
        public SubstraitQueryProvider()
        {
        }

        public override object? Execute(Expression expression)
        {
            var expressionVisitor = new DotnetExpressionVisitor();
            return expressionVisitor.VisitDotnetExpression(expression);
        }

    }
}