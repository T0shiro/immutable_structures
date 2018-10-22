using System;
using CSKicksCollection.Trees;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Add(6);
            tree.Add(4);
            tree.Add(12);
            tree.Add(7);
            tree.Add(5);
            tree.Add(8);
            tree.Add(9);
            tree.Add(0);
            tree.Add(13);
            tree.PrintPretty();
            Console.ReadLine();
        }
    }
}