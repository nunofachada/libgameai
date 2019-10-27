/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

namespace LibGameAI.DecisionTrees
{
    public abstract class Decision : IDTNode
    {
        private IDTNode trueNode, falseNode;
        protected abstract bool Test();

        public Decision(
          IDTNode trueNode, IDTNode falseNode)
        {
            this.trueNode = trueNode;
            this.falseNode = falseNode;
        }
        public IDTNode MakeDecision()
        {
            IDTNode branch = Test() ? trueNode : falseNode;
            return branch.MakeDecision();
        }
    }
}
