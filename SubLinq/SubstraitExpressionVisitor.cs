using System;
using System.Linq.Expressions;
using Substrait.Protobuf;
using Expr = System.Linq.Expressions.Expression;
using Expression = Substrait.Protobuf.Expression;
using Type = Substrait.Protobuf.Type;

namespace SubLinq
{
    public class SubstraitExpressionVisitor
    {
        private Type.Types.NamedStruct schema;
        
        public SubstraitExpressionVisitor(Type.Types.NamedStruct schema)
        {
            this.schema = schema;
        }

        public Rel VisitRel(Expr? expr)
        {
            switch (expr)
            {
                case MethodCallExpression methodCall:
                    return VisitMethodCall(methodCall);
                case ConstantExpression constCall:
                    return VisitSourceCall(constCall);
                default:
                    throw new Exception("Unrecognized node type when expecting a Rel");
            }
        }

        private int FieldIndex(string fieldName)
        {
            return schema.Names.IndexOf(fieldName);
        }
        
        private Rel VisitSourceCall(ConstantExpression constExpr)
        {
            var substQuery = constExpr.Value as ISubstraitQuery;
            if (substQuery == null)
            {
                throw new Exception(
                    $"Unrecognized constant type {constExpr.Type}.  The only constant allowed at the Rel level is a source query");
            }

            var namedTable = new ReadRel.Types.NamedTable();
            namedTable.Names.Add(substQuery.SourceTableName);

            return new Rel
            {
                Read = new ReadRel
                {
                    Common = SubstraitUtil.SimpleRelCommon,
                    Filter = SubstraitUtil.ExpressionTrue,
                    Projection = SubstraitUtil.DefaultProjection,
                    BaseSchema = substQuery.Schema,
                    NamedTable = namedTable
                }
            };
        }

        private Expression VisitComparisonFunction(BinaryExpression expr, ulong funcId)
        {
            Expression leftArg = VisitExpression(expr.Left);
            Expression rightArg = VisitExpression(expr.Right);
            var func = new Expression.Types.ScalarFunction
            {
                Id = new Extensions.Types.FunctionId { Id = funcId },
                OutputType = new Substrait.Protobuf.Type {Bool = { }}
            };
            func.Args.Add(leftArg);
            func.Args.Add(rightArg);
            return new Expression
            {
                ScalarFunction = func
            };
        }

        private Expression VisitFieldRef(MemberExpression expr)
        {
            return new Expression
            {
                Selection = new FieldReference
                {
                    DirectReference = new ReferenceSegment
                    {
                        StructField = new ReferenceSegment.Types.StructField
                        {
                            Field = FieldIndex(expr.Member.Name)
                        }
                    }
                }
            };
        }

        private Expression VisitLiteral(ConstantExpression expr)
        {
            return new Expression
            {
                Literal = ExpressionParser.ParseLiteral(expr.Value)
            };
        }
        
        private Expression VisitExpression(Expr quotedExpr)
        {
            var expr = EnsureUnquoted(quotedExpr);
            switch (expr)
            {
                case BinaryExpression bExpr:
                    switch (expr.NodeType)
                    {
                        case ExpressionType.LessThan:
                            return VisitComparisonFunction(bExpr, SubstraitUtil.WellKnownFunctionIds.LessThan);
                        default:
                            throw new Exception();
                    }
                case MemberExpression mExpr:
                    return VisitFieldRef(mExpr);
                case ConstantExpression cExpr:
                    return VisitLiteral(cExpr);
            }
            throw new Exception();
        }

        private Expr EnsureUnquoted(Expr expr)
        {
            if (expr.NodeType == ExpressionType.Quote)
            {
                return EnsureUnquoted(((UnaryExpression) expr).Operand);
            }
            if (expr.NodeType == ExpressionType.Lambda)
            {
                return EnsureUnquoted(((LambdaExpression) expr).Body);
            }

            return expr;
        }
        
        protected Rel VisitWhere(MethodCallExpression methodCall)
        {
            var sourceExpr = methodCall.Arguments[0];
            Rel input = VisitRel(sourceExpr);
            var predicateExpr = methodCall.Arguments[1];
            Expression condition = VisitExpression(predicateExpr);
            return new Rel
            {
                Filter = new FilterRel
                {
                    Common = SubstraitUtil.SimpleRelCommon,
                    Input = input,
                    Condition = condition
                }
            };
        }
        
        protected Rel VisitMethodCall(MethodCallExpression methodCall)
        {
            switch (methodCall.Method.Name)
            {
                case "Where":
                    return VisitWhere(methodCall);
                default:
                    throw new Exception($"Unrecognized method call {methodCall.Method.Name} in expression");
            }
        }

    }
}