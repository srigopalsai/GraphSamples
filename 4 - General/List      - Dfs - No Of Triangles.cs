using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    /*  http://www.careercup.com/question?id=5988741646647296
        Given an Undirected graph find number of triangles.
        Find cycle of length 3. Pass parent in DFS search.
        If there is a cycle check if my parent is neighbor of the the node which caused it to be a cycle.
         */
    public partial class GeneralSamples
    {
        public int CountTriangles(Graph graph)
        {
            Dictionary<Vertex, Boolean> visited = new Dictionary<Vertex, bool>();
            int count = 0;
            foreach (KeyValuePair<long, Vertex> vertexPair in graph.Verticies)
            {
                count += DFS(vertexPair.Value, visited, null);
            }
            return count;
        }

        public int DFS(Vertex vertex, Dictionary<Vertex, Boolean> visited, Vertex parent)
        {
            if (visited.ContainsKey(vertex))
                return 0;

            visited[vertex] = true;
            int count = 0;

            foreach (Vertex child in vertex.Adjacents)
            {
                if (child.Equals(parent))
                    continue;

                if (visited.ContainsKey(child))
                {
                    count += IsNeighbor(child, parent) ? 1 : 0;
                }
                else
                {
                    count += DFS(child, visited, vertex);
                }
            }
            return count;
        }

        private bool IsNeighbor(Vertex vertex, Vertex destVertex)
        {
            foreach (Vertex child in vertex.Adjacents)
            {
                if (child.Equals(destVertex))
                {
                    return true;
                }
            }
            return false;
        }

        public void CountTrianglesTest()
        {
            Graph graph = new Graph(false);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(1, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(0, 4);
            graph.AddEdge(0, 3);

            graph.Display();

            Console.WriteLine("Number of Triangles");
            Console.WriteLine(CountTriangles(graph));
        }
    }
}