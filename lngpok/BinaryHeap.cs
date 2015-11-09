using System;
using System.Collections;
using System.Collections.Generic;

namespace Common.DataStructures
{
    public class BinaryHeap<TKey> : IEnumerable<TKey> where TKey : IComparable<TKey>
    {
        private readonly IComparer<TKey> _comparer;
        private readonly List<TKey> _items;

        public BinaryHeap(bool minimum, int capacity, IComparer<TKey> comparer)
        {
            _items = new List<TKey>(capacity);

            if (comparer != null)
            {
                _comparer = comparer;
            }
            else if(minimum)
            {
                _comparer = new GreaterComparer<TKey>();
            }
            else
            {
                _comparer = new LesserComparer<TKey>();
            }
        }

        public BinaryHeap(bool minimum, int capacity) : this(minimum, capacity, null)
        {
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public void Add(TKey item)
        {
            _items.Add(item);
            BubbleUp(Count - 1);
        }

        public TKey Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Cannot peek when the heap is empty.");
            }

            return _items[0];
        }

        public TKey Remove()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Cannot remove when the heap is empty.");
            }

            TKey min = _items[0];
            Swap(0, Count - 1);
            _items.RemoveAt(_items.Count - 1);
            BubbleDown(0);

            return min;
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void BubbleUp(int index)
        {
            while (CompareAndSwapWithParentIfNecessary(index))
            {
                index = GetParentIndex(index);
            }
        }

        private void BubbleDown(int index)
        {
            if (!HasChildren(index))
            {
                return;
            }

            int bestChildIndex = GetBestChildIndex(index);
            if (CompareAndSwapWithParentIfNecessary(bestChildIndex))
            {
                BubbleDown(bestChildIndex);
            }
        }

        private bool CompareAndSwapWithParentIfNecessary(int childIndex)
        {
            if (childIndex == 0)
            {
                return false;
            }

            int parentIndex = GetParentIndex(childIndex);
            if (GetBestIndex(parentIndex, childIndex) == childIndex)
            {
                Swap(parentIndex, childIndex);
                return true;
            }
            return false;
        }

        private int GetBestIndex(int index1, int index2)
        {
            return _comparer.Compare(_items[index1], _items[index2]) < 0 ? index1 : index2;
        }

        private int GetBestChildIndex(int parentIndex)
        {
            int leftChildIndex = GetLeftChildIndex(parentIndex);
            int rightChildIndex = GetRightChildIndex(parentIndex);

            if (ItemExists(rightChildIndex))
            {
                return GetBestIndex(leftChildIndex, rightChildIndex);
            }

            return leftChildIndex;
        }

        private bool HasChildren(int index)
        {
            return ItemExists(GetLeftChildIndex(index));
        }

        private bool ItemExists(int index)
        {
            return index < Count;
        }

        private void Swap(int index1, int index2)
        {
            TKey temp = _items[index1];
            _items[index1] = _items[index2];
            _items[index2] = temp;
        }

        private int GetParentIndex(int childIndex)
        {
            return (childIndex - 1)/2;
        }

        private int GetLeftChildIndex(int parentIndex)
        {
            return 2*parentIndex + 1;
        }

        private int GetRightChildIndex(int parentIndex)
        {
            return 2*parentIndex + 2;
        }

        private class GreaterComparer<T> : IComparer<T> where T : IComparable<T>
        {
            public int Compare(T x, T y)
            {
                return x.CompareTo(y);
            }
        }

        private class LesserComparer<T> : IComparer<T> where T : IComparable<T>
        {
            public int Compare(T x, T y)
            {
                return x.CompareTo(y) * -1;
            }
        }
    }
}