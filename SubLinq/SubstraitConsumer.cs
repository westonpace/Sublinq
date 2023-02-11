using System.Collections.Generic;

namespace SubLinq
{
    public class SubstraitConsumer
    {
        private readonly SubstraitQueryProvider queryProvider = new SubstraitQueryProvider();

        public SubstraitQuery<T> NamedTable<T>(List<string> tableName)
        {
            var outputSchema = TypeParser.SchemaFromType(typeof(T));

            var namedTable = new Substrait.Protobuf.ReadRel.Types.NamedTable();
            namedTable.Names.AddRange(tableName);

            var read = new Substrait.Protobuf.ReadRel
            {
                BaseSchema = outputSchema,
                NamedTable = namedTable
            };

            return new SubstraitQuery<T>(queryProvider,
                new SubstraitSource(new Substrait.Protobuf.Rel { Read = read }, outputSchema));
        }

        public SubstraitQuery<T> NamedTable<T>(string tableName)
        {
            return NamedTable<T>(new List<string> { tableName });
        }
    }
}