using System;
using System.Collections.Generic;

namespace GraphSamples
{
     //Find single source shortest path using Dijkstra's algorithm 
     //Space Complexity - O(E + V)
     //Time  Complexity - O(E log V)

     //https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm

    public partial class TraversalSamples
    {
        private const int NO_PARENT = -1;

        private static void DijkstraSingleSourceShortestPath(int[,] adjacencyMatrix, int startVertex)
        {
            int nVertices = adjacencyMatrix.GetLength(0);

            int[] shortestDistances = new int[nVertices];
            // added[i] will true if vertex i is included / in shortest path tree
            // or shortest distance from src to i is readonlyized
            bool[] added = new bool[nVertices];

            // Initialize all distances as INFINITE and added[] as false

            for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
            {
                shortestDistances[vertexIndex] = int.MaxValue;
                added[vertexIndex] = false;
            }

            // Distance of source vertex from itself is always 0
            shortestDistances[startVertex] = 0;

            // Parent array to store shortest path tree
            int[] parents = new int[nVertices];

            // The starting vertex does not have a parent
            parents[startVertex] = NO_PARENT;

            // Find shortest path for all vertices

            for (int indx = 1; indx < nVertices; indx++)
            {
                // Pick the minimum distance vertex from the set of vertices not yet processed. 
                // nearestVertex is always equal to startNode in first iteration.

                int nearestVertex = -1;
                int shortestDistance = int.MaxValue;

                for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
                {
                    if (!added[vertexIndex] && shortestDistances[vertexIndex] < shortestDistance)
                    {
                        nearestVertex = vertexIndex;
                        shortestDistance = shortestDistances[vertexIndex];
                    }
                }

                // Mark the picked vertex as processed
                added[nearestVertex] = true;

                // Update dist value of the adjacent vertices of the picked vertex.

                for (int vertexIndex = 0;   vertexIndex < nVertices; vertexIndex++)
                {
                    int edgeDistance = adjacencyMatrix[nearestVertex, vertexIndex];

                    if (edgeDistance > 0  && ((shortestDistance + edgeDistance) <  shortestDistances[vertexIndex]))
                    {
                        parents[vertexIndex] = nearestVertex;
                        shortestDistances[vertexIndex] = shortestDistance + edgeDistance;
                    }
                }
            }

            printSolution(startVertex, shortestDistances, parents);
        }

        // A utility function to print the constructed distances array and shortest paths

        private static void printSolution(int startVertex, int[] distances, int[] parents)
        {
            int nVertices = distances.Length;
            Console.Write("Vertex\t Distance\tPath");

            for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
            {
                if (vertexIndex != startVertex)
                {
                    Console.Write("\n" + startVertex + " -> ");
                    Console.Write(vertexIndex + " \t\t ");
                    Console.Write(distances[vertexIndex] + "\t\t");

                    printPath(vertexIndex, parents);
                }
            }
        }

        // Function to print shortest path from source to currentVertex using parents array

        private static void printPath(int currentVertex, int[] parents)
        {
            // Base case : Source node has been processed

            if (currentVertex == NO_PARENT)
            {
                return;
            }

            printPath(parents[currentVertex], parents);

            Console.Write(currentVertex + " ");
        }

        public static void DijkstraSingleSourceShortestPathTest()
        {
            int[,] adjacencyMatrix = {  { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
                                        { 4, 0, 8, 0, 0, 0, 0, 11, 0 },
                                        { 0, 8, 0, 7, 0, 4, 0, 0, 2 },
                                        { 0, 0, 7, 0, 9, 14, 0, 0, 0 },
                                        { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
                                        { 0, 0, 4, 0, 10, 0, 2, 0, 0 },
                                        { 0, 0, 0, 14, 0, 2, 0, 1, 6 },
                                        { 8, 11, 0, 0, 0, 0, 1, 0, 7 },
                                        { 0, 0, 2, 0, 0, 0, 6, 7, 0 } };

            DijkstraSingleSourceShortestPath(adjacencyMatrix, 0);
        }
    }
}