using System.Collections.Generic;
using NUnit.Framework;
using Substrait.Protobuf;

namespace DataLinq.Tests
{
    public class TypeParserTest
    {
        [Test]
        public void TestSampleTable1()
        {
            var expectedSchema = new Type.Types.NamedStruct();
            expectedSchema.Names.Add(new List<string>() {"foo", "bar", "baz"});
            expectedSchema.Struct = new Type.Types.Struct
            {
                Nullability = Type.Types.Nullability.Nullable
            };
            expectedSchema.Struct.Types_.Add(new Type {I32 = new Type.Types.I32 { Nullability = Type.Types.Nullability.Nullable}});
            expectedSchema.Struct.Types_.Add(new Type {I64 = new Type.Types.I64 { Nullability = Type.Types.Nullability.Nullable}});
            expectedSchema.Struct.Types_.Add(new Type {Fp64 = new Type.Types.FP64 { Nullability = Type.Types.Nullability.Nullable}});

            var actualSchema = TypeParser.SchemaFromType<SampleTable1>();
            Assert.AreEqual(expectedSchema, actualSchema);
        }
    }
}