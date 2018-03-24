using System;

namespace GraphSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            TraversalSamplesTest();
            //NPSamplesTest();
            //GeneralSamplesTest();
            //CycleSamplesTest();

            //Graph2 adjList = new Graph2(2);// 4);
            //adjList.IsDirectedAcyclicGraphTest();

            Console.ReadLine();
        }

        private static void TraversalSamplesTest()
        {
            TraversalSamples traversalSamples = new TraversalSamples();
            traversalSamples.AllPathsFromSourceToDestinationTest();
            //traversalSamples.FWShortestPathTest();
            //traversalSamples.DijkstraShortestPathTest();
            //traversalSamples.IsConnectedByDfsIterativeTest();
        }

        private static void CycleSamplesTest()
        {
            CycleSamples cycleSamples = new CycleSamples();
            //cycleSamples.DetectAllCyclesJSCTest(); // Failing
            //cycleSamples.HasCycleInDirectedTest();
            //cycleSamples.HasCycleInUnDirectedTest();
        }

        private static void GeneralSamplesTest()
        {
            GeneralSamples generalSamples = new GeneralSamples();
            //genSamples.NumberOfIslandsTest();
            //genSamples.TopologicalSortTest();
            //genSamples.TarganStronglyConnectedComponentsTest();
            //genSamples.FindLaddersTest();
            //genSamples.GetAllBridgesTest();
        }

        private static void NPSamplesTest()
        {
            NPSamples npSamples = new NPSamples();
            npSamples.TSPUsingNNATest();
            //npSamples.FindGoodFeedbackSetTest();
            //npSamples.LongestPathInDAGTest();
            //npSamples.HamiltonianCycleMatrixTest();
        }
    }
}