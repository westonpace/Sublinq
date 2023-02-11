using Substrait.Protobuf;

namespace SubLinq
{
    public class RelBuilder
    {
        public PlanRel PlanRel;
        protected PlanBuilder PlanBuilder { get; }

        public RelBuilder(PlanBuilder planBuilder, PlanRel rel)
        {
            PlanBuilder = planBuilder;
            PlanRel = rel;
        }

        protected Plan ToPlan()
        {
            return PlanBuilder.ToPlan();
        }
    }
}