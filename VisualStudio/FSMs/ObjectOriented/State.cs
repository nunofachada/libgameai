using System;
using System.Collections.Generic;
public class State
{
    public Action EntryAction { get; }
    public Action StateAction { get; }
    public Action ExitAction { get; }

    public IEnumerable<Transition> Transitions { get; }

    public State(
        Action entryAction, Action stateAction, Action exitAction,
        IEnumerable<Transition> transitions)
    {
        EntryAction = entryAction;
        StateAction = stateAction;
        ExitAction = exitAction;
        Transitions = transitions;
    }
}
