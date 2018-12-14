/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

namespace BehaviorTree
{
    // Decorator which limits the amount of times the decorated task can be run
    public class LimitDecoratorTask : DecoratorTask
    {
        // The run limit and number of runs so far
        private int runLimit, runSoFar;

        // Constructor, requires the run limit and the decorated task which it
        // passes on to the base class constructor
        public LimitDecoratorTask(ITask decoratedTask, int runLimit)
            : base(decoratedTask)
        {
            this.runLimit = runLimit;
            runSoFar = 0;
        }

        // Run the decorated task if number of runs is not higher than the run
        // limit
        public override TaskResult Run()
        {
            if (runSoFar >= runLimit) return TaskResult.Failure;
            runSoFar++;
            return DecoratedTask.Run();
        }
    }
}
