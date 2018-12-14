/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

namespace BehaviorTree
{
    // Decorator which repeats decorated task until it fails
    public class UntilFailDecoratorTask : DecoratorTask
    {


        // Constructor, requires the decorated task which it passes on to the
        // base class constructor
        public UntilFailDecoratorTask(ITask decoratedTask)
            : base(decoratedTask)
        {
        }

        // Run the decorated task until it fails
        public override TaskResult Run()
        {
            while (DecoratedTask.Run() == TaskResult.Success);
            return TaskResult.Success;
        }

    }
}
