using System;
using System.Collections.Generic;

namespace LibGameAI.HFSMs
{
    public abstract class HFSMBase
    {
        public virtual Action Actions => null;

        public abstract IEnumerable<State> States { get; }
    }
}