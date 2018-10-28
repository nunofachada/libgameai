using System;
//using UnityEngine;

namespace DecisionTree
{
    public class RandomDecision : Decision
    {
        private int lastFrame, firstFrame;
        private bool lastDecision = false;
        private int timeOut;
        // In Unity we can get rid of the code below and use Unity's Random
        // class instead
        private Random rng;

        public RandomDecision(IDecisionTreeNode trueNode, IDecisionTreeNode falseNode, int timeOut)
            : base(trueNode, falseNode)
        {
            this.timeOut = timeOut;
            this.rng = new Random();
        }

        private int frame()
        {
            // To work correctly in Unity, swap the comments below
            // return Time.frameCount;
            return lastFrame + 1;
        }

        protected override bool Test()
        {

            // Check if our stored decision is too old, or if we've timed out
            if ((frame() > lastFrame + 1) || (frame() > firstFrame + timeOut))
            {

                // Make a new decision and store it
                lastDecision = rng.Next(2) == 0;

                // Set when we made the decision
                firstFrame = frame();
            }

            // Either way we need to update the frame value
            lastFrame = frame();

            // We return the stored value
            return lastDecision;
        }
    }
}
