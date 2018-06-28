using System;
using System.Collections.Generic;

namespace GraphSamples
{
    public class Vertex
    {
        public long Id { get; set; }

        public string Data { get; set; }

        public double MinDistance { get; set; } = Double.PositiveInfinity;

        public bool HasVisited { get; set; }

        public Vertex Previous { get; set; }

        public List<Edge> Edges { get; set; }

        public List<Vertex> Adjacents { get; set; }

        public Vertex(long id)
        {
            Edges = new List<Edge>();
            Adjacents = new List<Vertex>();
            this.Id = id;
        }

        public void AddAdjacentVertex(Edge e, Vertex v)
        {
            Edges.Add(e);
            Adjacents.Add(v);
        }

        /// <summary>
        /// No of Edges
        /// </summary>
        /// <returns></returns>
        public int GetDegree()
        {
            return Edges.Count;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            if (this == obj)
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Vertex other = (Vertex)obj;

            if (Id != other.Id)
                return false;

            return true;
        }
    }
}