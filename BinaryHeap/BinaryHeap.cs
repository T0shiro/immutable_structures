using System;

namespace DataStructures.BinaryHeap
{
    public class BinaryHeap
    {
        private int[] tab;

        public BinaryHeap(int[] tab)
        {
            this.tab = Heapify(tab);
        }

        private int[] Heapify(int[] ints)
        {
            int[] heap = new int[ints.Length];
            ints.CopyTo(heap, 0);

            int rootIndex = heap.Length / 2 - 1;
            while (rootIndex >= 0)
            {
                PercolateDown(heap, rootIndex);
                rootIndex--;
            }

            return heap;
        }

        private void PercolateDown(int[] heap, int rootIndex)
        {
            int leftChildIndex = 2 * rootIndex + 1;

            while (leftChildIndex < heap.Length)
            {
                if (leftChildIndex < heap.Length - 1 && heap[leftChildIndex] > heap[leftChildIndex + 1])
                {
                    leftChildIndex++;
                }

                if (heap[rootIndex] > heap[leftChildIndex])
                {
                    Swap(heap, rootIndex, leftChildIndex);
                    rootIndex = leftChildIndex;
                    leftChildIndex *= 2;
                }
                else
                {
                    break;
                }

                leftChildIndex++;
            }
        }

        public void Insert(int value)
        {
            Insert(tab, value);
        }

        /**
         * Includes Percolate up
         */
        private void Insert(int[] heap, int valueToInsert)
        {
            int insertIndex = heap.Length + 1;
            while (insertIndex > 1 && valueToInsert > heap[insertIndex / 2])
            {
                heap[insertIndex] = heap[insertIndex / 2];
                insertIndex /= 2;
            }

            int[] newHeap = new int[heap.Length + 1];


            for (int i = 0; i < insertIndex; i++)
            {
                newHeap[i] = heap[i];
            }

            newHeap[insertIndex] = valueToInsert;

            for (int i = insertIndex + 1; i < newHeap.Length; i++)
            {
                newHeap[i] = heap[i];
            }

            tab = newHeap;
        }

        private void Swap(int[] heap, int index1, int index2)
        {
            int temp = heap[index1];
            heap[index1] = heap[index2];
            heap[index2] = temp;
        }

        public string Heap => String.Join(", ", tab);
    }
}