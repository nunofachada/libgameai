/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using System;

namespace LibGameAI.DecisionTrees
{
    public class RandomDecision : Decision
    {
        private int lastFrame, firstFrame;
        private bool lastDecision = false;
        private int timeOut;

        private Func<float> nextRandomValue;
        private Func<int> frame;

        public RandomDecision(IDTNode trueNode, IDTNode falseNode,
            int timeOut, Func<float> nextRandomValue, Func<int> frame)
            : base(trueNode, falseNode)
        {
            this.timeOut = timeOut;
            this.nextRandomValue = nextRandomValue;
            this.frame = frame;
        }

        protected override bool Test()
        {

            // Check if our stored decision is too old, or if we've timed out
            if ((frame() > lastFrame + 1) || (frame() > firstFrame + timeOut))
            {

                // Make a new decision and store it
                lastDecision = nextRandomValue() > 0.5f;

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
