using System;
using System.Linq;

namespace DataStructures.BinaryHeap
{
    public class MinHeap
    {
        private int[] heap;
        private int _pos;

        public MinHeap(int[] values)
        {
            Heapify(values);
        }

        private void Heapify(int[] ints)
        {
            heap = new int[ints.Length];
            ints.CopyTo(heap, 0);
            _pos = heap.Length;
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

                if (HasRightChild(currentIndex) && heap[RightChild(currentIndex)] < heap[currentIndex] &&
                    heap[RightChild(currentIndex)] < heap[minIndex])
                {
                    minIndex = RightChild(currentIndex);
                }

                if (heap[minIndex] < heap[currentIndex])
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
            while (currentIndex > 0 && heap[parentIndex] > heap[currentIndex])
            {
                Swap(parentIndex, currentIndex);
                currentIndex = parentIndex;
                parentIndex = Parent(currentIndex);
            }
        }

        public void Insert(int i)
        {
//            int[] newHeap = new int[_pos + 1];
//            heap.CopyTo(newHeap, 0);
            heap.Append(i);
//            heap = newHeap;
            PercolateUp(_pos);
            _pos++;
        }

        public int Peek()
        {
            int x = heap[0];
            _pos--;
            heap[0] = heap[_pos];
            PercolateDown(0);
            return x;
        }

        private void Swap(int index1, int index2)
        {
            int temp = heap[index1];
            heap[index1] = heap[index2];
            heap[index2] = temp;
        }

        private int Parent(int node)
        {
            return (node - 1) / 2;
        }

        private int LeftChild(int node)
        {
            return 2 * node + 1;
        }

        private bool HasLeftChild(int node)
        {
            return LeftChild(node) < heap.Length && LeftChild(node) < _pos;
        }

        private int RightChild(int node)
        {
            return 2 * node + 2;
        }

        private bool HasRightChild(int node)
        {
            return RightChild(node) < heap.Length && RightChild(node) < _pos;
        }
        
        public string Heap => String.Join(", ", heap);
    }
}