/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using System;

namespace LibGameAI.BehaviorTrees
{
    // Decorator which guards a resource
    public class SemaphoreDecoratorTask : DecoratorTask
    {
        // Semaphore functions
        private Func<bool> acquire;
        private Action release;

        // Constructor, requires the decorated task which it passes on to the
        // base class constructor
        public SemaphoreDecoratorTask(
            ITask decoratedTask, Func<bool> acquire, Action release)
            : base(decoratedTask)
        {
            this.acquire = acquire;
            this.release = release;
        }

        // Run the decorated task until it fails
        public override TaskResult Run()
        {
            TaskResult result = TaskResult.Failure;
            if (acquire.Invoke())
            {
                result = DecoratedTask.Run();
                release.Invoke();
            }
            return result;
        }
    }
}
