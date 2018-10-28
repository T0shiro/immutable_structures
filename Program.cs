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

            Console.WriteLine("MAX ARRAY SIZE = 2^" + MAX_TWO_POW + ", ITERATIONS FOR EACH = " + ALGO_ITERATIONS);
            
            RunSimulations(new RunAVLTree(), "avl", MAX_TWO_POW, ALGO_ITERATIONS);
            RunSimulations(new RunMinHeap(), "heap", MAX_TWO_POW, ALGO_ITERATIONS);
            
        }

        private static void RunSimulations(Runner runner, string structure, int maxTwoPow,
            int algoIterations)
        {
            Console.WriteLine("===== {0} =====", structure);
            
            Console.WriteLine("Creation of {0}:", structure);
            GenerateOutput(runner.RunCreation, maxTwoPow, algoIterations, String.Format(@"plot_generation/{0}_creation.txt", structure));

            Console.WriteLine("\nInsertion of minimum : ");
            GenerateOutput(runner.RunInsertion, maxTwoPow, algoIterations, String.Format(@"plot_generation/{0}_insertion.txt", structure));

            Console.WriteLine("\nDeletion of minimum : ");
            GenerateOutput(runner.RunDeletion, maxTwoPow, algoIterations, String.Format(@"plot_generation/{0}_deletion.txt", structure));
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