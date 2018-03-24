using System;
using System.Collections.Generic;

namespace GraphSamples
{
    //Hamiltonian path (or traceable path) is a path in an undirected or directed graph that visits each vertex exactly once. 
    //A Hamiltonian cycle (or Hamiltonian circuit) is a Hamiltonian path that is a cycle. 
    //Determining whether such paths and cycles exist in graphs is the Hamiltonian path problem, which is NP-complete.

    //https://en.wikipedia.org/wiki/Hamiltonian_path
    //https://en.wikipedia.org/wiki/Hamiltonian_path_problem
    //https://github.com/mission-peace/interview/blob/master/src/com/interview/graph/HamiltonianCycle.java

    //E.g.
    //1. A complete graph with more than two vertices is Hamiltonian.
    //2. Every cycle graph is Hamiltonian.
    //3. Every tournament has an odd number of Hamiltonian paths.
    //4. Every platonic solid, considered as a graph, is Hamiltonian.
    //5. The Cayley graph of a finite Coxeter group is Hamiltonian (For more information on Hamiltonian paths in Cayley graphs.

    //Properties :
    //1. Any Hamiltonian cycle can be converted to a Hamiltonian path by removing one of its edges.
    //   But a Hamiltonian path can be extended to Hamiltonian cycle only if its endpoints are adjacent.
    //2. All Hamiltonian graphs are biconnected, but a biconnected graph need not be Hamiltonian.
    //3. An Eulerian graph G (a connected graph in which every vertex has even degree) necessarily has an Euler tour.
    //   A closed walk passing through each edge of G exactly once. 
    //   This tour corresponds to a Hamiltonian cycle in the line graph L(G), so the line graph of every Eulerian graph is Hamiltonian.
    //4. A tournament (with more than two vertices) is Hamiltonian if and only if it is strongly connected.

    public partial class NPSamples
    {
        public bool GetHamiltonianCycle(GraphSamples.Graph graph, List<GraphSamples.Vertex> result)
        {
            GraphSamples.Vertex startVertex = graph.Verticies[0];
            HashSet<GraphSamples.Vertex> visited = new HashSet<GraphSamples.Vertex>();

            return HamiltonianUtil(startVertex, startVertex, result, visited, graph.Verticies.Count);
        }

        private bool HamiltonianUtil(GraphSamples.Vertex startVertex, GraphSamples.Vertex currentVertex , List<GraphSamples.Vertex> result, HashSet<GraphSamples.Vertex> visited, int totalVertex)
        {
            visited.Add(currentVertex);
            result.Add(currentVertex);

            foreach (GraphSamples.Vertex child in currentVertex.Adjacents)
            {
                if (startVertex.Equals(child) && totalVertex == result.Count)
                {
                    result.Add(startVertex);
                    return true;
                }
                if (!visited.Contains(child))
                {
                    bool isHamil = HamiltonianUtil(startVertex, child, result, visited, totalVertex);
                    if (isHamil)
                    {
                        return true;
                    }
                }
            }

            result.RemoveAt(result.Count - 1);
            visited.Remove(currentVertex);

            return false;
        }

        public void HamiltonianCycleTest()
        {
            GraphSamples.Graph graph = new GraphSamples.Graph(false);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 5);
            graph.AddEdge(5, 2);
            graph.AddEdge(2, 4);
            graph.AddEdge(4, 1);
            graph.AddEdge(4, 5);

            List<GraphSamples.Vertex> result = new List<GraphSamples.Vertex>();
            bool isHamiltonian = GetHamiltonianCycle(graph, result);

            Console.WriteLine(isHamiltonian);

            if (isHamiltonian)
            {
                Console.WriteLine(result);
            }
        }
    }
}