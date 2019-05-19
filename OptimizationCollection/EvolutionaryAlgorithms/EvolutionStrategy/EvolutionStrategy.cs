using System;
using System.Collections.Generic;
using System.Text;
using EvolutionaryAlgorithms.Helper;

namespace EvolutionaryAlgorithms.EvolutionStrategy
{

    public class Individual
    {
        public double[] genes;
        public static (double min, double max)[] searchInterval;
        public double[] sigma;
        public double fitness;

        public Individual()
        {
        }
        public Individual(double[] genes, (double min, double max)[] staticSearchInterval, double[] sigma, double fitness = double.PositiveInfinity) : this(genes, sigma, fitness)
        {
            if (genes.Length != sigma.Length || sigma.Length != staticSearchInterval.Length)
                throw new RankException("genes and sigma have unequal length");

            searchInterval = staticSearchInterval;
        }

        public Individual(double[] genes, double[] sigma, double fitness = double.PositiveInfinity)
        {
            if (genes.Length != sigma.Length)
                throw new RankException("genes and sigma have unequal length");

            this.genes = genes;
            this.sigma = sigma;
            this.fitness = fitness;

            searchInterval = new (double min, double max)[genes.Length];
            for (int i = 0; i < genes.Length; ++i)
            {
                searchInterval[i] = (double.NegativeInfinity, double.PositiveInfinity);
            }
        }

        public override string ToString()
        {

            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder nullCheckedSearchInterval = new StringBuilder(20);
            stringBuilder.Append("Genes\tSigma\tFitness\tRange\n");
            for (int i = 0; i < genes.Length; i++)
            {
                nullCheckedSearchInterval.Clear();
                if (Individual.searchInterval == null)
                    nullCheckedSearchInterval.Append("(NaN, NaN)");
                else
                    nullCheckedSearchInterval.Append($"({searchInterval[i].min}, {searchInterval[i].max})");
                if (i == 0)
                    stringBuilder.Append($"{genes[i]:0.##}\t{sigma[i]:0.##}\t{fitness}\t{nullCheckedSearchInterval.ToString()}\n");
                else
                    stringBuilder.Append($"{genes[i]:0.##}\t{sigma[i]:0.##}\t\t{nullCheckedSearchInterval.ToString()}\n");
            }
            return stringBuilder.ToString();
        }
    }

    public class EvolutionSteps
    {
        public delegate double RecombinationFunction(double firstParam, double secondParam);
        public delegate void FitnessFunction(ref List<Individual> individuals);
        public delegate void MutationFunction(Individual individual, float tau1, float tau2);

        public static List<Individual> Recombine(ref List<Individual> parents, int lambda, RecombinationFunction recombinationTypeGenes, RecombinationFunction recombinationTypeSigma)
        {
            if (lambda <= 1)
            {
                throw new ArgumentOutOfRangeException("lambda", "Lambda too low");
            }


            int rand1, rand2;
            List<Individual> childs = new List<Individual>(lambda);
            for (int i = 0; i < lambda; i++)
            {
                //TODO rand1 could be rand2 ...
                rand1 = RandomNumbers.GetRandomIntNumber(0, parents.Count);
                do
                {
                    rand2 = RandomNumbers.GetRandomIntNumber(0, parents.Count);
                } while (rand2 == rand1 || parents.Count < 2);

                childs.Add(Recombine(parents[rand1], parents[rand2], recombinationTypeGenes, recombinationTypeSigma));
            }
            return childs;
        }

        private static Individual Recombine(Individual firstIndividual, Individual secondIndividual, RecombinationFunction recombinationTypeGenes, RecombinationFunction recombinationTypeSigma)
        {
            Individual individual = new Individual();
            //TODO securitycheckshere length etc
            individual.genes = RecombineArrays(firstIndividual.genes, secondIndividual.genes, recombinationTypeGenes);
            individual.sigma = RecombineArrays(firstIndividual.sigma, secondIndividual.sigma, recombinationTypeSigma);
            individual.fitness = float.PositiveInfinity;
            return individual;
        }

        private static double[] RecombineArrays(double[] firstParam, double[] secondParam, RecombinationFunction recombinationType)
        {
            if (firstParam.Length != secondParam.Length)
                throw new ArgumentOutOfRangeException("Unequal Length");

            double[] recombined = new double[firstParam.Length];

            for (int i = 0; i < recombined.Length; ++i)
            {
                recombined[i] = recombinationType(firstParam[i], secondParam[i]);
            }
            return recombined;
        }
        
        public static void Mutate(ref List<Individual> individuals, float tau1, float tau2, MutationFunction mutationFunction)
        {
            for(int i=0; i<individuals.Count; i++)
            {
                mutationFunction(individuals[i], tau1, tau2);
            }
        }
        
        public static void Evaluate(ref List<Individual> individuals, FitnessFunction fitnessFunction)
        {
            fitnessFunction(ref individuals);
        }

        public static void Selection(ref List<Individual> individuals, int mu, GenericComparer<Individual> comparer)
        {
            if(mu <= 0)
                throw new ArgumentException("mu to small");

            if (mu > individuals.Count)
                throw new ArgumentException("mu to big");
                          
            individuals.Sort(comparer);
            individuals.RemoveRange(mu, individuals.Count - mu);
        }
    }
}