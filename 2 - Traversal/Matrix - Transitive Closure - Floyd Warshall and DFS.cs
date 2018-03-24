using System;

namespace GraphSamples
{
    /*
     Constructing a data structure that makes it possible to answer reachability questions. 
 
     Given a directed graph, find out if a vertex j is reachable from another vertex i for all vertex pairs (i, j) in the given graph. 
     Here reachable mean that there is a path from vertex i to j. 
     The reach-ability matrix is called transitive closure of a graph.

     Time Complexity:
     Using Floyd Warshall: O(V^3) where V is number of vertices in the given graph.
     Using DFS : O(V^2) solution.
     https://en.wikipedia.org/wiki/Reachability
     http://www.geeksforgeeks.org/transitive-closure-of-a-graph/
     http://www.geeksforgeeks.org/transitive-closure-of-a-graph-using-dfs/
    */

    public partial class TraversalSamples
    {
        public bool[,] GetTransitiveClosureByFloydWarshall(int[,] graph)
        {
            int rowLen = graph.GetLength(0);
            int colLen = graph.GetLength(1);

            bool[,] result = new bool[rowLen, colLen];

            for (int rIndx = 0; rIndx < rowLen; rIndx++)
            {
                for (int cIndx = 0; cIndx < colLen; cIndx++)
                {
                    if (graph[rIndx, cIndx] != 100)
                    {
                        result[rIndx, cIndx] = true;
                    }
                }
            }

            for (int rIndx = 0; rIndx < rowLen; rIndx++)
            {
                for (int cIndx = 0; cIndx < rowLen; cIndx++)
                {
                    for (int k = 0; k < rowLen; k++)
                    {
                        result[rIndx, cIndx] = result[rIndx, cIndx] || (result[rIndx, k] && result[k, cIndx]);
                    }
                }
            }
            return result;
        }

        public void GetTransitiveClosureTest()
        {
            int[,] graph = { { 0, 2, 2, 4, 100 }, 
                            { 100, 0, 100, 1, 100 }, 
                            { 100, 100, 0, 3, 100 }, 
                            { 100, 100, 3, 0, 2 }, 
                            { 100, 3, 100, 100, 0 } };

            bool[,] result = GetTransitiveClosureByFloydWarshall(graph);

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    Console.Write(result[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}        