using System;
using System.Collections.Generic;

namespace LibGameAI.FSMs
{
    public class State
    {
        public string Name { get; }
        public Action EntryAction { get; }
        public Action StateAction { get; }
        public Action ExitAction { get; }

        public IEnumerable<Transition> Transitions => transitions;

        private IList<Transition> transitions;

        public State(
            string name, Action entryAction, Action stateAction, Action exitAction)
        {
            Name = name;
            EntryAction = entryAction;
            StateAction = stateAction;
            ExitAction = exitAction;
            transitions = new List<Transition>();
        }

        public void AddTransition(Transition transition)
        {
            transitions.Add(transition);
        }
    }
}
