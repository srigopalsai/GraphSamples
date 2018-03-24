using System;
using System.Collections.Generic;

namespace GraphSamples
{
    // Iterative Deepening Search(IDS) or Iterative Deepening Depth First Search(IDDFS)
    // IDDFS combines DFS space-efficiency and BFS fast search(for nodes closer to root).

    // How does IDDFS work?
    // IDDFS calls DFS for different depths starting from an initial value.
    // In every call, DFS is restricted from going beyond given depth. So basically we do DFS in a BFS fashion.
    // https://www.geeksforgeeks.org/iterative-deepening-searchids-iterative-deepening-depth-first-searchiddfs/
    // http://www.sanfoundry.com/java-program-implement-iterative-deepening/
    public partial class TraversalSamples
    {
        public static bool DftRecursiveWGBList(Vertex current, HashSet<Vertex> whiteSet, HashSet<Vertex> graySet, HashSet<Vertex> blackSet)
        {
            // Move current to gray set from white set and then explore it.
            MoveVertexHelper(current, whiteSet, graySet);

            foreach (Vertex neighbor in current.Adjacents)
            {
                // If in black set means already explored so continue.
                if (blackSet.Contains(neighbor))
                    continue;

                // If in gray set then cycle found.
                if (graySet.Contains(neighbor))
                    return true;

                if (DftRecursiveWGBList(neighbor, whiteSet, graySet, blackSet))
                    return true;
            }

            // Move vertex from gray set to black set when done exploring.
            MoveVertexHelper(current, graySet, blackSet);

            return false;
        }

        private static void MoveVertexHelper(Vertex vertex, HashSet<Vertex> sourceSet, HashSet<Vertex> destinationSet)
        {
            sourceSet.Remove(vertex);
            destinationSet.Add(vertex);
        }

        public void DfsRecursive(Vertex vertex)
        {
            Console.Write("\t" + vertex.Id);
            vertex.HasVisited = true;

            foreach (Vertex v in vertex.Adjacents)
            {
                if (v.HasVisited == false)
                {
                    DfsRecursive(v);
                }
            }
        }

        public void DfsRecursive2(Vertex vertex)
        {
            HashSet<Vertex> visited = new HashSet<Vertex>();

            foreach (Vertex v in vertex.Adjacents)
            {
                if (!visited.Contains(v))
                {
                    DfsRecursiveHelper2(v,visited);
                }
            }
        }

        public void DfsRecursiveHelper2(Vertex vertex, HashSet<Vertex> visited)
        {
            Console.Write("\t" + vertex.Id);
            visited.Add(vertex);

            foreach (Vertex v in vertex.Adjacents)
            {
                if (!visited.Contains(v))
                {
                    DfsRecursiveHelper2(vertex, visited);
                }
            }
        }

        public void DfsRecursive3(Vertex vertex)
        {
            HashSet<long> visited = new HashSet<long>();

            foreach (Vertex v in vertex.Adjacents)
            {
                if (!visited.Contains(v.Id))
                {
                    DfsRecursiveHelper3(v, visited);
                }
            }
        }

        private void DfsRecursiveHelper3(Vertex vertex, HashSet<long> visited)
        {
            Console.Write("\t" + vertex.Id);
            visited.Add(vertex.Id);

            foreach (Vertex v in vertex.Adjacents)
            {
                if (!visited.Contains(v.Id))
                {
                    DfsRecursiveHelper3(vertex, visited);
                }
            }
        }

        public void BfsIterative(Vertex srcVertex)
        {
            if (srcVertex == null)
                return;

            Queue<Vertex> vQueue = new Queue<Vertex>();
            vQueue.Enqueue(srcVertex);

            while (vQueue.Count > 0)
            {
                Vertex vertex = vQueue.Dequeue();
                vertex.HasVisited = true;

                Console.Write(" " + vertex.Id);

                foreach (Vertex v in vertex.Adjacents)
                {
                    if (v.HasVisited == false)
                    {
                        vQueue.Enqueue(v);
                    }
                }
            }
        }

        public bool DepthLimitedSearch(Vertex src, Vertex target, int dLimit)
        {
            if (src.Id == target.Id)
            {
                return true;
            }

            // If reached the maximum depth, stop recursing.
            if (dLimit <= 0)
            {
                return false;
            }

            foreach (Vertex neighbour in src.Adjacents)
            {
                bool found = DepthLimitedSearch(neighbour, target, dLimit - 1);

                if (found == true)
                    return true;
            }

            return false;
        }

        public bool IterativeDeepingDfs(Vertex src, Vertex target, int maxDepth)
        {
            for (int dIndx = 0; dIndx <= maxDepth; dIndx++)
            {
                bool found = DepthLimitedSearch(src, target, dIndx);

                if (found == true)
                    return true;
            }
            return false;
        }

        public void IterativeDeepingDfsListTest()
        {
            Graph  graph = new Graph(true);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 5);
            graph.AddEdge(2, 6);

            Vertex src = graph.Edges[0].Vertex1;
            Vertex target = graph.Edges[graph.Edges.Count - 1].Vertex2;
            int maxDepth = 3;

            bool canReach = IterativeDeepingDfs(src, target, maxDepth);
            Console.WriteLine(string.Format("Target {0} is reachable from source {1} within max depth {2}",
                                src.Id, target.Id, maxDepth));
        }

        public void ListTraversalTest()
        {
            Graph graph = new Graph(true);

            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 6);
            graph.AddEdge(6, 5);
            graph.AddEdge(5, 1);
            graph.AddEdge(5, 3);

            BfsIterative(graph.Verticies[0]);
        }
    }
}