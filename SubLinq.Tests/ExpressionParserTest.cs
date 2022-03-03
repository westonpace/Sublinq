using System;
using NUnit.Framework;
using Substrait.Protobuf;

using Literal = Substrait.Protobuf.Expression.Types.Literal;

namespace SubLinq.Tests
{
    public class ExpressionParserTest
    {
        class TestObject
        {
            public sbyte MySbyte { get; private set; }
            public byte MyByte { get; private set; }
            public int MyInt { get; private set; }
        }

        [Test]
        public void TestLiterals()
        {
            Assert.AreEqual(new Literal {Boolean = true}, ExpressionParser.ParseLiteral(true));
            Assert.AreEqual(new Literal {Boolean = false}, ExpressionParser.ParseLiteral(false));
            Assert.AreEqual(new Literal {I8 = 12}, ExpressionParser.ParseLiteral((sbyte)12));
            Assert.AreEqual(new Literal {I8 = 12}, ExpressionParser.ParseLiteral((byte)12));
            Assert.AreEqual(new Literal {I16 = 200}, ExpressionParser.ParseLiteral((byte)200));
        }
        
    }
}