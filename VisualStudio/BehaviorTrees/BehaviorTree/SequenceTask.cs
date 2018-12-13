/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

namespace BehaviorTree
{
    // Implements a sequence node
    public class SequenceTask : CompositeTask
    {

        // Constructor, simply invokes the base constructor
        public SequenceTask(params ITask[] tasks) : base(tasks) { }

        // Invokes the child tasks and returns as soon as one them returns false
        public override bool Run()
        {
            foreach (ITask child in Children)
            {
                if (!child.Run()) return false;
            }
            return true;
        }
    }
}