﻿using System;
using System.Collections.Generic;

namespace GraphSamples
{
    // Is a directed graph with no directed cycles. 
    // I.e. It is formed by a collection of vertices and directed edges, each edge connecting one vertex to another.
    // Such that there is no way to start at some vertex v and follow a sequence of edges that eventually loops back to v again.
    
    // https://en.wikipedia.org/wiki/Directed_acyclic_graph
    // http://www.geeksforgeeks.org/shortest-path-for-directed-acyclic-graphs/

    public partial class TraversalSamples
    {
        public Dictionary<Vertex, int> BfsShortestPathForDAC(Graph graph, Vertex startVertex)
        {
            Dictionary<Vertex, int> distance = new Dictionary<Vertex, int>();
            GeneralSamples generalSamples = new GeneralSamples();

            Queue<Vertex> deque = generalSamples.TopologicalSort(graph);
            distance[startVertex] = 0;

            while (deque.Count > 0)
            {
                Vertex vertex = deque.Dequeue();

                foreach (Edge edge in vertex.Edges)
                {
                    int v1Distance = distance.ContainsKey(edge.Vertex1) ? distance[vertex] : 1000;
                    int v2Distance = distance.ContainsKey(edge.Vertex2) ? distance[vertex] : 1000;

                    if (v2Distance > v1Distance + edge.Weight)
                    {
                        distance[edge.Vertex2] = v1Distance + edge.Weight;
                    }
                }
            }

            return distance;
        }

        public void ShortestPathDAGTest()
        {
            Graph graph = new Graph(true);
            graph.AddEdge(1, 2, 4);
            graph.AddEdge(2, 3, 3);
            graph.AddEdge(2, 4, 2);
            graph.AddEdge(1, 3, 2);
            graph.AddEdge(3, 5, 1);
            graph.AddEdge(4, 5, 5);
            graph.AddEdge(5, 6, 2);
            graph.AddEdge(4, 7, 3);

            graph.Display();

            Console.WriteLine("ShortestPath for Directed Acyclic Graph Test");
            Vertex srcVertex = graph.Verticies[0];

            Dictionary<Vertex, int> distance = BfsShortestPathForDAC(graph, srcVertex);

            foreach (KeyValuePair<Vertex, int> vertexPath in distance)
            {
                Console.WriteLine(srcVertex.Id + " to " + vertexPath.Key.Id + " " + vertexPath.Value);
            }
        }
    }
}