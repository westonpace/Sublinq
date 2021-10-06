using System.Collections.Generic;
using Substrait.Protobuf;

namespace DataLinq
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
            plan.Relations.Add(GetRoot().Rel);
            return plan;
        }

        public void AddRel(RelBuilder relBuilder)
        {
            relBuilders.Add(relBuilder);
        }
    }
}