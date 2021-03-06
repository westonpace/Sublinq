using Substrait.Protobuf;

namespace SubLinq
{
    public class RelBuilder
    {
        public Rel Rel;
        protected PlanBuilder PlanBuilder { get; }

        public RelBuilder(PlanBuilder planBuilder, Rel rel)
        {
            PlanBuilder = planBuilder;
            Rel = rel;
        }
        
        protected Plan ToPlan()
        {
            return PlanBuilder.ToPlan();
        }
    }
}