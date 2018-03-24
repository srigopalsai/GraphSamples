using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace GraphSamples
{
    public class Graph
    {
        // long is for Vertex Id as Key. ConcurrentDictionary can be modified in foreach 
        public ConcurrentDictionary<long, Vertex> Verticies { get; set; }

        public List<Edge> Edges { get; set; }

        public bool IsDirected { get; set; }

        public Graph(bool isDirected)
        {
            Edges = new List<Edge>();
            Verticies = new ConcurrentDictionary<long, Vertex>();
            this.IsDirected = isDirected;
        }

        public void AddEdge(long id1, long id2)
        {
            AddEdge(id1, id2, 0);
        }

        //This works only for directed graph because for undirected graph we can end up adding edges two times to allEdges

        public void AddVertex(Vertex vertex)
        {
            if (Verticies.ContainsKey(vertex.Id))
                return;

            Verticies[vertex.Id] = vertex;

            foreach (Edge edge in vertex.Edges)
            {
                Edges.Add(edge);
            }
        }

        public Vertex AddSingleVertex(long id)
        {
            if (Verticies.ContainsKey(id))
                return Verticies[id];

            Verticies[id] = new Vertex(id);
            return Verticies[id];
        }

        public Vertex GetVertexById(long id)
        {
            return Verticies[id];
        }

        public void AddEdge(long id1, long id2, int weight)
        {
            Vertex vertex1 = null;

            if (Verticies.ContainsKey(id1))
            {
                vertex1 = Verticies[id1];
            }
            else
            {
                vertex1 = new Vertex(id1);
                Verticies[id1] = vertex1;
            }

            Vertex vertex2 = null;

            if (Verticies.ContainsKey(id2))
            {
                vertex2 = Verticies[id2];
            }
            else
            {
                vertex2 = new Vertex(id2);
                Verticies[id2] = vertex2;
            }

            Edge edge = new Edge(vertex1, vertex2, IsDirected, weight);

            Edges.Add(edge);

            vertex1.AddAdjacentVertex(edge, vertex2);

            if (!IsDirected)
            {
                vertex2.AddAdjacentVertex(edge, vertex1);
            }
        }

        public Graph ReverseGraph()
        {
            Graph reverseGraph = new Graph(true);

            foreach (Edge edge in this.Edges)
                reverseGraph.AddEdge(edge.Vertex2.Id, edge.Vertex1.Id, edge.Weight);

            return reverseGraph;
        }

        public bool IsDirectedAcyclicGraph()
        {
            int visitCount = 0;
            IEnumerable<long> IteratorI = Verticies.Keys;
            int noOfVertices = Verticies.Count - 1;

            foreach (int srcVertex in IteratorI)
            {
                if (visitCount == noOfVertices)
                    return true;

                List<Vertex> srcAdjList = Verticies[srcVertex].Adjacents;

                if (srcAdjList.Count == 0)
                {
                    visitCount++;
                    Console.WriteLine("Target Node - " + srcVertex);
                    IEnumerable<long> iteratorJ = Verticies.Keys;

                    foreach (long adjVertex in iteratorJ)
                    {
                        List<Vertex> adjList = Verticies[adjVertex].Adjacents;

                        if (adjList.Contains(Verticies[srcVertex]))
                        {
                            adjList.Remove(Verticies[srcVertex]);
                            Console.WriteLine("Deleting edge between target node " + srcVertex + " - " + adjVertex + " ");
                        }
                    }
                    Vertex v = new Vertex(0);
                    Verticies.TryRemove(srcVertex, out v);
                    IteratorI = Verticies.Keys;
                }
            }
            return false;
        }

        public void Display()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("Verticies : \n");

            foreach (KeyValuePair<long,Vertex> vertexPair in Verticies)
            {
                buffer.AppendLine(vertexPair.Key + " No Of Edges : " + vertexPair.Value.Edges.Count);
            }

            buffer.AppendLine();
            buffer.AppendLine("Edges : ");

            foreach (Edge edge in Edges)
            {
                buffer.AppendLine(edge.Vertex1.Id + " to " + edge.Vertex2.Id + " - " + edge.Weight);
            }

            Console.WriteLine("\n=========================================================================================\n");
            Console.WriteLine(buffer.ToString());
            Console.WriteLine("=========================================================================================\n");
        }
    }
}