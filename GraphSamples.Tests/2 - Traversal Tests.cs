using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static GraphSamples.NPSamples;

namespace GraphSamples.Tests
{
    [TestClass]
    public class TraversalTests
    {
        TraversalSamples traversalSamples = new TraversalSamples();
        public TestContext TestContext { get; set; }
        [TestMethod]
        [TestCategory("Dfs")]
        [TestCategory("Traversal")]
        public void IterativeDeepingDfsListTest()
        {
            Graph graph = new Graph(true);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 5);
            graph.AddEdge(2, 6);

            Vertex src = graph.Edges[0].Vertex1;
            Vertex target = graph.Edges[graph.Edges.Count - 1].Vertex2;
            int maxDepth = 3;

            bool canReach = traversalSamples.IterativeDeepingDfs(src, target, maxDepth);
            Console.WriteLine(string.Format("Target {0} is reachable from source {1} within max depth {2}",
                                src.Id, target.Id, maxDepth));
        }

        [TestMethod]
        [TestCategory("Bfs")]
        [TestCategory("Traversal")]
        public void ListTraversalTest()
        {
            Graph graph = new Graph(true);

            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 6);
            graph.AddEdge(6, 5);
            graph.AddEdge(5, 1);
            graph.AddEdge(5, 3);

            traversalSamples.BfsIterative(graph.Verticies[0]);
        }

        [TestMethod]
        public void FindMinMovesInSnakeAndLadderTest()
        {
            int size = 36;

            int[] board = new int[size];

            for (int indx = 0; indx < size; indx++)
            {
                board[indx] = -1;
            }

            //ladders
            board[2] = 15;
            board[14] = 24;
            board[20] = 31;

            // Snakes
            board[11] = 1;
            board[29] = 3;
            board[34] = 21;
            SnakeAndLadder snakeAndLadder = new SnakeAndLadder();
            Console.WriteLine("Minimum Dice throws needed to reach to end: " + snakeAndLadder.FindMinMovesInSnakeAndLadder(board));
        }

        [TestMethod]
        public void MaxFlowByEdmondsKarpAlgorithmTest()
        {
            int[,] capacity = {{0, 3, 0, 3, 0, 0, 0},
                            {0, 0, 4, 0, 0, 0, 0},
                            {3, 0, 0, 1, 2, 0, 0},
                            {0, 0, 0, 0, 2, 6, 0},
                            {0, 1, 0, 0, 0, 0, 1},
                            {0, 0, 0, 0, 0, 0, 9},
                            {0, 0, 0, 0, 0, 0, 0}};

            Console.WriteLine("\nMaximum capacity " + traversalSamples.MaxFlowByEdmondsKarpAlgorithm(capacity, 0, 6));
        }

        [TestMethod]
        public void AllPathsFromSourceToDestinationTest()
        {
            Graph graph = new Graph(true);

            graph.AddEdge(10, 20);
            graph.AddEdge(10, 30);
            graph.AddEdge(20, 40);
            graph.AddEdge(20, 50);
            graph.AddEdge(30, 60);
            graph.AddEdge(50, 60);
            graph.AddEdge(50, 70);
            graph.AddEdge(60, 70);
            graph.AddEdge(40, 70);
            graph.AddEdge(10, 80);
            graph.AddEdge(80, 90);
            graph.AddEdge(90, 10);

            graph.Display();

            Vertex srcVertex = graph.GetVertexById(10);
            Vertex destVertex = graph.GetVertexById(70);

            Console.WriteLine("All paths from given source " + srcVertex.Id + " to given destination " + destVertex.Id);
            traversalSamples.AllPathsFromSourceToDestination(srcVertex, destVertex);
        }

        [TestMethod]
        public void CountAllPossibleWalksWithKEdgesTest()
        {
            //int[,] graph = new int[,] { {0, 1, 1, 1},
            //                            {0, 0, 0, 1},
            //                            {0, 0, 0, 1},
            //                            {0, 0, 0, 0} };

                                      // A  B  C  D  E  F  G  H  I  J
            int[,] graph = new int[,] { {0, 1, 1, 0, 1, 0, 0, 1, 1, 0},   // A
                                        {1, 0, 1, 1, 1, 0, 0, 0, 0, 0},   // B
                                        {1, 1, 0, 1, 0, 1, 0, 0, 0, 0},   // C
                                        {0, 1, 1, 0, 1, 0, 1, 0, 0, 0},   // D
                                        {1, 1, 0, 1, 0, 1, 1, 0, 0, 0},   // E
                                        {0, 0, 1, 0, 1, 0, 1, 0, 0, 0},   // F
                                        {0, 0, 0, 1, 1, 1, 0, 0, 0, 0},   // G 
                                        {1, 0, 0, 0, 0, 0, 0, 0, 1, 0},   // H
                                        {1, 0, 0, 0, 0, 0, 0, 1, 0, 0},   // I
                                        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0} }; // J

            int srcVertex = 0;
            int destVertex = 4;
            int k = 5;   // When K = 1,2, 3 - Result 1

            //Console.Write(traversalSamples.CountAllPossibleWalksWithKEdges(graph, srcVertex, destVertex, k));

            bool[,] visited = new bool[graph.GetLength(0), graph.GetLength(1)];
            Console.Write(traversalSamples.CountAllPossibleWalksWithKEdges(graph, srcVertex, destVertex, k, visited));
        }

        [TestMethod]
        public void CountAllPossibleWalksWithKEdges2Test()
        {
            int[,] graph = { {0, 1, 1, 1},
                             {0, 0, 0, 1},
                             {0, 0, 0, 1},
                             {0, 0, 0, 0} };

            int srcVertex = 0;
            int dstVertex = 3;
            int kEdges = 2;

            int noOfPaths = traversalSamples.CountAllPossibleWalksWithKEdges2(graph, srcVertex, dstVertex, kEdges);
            Console.WriteLine("No of paths from source to destination " + noOfPaths);
        }

        [TestMethod]
        public void EulerianPathTest()
        {
            Graph graph = new Graph(false);
            graph.AddSingleVertex(1);
            graph.AddSingleVertex(2);
            graph.AddSingleVertex(3);

            graph.AddEdge(1, 4);
            graph.AddEdge(4, 5);
            graph.AddEdge(6, 4);
            graph.AddEdge(5, 6);

            graph.Display();

            TraversalSamples.Eulerian result = traversalSamples.IsEulerianPath(graph);
            
            Console.WriteLine("Eulerian Path : ");
            Console.WriteLine(result.ToString());
        }

        [TestMethod]
        public void GabowShortestPathTest()
        {
            Console.WriteLine("Gabow algorithm Test\n");
            Console.WriteLine("Enter number of Vertices");

            /** number of vertices **/
            int V = Convert.ToInt32(Console.ReadLine());

            List<int>[] g = new List<int>[V];

            for (int i = 0; i < V; i++)
                g[i] = new List<int>();

            Console.WriteLine("\nEnter number of edges");
            int E = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter " + E + " x, y coordinates");

            for (int i = 0; i < E; i++)
            {
                int x = Convert.ToInt32(Console.ReadLine());
                int y = Convert.ToInt32(Console.ReadLine());
                g[x].Add(y);
            }

            Console.WriteLine("\nSCC : ");

            List<List<int>> scComponents = traversalSamples.GetSCComponents(g);
            Console.WriteLine(scComponents);
        }
    }
}