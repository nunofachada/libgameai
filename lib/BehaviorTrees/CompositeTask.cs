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
    // Base class for composite tasks
    public abstract class CompositeTask : ITask
    {
        // Task order strategy
        private readonly ITaskOrderStrategy taskOrderStrategy;

        // Child tasks
        private readonly ITask[] tasks;

        // Subclass accessor for child tasks
        protected IEnumerable<ITask> Tasks =>
            taskOrderStrategy.GetTasks(tasks);

        // Constructor, should be called from base classes
        public CompositeTask(
            ITaskOrderStrategy taskOrderStrategy, params ITask[] tasks)
        {
            this.taskOrderStrategy = taskOrderStrategy;
            this.tasks = new ITask[tasks.Length];
            Array.Copy(this.tasks, tasks, tasks.Length);
        }

        // Subclasses should implement this method
        public abstract TaskResult Run();
    }
}
