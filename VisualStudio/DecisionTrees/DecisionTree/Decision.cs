public abstract class Decision : IDTNode
{
    private IDTNode trueNode, falseNode;
    protected abstract bool Test();

    public Decision(
      IDTNode trueNode, IDTNode falseNode)
    {
        this.trueNode = trueNode;
        this.falseNode = falseNode;
    }
    public IDTNode MakeDecision()
    {
        IDTNode branch = Test() ? trueNode : falseNode;
        return branch.MakeDecision();
    }
}