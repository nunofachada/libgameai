using System;
using LibGameAI.Util;

namespace TestUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            PriorityQueue<int> pq = new PriorityQueue<int>();
            pq.Enqueue(3);
            pq.Enqueue(2);
            pq.Enqueue(6);
            pq.Enqueue(-1);
            pq.Enqueue(-1);
            pq.Enqueue(100);
            pq.Enqueue(123);
            pq.Enqueue(int.MaxValue);


            Console.WriteLine(pq.Dequeue());
            Console.WriteLine(pq.Dequeue());
            Console.WriteLine(pq.Dequeue());
            Console.WriteLine(pq.Dequeue());
            Console.WriteLine(pq.Dequeue());
            Console.WriteLine(pq.Dequeue());
            Console.WriteLine(pq.Dequeue());
        }
    }
}
