using System.Collections;
using System.Collections.Generic;

namespace NGrams
{
    // Data type for holding the actions and their frequencies
    public class ActionFrequency<T>
    {
        // This dictionary relates actions and their frequencies
        private Dictionary<T, int> actionFrequencies;

        // Number of times the sequence associated to this action has been
        // executed
        public int Total { get; private set; }

        // More likely action
        public T BestAction
        {
            get
            {
                // The highest frequency of an action
                int highestFreq = 0;
                // The best action
                T bestAction = default(T);

                // Cycle through action-frequency pairs
                foreach (KeyValuePair<T, int> kvp in actionFrequencies)
                {
                    // If current action is the most frequent so far, keep it
                    if (kvp.Value > highestFreq)
                    {
                        bestAction = kvp.Key;
                        highestFreq = kvp.Value;
                    }
                }

                // Return the best action
                return bestAction;
            }
        }

        // Create new action frequency data record
        public ActionFrequency()
        {
            actionFrequencies = new Dictionary<T, int>();
        }

        public void IncrementAction(T action)
        {
            if (!actionFrequencies.ContainsKey(action))
                actionFrequencies[action] = 0;
            actionFrequencies[action]++;
            Total++;
        }

    }
}