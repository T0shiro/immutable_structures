using System;

namespace DataStructures
{
    public abstract class Runner
    {
        public abstract Tuple<double, double> RunCreation(int arraySize, int nbArrays);

        public abstract Tuple<double, double> RunInsertion(int arraySize, int nbArrays);

        public abstract Tuple<double, double> RunDeletion(int arraySize, int nbArrays);

    }
}