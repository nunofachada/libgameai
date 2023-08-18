using System;

namespace LibGameAI.HFSMs
{
    public class UpdateResult
    {
        public Action Actions { get; }
        public Transition Transition { get; }
        public int Level { get; }

        public UpdateResult(Action actions, Transition transition, int level)
        {
            Actions = actions;
            Transition = transition;
            Level = level;
        }
    }
}