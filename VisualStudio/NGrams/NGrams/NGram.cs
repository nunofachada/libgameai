using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

namespace NGrams
{
    public class NGram<T> : INGram<T>
    {
        // The N in N-Gram (window size + 1)
        private int nValue;
        // Dictionary which relates a sequence to a set of actions and
        // probabilities
        private Dictionary<string, ActionFrequency<T>> data;

        // Constructor, accepts the window size
        public NGram(int windowSize)
        {
            nValue = windowSize + 1;
            data = new Dictionary<string, ActionFrequency<T>>();
        }

        // Converts a sequence of actions to a string
        public static string ArrayToStringKey(T[] actions)
        {
            StringBuilder builder = new StringBuilder();
            foreach (T a in actions)
            {
                builder.Append(a.ToString());
            }
            return builder.ToString();
        }

        // Register a sequence of actions
        public void RegisterSequence(T[] actions)
        {

            // Can only register sequence if its size N
            if (actions.Length == nValue)
            {

                // Previous actions
                T[] prevActions = new T[nValue - 1];

                // Previous actions in key form
                string prevActionsKey;

                // Action performed
                T actionPerformed;

                // Split the sequence into a key and value
                Array.ConstrainedCopy(actions, 0, prevActions, 0, nValue - 1);
                actionPerformed = actions[nValue - 1];
                prevActionsKey = ArrayToStringKey(prevActions);

                // Check if our data contains the key (i.e. sequence of actions)
                // If not, create a new record for this sequence of actions
                if (!data.ContainsKey(prevActionsKey))
                    data[prevActionsKey] = new ActionFrequency<T>();

                // Get the data record for this sequence of actions
                ActionFrequency<T> af = data[prevActionsKey];

                // Increment the number of times this action was performed
                // after the given sequence of actions
                af.IncrementAction(actionPerformed);
            }
        }

        // Get the most likely action given a sequence of actions
        // actions should be of size N-1
        public T GetMostLikely(T[] actions)
        {
            // The most likely action, initially set to its default value
            T bestAction = default(T);

            // The actions array must have the window size
            if (actions.Length == nValue - 1)
            {
                // First, convert sequence of actions to string (i.e. the key)
                string key = ArrayToStringKey(actions);

                // The data record for this sequence of actions
                ActionFrequency<T> af;

                // Try to get the best action for the given sequence of actions
                if (data.TryGetValue(key, out af))
                {
                    bestAction = af.BestAction;
                }
            }

            // Return the most likely/most frequent action
            return bestAction;
        }

        // Return the number of times this sequence has been seen
        // actions should be of size N-1
        public int GetActionsNum(T[] actions)
        {
            // Number of times this sequence of actions has been seen
            int actionCount = 0;

            if (actions.Length == nValue - 1)
            {
                // First, convert sequence of actions to string (i.e. the key)
                string key = ArrayToStringKey(actions);

                // If there is data for this sequence, get the number of times
                // this seqence has been seen
                if (data.ContainsKey(key)) actionCount = data[key].Total;
            }
            return actionCount;
        }
    }
}