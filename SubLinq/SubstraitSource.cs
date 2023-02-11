using Substrait.Protobuf;

namespace SubLinq
{
    public class SubstraitSource
    {
        public Rel Rel { get; init; }
        public NamedStruct OutputSchema { get; init; }

        public SubstraitSource(Rel rel, NamedStruct outputSchema)
        {
            Rel = rel;
            OutputSchema = outputSchema;
        }
    }

}
