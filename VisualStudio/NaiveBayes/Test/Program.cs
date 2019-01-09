using System;
using System.Collections.Generic;
using NaiveBayes;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Attrib distance =
                new Attrib("distance", new string[] { "near", "far" });
            Attrib speed =
                new Attrib("speed", new string[] { "slow", "fast" });

            NaiveBayesClassifier nbc = new NaiveBayesClassifier(
                new string[] { "Y", "N" },
                new Attrib[] { distance, speed });

            nbc.Update("Y", new Dictionary<Attrib, string>()
            {
                { distance, "near"},
                { speed, "slow" }
            });

            nbc.Update("Y", new Dictionary<Attrib, string>()
            {
                { distance, "near"},
                { speed, "fast" }
            });

            nbc.Update("N", new Dictionary<Attrib, string>()
            {
                { distance, "far"},
                { speed, "fast" }
            });

            nbc.Update("Y", new Dictionary<Attrib, string>()
            {
                { distance, "far"},
                { speed, "fast" }
            });

            nbc.Update("N", new Dictionary<Attrib, string>()
            {
                { distance, "near"},
                { speed, "slow" }
            });

            nbc.Update("Y", new Dictionary<Attrib, string>()
            {
                { distance, "far"},
                { speed, "slow" }
            });

            nbc.Update("Y", new Dictionary<Attrib, string>()
            {
                { distance, "near"},
                { speed, "fast" }
            });

            string prediction = nbc.Predict(new Dictionary<Attrib, string>()
            {
                { distance, "far" },
                { speed, "slow" }
            });

            Console.WriteLine($"Brake? {prediction}");
        }
    }
}
