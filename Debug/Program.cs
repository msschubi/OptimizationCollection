using System;
using System.Collections.Generic;
using EvolutionaryAlgorithms.EvolutionStrategy;
using EvolutionaryAlgorithms.Helper;

public class Program
{
    static void Main(string[] args)
    {

        int mu = 10;
        int lambda = 70;
        int generations = 10;
        GenericComparer<Individual> comparer = new GenericComparer<Individual>(CompareFunctions.CompareRating);


        //create parent generation
        List<Individual> parents = new List<Individual>();
        List<Individual> childs = null;

        for (int i = 0; i < mu / 2; i++)
        {
            parents.Add(new Individual(new[] { 2.0, 200.0 }, new[] { 2.0, 2.0 }));
            parents.Add(new Individual(new[] { 1.0, 2000.0 }, new[] { 2.0, 2.0 }));
        }

        //start evolving!
        for (int i = 0; i < generations; i++)
        {
            childs = EvolutionSteps.Recombine(ref parents, lambda, BasicEvolutionStrategyFunctions.IntermediateRecombination, BasicEvolutionStrategyFunctions.DiscreteRecombination);
            EvolutionSteps.Mutate(ref childs, 0.1f, 0.2f, BasicEvolutionStrategyFunctions.Mutate);
            EvolutionSteps.Evaluate(ref childs, BasicEvolutionStrategyFunctions.SimpleZeroRating);
            EvolutionSteps.Selection(ref childs, mu, comparer);
            parents = childs;
        }

        //show me
        foreach(Individual ind in childs)
        {
            Console.WriteLine(ind);
        }
    }
}