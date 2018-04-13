namespace DecisionTree
{
    public abstract class Decision : IDecisionTreeNode
    {
        protected IDecisionTreeNode TrueNode, FalseNode;
        protected object testObject;

        protected abstract IDecisionTreeNode GetBranch();

        public IDecisionTreeNode MakeDecision()
        {
            IDecisionTreeNode branch = GetBranch();
            return branch.MakeDecision();
        }
    }
}
