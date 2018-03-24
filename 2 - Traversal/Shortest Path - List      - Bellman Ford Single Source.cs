using System;
using System.Collections.Generic;

namespace GraphSamples
{
    /* Bellman Ford algorithm to find single source shortest path in directed graph.
       It negative edges as well unlike Dijksra's algorithm. If there is negative weight cycle it detects it.

       Time complexity - O(EV)
       Space complexity - O(V)

       References
       https://en.wikipedia.org/wiki/Bellman%E2%80%93Ford_algorithm
       http://www.geeksforgeeks.org/dynamic-programming-set-23-bellman-ford-algorithm/
       */

    public partial class TraversalSamples
    {
        //some random big number is treated as infinity. I m not taking int_MAX as infinity because
        //doing any addition on that causes int overflow
        private static int INFINITY = 10000000;

        public Dictionary<Vertex, int> GetShortestPath(Graph graph, Vertex sourceVertex)
        {
            Dictionary<Vertex, int> distance = new Dictionary<Vertex, int>();
            Dictionary<Vertex, Vertex> parent = new Dictionary<Vertex, Vertex>();

            // Set distance of every vertex to be infinity initially
            foreach (KeyValuePair<long, Vertex> vertexPair in graph.Verticies)
            {
                distance[vertexPair.Value] = INFINITY;
                parent[vertexPair.Value] = null;
            }

            // Set distance of source vertex to be 0
            distance[sourceVertex] = 0;

            int VLen = graph.Verticies.Count;

            // Relax edges repeatedly V - 1 times
            for (int i = 0; i < VLen - 1; i++)
            {
                foreach (Edge edge in graph.Edges)
                {
                    Vertex u = edge.Vertex1;
                    Vertex v = edge.Vertex2;

                    // Relax the edge if we get better distance to v via u then use this distance and set u as parent of v.
                    if (distance[u] + edge.Weight < distance[v])
                    {
                        distance[v] = distance[u] + edge.Weight;
                        parent[v] = u;
                    }
                }
            }

            // Relax all edges again. If we still get lesser distance it means
            // There is negative weight cycle in the graph. Throw exception in that case
            foreach (Edge edge in graph.Edges)
            {
                Vertex u = edge.Vertex1;
                Vertex v = edge.Vertex2;

                if (distance[u] + edge.Weight < distance[v])
                    Console.WriteLine("Negative Weight Cycle Exception ");
            }
            return distance;
        }

        public void BellmanFordShortestPathTest()
        {
            Graph graph = new Graph(false);

            graph.AddEdge(0, 3, 8);
            graph.AddEdge(0, 1, 4);
            graph.AddEdge(0, 2, 5);
            graph.AddEdge(1, 2, -3);
            graph.AddEdge(2, 4, 4);
            graph.AddEdge(3, 4, 2);
            graph.AddEdge(4, 3, 1);

            graph.Display();

            Vertex startVertex = graph.Verticies[0];
            Dictionary<Vertex, int> distance = GetShortestPath(graph, startVertex);

            foreach (KeyValuePair<Vertex, int> vPair in distance)
                Console.WriteLine("Shortest Distance from " + vPair.Key + " is " + vPair.Value);
        }
    }
}