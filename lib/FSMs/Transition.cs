/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using System;

namespace LibGameAI.FSMs
{
    public class Transition
    {

        public Action TransAction { get; }
        public State TargetState { get; }

        private Func<bool> condition;

        public bool IsTriggered()
        {
            return condition();
        }

        public Transition(
            Func<bool> condition, Action transAction, State targetState)
        {
            this.condition = condition;
            TransAction = transAction;
            TargetState = targetState;
        }
    }
}
