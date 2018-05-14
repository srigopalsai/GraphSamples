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
    }
}