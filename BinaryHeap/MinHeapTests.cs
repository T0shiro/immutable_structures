using System;
using System.Linq;

namespace DataStructures.BinaryHeap
{
    using NUnit.Framework;

    [TestFixture]
    public class MinHeapTests
    {
        [Test]
        public void Pop()
        {
            int[] values = {10, 9, 8, 7, 6};
            MinHeap heap = new MinHeap(values);
            Assert.AreEqual(heap.Peek(), values.Min());
            int pop = heap.Pop();
            Assert.AreEqual(pop, values.Min());
            Assert.AreEqual(values.Length - 1, heap.Heap.Count);
        }
    }
}