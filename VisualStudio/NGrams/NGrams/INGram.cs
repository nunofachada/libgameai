namespace NGrams
{
    public interface INGram<T>
    {
        // Register a sequence of actions
        void RegisterSequence(T[] actions);

        // Get the most likely action given a sequence of actions
        T GetMostLikely(T[] actions);
    }
}