using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples.General
{
    /*
      Find articulation points in connected undirected graph.
      Articulation points are vertices such that removing any one of them disconnects the graph.
     
      We need to do DFS of this graph and keep visitedTime and lowTime for each vertex.
      lowTime is keeps track of back edges.
     
      If any one of following condition meets then vertex is articulation point.
     
      1) If vertex is root of DFS and has atlesat 2 independent children.(By independent it means they are
      not connected to each other except via this vertex). This condition is needed because if we
      started from corner vertex it will meet condition 2 but still is not an articulation point. To filter
      out those vertices we need this condition.
     
      2) It is not root of DFS and if visitedTime of vertex <= lowTime of any adjacent vertex then its articulation point.
     
      Time complexity is O(E + V)
      Space complexity is O(V)
     
      References:
      https://en.wikipedia.org/wiki/Biconnected_component
      http://www.geeksforgeeks.org/articulation-points-or-cut-vertices-in-a-graph/
     */

    public partial class GeneralSamples
    {
        private int time;

        public HashSet<Vertex> FindarticulationPoints(Graph graph)
        {
            time = 0;
            Vertex startVertex = graph.Verticies[0];

            HashSet<Vertex> visited = new HashSet<Vertex>();
            HashSet<Vertex> articulationPoints = new HashSet<Vertex>();

            Dictionary <Vertex, int> visitedTime = new Dictionary<Vertex, int>();
            Dictionary<Vertex, int> lowTime = new Dictionary<Vertex, int>();
            Dictionary<Vertex, Vertex> parent = new Dictionary<Vertex, Vertex>();

            DFS(visited, articulationPoints, startVertex, visitedTime, lowTime, parent);

            return articulationPoints;
        }

        private void DFS(   HashSet<Vertex> visited,
                            HashSet<Vertex> articulationPoints, 
                            Vertex vertex,
                            Dictionary<Vertex, int> visitedTime,
                            Dictionary<Vertex, int> lowTime, 
                            Dictionary<Vertex, Vertex> parent)
        {
            visited.Add(vertex);
            visitedTime[vertex] = time;
            lowTime[vertex] = time;
            time++;

            int childCount = 0;
            bool isArticulationPoint = false;

            foreach (Vertex adj in vertex.Adjacents)
            {
                //if adj is same as parent then just ignore this vertex.
                if (adj.Equals(parent[vertex]))
                    continue;

                //if adj has not been visited then visit it.
                if (!visited.Contains(adj))
                {
                    parent[adj] = vertex;
                    childCount++;
                    DFS(visited, articulationPoints, adj, visitedTime, lowTime, parent);

                    if (visitedTime[vertex] <= lowTime[adj])
                    {
                        isArticulationPoint = true;
                    }
                    else
                    {
                        lowTime[vertex] = Math.Min(lowTime[vertex], lowTime[adj]);
                    }
                }
                else
                { 
                    // If adj is already visited see if you can get better low time.
                    lowTime[vertex] = Math.Min(lowTime[vertex], visitedTime[adj]);
                }
            }

            //checks if either condition 1 or condition 2 meets). If yes then it is articulation point.
            if ((parent[vertex] == null && childCount >= 2) || parent[vertex] != null && isArticulationPoint)
            {
                articulationPoints.Add(vertex);
            }
        }

        public void ArticulationPointTest()
        {
            Graph graph = new Graph(false);

            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(4, 5);
            graph.AddEdge(5, 6);
            graph.AddEdge(6, 7);
            graph.AddEdge(7, 5);
            graph.AddEdge(6, 8);

            //bigger example
            /*
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(0, 3);
            graph.AddEdge(0, 4);
            graph.AddEdge(4, 2);
            graph.AddEdge(3, 5);
            graph.AddEdge(4, 6);
            graph.AddEdge(6, 3);
            graph.AddEdge(6, 7);
            graph.AddEdge(6, 8);
            graph.AddEdge(7, 9);
            graph.AddEdge(9, 10);
            graph.AddEdge(8, 10);*/

            graph.Display();

            HashSet<Vertex> artPoints = FindarticulationPoints(graph);

            foreach (Vertex vertex in artPoints)
            {
                Console.WriteLine(vertex.Id + " ");
            }
        }
    }
}
