using System.Collections.Generic;

namespace NaiveBayes
{
    public class AttribCount
    {
        private IDictionary<string, int> counts;

        public AttribCount(Attrib attribute)
        {
            counts = new Dictionary<string, int>();
            foreach (string value in attribute.Values)
            {
                counts[value] = 0;
            }
        }

        public int GetCount(string value)
        {
            return counts[value];
        }

        public void AddCount(string value, int count)
        {
            counts[value] += count;
        }

    }

}