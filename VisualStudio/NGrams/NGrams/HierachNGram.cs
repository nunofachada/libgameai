using System;
using System.Collections;
using System.Text;

namespace NGrams
{
    // Implementation of an hierarchical N-Gram
    public class HierarchNGram<T> : INGram<T>
    {
        // Threshold: minimum frequency for sequence of actions to be considered
        private int threshold;

        // Array of N-Grams
        private NGram<T>[] predictors;

        // The N in N-Gram
        private int nValue;

        // Constructor
        public HierarchNGram(int windowSize, int threshold)
        {
            // Keep the N value
            nValue = windowSize + 1;
            // Keep the threshold
            this.threshold = threshold;
            // Instantiate the array of N-Grams
            predictors = new NGram<T>[nValue];
            // Instantiate the individual N-Grams
            for (int i = 0; i < nValue; i++)
                predictors[i] = new NGram<T>(i + 1);
        }

        // Register a sequence of actions
        // actions should be of size N
        public void RegisterSequence(T[] actions)
        {
            // Register the sequence of actions in each Ni-Gram
            for (int i = 0; i < nValue; i++)
            {
                if (actions.Length >= i + 1)
                {
                    // Create array of actions for the current Ni-Gram
                    T[] subactions = new T[i + 1];
                    // Copy the actions to the array of actions for the current Ni-Gram
                    Array.Copy(actions, nValue - i - 1, subactions, 0, i + 1);
                    // Register the sequence of actions in the current Ni-Gram
                    predictors[i].RegisterSequence(subactions);
                }
            }

        }

        // Get the most likely action given a sequence of actions
        // actions should be of size N-1
        public T GetMostLikely(T[] actions)
        {
            // Declare variable for best action and set it to its default value
            T bestAction = default(T);

            // Go through the various Ni-Grams
            for (int i = 0; i < nValue; i++)
            {
                // Get current Ni-Gram
                NGram<T> p = predictors[nValue - i - 1];
                // Create array of actions for the current Ni-Gram
                T[] subactions = new T[nValue - i - 1];
                // Copy the actions to the array of actions for the current
                // Ni-Gram
                Array.Copy(actions, i, subactions, 0, nValue - i - 1);
                // Get frequency of action sequence in the current Ni-Gram
                int numActions = p.GetActionsNum(subactions);
                // Is that frequency larger than the threshold?
                if (numActions > threshold)
                {
                    // Then use this action
                    bestAction = p.GetMostLikely(actions);
                    break;
                }
            }
            // Return the best action
            return bestAction;
        }
    }
}