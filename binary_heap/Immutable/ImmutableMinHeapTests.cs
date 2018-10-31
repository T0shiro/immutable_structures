using System.Linq;

namespace DataStructures.BinaryHeap
{
    using NUnit.Framework;

    [TestFixture]
    public class ImmutableMinHeapTests
    {

        private static int[][] arrays = ArraysGenerator.getRandomArrays();

        [Test, TestCaseSource(nameof(arrays))]
        public void Heapify(int[] values)
        {
            ImmutableMinHeap heap = new ImmutableMinHeap().Heapify(values);
            Assert.AreEqual(values.Length, heap.Size);
            Assert.IsTrue(isHeapValid(heap));
        }

        [Test, TestCaseSource(nameof(arrays))]
        public void Insert(int[] values)
        {
            ImmutableMinHeap heap = new ImmutableMinHeap().Heapify(values).Insert(5);
//            var insert = heap.Insert(5);
            Assert.AreEqual(values.Length + 1, heap.Size);
            Assert.IsTrue(isHeapValid(heap));
        }

        [Test, TestCaseSource(nameof(arrays))]
        public void Peek(int[] values)
        {
            ImmutableMinHeap heap = new ImmutableMinHeap().Heapify(values).Peek();
            Assert.AreEqual(values.Min(), heap.Value);
            Assert.AreEqual(values.Length, heap.Size);
        }

        [Test, TestCaseSource(nameof(arrays))]
        public void Pop(int[] values)
        {
            ImmutableMinHeap heap = new ImmutableMinHeap().Heapify(values).Pop();
            Assert.AreEqual(values.Min(), heap.Value);
            Assert.AreEqual(values.Length - 1, heap.Size);
            Assert.IsTrue(isHeapValid(heap));
        }


        private bool isHeapValid(ImmutableMinHeap heap)
        {
            if (heap.Left.Equals(null) && heap.Right.Equals(null))
            {
                return true;
            }
            if(heap.Left != null)
            {
                return heap.Left.Value < heap.Value && isHeapValid(heap.Left);
            }
            if(heap.Right != null)
            {
                return heap.Right.Value < heap.Value && isHeapValid(heap.Right);
            }
            return false;
        }
    }
}