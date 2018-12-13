/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

namespace BehaviorTree
{
    // Implements a selector node
    public class SelectorTask : CompositeTask
    {

        // Constructor, simply invokes the base constructor
        public SelectorTask(params ITask[] tasks) : base(tasks) {}

        // Invokes the child tasks and returns as soon as one them returns true
        public override bool Run() {
            foreach (ITask child in Children) {
                if (child.Run()) return true;
            }
            return false;
        }
    }
}