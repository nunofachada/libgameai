namespace DecisionTree
{
    public abstract class Decision : IDecisionTreeNode
    {

        private IDecisionTreeNode trueNode, falseNode;

        protected abstract bool Test();

        public Decision(IDecisionTreeNode trueNode, IDecisionTreeNode falseNode)
        {
            this.trueNode = trueNode;
            this.falseNode = falseNode;
        }

        public IDecisionTreeNode MakeDecision()
        {
            IDecisionTreeNode branch = Test() ? trueNode : falseNode;
            return branch.MakeDecision();
        }
    }
}
