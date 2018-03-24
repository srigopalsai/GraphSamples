using System;
using System.Collections.Generic;

namespace GraphSamples
{
    /*
    https://en.wikipedia.org/wiki/Minimum_spanning_tree
    https://en.wikipedia.org/wiki/Kruskal%27s_algorithm
    Time  Complexity - O(E log E)
    Space Complexity - O(E + V)
    */

    public partial class MSTSamples
    {
        public class EdgeComparator : IComparer<Edge>
        {
            public int Compare(Edge edge1, Edge edge2)
            {
                if (edge1.Weight <= edge2.Weight)
                    return -1;
                else
                    return 1;
            }
        }

        public List<Edge> GetKruskalsMST(Graph graph)
        {
            graph.Edges.Sort(new EdgeComparator());

            DisjointSet disjointSet = new DisjointSet();

            // Create as many disjoint sets as the total vertices
            foreach (KeyValuePair<long, Vertex> vertexPair in graph.Verticies)
            {
                disjointSet.MakeSet(vertexPair.Value.Id);
            }

            List<Edge> resultEdges = new List<Edge>();

            foreach (Edge edge in graph.Edges)
            {
                // Get the sets of two vertices of the edge
                long root1 = disjointSet.FindSet(edge.Vertex1.Id);
                long root2 = disjointSet.FindSet(edge.Vertex2.Id);

                // Check if the vertices are in same set or different set
                // If verties are in same set then ignore the edge
                if (root1 == root2)
                {
                    continue;
                }
                else
                {
                    // If vertices are in different set then add the edge to result and union these two sets into one
                    resultEdges.Add(edge);
                    disjointSet.Union(edge.Vertex1.Id, edge.Vertex2.Id);
                }
            }
            return resultEdges;
        }

        public void MSTKruskalsAlgorithmTest()
        {
            Graph graph = new Graph(false);
            graph.AddEdge(1, 2, 4);
            graph.AddEdge(1, 3, 1);
            graph.AddEdge(2, 5, 1);
            graph.AddEdge(2, 6, 3);
            graph.AddEdge(2, 4, 2);
            graph.AddEdge(6, 5, 2);
            graph.AddEdge(6, 4, 3);
            graph.AddEdge(4, 7, 2);
            graph.AddEdge(3, 4, 5);
            graph.AddEdge(3, 7, 8);

            graph.Display();
            List<Edge> result = GetKruskalsMST(graph);

            foreach (Edge edge in result)
            {
                Console.WriteLine(edge.Vertex1 + " " + edge.Vertex2);
            }
        }
    }
}