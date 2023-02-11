using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Google.Protobuf;
using Expression = System.Linq.Expressions.Expression;
using Literal = Substrait.Protobuf.Expression.Types.Literal;
using SubstraitExpression = Substrait.Protobuf.Expression;

namespace SubLinq
{
    public static class LiteralParser
    {
        public static Literal ParseLiteral(object? value)
        {
            switch (value)
            {
                case bool b:
                    return new Literal { Boolean = b };
                case byte b:
                    if (b >= 128)
                        return new Literal { I16 = b };
                    else
                        return new Literal { I8 = b };
                case sbyte s:
                    return new Literal { I8 = s };
                case short s:
                    return new Literal { I16 = s };
                case ushort s:
                    if (s >= short.MaxValue)
                        return new Literal { I32 = s };
                    else
                        return new Literal { I16 = s };
                case int i:
                    return new Literal { I32 = i };
                case uint i:
                    if (i >= int.MaxValue)
                        return new Literal { I64 = i };
                    else
                        return new Literal { I32 = (int)i };
                case long l:
                    return new Literal { I64 = l };
                case ulong l:
                    if (l >= long.MaxValue)
                        throw new Exception($"{l} exceeds max value for substrait integer literal");
                    else
                        return new Literal { I64 = (long)l };
                case float f:
                    return new Literal { Fp32 = f };
                case double d:
                    return new Literal { Fp64 = d };
                case string s:
                    return new Literal { String = s };
                case byte[] b:
                    return new Literal { Binary = ByteString.CopyFrom(b) };
                case DateTime d:
                    var dDiff = d - DateTime.UnixEpoch;
                    if (d.Kind == DateTimeKind.Local)
                    {
                        return new Literal { Timestamp = ((long)(dDiff.TotalMilliseconds)) * 1000L };
                    }
                    else
                    {
                        return new Literal { TimestampTz = ((long)(dDiff.TotalMilliseconds)) * 1000L };
                    }
                case DateOnly dateOnly:
                    var diff = dateOnly.ToDateTime(TimeOnly.MinValue) - DateTime.UnixEpoch;
                    return new Literal { Date = diff.Days };
                case TimeSpan timeSpan:
                    return new Literal
                    {
                        IntervalDayToSecond = new Literal.Types.IntervalDayToSecond
                        {
                            Days = timeSpan.Days,
                            Seconds = timeSpan.Seconds,
                            Microseconds = timeSpan.Milliseconds * 1000
                        }
                    };
                case decimal dc:
                    // C#'s decimal is a bit different than Arrow/Substrait decimals
                    // as it encodes the scale into the decimal itself and has a fixed
                    // precision.  It only uses 12 bytes for the value, which is always
                    // positive and saves 4 bytes for a sign bit, scale, and other metadata
                    var ints = System.Decimal.GetBits(dc);
                    var bytes = MemoryMarshal.AsBytes(ints.AsSpan()).ToArray();
                    var scale = bytes[14];
                    if (bytes[15] != 0)
                    {
                        // negative decimal, must 2's complement the bytes
                        bool sign_bit_swallowed = false;
                        for (int i = 0; i < 12; i++)
                        {
                            bytes[i] = (byte)~bytes[i];
                            if (!sign_bit_swallowed)
                            {
                                if (bytes[i] == 0xFF)
                                {
                                    bytes[i] = 0;
                                }
                                else
                                {
                                    bytes[i] += 1;
                                    sign_bit_swallowed = true;
                                }
                            }
                        }
                        if (!sign_bit_swallowed)
                        {
                            // input was "negative 0", not clear if this is a
                            // reachable C# case but output is 0 which is positive
                            bytes[12] = 0;
                            bytes[13] = 0;
                            bytes[14] = 0;
                            bytes[15] = 0;
                        }
                        else
                        {
                            // input was negative non-zero.  Highest four bytes all FF
                            bytes[12] = 0xFF;
                            bytes[13] = 0xFF;
                            bytes[14] = 0xFF;
                            bytes[15] = 0xFF;
                        }
                    }
                    else
                    {
                        // input was positive non-zero.  Highest four bytes all 00
                        bytes[12] = 0;
                        bytes[13] = 0;
                        bytes[14] = 0;
                        bytes[15] = 0;
                    }
                    return new Literal
                    {
                        Decimal = new Literal.Types.Decimal
                        {
                            Precision = 28,
                            Scale = scale,
                            Value = ByteString.CopyFrom(bytes)
                        }
                    };
                case Guid guid:
                    return new Literal
                    {
                        Uuid = ByteString.CopyFrom(guid.ToByteArray())
                    };
                default:
                    throw new Exception($"Cannot convert literal of type {value?.GetType()} to Substrait literal");
            }
        }

    }
}