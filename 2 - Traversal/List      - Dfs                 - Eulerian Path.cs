using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    // https://en.wikipedia.org/wiki/Eulerian_path

    public class EulerianPathAndCircuit
    {
        public enum Eulerian
        {
            NOT_EULERIAN,
            EULERIAN,
            SEMIEULERIAN
        }

        private bool IsConnected(Graph graph)
        {
            Vertex startVertex = null;

            foreach (KeyValuePair<long,Vertex> vertexPair in graph.Verticies)
            {
                if (vertexPair.Value.GetDegree() != 0)
                {
                    startVertex = vertexPair.Value;
                    break;
                }
            }

            if (startVertex == null)
            {
                return true;
            }

            Dictionary<Vertex, bool> visited = new Dictionary<Vertex, bool>();
            DFS(startVertex, visited);

            foreach (KeyValuePair<long,Vertex> vertexPair in graph.Verticies)
            {
                if (vertexPair.Value.GetDegree() != 0 && visited.ContainsKey(vertexPair.Value) == false)
                {
                    return false;
                }
            }
            return true;

        }

        private void DFS(Vertex startVertex, Dictionary<Vertex, bool> visited)
        {
            visited[startVertex] = true;

            foreach (Vertex child in startVertex.Adjacents)
            {
                if (!visited.ContainsKey(child))
                {
                    DFS(child, visited);
                }
            }
        }

        public Eulerian IsEulerian(Graph graph)
        {
            if (!IsConnected(graph))
            {
                return Eulerian.NOT_EULERIAN;
            }

            int oddCnt = 0;

            foreach (KeyValuePair<long,Vertex> vertexPair in graph.Verticies)
            {
                if (vertexPair.Value.GetDegree() != 0 && vertexPair.Value.GetDegree() % 2 != 0)
                {
                    oddCnt++;
                }
            }

            if (oddCnt > 2)
            {
                return Eulerian.NOT_EULERIAN;
            }

            return oddCnt == 0 ? Eulerian.EULERIAN : Eulerian.SEMIEULERIAN;
        }

        public void EulerianPathTest()
        {
            Graph graph = new Graph(false);
            graph.AddSingleVertex(1);
            graph.AddSingleVertex(2);
            graph.AddSingleVertex(3);

            graph.AddEdge(4, 5);
            graph.AddEdge(6, 4);
            graph.AddEdge(5, 6);

            graph.Display();

            Eulerian result = IsEulerian(graph);

            Console.WriteLine("Eulerian Path : " );
            Console.WriteLine(result.ToString());
        }
    }
}