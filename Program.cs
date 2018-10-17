using System;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] values = {10, 4, 5, 2, 1};
            BinaryHeap.BinaryHeap heap = new BinaryHeap.BinaryHeap(values);
            Console.WriteLine(heap.Heap);
//            heap.Insert(6);
            Console.WriteLine(heap.Heap);
        }
    }
}