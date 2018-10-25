using System;
using System.Collections.Generic;

namespace DataStructures.BinaryHeap
{
    public class MinHeap
    {
        private List<int> _heap;
        private int _pos;

        public MinHeap(int[] values)
        {
            Heapify(values);
        }

        private void Heapify(int[] ints)
        {
            _heap = new List<int>(ints);
            _pos = _heap.Count;
            int mid = ints.Length / 2;
            for (int i = mid; i >= 0; i--)
            {
                PercolateDown(i);
            }
        }

        private void PercolateDown(int index)
        {
            int currentIndex = index;
            int minIndex = currentIndex;
            while (HasLeftChild(currentIndex) || HasRightChild(currentIndex))
            {
                if (HasLeftChild(currentIndex))
                {
                    minIndex = LeftChild(currentIndex);
                }

                if (HasRightChild(currentIndex) && _heap[RightChild(currentIndex)] < _heap[currentIndex] &&
                    _heap[RightChild(currentIndex)] < _heap[minIndex])
                {
                    minIndex = RightChild(currentIndex);
                }

                if (_heap[minIndex] < _heap[currentIndex])
                {
                    Swap(minIndex, currentIndex);
                }

                currentIndex = minIndex;
            }
        }

        private void PercolateUp(int index)
        {
            int parentIndex = Parent(index);
            int currentIndex = index;
            while (currentIndex > 0 && _heap[parentIndex] > _heap[currentIndex])
            {
                Swap(parentIndex, currentIndex);
                currentIndex = parentIndex;
                parentIndex = Parent(currentIndex);
            }
        }

        public void Insert(int i)
        {
            _heap.Add(i);
            PercolateUp(_pos);
            _pos++;
        }

        public int Peek()
        {
            return _heap[0];
        }

        public int Pop()
        {
            int x = _heap[0];
            _pos--;
            _heap[0] = _heap[_pos];
            PercolateDown(0);
            _heap.RemoveAt(_heap.Count - 1);
            return x;
        }

        private void Swap(int index1, int index2)
        {
            int temp = _heap[index1];
            _heap[index1] = _heap[index2];
            _heap[index2] = temp;
        }

        private int Parent(int node)
        {
            return (node - 1) / 2;
        }

        internal int LeftChild(int node)
        {
            return 2 * node + 1;
        }

        private bool HasLeftChild(int node)
        {
            return LeftChild(node) < _heap.Count && LeftChild(node) < _pos;
        }

        internal int RightChild(int node)
        {
            return 2 * node + 2;
        }

        private bool HasRightChild(int node)
        {
            return RightChild(node) < _heap.Count && RightChild(node) < _pos;
        }

        public string HeapString => String.Join(", ", _heap);

        public List<int> Heap => _heap;
    }
}