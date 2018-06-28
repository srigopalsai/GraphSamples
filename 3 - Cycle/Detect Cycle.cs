using System;
using System.Collections.Generic;

namespace GraphSamples
{
    /* Complexity for Disjoint Sets and DFS
     * Time  O(V)   Space O(V)
     */

    public partial class CycleSamples
    {
        public bool HasCycleInDirected(Graph graph)
        {
            HashSet<Vertex> whiteSet = new HashSet<Vertex>();
            HashSet<Vertex> graySet = new HashSet<Vertex>();
            HashSet<Vertex> blackSet = new HashSet<Vertex>();

            foreach (KeyValuePair<long, Vertex> vertexPair in graph.Verticies)
            {
                whiteSet.Add(vertexPair.Value);
            }

            foreach (var current in whiteSet)
            {
                if (TraversalSamples.DftRecursiveWGBList(current, whiteSet, graySet, blackSet))
                {
                    return true;
                }
            }

            return false;
        }
    
        //=======================================================================================================================

        public bool HasCycleUsingDisjointSets(Graph graph)
        {
            DisjointSet disjointSet = new DisjointSet();

            foreach (KeyValuePair<long,Vertex> vertexPair in graph.Verticies)
            {
                disjointSet.MakeSet(vertexPair.Key);
            }

            foreach (Edge edge in graph.Edges)
            {
                long parent1 = disjointSet.FindSet(edge.Vertex1.Id);
                long parent2 = disjointSet.FindSet(edge.Vertex2.Id);

                if (parent1 == parent2)
                {
                    return true;
                }

                disjointSet.Union(edge.Vertex1.Id, edge.Vertex2.Id);
            }

            return false;
        }

        public bool HasCycleInUnDirectedDFS(Graph graph)
        {
            HashSet<Vertex> visited = new HashSet<Vertex>();

            foreach (KeyValuePair<long,Vertex> vertexPair in graph.Verticies)
            {
                if (visited.Contains(vertexPair.Value))
                {
                    continue;
                }

                bool flag = HasCycleDFSUtil(vertexPair.Value, visited, null);
                if (flag)
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasCycleDFSUtil(Vertex vertex, HashSet<Vertex> visited, Vertex parent)
        {
            visited.Add(vertex);

            foreach (Vertex adjVertex in vertex.Adjacents)
            {
                if (adjVertex.Equals(parent))
                {
                    continue;
                }

                if (visited.Contains(adjVertex))
                {
                    return true;
                }

                bool hasCycle = HasCycleDFSUtil(adjVertex, visited, vertex);

                if (hasCycle)
                {
                    return true;
                }
            }

            return false;
        }

        public void HasCycleInUnDirectedTest()
        {
            //Graph undGraph = new Graph(false);

            //undGraph.AddEdge(0, 1);
            //undGraph.AddEdge(1, 2);
            //undGraph.AddEdge(0, 3);
            //undGraph.AddEdge(3, 4);
            //undGraph.AddEdge(4, 5);
            //undGraph.AddEdge(5, 1);

            //undGraph.Display();
            bool hasCycle = false;

            //Graph undGraph = new Graph(false);
            //undGraph.AddEdge(1, 0);
            //undGraph.AddEdge(0, 2);
            //undGraph.AddEdge(2, 0);
            //undGraph.AddEdge(0, 3);
            //undGraph.AddEdge(3, 4);

            Graph undGraph = new Graph(false);
            undGraph.AddEdge(0, 1);
            undGraph.AddEdge(1, 2);

            undGraph.Display();
            hasCycle = HasCycleInUnDirectedDFS(undGraph);
            Console.WriteLine("Has Cycle - Un Directed Graph - DFS : " + hasCycle);

            hasCycle = HasCycleUsingDisjointSets(undGraph);
            Console.WriteLine("Has Cycle - Un Directed Graph - DisjointSets : " + hasCycle);

            Graph dGraph = TestData.DirectedGraphs[TestData.Keys.DGraph9e];

            //Graph dGraph = new Graph(false);
            //dGraph.AddEdge(1, 0);
            //dGraph.AddEdge(0, 2);
            //dGraph.AddEdge(2, 0);
            //dGraph.AddEdge(0, 3);
            //dGraph.AddEdge(3, 4);

            dGraph.Display();

            hasCycle = HasCycleInDirected(dGraph);
            Console.WriteLine("Has Cycle - Directed Graph - DFS :  " + hasCycle);
        }
    }
}