using System;
using System.Collections.Generic;
using EvolutionaryAlgorithms.EvolutionStrategy;
namespace EvolutionaryAlgorithms.Helper
{
    public class BasicEvolutionStrategyFunctions
    {
        #region recombination
        public static double IntermediateRecombination(double v1, double v2)
        {
            double sum = v1 + v2;
            if (double.IsInfinity(sum))
            {
                throw new ArgumentOutOfRangeException("v1 and v2", "Sum of values is greater than double.MaxValue or smaller than double.MinValue");
            }
            return (v1 + v2) / 2.0;
        }
        public static double DiscreteRecombination(double v1, double v2)
        {
            if (double.IsInfinity(v1))
            {
                throw new ArgumentOutOfRangeException("v1", "v1 is infinity");
            }
            if (double.IsInfinity(v2))
            {
                throw new ArgumentOutOfRangeException("v2", "v2 is infinity");
            }

            return RandomNumbers.GetRandomNumber() >= 0.5 ? v1 : v2;
        }
        #endregion

        #region evaluation
        public static void SimpleZeroRating(ref List<Individual> individuals)
        {
            if (individuals == null)
                throw new NullReferenceException("individuals is null");
            if (individuals.Count == 0)
                throw new ArgumentOutOfRangeException("individuals", "The list of individuals needs at least one individual");
            if (Individual.searchInterval == null)
                throw new NullReferenceException("search interval is null");

                foreach (Individual individual in individuals)
                {
                    individual.fitness = 0.0;
                    for (int i = 0; i < individual.genes.Length; i++)
                    {
                        if (individual.genes[i] < Individual.searchInterval[i].min || individual.genes[i] > Individual.searchInterval[i].max)
                        {
                            individual.fitness = float.PositiveInfinity;
                            break;
                        }
                        individual.fitness += Math.Abs(individual.genes[i]);
                    }
                }
        }
        #endregion

        #region mutation
        public static void Mutate(Individual individual, float tau1, float tau2)
        {
            double[] randomNumbersGenes = RandomNumbers.GetNormalDistributedRandomNumbers(individual.genes.Length);
            double[] randomNumbersSigma = RandomNumbers.GetNormalDistributedRandomNumbers(individual.genes.Length);
            double randomNumberSigmaGlobal = RandomNumbers.GetNormalDistributedRandomNumber();

            for (int i = 0; i < individual.genes.Length; i++)
            {
                individual.sigma[i] = (float)(individual.sigma[i] * Math.Exp(tau1 * randomNumberSigmaGlobal + tau2 * randomNumbersSigma[i]));
                individual.genes[i] = individual.genes[i] + individual.sigma[i] * randomNumbersGenes[i];
            }
        }
        #endregion

    }
}