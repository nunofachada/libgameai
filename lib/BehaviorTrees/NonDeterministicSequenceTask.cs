/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using System;
using System.Collections.Generic;

namespace LibGameAI.BehaviorTrees
{
    // Implements a non-deterministic sequence node
    public class NonDeterministicSequenceTask : CompositeTask
    {
        // Reference to a method that returns an int between 0 and the
        // specified value - 1
        private Func<int, int> nextInt;

        // Constructor, requires a reference to the random int method
        public NonDeterministicSequenceTask(
            Func<int, int> nextInt, params ITask[] tasks) : base(tasks)
        {
            this.nextInt = nextInt;
        }
        // Invokes the child tasks in random order and returns as soon as one
        // them returns false
        public override TaskResult Run()
        {
            ITask[] tasks = new ITask[Children.Count];
            Children.CopyTo(tasks, 0);
            tasks.Shuffle(nextInt);
            foreach (ITask child in tasks)
            {
                TaskResult result = child.Run();
                if (result != TaskResult.Success) return result;
            }
            return TaskResult.Success;
        }

    }
}
