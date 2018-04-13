namespace DecisionTree
{
    public class FloatDecision : Decision
    {
        private float minValue, maxValue;

        protected override IDecisionTreeNode GetBranch()
        {
            if (maxValue >= (float)testObject)
                return TrueNode;
            else
                return FalseNode;
        }
    }
}
