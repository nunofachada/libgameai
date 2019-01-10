/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */
using System;
using System.Collections.Generic;
using NaiveBayes;

namespace Test
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
