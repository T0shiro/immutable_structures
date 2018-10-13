using System;
using CSKicksCollection.Trees;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int> binaryTree = new BinaryTree<int>();
            binaryTree.Add(1);
            Console.WriteLine(binaryTree.GetHeight());
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}