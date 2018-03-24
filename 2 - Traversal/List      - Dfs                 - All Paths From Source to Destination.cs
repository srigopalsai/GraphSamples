using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    public partial class TraversalSamples
    {
        public void AllPathsFromSourceToDestination(Vertex srcVertex, Vertex destVertex)
        {
            HashSet<Vertex> visitedHSet = new HashSet<Vertex>();
            VisitPathDfsRecursive(srcVertex, destVertex, visitedHSet);
        }

        private void VisitPathDfsRecursive(Vertex srcVertex, Vertex destVertex, HashSet<Vertex> visitedHSet)
        {
            if (visitedHSet.Contains(srcVertex))
                return;

            if (srcVertex.Equals(destVertex))
            {
                foreach (Vertex visitedVertex in visitedHSet)
                {
                    Console.Write(visitedVertex.Id + " ");
                }
                Console.WriteLine(destVertex.Id);

                return;
            }

            visitedHSet.Add(srcVertex);

            foreach (Vertex neighbor in srcVertex.Adjacents)
            {
                VisitPathDfsRecursive(neighbor, destVertex, visitedHSet);
            }

            visitedHSet.Remove(srcVertex);
        }

        public void AllPathsFromSourceToDestinationTest()
        {
            Graph graph = new Graph(true);

            graph.AddEdge(10, 20);
            graph.AddEdge(10, 30);
            graph.AddEdge(20, 40);
            graph.AddEdge(20, 50);
            graph.AddEdge(30, 60);
            graph.AddEdge(50, 60);
            graph.AddEdge(50, 70);
            graph.AddEdge(60, 70);
            graph.AddEdge(40, 70);
            graph.AddEdge(10, 80);
            graph.AddEdge(80, 90);
            graph.AddEdge(90, 10);

            graph.Display();

            Vertex srcVertex = graph.GetVertexById(10);
            Vertex destVertex = graph.GetVertexById(70);

            Console.WriteLine("All paths from given source " + srcVertex.Id + " to given destination " + destVertex.Id);
            AllPathsFromSourceToDestination(srcVertex, destVertex);
        }
    }
}