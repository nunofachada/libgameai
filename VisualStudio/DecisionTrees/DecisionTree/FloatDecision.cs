namespace DecisionTree
{
    public class FloatDecision : Decision
    {
        private float minValue, maxValue;

        public FloatDecision(IDecisionTreeNode trueNode, IDecisionTreeNode falseNode, float minValue, float maxValue)
            : base(trueNode, falseNode)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;

        }

        protected override bool Test()
        {
            return (maxValue >= (float)testObject);
        }
    }
}
