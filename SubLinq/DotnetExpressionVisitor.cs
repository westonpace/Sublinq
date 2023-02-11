using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Substrait.Protobuf;
using Expr = System.Linq.Expressions.Expression;
using Expression = Substrait.Protobuf.Expression;

namespace SubLinq
{
    public class DotnetExpressionVisitor
    {
        private NamedStruct? currentSchema;

        public Rel VisitDotnetExpression(Expr? expr)
        {
            return VisitRel(expr);
        }

        private Rel VisitRel(Expr? expr)
        {
            switch (expr)
            {
                case MethodCallExpression methodCall:
                    return VisitMethodCall(methodCall);
                case ConstantExpression constCall:
                    return VisitConstantExpression(constCall);
                default:
                    throw new Exception("Unrecognized node type when expecting a Rel");
            }
        }

        private int FieldIndex(string fieldName)
        {
            return currentSchema!.Names.IndexOf(fieldName);
        }

        private Rel VisitConstantExpression(ConstantExpression constExpr)
        {
            var substQuery = constExpr.Value as SubstraitSource;
            if (substQuery == null)
            {
                throw new Exception(
                    $"Unrecognized constant type {constExpr.Type}.  The only constant allowed at the Rel level is a source.");
            }

            currentSchema = substQuery.OutputSchema;
            return substQuery.Rel;
        }

        private Expression VisitComparisonFunction(BinaryExpression expr, uint funcId)
        {
            Expression leftArg = VisitExpression(expr.Left);
            Expression rightArg = VisitExpression(expr.Right);
            var func = new Expression.Types.ScalarFunction
            {
                FunctionReference = funcId,
                OutputType = new Substrait.Protobuf.Type { Bool = { } }
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
                Selection = new Substrait.Protobuf.Expression.Types.FieldReference
                {
                    DirectReference = new Substrait.Protobuf.Expression.Types.ReferenceSegment
                    {
                        StructField = new Substrait.Protobuf.Expression.Types.ReferenceSegment.Types.StructField
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
                Literal = LiteralParser.ParseLiteral(expr.Value)
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
                return EnsureUnquoted(((UnaryExpression)expr).Operand);
            }
            if (expr.NodeType == ExpressionType.Lambda)
            {
                return EnsureUnquoted(((LambdaExpression)expr).Body);
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
                case "Select":
                    return VisitSelect(methodCall);
                default:
                    throw new Exception($"Unrecognized method call {methodCall.Method.Name} in expression");
            }
        }

        protected Rel VisitSelect(MethodCallExpression methodCall)
        {
            if (methodCall.Arguments.Count != 2)
            {
                throw new Exception("Unexpected number of arguments in select call");
            }

            Rel source = VisitRel(methodCall.Arguments[0]);
            Expr projection = EnsureUnquoted(methodCall.Arguments[1]);
            if (projection is NewExpression newExpr)
            {
                List<Expression> expressions = newExpr.Arguments.Select(VisitExpression).ToList();
                ProjectRel projectRel = new ProjectRel
                {
                    Common = SubstraitUtil.SimpleRelCommon,
                    Input = source
                };
                projectRel.Expressions.AddRange(expressions);
                return new Rel
                {
                    Project = projectRel
                };
            }

            throw new Exception("Projection expression was not a new expression");
        }

    }
}