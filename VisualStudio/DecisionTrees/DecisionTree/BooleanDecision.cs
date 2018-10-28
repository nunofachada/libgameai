namespace DecisionTree
{
    class BooleanDecision : Decision
    {
        private bool decision;

        public BooleanDecision(IDecisionTreeNode trueNode, IDecisionTreeNode falseNode, bool decision)
            : base(trueNode, falseNode)
        {
            this.decision = decision;

        }

        protected override IDecisionTreeNode GetBranch()
        {
            if (decision)
                return trueNode;
            else
                return falseNode;
        }
    }
}
