using System;
using System.Collections.Generic;
using Xunit;
using LibGameAI.Util;

namespace Tests.Util
{
    public class PriorityQueueTests
    {
        public static IEnumerable<object[]> NumData =>
            new List<object[]>()
            {
                new object[] { new int[] { 3, 0, -10, 100 } },
                new object[] { new float[] { -13, 0.0f, -1.5f, 11, -13 } },
                new object[] { new byte[] {4, 4, 4, 0, 0, 10 } }
            };

        [Theory]
        [MemberData(nameof(NumData))]
        public void Test_Dequeue_Numeric_Collection<T>(T[] stuff)
            where T : IComparable<T>
        {
            PriorityQueue<T> pq = new PriorityQueue<T>(stuff);

            // T smaller = stuff[0];
            // for (int i = 1; i < stuff.Length; i++)
            // {
            //     if (stuff[i].CompareTo(smaller) < 0)
            //         smaller = stuff[i];
            // }

            // T smallerFromPQ = pq.Dequeue();

            //Assert.Equal(smaller, smallerFromPQ);
        }
    }
}
