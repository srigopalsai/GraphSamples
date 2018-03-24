using System;
using System.Collections.Generic;

namespace GraphSamples
{
    /**
    Feedback Vertex Set : NP-Complete
    A feedback vertex set of a graph is a set of vertices whose removal leaves a graph without cycles. 
    In other words, each feedback vertex set contains at least one vertex of any cycle in the grap
    OR
    Is a set of vertices containing at least one vertex from every cycle in the directed graph, and the minimum spanning tree, which is the undirected variant of the feedback arc set problem.

    https://en.wikipedia.org/wiki/Feedback_vertex_set
    
    -------------------------------------------------------------------------------------------------------------------------------------------------------

    Feedback Edge (Arc) Set :    NP-Complete
    A directed graph may contain directed cycles, a one-way loop of edges. 
        In some applications, such cycles are undesirable, and we wish to eliminate them and obtain a directed acyclic graph (DAG). 
        One way to do this is simply to drop edges from the graph to break the cycles. 
        A feedback arc set (FAS) or feedback edge set is a set of edges which, when removed from the graph, leave a DAG. 
        Put in another way, it's a set containing at least one edge of every cycle in the graph

    Note that the problem of deleting as few edges as possible to make the graph cycle-free is equivalent to finding a spanning tree, which can be done in polynomial time. 
        In contrast, the problem of deleting edges from a directed graph to make it acyclic, the feedback arc set problem, is NP-complete

    https://en.wikipedia.org/wiki/Feedback_arc_set
    http://www.sanfoundry.com/java-program-find-good-feedback-edge-set-graph/
    -------------------------------------------------------------------------------------------------------------------------------------------------------

    Minimum feedback arc set : NP-Hard

    Figuring out the minimum number of edges to remove is an NP-hard problem called the minimum feedback arc set or maximum acyclic subgraph problem.
    */

    public partial class NPSamples
    {
        public  void FindGoodFeedbackSetTest()
        {
            try
            {
                Console.WriteLine("Find Good Feedback Vertex Set Demo");

                int v = 6;
                Graph2 glist = new Graph2(v);

                glist.AddEdge(1, 2);
                glist.AddEdge(2, 3);
                glist.AddEdge(2, 4);
                glist.AddEdge(4, 5);
                glist.AddEdge(5, 6);
                glist.AddEdge(6, 4);
                glist.AddEdge(6, 3);

                glist.DisplayGraph();
                glist.FindGoodFeedbackSet(v, false);

                Console.WriteLine("\nFind Good Feedback Edge Set Demo");

                glist = new Graph2(v);

                glist.AddEdge(1, 2);
                glist.AddEdge(2, 3);
                glist.AddEdge(2, 4);
                glist.AddEdge(4, 5);
                glist.AddEdge(5, 6);
                glist.AddEdge(6, 4);
                glist.AddEdge(6, 3);

                glist.DisplayGraph();
                glist.FindGoodFeedbackSet(v, true);
            }
            catch (Exception exception)
            {
                Console.WriteLine("You are trying to access empty adjacency list of a node. " + exception.Message);
            }
        }
    }
}