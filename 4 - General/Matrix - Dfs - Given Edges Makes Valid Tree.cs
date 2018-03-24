using System.Collections.Generic;

namespace GraphSamples
{
    /*
     Given n nodes labeled from 0 to n - 1 and a list of undirected edges (each edge is a pair of nodes),
     write a function to check whether these edges make up a valid tree.
     https://leetcode.com/problems/graph-valid-tree/
        
    There are 3 properties to check if a graph is a tree:
    (1) The number of edges in the graph is exactly one less than the number of vertices |E| = |V| - 1
    (2) There are no cycles
    (3) The graph is connected
     */

    public partial class GeneralSamples
    {
        public bool GivenEdgesMakesValidTree(int n, int[,] edgesMatrix)
        {
            if (n <= 1)
                return true;

            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();

            for(int rIndx =0; rIndx < edgesMatrix.GetLength(0); rIndx++)
            {
                List<int> neighbors = graph[edgesMatrix[rIndx, 0]];
                if (neighbors == null)
                {
                    neighbors = new List<int>();
                    graph[edgesMatrix[rIndx, 0]]= neighbors;
                }

                neighbors.Add(edgesMatrix[rIndx, 1]);

                neighbors = graph[edgesMatrix[rIndx, 1]];

                if (neighbors == null)
                {
                    neighbors = new List<int>();   
                    graph[edgesMatrix[rIndx, 1]] = neighbors;
                }

                neighbors.Add(edgesMatrix[rIndx, 0]);
            }

            bool[] visited = new bool[n];
            bool hasCycle = HasCycle(0, graph, -1, visited);

            if (hasCycle)
                return false;

            for (int i = 0; i < visited.Length; i++)
            {
                if (!visited[i])
                    return false;
            }
            return true;
        }

        bool HasCycle(int vertex, Dictionary<int, List<int>> graphDictList, int parent, bool[] visited)
        {
            if (visited[vertex])
                return true;

            visited[vertex] = true;

            if (graphDictList[vertex] == null)
                return false;

            foreach (int i in graphDictList[vertex])
            {
                if (i == parent)
                    continue;

                bool hasCycle = HasCycle(i, graphDictList, vertex, visited);

                if (hasCycle == true)
                    return true;
            }

            return false;
        }
    }
}
