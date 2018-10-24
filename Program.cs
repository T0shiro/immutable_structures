using System;
using CSKicksCollection.Trees;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 0, 1, 2, 3, 4, 5 };
            AVLTree<int> tree = new AVLTree<int>(array);
            tree.PrintPretty();
            Console.WriteLine(tree.popMinimum());
            tree.PrintPretty();
            Console.ReadLine();
        }
    }
}