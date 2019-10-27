using System;

namespace LibGameAI.DecisionTrees
{
    public class BooleanDecision : Decision
    {
        private Func<bool> getValueToTest;

        public BooleanDecision(
            IDTNode trueNode, IDTNode falseNode, Func<bool> getValueToTest)
            : base(trueNode, falseNode)
        {
            this.getValueToTest = getValueToTest;
        }

        protected override bool Test()
        {
            return getValueToTest();
        }
    }
}
