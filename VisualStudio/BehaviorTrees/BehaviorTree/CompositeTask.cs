/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using System.Collections.Generic;

namespace BehaviorTree
{
    // Base class for composite tasks
    public abstract class CompositeTask : ITask
    {
        // We keep the list of tasks here
        private readonly List<ITask> children;
        // Subclasses can only access a readonly version of this list
        protected IList<ITask> Children => children.AsReadOnly();

        // Constructor, should be called from base classes
        public CompositeTask(params ITask[] tasks)
        {
            children = new List<ITask>(tasks);
        }

        // Subclasses should implement this method
        public abstract bool Run();
    }
}
