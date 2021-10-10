using System;
using System.Linq.Expressions;
using Google.Protobuf;
using Expression = System.Linq.Expressions.Expression;
using Literal = Substrait.Protobuf.Expression.Types.Literal;
using SubstraitExpression = Substrait.Protobuf.Expression;

namespace SubLinq
{
    public static class ExpressionParser
    {
        public static Literal ParseLiteral(object? value)
        {
            switch (value)
            {
                case bool b:
                    return new Literal {Boolean = b};
                case byte b:
                    if (b >= 128)
                        return new Literal {I16 = b};
                    else
                        return new Literal {I8 = b};
                case sbyte s:
                    return new Literal {I8 = s};
                case short s:
                    return new Literal {I16 = s};
                case ushort s:
                    if (s >= short.MaxValue)
                        return new Literal {I32 = s};
                    else
                        return new Literal {I16 = s};
                case int i:
                    return new Literal {I32 = i};
                case uint i:
                    if (i >= int.MaxValue)
                        return new Literal {I64 = i};
                    else
                        return new Literal {I32 = (int) i};
                case long l:
                    return new Literal {I64 = l};
                case ulong l:
                    if (l >= long.MaxValue)
                        throw new Exception($"{l} exceeds max value for substrait integer literal");
                    else
                        return new Literal {I64 = (long) l};
                case float f:
                    return new Literal {Fp32 = f};
                case double d:
                    return new Literal {Fp64 = d};
                case string s:
                    return new Literal {String = s};
                case byte[] b:
                    return new Literal {Binary = ByteString.CopyFrom(b)};
                case DateTime:
                    throw new Exception("I don't want to figure this out yet");
                default:
                    throw new Exception($"Cannot convert literal of type {value?.GetType()} to Substrait literal");
            }
        }

        public static SubstraitExpression ParseExpression(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Constant:
                    return new SubstraitExpression
                    {
                        Literal = ParseLiteral(((ConstantExpression) expression).Value!)
                    };
                default:
                    throw new Exception($"Unknown expression node type: {expression.NodeType}");
            }
        }
        
        public static SubstraitExpression ParseExpression<T>(Expression<T> expression)
        {
            switch (expression.NodeType)
            {
                default:
                    throw new Exception($"Unknown expression node type: {expression.NodeType}");
            }
        }

        // public SubstraitExpression ParseFieldReference(Expression expr)
        // {
        //     switch (expr)
        //     {
        //         case MemberExpression mExpr:
        //             return new FieldReference { DirectReference = new ReferenceSegment { }mExpr.Member.Name);}
        //         default:
        //             throw new Exception($"Unknown expression type for field reference: {expr.GetType()}");
        //     }
        // }
        //
        // public SubstraitExpression ParsePredicate<T>(Expression<Func<T, bool>> expression)
        // {
        //     switch (expression.Body.NodeType)
        //     {
        //         default:
        //             throw new Exception($"Unknown predicate node type: {expression.Body.NodeType}");
        //     }
        // }

    }
}