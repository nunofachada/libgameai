/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using System;

namespace BehaviorTree
{
    // Represents the leaf tasks of the behavior tree, namely conditions and
    // actions
    public class LeafTask : ITask
    {
        // The task to perform

        private readonly Func<TaskResult> task;

        // Constructor, requires the task to perform
        public LeafTask(Func<TaskResult> task)
        {
            this.task = task;
        }

        // Execute the task
        public TaskResult Run()
        {
            return task();
        }
    }
}
