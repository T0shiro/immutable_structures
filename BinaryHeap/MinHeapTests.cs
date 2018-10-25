using System.Linq;

namespace DataStructures.BinaryHeap
{
    using NUnit.Framework;

    [TestFixture]
    public class MinHeapTests
    {
        
        int[] values = {10, 9, 8, 7, 6};
        
        [Test]
        public void Heapify()
        {
            
            MinHeap heap = new MinHeap(values);
            Assert.AreEqual(values.Length, heap.Heap.Count);
            Assert.IsTrue(isHeapValid(heap));
        }

        [Test]
        public void Insert()
        {
            MinHeap heap = new MinHeap(values);
            heap.Insert(5);
            Assert.AreEqual(values.Length + 1, heap.Heap.Count);
            Assert.IsTrue(isHeapValid(heap));
        }

        [Test]
        public void Peek()
        {
            MinHeap heap = new MinHeap(values);
            int peek = heap.Peek();
            Assert.AreEqual(peek, values.Min());
            Assert.AreEqual(values.Length, heap.Heap.Count);
        }

        [Test]
        public void Pop()
        {
            MinHeap heap = new MinHeap(values);
            int pop = heap.Pop();
            Assert.AreEqual(pop, values.Min());
            Assert.AreEqual(values.Length - 1, heap.Heap.Count);
            Assert.IsTrue(isHeapValid(heap));
        }


        private bool isHeapValid(MinHeap heap)
        {
            for (int i = 0; 2 * i + 2 <= heap.Heap.Count; i++)
            {
                if (heap.Heap[i] < heap.LeftChild(i) && heap.Heap[i] < heap.RightChild(i)) return false;
            }

            return true;
        }
    }
}