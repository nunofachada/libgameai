using System;
using System.Collections.Generic;

namespace LibGameAI.HFSMs
{
    public class HierachicalStateMachine : HFSMBase
    {
        // List of states at this level of the hierarchy
        private IEnumerable<State> states;

        // Initial state when the FSM has no current state
        private State initialState;

        // Current state of the FSM
        private State currentState;

        /// <summary>
        /// Gets the current state stack.
        /// </summary>
        public override IEnumerable<State> States =>
            currentState?.States ?? new State[] { };

        /// <summary>
        /// Recursively updates the machine.
        /// </summary>
        /// <returns></returns>
        public async Action Update()
        {
            // The triggered transition, if any
            Transition triggeredTransition = null;

            // If we're in no strate, use the initial state
            if (currentState is null)
            {
                currentState = initialState;
                return currentState.EntryActions;
            }

            // Try to find a transition in the current state
            foreach (Transition t in currentState.Transitions)
            {
                if (t.IsTriggered())
                {
                    triggeredTransition = t;
                    break;
                }
            }



            return null;
        }

    }
}