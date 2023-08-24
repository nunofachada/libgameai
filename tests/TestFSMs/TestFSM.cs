using Xunit;
using LibGameAI.FSMs;

namespace TestFSMs;

public class TestFSM
{
    [Fact]
    public void Test1()
    {
        State state = new State("TestState", null, null, null);
        StateMachine sm = new StateMachine(state);
    }
}