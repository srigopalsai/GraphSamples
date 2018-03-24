using System;

namespace GraphSamples
{
    //Is a directed graph with no directed cycles. 
    //That is, it is formed by a collection of vertices and directed edges, each edge connecting one vertex to another.
    //Such that there is no way to start at some vertex v and follow a sequence of edges that eventually loops back to v again.
    //http://www.sanfoundry.com/java-program-check-whether-graph-dag/

    public partial class CycleSamples
    {
        public void IsDirectedAcyclicGraphTest()
        {
            try
            {
                Graph2 glistDAG = new Graph2(9);

                glistDAG.AddEdge(1, 2);
                glistDAG.AddEdge(1, 4);
                glistDAG.AddEdge(2, 3);
                glistDAG.AddEdge(2, 7);
                glistDAG.AddEdge(4, 5);
                glistDAG.AddEdge(4, 6);
                glistDAG.AddEdge(4, 9);
                glistDAG.AddEdge(5, 8);
                glistDAG.AddEdge(5, 9);
                glistDAG.AddEdge(6, 7);
                glistDAG.AddEdge(7, 8);

                glistDAG.DisplayGraph();

                Console.WriteLine("--Processing graph...\n");

                glistDAG.IsDirectedAcyclicGraph();

                Graph2 glistNotDAG = new Graph2(6);
                glistNotDAG.AddEdge(1, 2);
                glistNotDAG.AddEdge(2, 3);
                glistNotDAG.AddEdge(2, 4);
                glistNotDAG.AddEdge(4, 5);
                glistNotDAG.AddEdge(5, 6);
                glistNotDAG.AddEdge(6, 4);
                glistNotDAG.AddEdge(6, 3);
                glistNotDAG.DisplayGraph();

                Console.WriteLine("--Processing graph...\n");

                glistNotDAG.IsDirectedAcyclicGraph();
            }

            catch (Exception e)
            {
                Console.WriteLine("Cannot access empty adjacency list of a node." + e.Message);
            }
        }
    }
}