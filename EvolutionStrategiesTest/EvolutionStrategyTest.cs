using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvolutionaryAlgorithms.EvolutionStrategy;
using System.Collections.Generic;
using EvolutionaryAlgorithms.Helper;
using System;

namespace EvolutionStrategiesTest
{

    [TestClass]
    public class EvolutionStrategyTest
    {
        List<Individual> parents = new List<Individual>();
        Individual ind1 = new Individual(new[] { 2.0, 2.0 }, new[] { (0.0, 10.0), (0.0, 10.0) }, new[] { 3.0, 4.0 });
        Individual ind2 = new Individual(new[] { 4.0, 6.0 }, new[] { (0.0, 10.0), (0.0, 10.0) }, new[] { 3.0, 6.0 });

        [TestMethod]
        public void RecombineNumberOfChildsIsFive()
        {
            parents.Clear();
            parents.Add(ind1);
            parents.Add(ind2);

            List<Individual> childs = EvolutionSteps.Recombine(ref parents, 5, BasicEvolutionStrategyFunctions.IntermediateRecombination, BasicEvolutionStrategyFunctions.IntermediateRecombination);
            Assert.IsTrue(childs.Count == 5);
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException), "Lambda too low")]
        public void RecombineNumberOfChildsIsZero()
        {
            parents.Clear();
            parents.Add(ind1);
            parents.Add(ind2);
            try
            {
                List<Individual> childs = EvolutionSteps.Recombine(ref parents, 0, BasicEvolutionStrategyFunctions.IntermediateRecombination, BasicEvolutionStrategyFunctions.IntermediateRecombination);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Assert.AreEqual(exception.ParamName, "lambda");
            }
        }

        [TestMethod]
        public void RecombineNumberOfChildsIsOne()
        {
            parents.Clear();
            parents.Add(ind1);
            parents.Add(ind2);
            try
            {
                List<Individual> childs = EvolutionSteps.Recombine(ref parents, 1, BasicEvolutionStrategyFunctions.IntermediateRecombination, BasicEvolutionStrategyFunctions.IntermediateRecombination);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Assert.AreEqual(exception.ParamName, "lambda");
            }
        }

        [TestMethod]
        public void RecombineUnequalIntervals()
        {
            Individual ind1 = new Individual(new[] { 2.0, 2.0 }, new[] { (0.0, 9.0), (0.0, 10.0) }, new[] { 0.0, 3.0 });
            Individual ind2 = new Individual(new[] { 4.0, 6.0 }, new[] { (0.0, 10.0), (0.0, 10.0) }, new[] { 0.0, 3.0 });
            parents.Clear();
            parents.Add(ind1);
            parents.Add(ind2);

            List<Individual> childs = EvolutionSteps.Recombine(ref parents, 5, BasicEvolutionStrategyFunctions.IntermediateRecombination, BasicEvolutionStrategyFunctions.IntermediateRecombination);
            Assert.IsTrue(Individual.searchInterval[0].max == 10.0);
        }

        [TestMethod]
        public void RecombineResultOfIntermediateRecombination()
        {
            parents.Clear();
            parents.Add(ind1);
            parents.Add(ind2);
            List<Individual> childs = EvolutionSteps.Recombine(ref parents, 5, BasicEvolutionStrategyFunctions.IntermediateRecombination, BasicEvolutionStrategyFunctions.IntermediateRecombination);
            foreach (Individual child in childs)
            {
                Assert.IsTrue(child.genes[0] == 3.0);
                Assert.IsTrue(child.genes[1] == 4.0);
                Assert.IsTrue(child.sigma[0] == 3.0);
                Assert.IsTrue(child.sigma[1] == 5.0);
            }
        }
    }
}
