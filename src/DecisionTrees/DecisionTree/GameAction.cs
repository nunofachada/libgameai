using System;

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