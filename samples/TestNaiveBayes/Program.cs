/* Copyright (c) 2018-2023 Nuno Fachada and contributors
 * Distributed under the MIT License (See accompanying file LICENSE or copy
 * at http://opensource.org/licenses/MIT) */

using System;
using System.Collections.Generic;
using LibGameAI.NaiveBayes;

namespace LibGameAI.Samples.TestNaiveBayes
{
    // Test the Naive Bayes Classifier
    class Program
    {
        static void Main(string[] args)
        {
            // Create two attributes and specify their possible values
            Attrib distance =
                new Attrib("distance", new string[] { "near", "far" });
            Attrib speed =
                new Attrib("speed", new string[] { "slow", "fast" });

            // Create a naive Bayes classifier with a set of labels and a
            // set of attributes
            NaiveBayesClassifier nbc = new NaiveBayesClassifier(
                new string[] { "Y", "N" },
                new Attrib[] { distance, speed });

            // Pass a few observations to the naive Bayes classifier

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

            // Make a prediction given a set of attribute-value pairs
            string prediction = nbc.Predict(new Dictionary<Attrib, string>()
            {
                { distance, "far" },
                { speed, "slow" }
            });

            // Show prediction
            Console.WriteLine($"Brake? {prediction}");
        }
    }
}
