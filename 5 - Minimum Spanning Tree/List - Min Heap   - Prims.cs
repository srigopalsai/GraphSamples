using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    /*  Space complexity - O(E + V)
        Time complexity - O(ElogV)

        References        https://en.wikipedia.org/wiki/Prim%27s_algorithm
    */

    public partial class MSTSamples
    {
        public List<Edge> PrimsMST(Graph graph)
        {
            BinaryMinHeap<Vertex> minHeap = new BinaryMinHeap<Vertex>();

            // Dictionary of verticies to edge which gave minimum weight to this vertex.
            Dictionary<Vertex, Edge> vertexToEdgeDictionary = new Dictionary<Vertex, Edge>();

            List<Edge> result = new List<Edge>();

            // Insert all vertices with infinite value initially.
            foreach (KeyValuePair<long,Vertex> vertexPair in graph.Verticies)
            {
                minHeap.Add(vertexPair.Value, int.MaxValue);
            }

            // Start from any random vertex
            Vertex startVertex = graph.Verticies[0];

            // For the start vertex decrease the value in heap, Dictionary to 0
            minHeap.DecreaseWeight(startVertex, 0);

            // Iterate till heap, Dictionary has elements in it
            while (!minHeap.IsEmpty())
            {
                // Extract min value vertex from heap + Dictionary
                Vertex current = minHeap.ExtractMin();

                // Get the corresponding edge for this vertex if present and add it to final result.
                // This edge wont be present for first vertex.
                Edge spanningTreeEdge = vertexToEdgeDictionary[current];

                if (spanningTreeEdge != null)
                {
                    result.Add(spanningTreeEdge);
                }

                foreach (Edge edge in current.Edges)
                {
                    Vertex adjacent = edge.Vertex1.Equals(current) ? edge.Vertex2 : edge.Vertex1;

                    // Check if adjacent vertex exist in heap + map and weight attached with this vertex is greater than this edge weight
                    if (minHeap.Contains(adjacent) && minHeap.GetWeightByKey(adjacent) > edge.Weight)
                    {
                        // Decrease the value of adjacent vertex to this edge weight.
                        minHeap.DecreaseWeight(adjacent, edge.Weight);
                        
                        // Add vertex->edge mapping in the graph.
                        vertexToEdgeDictionary[adjacent] = edge;
                    }
                }
            }
            return result;
        }

        public void PrimsMSTTest()
        {
            Graph graph = new Graph(false);

            /* graph.AddEdge(0, 1, 4);
               graph.AddEdge(1, 2, 8);
               graph.AddEdge(2, 3, 7);
               graph.AddEdge(3, 4, 9);
               graph.AddEdge(4, 5, 10);
               graph.AddEdge(2, 5, 4);
               graph.AddEdge(1, 7, 11);
               graph.AddEdge(0, 7, 8);
               graph.AddEdge(2, 8, 2);
               graph.AddEdge(3, 5, 14);
               graph.AddEdge(5, 6, 2);
               graph.AddEdge(6, 8, 6);
               graph.AddEdge(6, 7, 1);
               graph.AddEdge(7, 8, 7);*/

            graph.AddEdge(1, 2, 3);
            graph.AddEdge(2, 3, 1);
            graph.AddEdge(3, 1, 1);
            graph.AddEdge(1, 4, 1);
            graph.AddEdge(2, 4, 3);
            graph.AddEdge(4, 5, 6);
            graph.AddEdge(5, 6, 2);
            graph.AddEdge(3, 5, 5);
            graph.AddEdge(3, 6, 4);

            graph.Display();

            List<Edge> edges = PrimsMST(graph);
            foreach (Edge edge in edges)
            {
                Console.WriteLine(edge.Vertex1.Id + " to " + edge.Vertex2.Id + " " + edge.Weight);
            }
        }
    }
}