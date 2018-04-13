namespace DecisionTree
{
    public class Action : IDecisionTreeNode
    {
        public IDecisionTreeNode MakeDecision()
        {
            return this;
        }
    }
}
