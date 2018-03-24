using System;

namespace GraphSamples
{
    public class Edge
    {
        public bool IsDirected { get; set;}

        public int Weight { get; set; }

        public Vertex Vertex1 { get; set; }

        public Vertex Vertex2 { get; set; }

        public Edge(Vertex vertex1, Vertex vertex2)
        {        
            this.Vertex1 = vertex1;
            this.Vertex2 = vertex2;
        }

        public Edge(Vertex vertex1, Vertex vertex2, bool isDirected)
        {
            this.Vertex1 = vertex1;
            this.Vertex2 = vertex2;

            this.IsDirected = isDirected;
        }

        public Edge(Vertex vertex1, Vertex vertex2, bool isDirected, int weight)
        {
            this.Vertex1 = vertex1;
            this.Vertex2 = vertex2;

            this.Weight = weight;
            this.IsDirected = isDirected;
        }
       
        public override String ToString()
        {
            return "IsDirected = " + IsDirected + ", vertex1 = " + Vertex1 + ", vertex2 = " + Vertex2 + ", Weight = " + Weight;
        }
    }
}