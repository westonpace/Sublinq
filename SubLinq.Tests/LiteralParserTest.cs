using System;
using NUnit.Framework;

using Literal = Substrait.Protobuf.Expression.Types.Literal;

namespace SubLinq.Tests
{
    public class LiteralParserTest
    {
        [Test]
        public void TestLiterals()
        {
            Assert.AreEqual(new Literal { Boolean = true }, LiteralParser.ParseLiteral(true));
            Assert.AreEqual(new Literal { Boolean = false }, LiteralParser.ParseLiteral(false));
            Assert.AreEqual(new Literal { I8 = 12 }, LiteralParser.ParseLiteral((sbyte)12));
            Assert.AreEqual(new Literal { I8 = 12 }, LiteralParser.ParseLiteral((byte)12));
            Assert.AreEqual(new Literal { I16 = 200 }, LiteralParser.ParseLiteral((byte)200));
            Assert.AreEqual(new Literal { I32 = 30000 }, LiteralParser.ParseLiteral(30000));
            Assert.AreEqual(new Literal { I64 = ((long)Int32.MaxValue + 1) },
                LiteralParser.ParseLiteral((long)Int32.MaxValue + 1));

            var someBytes = new byte[] { 0, 1, 200 };
            Assert.AreEqual(new Literal { Binary = Google.Protobuf.ByteString.CopyFrom(someBytes) },
                LiteralParser.ParseLiteral(someBytes));

            var theMostImportantDateTime = DateTime.Parse("1986-04-26T07:00:00-05:00", null, System.Globalization.DateTimeStyles.AdjustToUniversal);
            var theMostImportantCalendarTime = DateTime.Parse("1986-04-26T07:00:00", null, System.Globalization.DateTimeStyles.AssumeLocal);
            var timeSinceEpoch = theMostImportantDateTime - DateTime.UnixEpoch;
            var localTimeSinceEpoch = theMostImportantCalendarTime - DateTime.UnixEpoch;
            var daysSinceEpoch = timeSinceEpoch.Days;
            var theMostImportantDate = DateOnly.FromDateTime(theMostImportantDateTime);
            Assert.AreEqual(new Literal { Date = daysSinceEpoch },
                LiteralParser.ParseLiteral(theMostImportantDate));

            //little-endian 16 byte 2's complement of 12345
            var decimalBytes = new byte[] { 0x39, 0x30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Assert.AreEqual(new Literal
            {
                Decimal = new Literal.Types.Decimal
                {
                    Value = Google.Protobuf.ByteString.CopyFrom(decimalBytes),
                    Precision = 28,
                    Scale = 2
                }
            }, LiteralParser.ParseLiteral(123.45m));
            //little-endian 16 byte 2's complement of -12345
            decimalBytes = new byte[] { 0xC7, 0xCF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                                        0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            Assert.AreEqual(new Literal
            {
                Decimal = new Literal.Types.Decimal
                {
                    Value = Google.Protobuf.ByteString.CopyFrom(decimalBytes),
                    Precision = 28,
                    Scale = 2
                }
            }, LiteralParser.ParseLiteral(-123.45m));

            Assert.AreEqual(new Literal { Fp32 = 1.2f }, LiteralParser.ParseLiteral(1.2f));
            Assert.AreEqual(new Literal { Fp64 = 1.2 }, LiteralParser.ParseLiteral(1.2));
            Assert.AreEqual(new Literal
            {
                IntervalDayToSecond = new Literal.Types.IntervalDayToSecond
                {
                    Days = timeSinceEpoch.Days,
                    Seconds = timeSinceEpoch.Seconds,
                    Microseconds = timeSinceEpoch.Milliseconds * 1000
                }
            }, LiteralParser.ParseLiteral(timeSinceEpoch));

            Assert.AreEqual(new Literal { String = "test" }, LiteralParser.ParseLiteral("test"));
            Assert.AreEqual(new Literal { TimestampTz = (long)(timeSinceEpoch.TotalMilliseconds) * 1000L },
                LiteralParser.ParseLiteral(theMostImportantDateTime));

            Assert.AreEqual(new Literal { Timestamp = (long)(localTimeSinceEpoch.TotalMilliseconds) * 1000L },
                LiteralParser.ParseLiteral(theMostImportantCalendarTime));

            var guid = Guid.NewGuid();
            var guidBytes = guid.ToByteArray();
            Assert.AreEqual(new Literal { Uuid = Google.Protobuf.ByteString.CopyFrom(guidBytes) },
                LiteralParser.ParseLiteral(guid));

            // No direct mapping to
            // - fixed char
            // - fixed binary
            // - null (requires type)
            // - interval_year_to_month
            // - varchar
            //
            // These types don't exist naturally within C# and so we are unlikely to encounter them as literals

            // TODO: nested types (struct, list, map)
            // TODO: user defined types
        }

    }
}