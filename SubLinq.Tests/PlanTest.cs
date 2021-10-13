using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SubLinq.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ScanOnly()
        {
            var substraitQueryProvider = new SubstraitQueryProvider(TypeParser.SchemaFromType<SampleTable1>());
            var substraitQuery =
                new SubstraitQuery<SampleTable1>(substraitQueryProvider, "myTable");
            var result = substraitQuery.ToList();
        }

        [Test]
        public void ProjectionWithFilter()
        {
            var substraitQueryProvider = new SubstraitQueryProvider(TypeParser.SchemaFromType<SampleTable1>());
            var substraitQuery =
                new SubstraitQuery<SampleTable1>(substraitQueryProvider, "tbl");
            var result = substraitQuery.Select(t => new { foo = t.bar, baz = t.baz }).Where(t => t.foo < 3).ToList();
        }
    }
}