using System;
using CSKicksCollection.Trees;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            RBTree tree = new RBTree();
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);
            tree.Insert(1);
            tree.Insert(9);
            tree.Insert(11);
            tree.Insert(6);
            tree.Delete(9);
            tree.Delete(5);
            tree.DisplayTree();
            Console.ReadLine();
        }
    }
}