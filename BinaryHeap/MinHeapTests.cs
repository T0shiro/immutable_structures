using System.Linq;

namespace DataStructures.BinaryHeap
{
    using NUnit.Framework;

    [TestFixture]
    public class MinHeapTests
    {

        private static int[][] arrays =
        {
            new[] {10, 9, 8, 7, 6},
            new[] {69, 42, 110, 3, 7, 1, 39}
        };

        [Test, TestCaseSource(nameof(arrays))]
        public void Heapify(int[] values)
        {
            MinHeap heap = new MinHeap(values);
            Assert.AreEqual(values.Length, heap.Heap.Count);
            Assert.IsTrue(isHeapValid(heap));
        }

        [Test, TestCaseSource(nameof(arrays))]
        public void Insert(int[] values)
        {
            MinHeap heap = new MinHeap(values);
            heap.Insert(5);
            Assert.AreEqual(values.Length + 1, heap.Heap.Count);
            Assert.IsTrue(isHeapValid(heap));
        }

        [Test, TestCaseSource(nameof(arrays))]
        public void Peek(int[] values)
        {
            MinHeap heap = new MinHeap(values);
            int peek = heap.Peek();
            Assert.AreEqual(peek, values.Min());
            Assert.AreEqual(values.Length, heap.Heap.Count);
        }

        [Test, TestCaseSource(nameof(arrays))]
        public void Pop(int[] values)
        {
            MinHeap heap = new MinHeap(values);
            int pop = heap.Pop();
            Assert.AreEqual(pop, values.Min());
            Assert.AreEqual(values.Length - 1, heap.Heap.Count);
            Assert.IsTrue(isHeapValid(heap));
        }


        private bool isHeapValid(MinHeap heap)
        {
            for (int i = 0; heap.RightChild(i) < heap.Heap.Count; i++)
            {
                if (heap.Heap[i] > heap.Heap[heap.LeftChild(i)] || heap.Heap[i] > heap.Heap[heap.RightChild(i)])
                {
                    return false;
                }
            }

            return true;
        }
    }
}