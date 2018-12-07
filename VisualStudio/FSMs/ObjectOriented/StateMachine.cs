using System;
using System.Collections.Generic;

public class StateMachine
{

    private State currentState;

    public StateMachine(State initialState)
    {
        currentState = initialState;
    }

    public Action Update()
    {
        // Assume no transition is triggered
        Transition triggeredTransition = null;

        // Check through each transition and store the first one that triggers
        foreach (Transition transition in currentState.Transitions)
        {
            if (transition.IsTriggered())
            {
                triggeredTransition = transition;
                break;
            }
        }

        // Check if we have a transition to fire
        if (triggeredTransition != null)
        {

            // Actions to perform
            Action actions = null;

            // Find the target state
            State targetState = triggeredTransition.TargetState;

            // Add the exit action of the old state, the transition action and
            // the entry for the new state
            actions += currentState.ExitAction;
            actions += triggeredTransition.TransAction;
            actions += targetState.EntryAction;

            // Complete the transition and return the action list
            currentState = targetState;
            return actions;

        }

        // Return the action for the current state
        return currentState.StateAction;
    }

}
