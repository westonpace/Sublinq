using System;
using Substrait.Protobuf;
using Expression = System.Linq.Expressions.Expression;
using Type = Substrait.Protobuf.Type;

namespace SubLinq
{
    public class SubstraitQueryProvider : BaseQueryProvider
    {
        private Type.Types.NamedStruct schema;

        public SubstraitQueryProvider(Type.Types.NamedStruct schema)
        {
            this.schema = schema;
        }
        
        public override object? Execute(Expression expression)
        {
            var expressionVisitor = new SubstraitExpressionVisitor(schema);
            var rel = expressionVisitor.VisitRel(expression);
            Console.WriteLine(rel);
            SubstraitToArrow substraitToArrow = new SubstraitToArrow();
            substraitToArrow.VisitRel(rel);
            substraitToArrow.Write();
            throw new Exception();
        }
    }
}