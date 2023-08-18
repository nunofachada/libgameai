/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

namespace LibGameAI.BehaviorTrees
{
    public static class TaskResultExtensions
    {
        public static TaskResult Negate(this TaskResult result)
        {
            if (result == TaskResult.Failure) return TaskResult.Success;
            if (result == TaskResult.Success) return TaskResult.Failure;
            return result;
        }
    }
}
