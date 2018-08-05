using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GraphSamples
{
    // DFS https://www.geeksforgeeks.org/find-paths-given-source-destination/
    // BFS https://www.geeksforgeeks.org/print-paths-given-source-destination-using-bfs/
    // DFS Matrix https://www.geeksforgeeks.org/count-possible-paths-source-destination-exactly-k-edges/

    public partial class TraversalSamples
    {
        private int kEdges;

        public void AllPathsFromSourceToDestination(Vertex srcVertex, Vertex destVertex)
        {
            HashSet<Vertex> visitedHSet = new HashSet<Vertex>();
            VisitPathDfsRecursive(srcVertex, destVertex, visitedHSet);
        }

        private void VisitPathDfsRecursive(Vertex srcVertex, Vertex destVertex, HashSet<Vertex> visitedHSet)
        {
            if (visitedHSet.Contains(srcVertex))
                return;

            if (srcVertex.Equals(destVertex))
            {
                foreach (Vertex visitedVertex in visitedHSet)
                {
                    Console.Write(visitedVertex.Id + " ");
                }
                Console.WriteLine(destVertex.Id);

                return;
            }

            visitedHSet.Add(srcVertex);

            foreach (Vertex neighbor in srcVertex.Adjacents)
            {
                VisitPathDfsRecursive(neighbor, destVertex, visitedHSet);
            }

            visitedHSet.Remove(srcVertex);
        }

        // https://www.geeksforgeeks.org/count-possible-paths-source-destination-exactly-k-edges/
        // Number of vertices
        // No Visitor tracking, Time Complexity O(V^k)
        public int CountAllPossiblePathsWithKEdges(int[,] graph, int srcVertex, int destVertex, int kEdges)
        {
            if ((kEdges == 0 && srcVertex == destVertex) || (kEdges == 1 && graph[srcVertex, destVertex] == 1))
            {
                return 1;
            }

            if (kEdges <= 0)
                return 0;

            int count = 0;

            // Visit all adjacents of srcVertext
            for (int adjIndx = 0; adjIndx < graph.GetLength(0); adjIndx++)
            {
                // Check if is adjacent of srcVertex
                if (graph[srcVertex, adjIndx] == 1)
                {
                    count += CountAllPossiblePathsWithKEdges(graph, adjIndx, destVertex, kEdges - 1);
                }
            }

            return count;
        }

        // With visitor back tracking
        public int CountAllPossibleWalksWithKEdges(int[,] graph, int srcVertex, int destVertex, int kEdges, bool[,] visited)
        {
            if ((kEdges == 0 && srcVertex == destVertex) || (kEdges == 1 && graph[srcVertex, destVertex] == 1))
            {
                return 1;
            }

            if (kEdges <= 0 || visited[srcVertex, destVertex] == true)
            {
                return 0;
            }

            visited[srcVertex, destVertex] = true;

            int count = 0;

            // Visit all adjacents of srcVertext
            for (int adjIndx = 0; adjIndx < graph.GetLength(0); adjIndx++)
            {
                // Check if is adjacent of srcVertex
                if (graph[srcVertex, adjIndx] == 1)
                {
                    count += CountAllPossiblePathsWithKEdges(graph, adjIndx, destVertex, kEdges - 1);
                }
            }

            visited[srcVertex, destVertex] = false;

            return count;
        }

        // A Dynamic programming based function // to count walks from u to v with k edges
        public int CountAllPossibleWalksWithKEdgesDP(int[,] graph, int sourceVertex, int targetVertex, int kEdges)
        {
            // The value count[src][dst][edg] will/ store count of possible walks from src to dst with exactly k edges

            int[,,] dpLkUp = new int[graph.GetLength(0), graph.GetLength(1), kEdges + 1];

            for (int edgIndx = 0; edgIndx <= kEdges; edgIndx++)
            {
                for (int srcIndx = 0; srcIndx < graph.GetLength(0); srcIndx++)
                {
                    for (int dstIndx = 0; dstIndx < graph.GetLength(1); dstIndx++)
                    {
                        dpLkUp[srcIndx, dstIndx, edgIndx] = 0;

                        // from base cases
                        if (edgIndx == 0 && srcIndx == dstIndx)
                        {
                            dpLkUp[srcIndx, dstIndx, edgIndx] = 1;
                        }
                        if (edgIndx == 1 && graph[srcIndx, dstIndx] == 1)
                        {
                            dpLkUp[srcIndx, dstIndx, edgIndx] = 1;
                        }

                        if (edgIndx <= 1)
                            continue;

                        // Goto adjacent only when number of edges is more than 1
                        for (int nbrIndx = 0; nbrIndx < graph.GetLength(0); nbrIndx++)
                        {
                            if (graph[srcIndx, nbrIndx] == 1)
                            {
                                dpLkUp[srcIndx, dstIndx, edgIndx] += dpLkUp[nbrIndx, dstIndx, edgIndx - 1];
                            }
                        }
                    }
                }
            }

            return dpLkUp[sourceVertex, targetVertex, kEdges];
        }

        // https://www.geeksforgeeks.org/shortest-path-exactly-k-edges-directed-weighted-graph/
        // Define number of vertices in the graph and infinite value
        // A naive recursive function to count walks from srcVertex to destVertex with k edges
        // Worst Time O(V^k)

        static readonly int INF = int.MaxValue;
        public int ShortestPath(int[,] graph, int srcVertex, int destVertex, int kEdges)
        {
            if (kEdges == 0 && srcVertex == destVertex)
            {
                return 0;
            }

            if (kEdges == 1 && graph[srcVertex, destVertex] != INF)
            {
                return graph[srcVertex, destVertex];
            }

            if (kEdges <= 0)
            {
                return INF;
            }

            int shortPath = INF;

            for (int adjIndx = 0; adjIndx < graph.GetLength(0); adjIndx++)
            {
                if (graph[srcVertex, adjIndx] != INF && srcVertex != adjIndx && destVertex != adjIndx)
                {
                    int recSPath = ShortestPath(graph, adjIndx, destVertex, kEdges - 1);

                    if (recSPath != INF)
                    {
                        shortPath = Math.Min(shortPath, graph[srcVertex, adjIndx] + recSPath);
                    }
                }
            }

            return shortPath;
        }

        public void ShortestPathTest()
        {
            int[,] graph = new int[,]{ {0, 10, 3, 2},
                                     {INF, 0, INF, 7},
                                     {INF, INF, 0, 6},
                                     {INF, INF, INF, 0}
                                   };

            int srcVertex = 0;
            int destVertex = 3;
            int kEdge = 2;

            Console.WriteLine("Weight of the shortest path is " + ShortestPath(graph, srcVertex, destVertex, kEdge));
        }

        int V = 4;
        // A Dynamic programming based function to find the shortest path from u to v with exactly k edges.
        public int ShortestPathDP(int[,] graph, int srcVertex, int destVertex, int kEdges)
        {
            // Table to be filled up using DP. The value sp[i,j,e] will
            // store weight of the shortest path from i to j with exactly k edges
            int[,,] sp = new int[V, V, kEdges + 1];

            // Loop for number of edges from 0 to k

            for (int e = 0; e <= kEdges; e++)
            {
                for (int i = 0; i < V; i++)  // for source
                {
                    for (int j = 0; j < V; j++) // for destination
                    {
                        sp[i, j, e] = INF;

                        if (e == 0 && i == j)
                        {
                            sp[i, j, e] = 0;
                        }

                        if (e == 1 && graph[i, j] != INF)
                        {
                            sp[i, j, e] = graph[i, j];
                        }

                        // go to adjacent only when number of edges is more than 1

                        if (e > 1)
                        {
                            for (int a = 0; a < V; a++)
                            {
                                // There should be an edge from i to a and a should not be same as either i or j

                                if (graph[i, a] != INF && i != a && j != a && sp[a, j, e - 1] != INF)
                                {
                                    sp[i, j, e] = Math.Min(sp[i, j, e], graph[i, a] + sp[a, j, e - 1]);
                                }
                            }
                        }
                    }
                }
            }
            return sp[srcVertex, destVertex, kEdges];
        }

        // https://stackoverflow.com/questions/40709283/count-paths-from-source-to-destination-in-a-matrix-moving-in-all-4-directions
        public int CountPathsFromSourceToDestination(int[,] srcMatrix, int rIndx, int cIndx, bool[,] visited)
        {
            if (rIndx < 0 || cIndx < 0 || rIndx == srcMatrix.GetLength(0) || cIndx == srcMatrix.GetLength(1) || visited[rIndx, cIndx] == true)
                return 0;

            if (rIndx == srcMatrix.GetLength(0) - 1 && cIndx == srcMatrix.GetLength(1) - 1)
                return 1;

            visited[rIndx, cIndx] = true;

            int right = CountPathsFromSourceToDestination(srcMatrix, rIndx, cIndx + 1, visited);
            int down = CountPathsFromSourceToDestination(srcMatrix, rIndx + 1, cIndx, visited);
            int left = CountPathsFromSourceToDestination(srcMatrix, rIndx, cIndx - 1, visited);
            int up = CountPathsFromSourceToDestination(srcMatrix, rIndx - 1, cIndx, visited);

            visited[rIndx, cIndx] = false;

            return left + down + right + up;
        }
    }
}