using Substrait.Protobuf;

namespace SubLinq
{
    public static class SubstraitUtil
    {
        public static readonly RelCommon.Types.RuntimeConstraint NoConstraints = new RelCommon.Types.RuntimeConstraint();
        public static readonly RelCommon.Types.Hint NoHints = new RelCommon.Types.Hint();
        public static readonly RelCommon.Types.Direct Direct = new RelCommon.Types.Direct();

        public static readonly RelCommon SimpleRelCommon = new RelCommon
        {
            Constraint = NoConstraints,
            Hint = NoHints,
            Direct = Direct
        };

        public static readonly Expression.Types.Literal LiteralTrue = new Expression.Types.Literal
        {
            Boolean = true
        };
        
        public static readonly Expression ExpressionTrue = new Expression
        {
            Literal = LiteralTrue
        };

        public static readonly MaskExpression DefaultProjection = new MaskExpression
        {
        };

        public static class WellKnownFunctionIds
        {
            public const ulong LessThan = 1;
        }
    }
}