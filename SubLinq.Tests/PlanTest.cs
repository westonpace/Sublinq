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
        public void ProjectionWithFilter()
        {
            var substraitQueryProvider = new SubstraitQueryProvider(TypeParser.SchemaFromType<SampleTable1>());
            var substraitQuery =
                new SubstraitQuery<SampleTable1>(substraitQueryProvider, new List<string>() {"foo.parquet"});
            var result = substraitQuery.Where(t => t.foo < 3).ToList();
        }
    }
}