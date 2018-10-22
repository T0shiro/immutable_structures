using System;
using CSKicksCollection.Trees;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Add(1);
            Console.WriteLine(tree.ToString());
            Console.ReadLine();
        }
    }
}