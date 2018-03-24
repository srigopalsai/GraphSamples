using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphSamples
{
    /**
    Longest Path Problem is the problem of finding a simple path of maximum length in a given graph. 
    A path is called simple if it does not have any repeated vertices; 
        the length of a path may either be measured by its number of edges, or (in weighted graphs) by the sum of the weights of its edges. 
    In contrast to the shortest path problem, which can be solved in polynomial time in graphs without negative-weight cycles, 
        the longest path problem is NP-hard, meaning that it cannot be solved in polynomial time for arbitrary graphs unless P = NP. 
    Stronger hardness results are also known showing that it is difficult to approximate. 
    However, it has a linear time solution for directed acyclic graphs, 
        which has important applications in finding the critical path in scheduling problems. 
    https://en.wikipedia.org/wiki/Longest_path_problem
    http://www.sanfoundry.com/java-program-find-longest-path-dag/
    */

    public partial class NPSamples
    {
        public class Vertex
        {
            public int Id { get; set; }                  // Vertex ID, started from 0 to n-1

            public List<int> Predecessors { get; set; }  // Predecessors

            public List<int> Neighbors { get; set; }     // Neighbors

            public List<int> BackwardEdges { get; set; } // Backward edges -vertex is end vertex (int)

            public List<int> ForwardEdges { get; set; }  // Forward edges -vertex is start vertex (int)

            public int PreviousVertex { get; set; }      // Previous vertex on the augmenting path

            public int PreviousEdge { get; set; }        // From which edge this vertex comes on the augmenting path

            public Vertex(int id)
            {
                Id = id;

                BackwardEdges = new List<int>();
                ForwardEdges = new List<int>();

                PreviousVertex = -1;
                PreviousEdge = -1;
            }
        }

        public class Edge
        {
            public int Id { get; set; }             // Edge ID, started from 0 to n-1

            public int StartVertex { get; set; }    // Start vertex of this edge

            public int EndVertex { get; set; }      // End vertex of this edge

            public int Direction { get; set; }      // Forwards (+1) or Backwards (-1) on augmenting path
                                                    // If 0 then not part of augmenting path
            public int Weight { get; set; }         // Capacity

            public int CurrentFlow { get; set; }    // Current Flow

            public Edge(int id)
            {
                Id = id;
                StartVertex = -1;
                EndVertex = -1;
                Direction = 0; // Default is both backwards and forwards
                Weight = 0;
                CurrentFlow = 0;
            }

            public override string ToString()
            {
                return Id + ": s = " + StartVertex + " e = " + EndVertex + " d = " + Direction;
            }
        }

        public class Graph
        {
            public int NoOfNodes { get; set; }  // Number of nodes

            int TargetNode { get; set; }            // Destination vertex

            int MinLength { get; set; }             // The minimal Length of each path

            public List<Vertex> Verticies { get; set; }        // Used to store Nodes

            public List<Edge> Edges { get; set; }   // Used to store Edges

            int[] TemporaryPath { get; set; }       // Used to store temporary path

            int Length { get; set; }                // Length of the path

            int Distance { get; set; }              // Distance of the path

            int[] BestTemporaryPath { get; set; }   // Used to store temporary path

            int BestLongestPath { get; set; }       // Length of the longest path

            public int DistanceOfLongestPath { get; set; }    // Distance of the longest path

            int[] Visited { get; set; }             // Used to mark a vertex as visited if set as 1

            public bool IsDirected { get; set; }

            public Graph(bool isDirected)
            {
                DistanceOfLongestPath = -1000000;
                Edges = new List<Edge>();
                Verticies = new List<Vertex>();
                IsDirected = isDirected;
            }

            public bool FindLongestPath(int begin, int end, int minLen)
            {
                Visited = new int[Verticies.Count];
                TemporaryPath = new int[Verticies.Count];
                BestTemporaryPath = new int[Verticies.Count];

                TargetNode = end;
                DistanceOfLongestPath = -100000000;
                MinLength = minLen;

                DfsLongestPath(begin);

                if (DistanceOfLongestPath == -100000000)
                    return false;
                else
                    return true;
            }

            private void DfsLongestPath(int current)
            {
                Visited[current] = 1;
                TemporaryPath[Length++] = current;

                if (current == TargetNode && Length >= MinLength)
                {
                    if (Distance > DistanceOfLongestPath)
                    {
                        for (int i = 0; i < Length; i++)
                            BestTemporaryPath[i] = TemporaryPath[i];

                        BestLongestPath = Length;
                        DistanceOfLongestPath = Distance;
                    }
                }
                else
                {
                    List<int> fors = Verticies[current].ForwardEdges;

                    for (int i = 0; i < fors.Count; i++)
                    {
                        int edge = fors.ElementAt(i);

                        if (Visited[Edges[edge].EndVertex] == 0)
                        {
                            Distance += Edges[edge].Weight;
                            DfsLongestPath(Edges[edge].EndVertex);
                            Distance -= Edges[edge].Weight;
                        }
                    }
                }

                Visited[current] = 0;
                Length--;
            }

            public override string ToString()
            {
                string output = "v" + BestTemporaryPath[0];

                for (int index = 1; index < BestLongestPath; index++)
                    output = output + " -> v" + BestTemporaryPath[index];

                return output;
            }

            public void AddEdge(int edgeId, int vertexVal1, int vertexVal2, int weight)
            {
                Vertex vertex1 = Verticies.FirstOrDefault(item => item.Id == vertexVal1);
                Edge edge = new Edge(edgeId);

                if (vertex1 != null)
                {
                    vertex1 = Verticies[vertexVal1];
                }
                else
                {
                    vertex1 = new Vertex(vertexVal1);
                    Verticies.Add(vertex1);
                }

                Vertex vertex2 = Verticies.FirstOrDefault(item => item.Id == vertexVal2);

                if (vertex2 != null)
                {
                    vertex2 = Verticies[vertexVal2];
                }
                else
                {
                    vertex2 = new Vertex(vertexVal2);
                    Verticies.Add(vertex2);
                }

                edge.StartVertex = vertexVal1;
                edge.EndVertex = vertexVal2;
                edge.Weight = weight;
                Edges.Add(edge);

                vertex1.ForwardEdges.Add(edge.Id);
                vertex2.BackwardEdges.Add(edge.Id);

                Console.WriteLine(edgeId + ". " + vertexVal1 + " - " + vertexVal2 + " - " + weight);
            }
        }

        public void LongestPathInDAGTest()
        {
            Graph graph = new Graph(false);
            graph.AddEdge(0, 0, 1, 2);
            graph.AddEdge(1, 1, 2, 3);
            graph.AddEdge(2, 1, 3, 4);
            graph.AddEdge(3, 3, 4, 5);
            graph.AddEdge(4, 4, 5, 6);
            graph.AddEdge(5, 5, 3, 7);
            graph.AddEdge(6, 5, 2, 8);

            if (graph.FindLongestPath(0, graph.Verticies.Count - 1, 1))
                Console.WriteLine("Longest Path is " + graph.ToString() + "   and the distance is " + graph.DistanceOfLongestPath);
            else
                Console.WriteLine("No path from v0 to v" + (graph.Verticies.Count - 1));

            if (graph.FindLongestPath(3, 5, 1))
                Console.WriteLine("Longest Path is " + graph.ToString() + "   and the distance is " + graph.DistanceOfLongestPath);
            else
                Console.WriteLine("No path from v3 to v5");

            if (graph.FindLongestPath(graph.Verticies.Count - 1, 3, 1))
                Console.WriteLine("Longest Path is " + graph.ToString() + "   and the distance is " + graph.DistanceOfLongestPath);
            else
                Console.WriteLine("No path from v5 to v3");
        }
    }
}