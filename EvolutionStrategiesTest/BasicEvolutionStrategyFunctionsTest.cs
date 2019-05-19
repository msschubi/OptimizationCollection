using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvolutionaryAlgorithms.EvolutionStrategy;
using System.Collections.Generic;
using EvolutionaryAlgorithms.Helper;
using System;

namespace EvolutionStrategiesTest
{

    [TestClass]
    public class BasicEvolutionStrategyFunctionTest
    {
        [TestMethod]
        public void IntermediateRecombinationDoubleValuesPositiveInfinity()
        {
            try
            {
                BasicEvolutionStrategyFunctions.IntermediateRecombination(double.MaxValue, double.MaxValue);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Assert.AreEqual(exception.ParamName, "v1 and v2");
            }
        }

        [TestMethod]
        public void IntermediateRecombinationDoubleValuesNegativeInfinity()
        {
            try
            {
                BasicEvolutionStrategyFunctions.IntermediateRecombination(double.MinValue, double.MinValue);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Assert.AreEqual(exception.ParamName, "v1 and v2");
            }
        }

        [TestMethod]
        public void IntermediateRecombinationBigPositiveAndNegativeDoubleValues()
        {
            Assert.IsTrue(BasicEvolutionStrategyFunctions.IntermediateRecombination(double.MinValue, double.MaxValue) == 0);
        }

        [TestMethod]
        public void DiscreteRecombinationGetRandomValue()
        {
            double v = BasicEvolutionStrategyFunctions.DiscreteRecombination(1.0, 2.0);
            Assert.IsTrue(v == 1.0 || v == 2.0);
        }

        [TestMethod]
        public void DiscreteRecombinationInfinityV1()
        {
            try
            {
                BasicEvolutionStrategyFunctions.DiscreteRecombination(double.PositiveInfinity, 1.0);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Assert.AreEqual(exception.ParamName, "v1");
            }
        }

        [TestMethod]
        public void DiscreteRecombinationInfinityV2()
        {
            try
            {
                BasicEvolutionStrategyFunctions.DiscreteRecombination(1.0, double.PositiveInfinity);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Assert.AreEqual(exception.ParamName, "v2");
            }
        }

        [TestMethod]
        public void SimpleZeroRatingAllGeneValuesAtZero()
        {
            List<Individual> parents = new List<Individual>();
            Individual ind1 = new Individual(new[] { 0.0, 0.0 }, new[] { (0.0, 10.0), (0.0, 10.0) }, new[] { 3.0, 4.0 });
            Individual ind2 = new Individual(new[] { 0.0, 0.0 }, new[] { (0.0, 10.0), (0.0, 10.0) }, new[] { 3.0, 6.0 });
            parents.Add(ind1);
            parents.Add(ind2);
            BasicEvolutionStrategyFunctions.SimpleZeroRating(ref parents);
            Assert.IsTrue(parents[0].fitness == 0.0);
            Assert.IsTrue(parents[1].fitness == 0.0);
        }

        [TestMethod]
        public void SimpleZeroRatingAllGeneValuesToBePlusMinus()
        {
            List<Individual> parents = new List<Individual>();
            Individual ind1 = new Individual(new[] { -1.0, 1.0 }, new[] { (-5.0, 5.0), (-5.0, 5.0) }, new[] { 3.0, 4.0 });
            Individual ind2 = new Individual(new[] { 3.0, -3.0 }, new[] { (-5.0, 5.0), (-5.0, 5.0) }, new[] { 3.0, 6.0 });
            parents.Add(ind1);
            parents.Add(ind2);
            BasicEvolutionStrategyFunctions.SimpleZeroRating(ref parents);
            Assert.IsTrue(parents[0].fitness == 2.0);
            Assert.IsTrue(parents[1].fitness == 6.0);
        }

        [TestMethod]
        public void SimpleZeroRatingValuesOutOfInterval()
        {
            List<Individual> parents = new List<Individual>();
            Individual ind1 = new Individual(new[] { -6.0, 1.0 }, new[] { (-5.0, 5.0), (-5.0, 5.0) }, new[] { 3.0, 4.0 });
            Individual ind2 = new Individual(new[] { 3.0, 6.0 }, new[] { (-5.0, 5.0), (-5.0, 5.0) }, new[] { 3.0, 6.0 });
            parents.Add(ind1);
            parents.Add(ind2);
            BasicEvolutionStrategyFunctions.SimpleZeroRating(ref parents);
            Assert.IsTrue(double.IsInfinity(parents[0].fitness));
            Assert.IsTrue(double.IsInfinity(parents[1].fitness));
        }

        [TestMethod]
        public void SimpleZeroRatingValuesExactlyOnIntervalLimit()
        {
            List<Individual> parents = new List<Individual>();
            Individual ind1 = new Individual(new[] { -5.0, 1.0 }, new[] { (-5.0, 5.0), (-5.0, 5.0) }, new[] { 3.0, 4.0 });
            Individual ind2 = new Individual(new[] { 3.0, 5.0 }, new[] { (-5.0, 5.0), (-5.0, 5.0) }, new[] { 3.0, 6.0 });
            parents.Add(ind1);
            parents.Add(ind2);
            BasicEvolutionStrategyFunctions.SimpleZeroRating(ref parents);
            Assert.IsTrue(parents[0].fitness == 6.0);
            Assert.IsTrue(parents[1].fitness == 8.0);
        }

        [TestMethod]
        public void SimpleZeroRatingValuesInfinityValues()
        {
            List<Individual> parents = new List<Individual>();
            Individual ind1 = new Individual(new[] { double.NegativeInfinity, 1.0 }, new[] { (double.NegativeInfinity, 5.0), (-5.0, 5.0) }, new[] { 3.0, 4.0 });
            Individual ind2 = new Individual(new[] { 3.0, double.PositiveInfinity }, new[] { (-5.0, 5.0), (-5.0, double.PositiveInfinity) }, new[] { 3.0, 6.0 });
            parents.Add(ind1);
            parents.Add(ind2);
            BasicEvolutionStrategyFunctions.SimpleZeroRating(ref parents);
            Assert.IsTrue(parents[0].fitness == double.PositiveInfinity);
            Assert.IsTrue(parents[1].fitness == double.PositiveInfinity);
        }

        [TestMethod]
        public void SimpleZeroRatingListIsNull()
        {
            List<Individual> parents = null;
            try
            {
                BasicEvolutionStrategyFunctions.SimpleZeroRating(ref parents);
            }
            catch (NullReferenceException exception)
            {
                Assert.AreEqual(exception.Message, "individuals is null");
            }
        }

        [TestMethod]
        public void SimpleZeroRatingListIsEmpty()
        {
            List<Individual> parents = new List<Individual>();
            try
            {
                BasicEvolutionStrategyFunctions.SimpleZeroRating(ref parents);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Assert.AreEqual(exception.ParamName, "individuals");
            }
        }

        [TestMethod]
        public void SimpleZeroRatingSearchIntervalIsNull()
        {
            List<Individual> parents = new List<Individual>();
            Individual ind1 = new Individual();
            ind1.genes = new[] { 1.0, 2.0 };
            ind1.sigma = new[] { 1.0, 2.0 };
            parents.Add(ind1);

            try
            {
                BasicEvolutionStrategyFunctions.SimpleZeroRating(ref parents);
            }
            catch (NullReferenceException exception)
            {
                Assert.AreEqual(exception.Message, "search interval is null");
            }
        }
    }
}
