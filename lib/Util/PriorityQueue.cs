/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LibGameAI.Util
{
    /// <summary>
    /// A basic priority queue implementation internally using a priority heap.
    /// </summary>
    /// <typeparam name="T">Type of item in the collection.</typeparam>
    /// <remarks>
    /// When .NET 6.0 arrives we can use native implementation of a priority
    /// queue instead.
    /// </remarks>
    public class PriorityQueue<T> : ICollection<T>, ICollection
    {
        private const int INIT_CAPACITY = 20;

        private int count;

        private T[] heap;

        private readonly Comparison<T> compare;

        // Constructors

        public PriorityQueue(Comparison<T> compare = null)
            : this(INIT_CAPACITY, compare)
        { }

        public PriorityQueue(int initCapacity, Comparison<T> compare = null)
            : this(true, compare)
        {
            heap = new T[initCapacity];
            count = initCapacity;
        }

        public PriorityQueue(IEnumerable<T> items, Comparison<T> compare = null)
            : this(true, compare)
        {
            heap = items.ToArray();
            count = heap.Length;
            Heapify();
        }

        private PriorityQueue(bool self, Comparison<T> compare)
        {
            System.Diagnostics.Debug.Assert(self);

            if (compare != null)
            {
                this.compare = compare;
            }
            else if ((compare is null) &&
                typeof(T).GetInterfaces().Any(
                    i => i.IsGenericType
                        && i.GetGenericTypeDefinition() == typeof(IComparable<>)))
            {
                this.compare = (T item1, T item2) =>
                    Equals(item1, item2)
                        ? 0
                        : (item1 as IComparable<T>)?.CompareTo(item2) ?? -1;
            }
            else
            {
                throw new ArgumentException(
                    "Priority heaps require comparable items.");
            }
        }

        // System.Collections.Generic.Queue<T> compatibility layer
        // Not all functionality is implemented

        public int Count => count;

        public void Clear()
        {
            Array.Clear(heap, 0, count);
            count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        /// <summary>
        /// Does not sort. Use ToArray() for getting an array sorted by
        /// priority. This is faster.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo (T[] array, int arrayIndex)
        {
            Array.Copy(heap, 0, array, arrayIndex, count);
        }

        public T Dequeue()
        {
            if (count == 0)
                throw new InvalidOperationException("Queue is empty");

            int s = --count;

            T item = heap[0];
            T x = heap[s];
            heap[s] = default;
            if (s != 0)
                MoveDown(0, x);
            return item;
        }

        public void Enqueue(T item)
        {
            int i = count;
            if (i >= heap.Length)
                Grow();
            count++;
            if (i == 0)
                heap[0] = item;
            else
                MoveUp(i, item);
        }

        public T Peek()
        {
            if (count == 0)
                throw new InvalidOperationException("Queue is empty");

            return heap[0];
        }

        /// <summary>
        /// Returns new array sorted by priority, but slower. Use CopyTo for
        /// faster but unsorted version.
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            T[] queueCopy = heap.Clone() as T[];
            Array.Sort(queueCopy, compare);
            return queueCopy;
        }

        // Private helper methods

        private T RemoveAt(int i)
        {
            System.Diagnostics.Debug.Assert(i >= 0 && i < count);
            int s = --count;

            // Remove last element
            if (s == i)
            {
                heap[i] = default;
            }
            else
            {
                T moved = heap[s];
                heap[s] = default;
                MoveDown(i, moved);
                if (Equals(heap[i], moved))
                {
                    MoveUp(i, moved);
                    if (!Equals(heap[i], moved))
                    {
                        return moved;
                    }
                }
            }
            return default;
        }

        private void MoveUp(int k, T item)
        {
            while (k > 0)
            {
                int parent = (int)((uint)(k - 1) >> 1);
                T e = heap[parent];
                if (compare(item, e) >= 0)
                    break;
                heap[k] = e;
                k = parent;
            }
            heap[k] = item;
        }

        private void MoveDown(int k, T item)
        {
            int half = (int)((uint)count >> 1);
            while (k < half)
            {
                int child = (k << 1) + 1;
                T c = heap[child];
                int right = child + 1;
                if (right < count && compare(c, heap[right]) > 0)
                {
                    c = heap[child = right];
                }
                if (compare(item, c) <= 0)
                {
                    break;
                }
                heap[k] = c;
                k = child;
            }
            heap[k] = item;
        }

        private void Heapify()
        {
            for (int i = (int)((uint)count >> 1) - 1; i >= 0; i--)
            {
                MoveDown(i, heap[i]);
            }
        }
        private void Grow()
        {
            int oldCapacity = heap.Length;

            int newCapacity = checked( // Checked allows to detect overflows
                oldCapacity
                + ((oldCapacity < 64) ? (oldCapacity + 2) : (oldCapacity >> 1)));

            T[] newHeap = new T[newCapacity];
            Array.Copy(heap, newHeap, heap.Length);
            heap = newHeap;
        }

        private int IndexOf(T item)
        {
            for (int i = 0; i < count; i++)
            {
                if (Equals(item, heap[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        // Implementation of System.Collections.Generic.ICollection<T> interface

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            Enqueue(item);
        }

        public bool Remove(T item)
        {
            int i = IndexOf(item);
            if (i == -1)
            {
                return false;
            }
            else
            {
                RemoveAt(i);
                return true;
            }
        }

        // Implementation of System.Collections.ICollection interface

        void ICollection.CopyTo (Array array, int index)
        {
            CopyTo(array as T[], index);
        }

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => this;

        // Implementation of System.Collections.Generic.IEnumerable<T> interface

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
                yield return heap[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}