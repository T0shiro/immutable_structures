using System;

namespace DataStructures.BinaryHeap
{
    public class ImmutableMinHeap
    {
        private ImmutableMinHeap left;
        private ImmutableMinHeap right;
        private int value;
        private int size;
        private int height;

        public ImmutableMinHeap(int value, ImmutableMinHeap left, ImmutableMinHeap right)
        {
            this.left = left;
            this.right = right;
            this.value = value;
            this.size = left.size + right.size + 1;
            this.height = Math.Max(left.height, right.height) + 1;
        }

        public ImmutableMinHeap(int value)
        {
            this.value = value;
        }

        public ImmutableMinHeap()
        {
        }

        public ImmutableMinHeap Insert(int x)
        {
            if (isEmpty()) return new ImmutableMinHeap(x);
            if (left.size < Math.Pow(left.height, 2) - 1) return PercolateUp(value, left.Insert(x), right);
            if (right.size < Math.Pow(right.height, 2) - 1) return PercolateUp(value, left, right.Insert(x));
            if (right.height < left.height) return PercolateUp(value, left, right.Insert(x));
            return PercolateUp(value, left.Insert(x), right);
        }


        public ImmutableMinHeap Heapify(int[] ints)
        {
            Func<int, ImmutableMinHeap> loop = null;
            loop = i => i < ints.Length
                ? PercolateDown(ints[i], loop(2 * i + 1), loop(2 * i + 2))
                : new Leaf();
            return loop(0);
        }

        private ImmutableMinHeap PercolateDown(int value, ImmutableMinHeap left, ImmutableMinHeap right)
        {
            if (left is Branch && right is Branch && right.value < left.value && value > right.value)
                return new ImmutableMinHeap(right.value, right.left, right.right);
            if (left is Branch && value > left.value)
                return new ImmutableMinHeap(left.value, PercolateDown(value, left.left, left.right), right);
            return new ImmutableMinHeap(value, left, right);
        }

        private ImmutableMinHeap PercolateUp(int value, ImmutableMinHeap left, ImmutableMinHeap right)
        {
            if (left is Branch && value > left.value)
                return new ImmutableMinHeap(left.value, new ImmutableMinHeap(value, left.left, left.right), right);
            if (right is Branch && value > right.value)
                return new ImmutableMinHeap(right.value, new ImmutableMinHeap(value, right.left, right.right), right);
            return new ImmutableMinHeap(value, left, right);
        }

        public ImmutableMinHeap Pop()
        {
            return PercolateRootDown(MergeChildren(left, right));
        }

        private ImmutableMinHeap PercolateRootDown(ImmutableMinHeap heap)
        {
            return isEmpty()
                ? new ImmutableMinHeap(Int32.MaxValue)
                : heap.PercolateDown(heap.value, heap.left, heap.right);
        }

        private ImmutableMinHeap MergeChildren(ImmutableMinHeap left, ImmutableMinHeap right)
        {
            if (left.isEmpty() && right.isEmpty()) return new ImmutableMinHeap(Int32.MaxValue);
            if (left.size < Math.Pow(left.height, 2) - 1)
                return FloatLeft(left.value, MergeChildren(left.left, left.right), right);
            if (right.size < Math.Pow(right.height, 2) - 1)
                return FloatRight(right.value, left, MergeChildren(right.left, right.right));
            if (right.height < left.height)
                return FloatLeft(left.value, MergeChildren(left.left, left.right), right);
            return FloatRight(right.value, left, MergeChildren(right.left, right.right));
        }

        private ImmutableMinHeap FloatLeft(int value, ImmutableMinHeap left, ImmutableMinHeap right)
        {
            return left is Branch
                ? new ImmutableMinHeap(left.value, new ImmutableMinHeap(value, left.left, left.right), right)
                : new ImmutableMinHeap(value, left, right);
        }

        private ImmutableMinHeap FloatRight(int value, ImmutableMinHeap left, ImmutableMinHeap right)
        {
            return right is Branch
                ? new ImmutableMinHeap(right.value, left, new ImmutableMinHeap(value, left.left, left.right))
                : new ImmutableMinHeap(value, left, right);
        }

        public int Peek()
        {
            return value;
        }

        private bool isEmpty()
        {
            return this is Leaf;
        }

        public class Branch : ImmutableMinHeap
        {
            public Branch(ImmutableMinHeap left, ImmutableMinHeap right, int value, int size, int height)
            {
                this.left = left;
                this.right = right;
                this.value = value;
                this.size = size;
                this.height = height;
            }
        }

        public class Leaf : ImmutableMinHeap
        {
            public Leaf()
            {
                this.left = null;
                this.right = null;
                this.value = Int32.MaxValue;
                this.size = 0;
                this.height = 0;
            }
        }
    }
}