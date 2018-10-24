using System;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
//            int[] values = {10, 4, 5, 2, 1};
            int[] values = {10, 9, 8, 7, 6};
            BinaryHeap.MinHeap heap = new BinaryHeap.MinHeap(values);
            Console.WriteLine(heap.HeapString);
            heap.Insert(1);
            Console.WriteLine(heap.HeapString);
            heap.Pop();
            Console.WriteLine(heap.HeapString);
        }
    }
}