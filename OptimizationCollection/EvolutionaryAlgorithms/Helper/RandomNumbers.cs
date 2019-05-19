using System;
namespace EvolutionaryAlgorithms.Helper
{
    public class RandomNumbers
    {
        private static readonly Random random = new Random();

        public static double GetRandomNumber()
        {
            return random.NextDouble();
        }

        public static int GetRandomIntNumber(int minInklusive, int maxExclusive)
        {
            return random.Next(minInklusive, maxExclusive);
        }

        public static double GetNormalDistributedRandomNumber()
        {
            return (Math.Sqrt(-2 * Math.Log(random.NextDouble())) * Math.Sin(2 * Math.PI * random.NextDouble()));
        }

        public static double[] GetNormalDistributedRandomNumbers(int numberOfValues)
        {
            if (numberOfValues <= 0)
                throw new ArgumentOutOfRangeException("Can't calculate that less random values");

            double[] normalDistRandom = new double[numberOfValues];
            double n1, n2;
            for (int i = 0; i < numberOfValues / 2; i++)
            {
                n1 = random.NextDouble();
                n2 = random.NextDouble();

                normalDistRandom[2 * i] = Math.Sqrt(-2 * Math.Log(n1)) * Math.Sin(2 * Math.PI * (n2));
                normalDistRandom[2 * i + 1] = Math.Sqrt(-2 * Math.Log(n1)) * Math.Cos(2 * Math.PI * (n2));
            }
            if (numberOfValues % 2 != 0)
            {
                n1 = random.NextDouble();
                normalDistRandom[numberOfValues - 1] = Math.Sqrt(-2 * Math.Log(n1)) * Math.Sin(2 * Math.PI * (n1));
            }
            return normalDistRandom;
        }

        public static void CalcRecombinationPairs(ref (int spouse1, int spouse2)[] pairs)
        {

        }
    }
}