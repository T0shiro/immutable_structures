using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructures.BinaryHeap;

namespace DataStructures
{
    public class RunMinHeap : Runner
    {
        private int[] createRandomArray(int size, Random rand)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < size; i++)
            {
                list.Add(rand.Next(1, 3 * size));
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
                arrays.Add(createRandomArray(arraySize, rnd));
            }
        }
        
        public override Tuple<double, double> RunCreation(int arraySize, int nbArrays)
        {
            Init(arraySize, nbArrays);
            Stopwatch watch;
            double elapsedMsMutable = 0;
            double elapsedMsImmutable = 0;
            for (int i = 0; i < nbArrays; i++)
            {                
                int[] array = (int[]) arrays[i].Clone();
                watch = Stopwatch.StartNew();
                MinHeap heap = new MinHeap(array);
                watch.Stop();
                double ticks = watch.ElapsedTicks;
                double microseconds = (ticks / Stopwatch.Frequency) * 1000000;
                elapsedMsMutable += microseconds;

                watch = Stopwatch.StartNew();
                ImmutableMinHeap immutableMeanHeap = new ImmutableMinHeap().Heapify(arrays[i]);
                watch.Stop();
                ticks = watch.ElapsedTicks;
                microseconds = (ticks / Stopwatch.Frequency) * 1000000;
                elapsedMsImmutable += microseconds;
            }
            elapsedMsMutable /= nbArrays;
            elapsedMsImmutable /= nbArrays;
            return new Tuple<double, double>(elapsedMsMutable, elapsedMsImmutable);
        }
        
        public override Tuple<double, double> RunInsertion(int arraySize, int nbArrays)
        {
            Init(arraySize, nbArrays);
            Stopwatch watch;
            double elapsedMsMutable = 0;
            double elapsedMsImmutable = 0;
            for (int i = 0; i < nbArrays; i++)
            {                
                MinHeap heap = new MinHeap(arrays[i]);
                watch = Stopwatch.StartNew();
                heap.Insert(0);
                watch.Stop();
                double ticks = watch.ElapsedTicks;
                double microseconds = (ticks / Stopwatch.Frequency) * 1000000;
                elapsedMsMutable += microseconds;

                ImmutableMinHeap immutableMinHeap = new ImmutableMinHeap().Heapify(arrays[i]);
                watch = Stopwatch.StartNew();
                immutableMinHeap.Insert(0);
                watch.Stop();
                ticks = watch.ElapsedTicks;
                microseconds = (ticks / Stopwatch.Frequency) * 1000000;
                elapsedMsImmutable += microseconds;
            }
            elapsedMsMutable /= nbArrays;
            elapsedMsImmutable /= nbArrays;
            return new Tuple<double, double>(elapsedMsMutable, elapsedMsImmutable);
        }
        
        public override Tuple<double, double> RunDeletion(int arraySize, int nbArrays)
        {
            Init(arraySize, nbArrays);
            Stopwatch watch;
            double elapsedMsMutable = 0;
            double elapsedMsImmutable = 0;
            for (int i = 0; i < nbArrays; i++)
            {                
                MinHeap heap = new MinHeap(arrays[i]);
                heap.Insert(0);
                watch = Stopwatch.StartNew();
                heap.Pop();
                watch.Stop();
                double ticks = watch.ElapsedTicks;
                double microseconds = (ticks / Stopwatch.Frequency) * 1000000;
                elapsedMsMutable += microseconds;

                ImmutableMinHeap immutableMinHeap = new ImmutableMinHeap().Heapify(arrays[i]);
                immutableMinHeap.Insert(0);
                watch = Stopwatch.StartNew();
                immutableMinHeap.Pop();
                watch.Stop();
                ticks = watch.ElapsedTicks;
                microseconds = (ticks / Stopwatch.Frequency) * 1000000;
                elapsedMsImmutable += microseconds;
            }
            elapsedMsMutable /= nbArrays;
            elapsedMsImmutable /= nbArrays;
            return new Tuple<double, double>(elapsedMsMutable, elapsedMsImmutable);
        }
    }
}