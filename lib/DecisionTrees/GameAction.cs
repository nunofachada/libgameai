/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using System;

namespace LibGameAI.DecisionTrees
{
    public class GameAction : IGameAction, IDTNode
    {
        private Action gameAction;

        public GameAction(Action gameAction)
        {
            this.gameAction = gameAction;
        }
        public void DoGameAction()
        {
            gameAction();
        }

        public IDTNode MakeDecision()
        {
            return this;
        }
    }
}
