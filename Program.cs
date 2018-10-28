using System;
using System.Collections.Generic;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            int MAX_TWO_POW = 10;
            int ALGO_ITERATIONS = 100;
            RunAVLTree runner = new RunAVLTree();
            Console.WriteLine("MAX ARRAY SIZE = 2^" + MAX_TWO_POW + ", ITERATIONS FOR EACH = " + ALGO_ITERATIONS);

            Console.WriteLine("Creation of tree : ");
            GenerateOutput(runner.RunCreation, MAX_TWO_POW, ALGO_ITERATIONS, @"plot_generation/creation.txt");

            Console.WriteLine("\nInsertion of minimum : ");
            GenerateOutput(runner.RunInsertion, MAX_TWO_POW, ALGO_ITERATIONS, @"plot_generation/insertion.txt");

            Console.WriteLine("\nDeletion of minimum : ");
            GenerateOutput(runner.RunDeletion, MAX_TWO_POW, ALGO_ITERATIONS, @"plot_generation/deletion.txt");


//            int[] values = {10, 4, 5, 2, 1};
            int[] values = {10, 9, 8, 7, 6};
            BinaryHeap.MinHeap heap = new BinaryHeap.MinHeap(values);
            Console.WriteLine(heap.HeapString);
            heap.Insert(1);
            Console.WriteLine(heap.HeapString);
            heap.Pop();
            Console.WriteLine(heap.HeapString);
        }

        private static void GenerateOutput(Func<int, int, Tuple<double, double>> function, int maxTwoPow,
            int algoIterations, string outputFile)
        {
            List<string> lines = new List<string>();
            for (int i = 2; i <= Math.Pow(2, maxTwoPow); i *= 2)
            {
                Console.WriteLine(i);
                lines.Add(i + " " + function(i, algoIterations));
            }

            System.IO.File.WriteAllLines(outputFile, lines);
        }
    }
}