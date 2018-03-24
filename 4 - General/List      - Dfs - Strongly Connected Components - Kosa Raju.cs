using System;
using System.Collections.Generic;

namespace GraphSamples
{
    /**
     Given a directed graph, find all strongly connected components in the graph using Kosaraju's algorithm. 
 
     Algorithm
     1. Create a order of vertices by finish time in decreasing order.
     2. Reverse the graph
     3. Do a DFS on reverse graph by finish time of vertex and created strongly connected components.
 
     Time  Complexity - O(V + E)
     Space Complexity - O(V)
 
     References
     https://en.wikipedia.org/wiki/Strongly_connected_component
     http://www.geeksforgeeks.org/strongly-connected-components/
     */

    public class StronglyConnectedComponent
    {
        public List<HashSet<Vertex>> StronglyConnectedKosaRaju(Graph graph)
        {
            // Holds vertices by finish time in reverse order.
            Stack<Vertex> stack = new Stack<Vertex>();

            // Holds visited vertices for DFS.
            HashSet<Vertex> visited = new HashSet<Vertex>();

            // Populate stack with vertices with vertex finishing last at the top.
            foreach (KeyValuePair<long,Vertex>  vertexPair in graph.Verticies)
            {
                if (visited.Contains(vertexPair.Value))
                    continue;

                DFSUtil(vertexPair.Value, visited, stack);
            }

            // Reverse the graph.
            Graph reverseGraph = graph.ReverseGraph();

            //Do a DFS based off vertex finish time in decreasing order on reverse graph.
            visited.Clear();

            List<HashSet<Vertex>> result = new List<HashSet<Vertex>>();

            while (stack.Count > 0)
            {
                Vertex vertex = reverseGraph.Verticies[stack.Pop().Id];

                if (visited.Contains(vertex))
                    continue;

                HashSet<Vertex> set = new HashSet<Vertex>();
                DFSUtilForReverseGraph(vertex, visited, set);
                result.Add(set);
            }
            return result;
        }
      
        private void DFSUtil(Vertex vertex, HashSet<Vertex> visited, Stack<Vertex> stack)
        {
            visited.Add(vertex);

            foreach (Vertex curVertex in vertex.Adjacents)
            {
                if (visited.Contains(curVertex))
                    continue;

                DFSUtil(curVertex, visited, stack);
            }
            stack.Push(vertex);
        }

        private void DFSUtilForReverseGraph(Vertex vertex, HashSet<Vertex> visited, HashSet<Vertex> set)
        {
            visited.Add(vertex);
            set.Add(vertex);

            foreach (Vertex v in vertex.Adjacents)
            {
                if (visited.Contains(v))
                    continue;

                DFSUtilForReverseGraph(v, visited, set);
            }
        }

        public void StronglyConnectedKosaRajuTest()
        {
            Graph graph = new Graph(true);
            graph.AddEdge(0, 1);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);
            graph.AddEdge(1, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 5);
            graph.AddEdge(5, 3);
            graph.AddEdge(5, 6);

            graph.Display();

            List<HashSet<Vertex>> result = StronglyConnectedKosaRaju(graph);

            foreach (HashSet<Vertex> vertexSet in result)
            {
                foreach (Vertex vertex in vertexSet)
                    Console.Write(vertex.Id + " ");
                Console.WriteLine();
            }
        }
    }
}