using System;
using System.Collections.Generic;
using System.Collections.Generic.RedBlack;
using System.Diagnostics;

namespace DataStructures
{
    public class CompareRedBlackAVL
    {
        private int[] createRandomArray(int size, Random rand, bool unique = false)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < size; i++)
            {
                {
                    var next = rand.Next(1, 3 * size);
                    if (!unique || !list.Contains(next)) list.Add(next);
                }
            }
            return list.ToArray();
        }
        
        private Random rnd = new Random();
        
        private List<int[]> arrays = new List<int[]>();

        private void Init(int arraySize, int nbArrays)
        {
            arrays.Clear();
            for (int i = 0; i < nbArrays; i++)
            {
                arrays.Add(createRandomArray(arraySize, rnd, true));
            }
        }
        
        public Tuple<double, double> RunSearch(int arraySize, int nbArrays)
        {
            Init(arraySize, nbArrays);
            Stopwatch watch;
            double elapsedMsRedBlack = 0;
            double elapsedMsAVL = 0;
            for (int i = 0; i < nbArrays; i++)
            {                
                int[] array = (int[]) arrays[i].Clone();
                RedBlackTree<int, string> rbTree = new RedBlackTree<int, string>();
                foreach (var v in array)
                {
                    rbTree.Add(v, DateTime.UtcNow.Millisecond.ToString());
                }
                watch = Stopwatch.StartNew();
                rbTree.GetMinKey();
                watch.Stop();
                double ticks = watch.ElapsedTicks;
                double microseconds = (ticks / Stopwatch.Frequency) * 1000000;
                elapsedMsRedBlack += microseconds;

                AVL AVLtree = new AVL(arrays[i]);
                watch = Stopwatch.StartNew();
                AVLtree.Peek();
                watch.Stop();
                ticks = watch.ElapsedTicks;
                microseconds = (ticks / Stopwatch.Frequency) * 1000000;
                elapsedMsAVL += microseconds;
            }
            elapsedMsRedBlack /= nbArrays;
            elapsedMsAVL /= nbArrays;
            return new Tuple<double, double>(elapsedMsRedBlack, elapsedMsAVL);
        }
        
        public Tuple<double, double> RunInsertion(int arraySize, int nbArrays)
        {
            Init(arraySize, nbArrays);
            Stopwatch watch;
            double elapsedMsRedBlack = 0;
            double elapsedMsAVL = 0;
            for (int i = 0; i < nbArrays; i++)
            {                
                RedBlackTree<int, string> rbTree = new RedBlackTree<int, string>();
                watch = Stopwatch.StartNew();
                rbTree.Add(0, "0");
                watch.Stop();
                double ticks = watch.ElapsedTicks;
                double microseconds = (ticks / Stopwatch.Frequency) * 1000000;
                elapsedMsRedBlack += microseconds;

                AVL avltree = new AVL(arrays[i]);
                watch = Stopwatch.StartNew();
                avltree.Add(0);
                watch.Stop();
                ticks = watch.ElapsedTicks;
                microseconds = (ticks / Stopwatch.Frequency) * 1000000;
                elapsedMsAVL += microseconds;
            }
            elapsedMsRedBlack /= nbArrays;
            elapsedMsAVL /= nbArrays;
            return new Tuple<double, double>(elapsedMsRedBlack, elapsedMsAVL);
        }
        
        public Tuple<double, double> RunDeletion(int arraySize, int nbArrays)
        {
            Init(arraySize, nbArrays);
            Stopwatch watch;
            double elapsedMsRedBlack = 0;
            double elapsedMsAVL = 0;
            for (int i = 0; i < nbArrays; i++)
            {                
                RedBlackTree<int, string> rbTree = new RedBlackTree<int, string>();
                rbTree.Add(0, "0");
                watch = Stopwatch.StartNew();
                rbTree.RemoveMin();
                watch.Stop();
                double ticks = watch.ElapsedTicks;
                double microseconds = (ticks / Stopwatch.Frequency) * 1000000;
                elapsedMsRedBlack += microseconds;

                AVL AVLtree = new AVL(arrays[i]);
                AVLtree.Add(0);
                watch = Stopwatch.StartNew();
                AVLtree.Delete(0);
                watch.Stop();
                ticks = watch.ElapsedTicks;
                microseconds = (ticks / Stopwatch.Frequency) * 1000000;
                elapsedMsAVL += microseconds;
            }
            elapsedMsRedBlack /= nbArrays;
            elapsedMsAVL /= nbArrays;
            return new Tuple<double, double>(elapsedMsRedBlack, elapsedMsAVL);
        }
    }
}