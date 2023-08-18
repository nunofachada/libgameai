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
    /// Tasks are returned in random, non-deterministic order.
    /// </summary>
    public class NonDeterministicTaskOrderStrategy : RandomTaskOrderStrategy
    {
        public NonDeterministicTaskOrderStrategy(Func<int, int> rng)
            : base(rng) { }

        public override IEnumerable<ITask> GetTasks(IList<ITask> tasks)
        {
            tasks.Shuffle(rng);
            return tasks;
        }
    }
}
