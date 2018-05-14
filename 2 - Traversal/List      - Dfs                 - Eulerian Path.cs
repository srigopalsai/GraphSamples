using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    // https://en.wikipedia.org/wiki/Eulerian_path
    // EulerianPathAndCircuit
    public partial class TraversalSamples
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
            EulerianByDFS(startVertex, visited);

            foreach (KeyValuePair<long,Vertex> vertexPair in graph.Verticies)
            {
                if (vertexPair.Value.GetDegree() != 0 && visited.ContainsKey(vertexPair.Value) == false)
                {
                    return false;
                }
            }
            return true;

        }

        private void EulerianByDFS(Vertex startVertex, Dictionary<Vertex, bool> visited)
        {
            visited[startVertex] = true;

            foreach (Vertex child in startVertex.Adjacents)
            {
                if (!visited.ContainsKey(child))
                {
                    EulerianByDFS(child, visited);
                }
            }
        }

        public Eulerian IsEulerianPath(Graph graph)
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
    }
}