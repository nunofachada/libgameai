/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

namespace BehaviorTree
{
    // Base class for decorators
    public abstract class DecoratorTask : ITask
    {
        // The decorated task
        protected ITask DecoratedTask { get; }

        // Constructor, accepts a decorated task, should by invoked by
        // subclasses
        public DecoratorTask(ITask decoratedTask)
        {
            DecoratedTask = decoratedTask;
        }

        // Subclasses should implement this method
        public abstract bool Run();
    }
}
