using System.Collections.Generic;

namespace NaiveBayes
{
    public class Attrib
    {
        public string Name { get; }
        public ICollection<string> Values => values;

        private List<string> values;

        public Attrib(string name, IEnumerable<string> values)
        {
            Name = name;
            this.values = new List<string>(values);
        }
    }
}