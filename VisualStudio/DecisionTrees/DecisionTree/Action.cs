namespace DecisionTree
{
    public class GameAction : IDecisionTreeNode
    {
        private string ActionMessage { get; }

        public GameAction(string actionMessage)
        {
            ActionMessage = actionMessage;
        }

        public IDecisionTreeNode MakeDecision()
        {
            return this;
        }
    }
}
