using System;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            int MAX_TWO_POW = 10;
            int ALGO_ITERATIONS = 100;
            runAVLTree runner = new runAVLTree();
            Console.WriteLine("MAX ARRAY SIZE = 2^"+MAX_TWO_POW+", ITERATIONS FOR EACH = "+ALGO_ITERATIONS);
            Console.WriteLine("Creation of tree : ");
            for (int i = 2; i <= Math.Pow(2, MAX_TWO_POW); i *= 2)
            {
                Console.WriteLine(i+" : "+runner.RunCreation(i, ALGO_ITERATIONS));
            }
            Console.WriteLine("\nInsertion of minimum : ");
            for (int i = 2; i <= Math.Pow(2, MAX_TWO_POW); i *= 2)
            {
                Console.WriteLine(i+" : "+runner.RunInsertion(i, ALGO_ITERATIONS));
            }
            Console.WriteLine("\nDeletion of minimum : ");
            for (int i = 2; i <= Math.Pow(2, MAX_TWO_POW); i *= 2)
            {
                Console.WriteLine(i+" : "+runner.RunDeletion(i, ALGO_ITERATIONS));
            }
        }
    }
}