using System;
using System.Collections.Generic;

namespace GraphSamples
{
    /*  Find all cycles in Directed graph using Johnson's algorithm
        Time  - O(E + V).(c+1) where c is number of cycles found
        Space - O(E + V + s) where s is sum of length of all cycles.

        Input :     1, 2
                    1, 8
                    1, 5
                    2, 9
                    2, 7
                    2, 3
                    3, 1
                    3, 2
                    3, 6
                    3, 4
                    6, 4
                    4, 5
                    5, 2
                    8, 9
                    9, 8

        Output :    1 -> 2 -> 3 -> 1
                    1 -> 5 -> 2 -> 3 -> 1
                    2 -> 3 -> 2         
                    2 -> 3 -> 6 -> 4 -> 5 -> 2
                    2 -> 3 -> 4 -> 5 -> 2
                    8 -> 9 -> 8 

        Ref https://youtu.be/johyrWospv0    */

    public partial class CycleSamples
    {
        HashSet<Vertex> blockedSet { get; set; }

        Dictionary<Vertex, HashSet<Vertex>> blockedMap { get; set; }

        Stack<Vertex> stack { get; set; }

        List<List<Vertex>> allCycles { get; set; }

        // Main function to find all cycles
        public List<List<Vertex>> SimpleCyles(Graph graph)
        {
            blockedSet = new HashSet<Vertex>();
            blockedMap = new Dictionary<Vertex, HashSet<Vertex>>();
            stack = new Stack<Vertex>();
            allCycles = new List<List<Vertex>>();

            long startIndex = 1;

            GeneralSamples tarjan = new GeneralSamples();

            while (startIndex <= graph.Verticies.Count)
            {
                Graph subGraph = CreateSubGraph(startIndex, graph);
                List<HashSet<Vertex>> sccs = tarjan.TarganStronglyConnectedComponents(subGraph);

                // This creates graph consisting of strongly connected components only and then returns the
                // least indexed vertex among all the strongly connected component graph.
                // It also ignore one vertex graph since it wont have any cycle.

                //Optional<Vertex> maybeLeastVertex = LeastIndexSCC(sccs, subGraph);
                //if (maybeLeastVertex.isPresent())
                Vertex leastVertex = LeastIndexSCC(sccs, subGraph);
                if (leastVertex != null)
                {
                    blockedSet.Clear();
                    blockedMap.Clear();

                    FindCyclesInSCG(leastVertex, leastVertex);
                    startIndex = leastVertex.Id + 1;
                }
                else
                    break;
            }
            return allCycles;
        }

        private Vertex LeastIndexSCC(List<HashSet<Vertex>> sccs, Graph subGraph)
        {
            long min = int.MaxValue;
            Vertex minVertex = null;
            HashSet<Vertex> minScc = null;

            foreach (HashSet<Vertex> scc in sccs)
            {
                if (scc.Count == 1)
                    continue;

                foreach (Vertex vertex in scc)
                {
                    if (vertex.Id < min)
                    {
                        min = vertex.Id;
                        minVertex = vertex;
                        minScc = scc;
                    }
                }
            }

            //*
            if (minVertex == null)
                return null;

            Graph graphScc = new Graph(true);

            foreach (Edge edge in subGraph.Edges)
            {
                if (minScc.Contains(edge.Vertex1) && minScc.Contains(edge.Vertex2))
                {
                    graphScc.AddEdge(edge.Vertex1.Id, edge.Vertex2.Id);
                }
            }
            //*
            return graphScc.GetVertexById(minVertex.Id);
        }

        private void Unblock(Vertex u)
        {
            blockedSet.Remove(u);

            if (blockedMap.ContainsKey(u))
            {
                foreach (Vertex v in blockedMap[u])
                {
                    if (blockedSet.Contains(v))
                    {
                        Unblock(v);
                    }
                }

                blockedMap.Remove(u);
            }
        }

        private bool FindCyclesInSCG(Vertex startVertex, Vertex currentVertex)
        {
            bool foundCycle = false;

            stack.Push(currentVertex);
            blockedSet.Add(currentVertex);

            foreach (Edge e in currentVertex.Edges)
            {
                Vertex neighbor = e.Vertex2;

                // If neighbor is same as start vertex means cycle is found.
                // Store contents of stack in final result.

                if (neighbor == startVertex)
                {
                    List<Vertex> cycle = new List<Vertex>();
                    stack.Push(startVertex);

                    cycle.AddRange(stack);
                    cycle.Reverse();

                    stack.Pop();
                    allCycles.Add(cycle);
                    foundCycle = true;
                }

                // Explore this neighbor only if it is not in blockedSet.
                else if (!blockedSet.Contains(neighbor))
                {
                    bool gotCycle = FindCyclesInSCG(startVertex, neighbor);
                    foundCycle = foundCycle || gotCycle;
                }
            }

            // If cycle is found with current vertex then recursively unblock vertex and all vertices which are dependent on this vertex.
            if (foundCycle)
            {
                // Remove from blockedSet and then remove all the other vertices dependent on this vertex from blockedSet
                Unblock(currentVertex);
            }
            else
            {
                // If no cycle is found with current vertex then don't unblock it. But find all its neighbors and add this
                // Vertex to their blockedMap. If any of those neighbors ever get unblocked then unblock current vertex as well.
                foreach (Edge e in currentVertex.Edges)
                {
                    Vertex w = e.Vertex2;
                    HashSet<Vertex> bSet = GetBSet(w);
                    bSet.Add(currentVertex);
                }
            }

            // Remove vertex from the stack.
            stack.Pop();
            return foundCycle;
        }

        private HashSet<Vertex> GetBSet(Vertex v)
        {
            // return blockedMap. .computeIfAbsent(v, (key)-> new HashSet<>());
            if (!blockedMap.ContainsKey(v))
                return(new HashSet<Vertex>());

            return blockedMap[v];
        }

        private Graph CreateSubGraph(long startVertex, Graph graph)
        {
            Graph subGraph = new Graph(true);
            foreach (Edge edge in graph.Edges)
            {
                if (edge.Vertex1.Id >= startVertex && edge.Vertex2.Id >= startVertex)
                {
                    subGraph.AddEdge(edge.Vertex1.Id, edge.Vertex2.Id);
                }
            }
            return subGraph;
        }

        public void DetectAllCyclesJSCTest()
        {
            Graph graph = new Graph(true);

            graph.AddEdge(1, 2);        
            graph.AddEdge(1, 8);
            graph.AddEdge(1, 5);
            graph.AddEdge(2, 9);
            graph.AddEdge(2, 7);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 1);
            graph.AddEdge(3, 2);
            graph.AddEdge(3, 6);
            graph.AddEdge(3, 4);
            graph.AddEdge(6, 4);
            graph.AddEdge(4, 5);
            graph.AddEdge(5, 2);
            graph.AddEdge(8, 9);
            graph.AddEdge(9, 8);

            graph.Display();

            // TODO Need to fix for missing output  1 -> 5 -> 2 -> 3 -> 1
            List<List<Vertex>> allCycles = SimpleCyles(graph);

            foreach (List<Vertex> vertexList in allCycles)
            {
                foreach (Vertex vertex in vertexList)
                {
                    Console.Write(vertex.Id + " -> ");
                }
                Console.WriteLine();
            }
        }
    }
}