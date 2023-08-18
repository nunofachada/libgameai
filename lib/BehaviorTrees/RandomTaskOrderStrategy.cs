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
    /// <summary>
    /// Tasks are returned at random and indefinitely.
    /// </summary>
    public class RandomTaskOrderStrategy : ITaskOrderStrategy
    {
        // Reference to a method that returns an int between 0 and the
        // specified value - 1
        protected readonly Func<int, int> rng;

        public RandomTaskOrderStrategy(Func<int, int> rng)
        {
            this.rng = rng;
        }

        public virtual IEnumerable<ITask> GetTasks(IList<ITask> tasks)
        {
            yield return tasks[rng(tasks.Count)];
        }
    }
}
