using System;

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
