namespace NGrams
{
    public interface INGram<T>
    {
        // The N in N-Gram (window size + 1)
        int NValue { get; }

        // Register a sequence of actions
        void RegisterSequence(T[] actions);

        // Get the most likely action given a sequence of actions
        T GetMostLikely(T[] actions);
    }
}