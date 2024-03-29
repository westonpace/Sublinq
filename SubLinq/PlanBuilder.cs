using System.Collections.Generic;
using Substrait.Protobuf;

namespace SubLinq
{
    public class PlanBuilder
    {
        private List<RelBuilder> relBuilders = new List<RelBuilder>();

        private RelBuilder GetRoot()
        {
            return null!;
        }

        public Plan ToPlan()
        {
            Plan plan = new Plan();
            plan.Relations.Add(GetRoot().PlanRel);
            return plan;
        }

        public void AddRel(RelBuilder relBuilder)
        {
            relBuilders.Add(relBuilder);
        }
    }
}