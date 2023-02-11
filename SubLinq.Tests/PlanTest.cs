using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SubLinq.Tests
{
    public class Tests
    {

        private SubstraitConsumer mockConsumer;

        [SetUp]
        public void Setup()
        {
            mockConsumer = new SubstraitConsumer();
        }

        private void TestPlan<T>(IEnumerable<T> plan)
        {
            var queryable = plan.AsQueryable();
            var query = queryable.Provider.Execute(queryable.Expression);
            Console.WriteLine(query);
        }

        [Test]
        public void ScanOnly()
        {
            TestPlan(mockConsumer.NamedTable<SampleTable1>("myTable"));
        }

        [Test]
        public void ProjectionWithFilter()
        {
            TestPlan(mockConsumer.NamedTable<SampleTable1>("tbl")
                .Select(t => new { foo = t.bar, baz = t.baz })
                .Where(t => t.foo < 3));
        }
    }
}