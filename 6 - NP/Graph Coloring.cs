using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    /// http in //www.geeksforgeeks.org/graph-coloring-set-2-greedy-algorithm/
    /// 

    public partial class NPSamples
    {
        public void WelshPowell()
        {
            GraphSamples.Graph graph = new GraphSamples.Graph(false);

            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(4, 6);
            graph.AddEdge(1, 7);
            graph.AddEdge(1, 8);
            graph.AddEdge(2, 9);
            graph.AddEdge(1, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 7);
            graph.AddEdge(2, 7);

            graph.Display();

            ComparatorVertex c = new ComparatorVertex();

            SortedSet<GraphSamples.Vertex> sortedSet = new SortedSet<GraphSamples.Vertex>(); //TODO FIX it (c);

            foreach (KeyValuePair<long, GraphSamples.Vertex> vertexPair in graph.Verticies)
                sortedSet.Add(vertexPair.Value);

            Dictionary<long, String> assignedColor = new Dictionary<long, String>();
            Dictionary<long, String> finalAssignedColor = new Dictionary<long, String>();

            Dictionary<String, bool> colorsUsed = new Dictionary<string, bool>();

            colorsUsed["Green"] = false;
            colorsUsed["Blue"] = false;
            colorsUsed["Red"] = false;
            colorsUsed["Yellow"] = false;
            colorsUsed["Orange"] = false;

            HashSet<GraphSamples.Vertex> RemoveSet = new HashSet<GraphSamples.Vertex>();

            while (sortedSet.Count() != RemoveSet.Count())
            {
                String color = null;

                foreach (GraphSamples.Vertex vertex in sortedSet)
                {
                    if (RemoveSet.Contains(vertex))
                    {
                        continue;
                    }
                    bool allUncolored = IsAllAdjacentsUnColored(vertex.Adjacents, assignedColor);
                    if (allUncolored)
                    {
                        color = GetUnusedColor(colorsUsed);
                        assignedColor[vertex.Id] = color;

                        RemoveSet.Add(vertex);
                        finalAssignedColor[vertex.Id] = color;
                    }
                }

                colorsUsed.Remove(color);
                assignedColor.Clear();
            }

            Console.WriteLine(finalAssignedColor);
        }

        public void colorGraph()
        {
            GraphSamples.Graph graph = new GraphSamples.Graph(false);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(4, 6);
            graph.AddEdge(1, 7);
            graph.AddEdge(1, 8);
            graph.AddEdge(2, 9);
            graph.AddEdge(1, 3);
            graph.AddEdge(3, 4);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 7);

            graph.Display();

            Dictionary<String, bool> colorsUsed = new Dictionary<String, bool>();

            colorsUsed["Green"] = false;
            colorsUsed["Blue"] = false;
            colorsUsed["Red"] = false;
            colorsUsed["Yellow"] = false;

            Dictionary<long, String> colorsAssigned = new Dictionary<long, String>();

            ConcurrentDictionary<long, GraphSamples.Vertex> allVertex = graph.Verticies;

            foreach (KeyValuePair<long, GraphSamples.Vertex> vertexPair in allVertex)
            {
                List<GraphSamples.Vertex> adjacentVertexes = vertexPair.Value.Adjacents;

                foreach (GraphSamples.Vertex adjacentVertex in adjacentVertexes)
                {
                    String curColor = colorsAssigned[adjacentVertex.Id];

                    if (curColor != null)
                    {
                        AssignColor(curColor, colorsUsed);
                    }
                }

                String color = GetUnusedColor(colorsUsed);
                colorsAssigned[vertexPair.Key] = color;
                ResetColor(colorsUsed);
            }

            Console.WriteLine(colorsAssigned);
        }

        private String GetUnusedColor(Dictionary<String, bool> colorsUsed)
        {
            foreach (String color in colorsUsed.Keys)
            {
                if (colorsUsed[color].Equals(false))
                {
                    return color;
                }
            }
            throw new Exception("Color not found in the set");
        }

        private void ResetColor(Dictionary<String, bool> colorsUsed)
        {
            HashSet<String> colors = new HashSet<String>();
            foreach (String color in colorsUsed.Keys)
            {
                colors.Add(color);
            }

            foreach (String color in colors)
            {
                colorsUsed.Remove(color);
                colorsUsed[color] = false;
            }
        }

        private void AssignColor(String color, Dictionary<String, bool> colorsUsed)
        {
            colorsUsed.Remove(color);
            colorsUsed[color] = true;
        }

        private bool IsAllAdjacentsUnColored(List<GraphSamples.Vertex> vertexes, Dictionary<long, String> colorsAssigned)
        {
            foreach (GraphSamples.Vertex vertex in vertexes)
            {
                if (colorsAssigned.ContainsKey(vertex.Id))
                {
                    return false;
                }
            }
            return true;
        }

        public void GraphColoringTest()
        {
            WelshPowell();
        }
    }

    class ComparatorVertex : Vertex
    {
        public ComparatorVertex() : base(0)
        {

        }
        // TODO Find override method and fix it.
        public int Compare(Vertex o1, Vertex o2)
        {
            if (o1.GetDegree() <= o2.GetDegree())
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}
