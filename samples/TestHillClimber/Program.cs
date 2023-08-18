/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using System;
using LibGameAI.Optimizers;

namespace LibGameAI.Samples.TestHillClimber
{
    class Program
    {
        struct Solution : ISolution {
            public float X { get; }
            public float Y { get; }
            public Solution (float x, float y)
            {
                X = x;
                Y = y;
            }
            public override string ToString() => $"({X}, {Y})";
        }

        private static Random rnd;

        static void Main(string[] args)
        {
            rnd = new Random();

            HillClimber hc = new HillClimber(
                FindNeighbor,
                s => SchafferF6(((Solution)s).X, ((Solution)s).Y),
                (a, b) => a < b,
                (a, b) => a < b);

            Result r = hc.Optimize(
                10000,
                0.00001f,
                () => new Solution(
                    (float)rnd.NextDouble() * 15,
                    (float)rnd.NextDouble() * 30),
                1000);

            Console.WriteLine(r);
        }

        private static float SchafferF6(float x, float y)
        {
            float temp1, temp2;

            temp1 = (float)Math.Sin(Math.Sqrt(x * x + y * y));
            temp2 = (float)(1 + 0.001f * (x * x + y * y));

            return 0.5f + (temp1 * temp1 - 0.5f) / (temp2 * temp2);
        }

        private static ISolution FindNeighbor(ISolution solution)
        {
            Solution sol = (Solution)solution;
            float angle = (float)(rnd.NextDouble() * Math.PI * 2);
            float magnitude = (float)(rnd.NextDouble() * 0.1);
            float dx = (float)Math.Cos(angle);
            float dy = (float)Math.Sin(angle);
            float neighX = Math.Clamp(sol.X + dx, -100, 100);
            float neighY = Math.Clamp(sol.Y + dy, -100, 100);
            return new Solution(neighX, neighY);
        }
    }

}
