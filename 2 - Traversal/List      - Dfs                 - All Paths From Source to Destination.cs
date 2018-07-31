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
            if ((kEdges == 0 && srcVertex == destVertex) ||
                 (kEdges == 1 && graph[srcVertex, destVertex] == 1))
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

        // With visitor tracking
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
        public int CountAllPossibleWalksWithKEdges2(int[,] srcGraph, int sourceVertex, int targetVertex, int kEdges)
        {
            // The value count[src][dst][edg] will/ store count of possible walks from src to dst with exactly k edges

            int[,,] dpLkUp = new int[srcGraph.GetLength(0), srcGraph.GetLength(1), kEdges + 1];

            for (int edgIndx = 0; edgIndx <= kEdges; edgIndx++)
            {
                for (int srcIndx = 0; srcIndx < srcGraph.GetLength(0); srcIndx++)
                {
                    for (int dstIndx = 0; dstIndx < srcGraph.GetLength(1); dstIndx++)
                    {
                        dpLkUp[srcIndx, dstIndx, edgIndx] = 0;

                        // from base cases
                        if (edgIndx == 0 && srcIndx == dstIndx)
                        {
                            dpLkUp[srcIndx, dstIndx, edgIndx] = 1;
                        }
                        if (edgIndx == 1 && srcGraph[srcIndx, dstIndx] == 1)
                        {
                            dpLkUp[srcIndx, dstIndx, edgIndx] = 1;
                        }

                        if (edgIndx <= 1)
                            continue;

                        // Goto adjacent only when number of edges is more than 1
                        for (int nbrIndx = 0; nbrIndx < srcGraph.GetLength(0); nbrIndx++)
                        {
                            if (srcGraph[srcIndx, nbrIndx] == 1)
                            {
                                dpLkUp[srcIndx, dstIndx, edgIndx] += dpLkUp[nbrIndx, dstIndx, edgIndx - 1];
                            }
                        }
                    }
                }
            }

            return dpLkUp[sourceVertex, targetVertex, kEdges];
        }

        // https://stackoverflow.com/questions/40709283/count-paths-from-source-to-destination-in-a-matrix-moving-in-all-4-directions
        public int CountPathsFromSourceToDestination(int[,] srcMatrix, int rIndx, int cIndx, bool[,] visited)
        {
            if (rIndx < 0 || cIndx < 0 || rIndx == srcMatrix.GetLength(0) || cIndx == srcMatrix.GetLength(1))
                return 0;

            if (rIndx == srcMatrix.GetLength(0) - 1 && cIndx == srcMatrix.GetLength(1) - 1)
                return 1;

            if (visited[rIndx, cIndx] == true)
                return 0;

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