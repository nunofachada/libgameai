using System;
using System.Linq;
using System.Collections.Generic;

namespace NaiveBayes
{
        public class NaiveBayesClassifier
    {

        // Number of examples with given label (prior)
        private IDictionary<string, int> labelsCount;

        // Number of times each attribute has a given value for each label
        private IDictionary<string, IDictionary<Attrib, AttribCount>>
            attribValueCounts;

        // Constructor
        public NaiveBayesClassifier(
            IEnumerable<string> labels, IEnumerable<Attrib> attributes)
        {
            labelsCount = new Dictionary<string, int>();
            attribValueCounts =
                new Dictionary<string, IDictionary<Attrib, AttribCount>>();
            foreach (string label in labels)
            {
                IDictionary<Attrib, AttribCount> attribCountDict =
                    new Dictionary<Attrib, AttribCount>();

                foreach (Attrib attrib in attributes)
                {
                    attribCountDict[attrib] = new AttribCount(attrib);
                }

                attribValueCounts[label] = attribCountDict;
                labelsCount[label] = 0;

            }
        }

        public void Update(
            string label, IDictionary<Attrib, string> attribValues)
        {

            labelsCount[label]++;

            foreach (Attrib attribute in attribValues.Keys)
            {
                string value = attribValues[attribute];
                attribValueCounts[label][attribute].AddCount(value, 1);
            }
        }

        public string Predict(IDictionary<Attrib, string> attribValues)
        {
            // Highest probability so far
            double bestP = 0;
            // Label with highest probability
            string bestLabel = "";
            // Total counts
            int totalCounts = labelsCount.Values.Sum();

            // Search for label with highest probability
            foreach (string label in labelsCount.Keys)
            {
                int labelCount = labelsCount[label];
                double p = NaiveProbabilities(
                    label,
                    attribValues, attribValueCounts[label],
                    labelCount, totalCounts);
                if (p > bestP)
                {
                    bestP = p;
                    bestLabel = label;
                }
            }

            // Return label with highest probabiliy
            return bestLabel;
        }

        public double NaiveProbabilities(
            string label,
            IDictionary<Attrib, string> attribValues,
            IDictionary<Attrib, AttribCount> counts,
            double labelCount,
            double totalCounts)
        {
            // Compute the prior
            double prior = labelCount / totalCounts;

            // Naive assumption of conditional independence
            double p = 1.0;

            foreach (Attrib attribute in attribValues.Keys)
            {
                p /= labelCount;
                if (attribValues[attribute] == label)
                    p *= counts[attribute].GetCount(label);
                else
                    p *= labelCount - counts[attribute].GetCount(label);
            }

            return prior * p;
        }
    }
}
