using System;
using System.Collections.Generic;
using EvolutionaryAlgorithms.EvolutionStrategy;
namespace EvolutionaryAlgorithms.Helper
{
    public class GenericComparer<T> : IComparer<T>
    {
        public delegate int Comparer(T x, T y);
        private Comparer comparer;

        public GenericComparer(Comparer comparer)
        {
            this.comparer = comparer;
        }

        private GenericComparer() { }

        public int Compare(T x, T y)
        {
            return comparer(x, y);
        }
    }

    public static class CompareFunctions
    {
        public static int CompareRating(Individual individual1, Individual individual2)
        {
            if (individual1.fitness == individual2.fitness)
                return 0;
            return individual1.fitness < individual2.fitness ? -1 : 1;
        }

    }
}