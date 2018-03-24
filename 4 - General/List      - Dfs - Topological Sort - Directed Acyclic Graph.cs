using System;
using System.Collections.Generic;

namespace GraphSamples
{
    /* Given a DAG, do a topological sort on this graph.
    Do DFS by keeping visited. 
    Put the vertex which are completely explored into a stack and Pop from stack to get sorted order.
    Space and time complexity is O(n).
    https://en.wikipedia.org/wiki/Topological_sorting
     */
    public partial class GeneralSamples
    {
        public Queue<Vertex> TopologicalSort(Graph graph)
        {
            Queue<Vertex> vQueue = new Queue<Vertex>();
            HashSet<Vertex> visited = new HashSet<Vertex>();

            foreach (KeyValuePair<long,Vertex> vertexPair in graph.Verticies)
            {
                if (visited.Contains(vertexPair.Value))
                {
                    continue;
                }

                TopSortUtil(vertexPair.Value, vQueue, visited);
            }
            return vQueue;
        }

        private void TopSortUtil(Vertex vertex, Queue<Vertex> vQueue, HashSet<Vertex> visited)
        {
            visited.Add(vertex);

            foreach (Vertex childVertex in vertex.Adjacents)
            {
                if (visited.Contains(childVertex))
                {
                    continue;
                }
                TopSortUtil(childVertex, vQueue, visited);
            }
            vQueue.Enqueue(vertex);
        }

        public void TopologicalSortTest()
        {
            Graph graph = new Graph(true);
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 2);
            graph.AddEdge(3, 4);
            graph.AddEdge(5, 6);
            graph.AddEdge(6, 3);
            graph.AddEdge(3, 8);
            graph.AddEdge(8, 11);

            Queue<Vertex> result = TopologicalSort(graph);

            while (result.Count > 0)
            {
                Console.WriteLine(result.Dequeue().Id);
            }
        }
    }
}