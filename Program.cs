using System;
using DataStructures;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = {5, 3, 7, 2};
            AVL tree1 = new AVL(array);
            tree1.DisplayTree();
            //int[] array = { 2,3,5,7 };
            ImmutableAVL tree = new ImmutableAVL(array);
            tree.DisplayTree();
            tree.Add(8);
            tree.DisplayTree();
            Console.ReadLine();
        }
    }
}