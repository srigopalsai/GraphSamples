using System;
using System.Collections.Generic;

namespace GraphSamples
{
    /*
     An edge in an undirected connected graph is a bridge iff removing it disconnects the graph
     http://www.geeksforgeeks.org/bridge-in-a-graph/
     Input : 2, 1
             3, 1
             1, 4
             4, 5
             5, 1

     Output:     2, 1 and 3,1
     */

    public partial class GeneralSamples
    {
        public HashSet<Edge> GetAllBridges(Graph graph)
        {
            HashSet<Edge> result = new HashSet<Edge>();
            Dictionary<Vertex, int> discovery = new Dictionary<Vertex, int>();
            Dictionary<Vertex, int> low = new Dictionary<Vertex, int>();
            Dictionary<Vertex, Vertex> parent = new Dictionary<Vertex, Vertex>();
            Dictionary<Vertex, bool> visited = new Dictionary<Vertex, bool>();

            foreach (KeyValuePair<long, Vertex> vertexPair in graph.Verticies)
            {
                if (!visited.ContainsKey(vertexPair.Value))
                {
                    BridgeUtil(vertexPair.Value, result, discovery, low, parent, visited);
                }
            }

            return result;
        }

        // A DFS Helper
        private void BridgeUtil(Vertex vertex, HashSet<Edge> result, Dictionary<Vertex, int> discovery,
                Dictionary<Vertex, int> low, Dictionary<Vertex, Vertex> parent, Dictionary<Vertex, bool> visited)
        {
            visited[vertex] = true;
            discovery[vertex] = time;
            low[vertex] = time;
            time++;

            foreach (Vertex neighbor in vertex.Adjacents)
            {
                if (!visited.ContainsKey(neighbor))
                {
                    parent[neighbor] = vertex;
                    BridgeUtil(neighbor, result, discovery, low, parent, visited);

                    if (discovery[vertex] < low[neighbor])
                    {
                        result.Add(new Edge(vertex, neighbor));
                    }

                    low[vertex] = Math.Min(discovery[vertex], low[neighbor]);
                }
                else
                {
                    if (!neighbor.Equals(parent[vertex]))
                    {
                        low[vertex] = Math.Min(discovery[vertex], low[neighbor]);
                    }
                }
            }
        }

        public void GetAllBridgesTest()
        {
            Graph graph = new Graph(false);
            graph.AddEdge(2, 1);
            graph.AddEdge(3, 1);
            graph.AddEdge(1, 4);
            graph.AddEdge(4, 5);
            graph.AddEdge(5, 1);

            graph.Display();

            HashSet<Edge> resultBridges = GetAllBridges(graph);

            foreach (Edge edge in resultBridges)
            {
                Console.WriteLine("Brige Edge is " + edge.Vertex1.Id + " - " + edge.Vertex2.Id);
            }
        }
    }
}