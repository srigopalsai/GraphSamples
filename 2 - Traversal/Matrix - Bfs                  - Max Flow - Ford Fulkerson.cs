using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    //Ford Fulkerson method Edmonds Karp algorithm for finding max flow

    //Capacity - Capacity of an edge to carry units from source to destination vertex
    //Flow - Actual flow of units from source to destination vertex of an edge
    //Residual capacity - Remaining capacity on this edge i.e capacity - flow
    //AugmentedPath - Path from source to sink which has residual capacity greater than 0

    //Time complexity is O(VE^2)

    //References:
    //http://www.geeksforgeeks.org/ford-fulkerson-algorithm-for-maximum-flow-problem/
    //https://en.wikipedia.org/wiki/Edmonds%E2%80%93Karp_algorithm

    public partial class TraversalSamples
    {
        public int MaxFlow(int[,] capacity, int source, int sink)
        {
            int[,] residualCapacity = new int[capacity.Length, capacity.GetLength(0)];

            for (int lpRIndx = 0; lpRIndx < capacity.Length; lpRIndx++)
            {
                for (int lpCIndx = 0; lpCIndx < capacity.GetLength(0); lpCIndx++)
                {
                    residualCapacity[lpRIndx, lpCIndx] = capacity[lpRIndx, lpCIndx];
                }
            }

            // For storing BFS parent
            Dictionary<int, int> parentDictionary = new Dictionary<int, int>();

            // Stores all the augmented paths
            List<List<int>> augmentedPaths = new List<List<int>>();

            //max flow we can get in this network
            int maxFlow = 0;

            // See if augmented path can be found from source to sink.
            while (BFS(residualCapacity, parentDictionary, source, sink))
            {
                List<int> augmentedPath = new List<int>();

                int flow = int.MaxValue;

                // Find minimum residual capacity in augmented path and also add vertices to augmented path list

                int vertex = sink;
                while (vertex != source)
                {
                    augmentedPath.Add(vertex);
                    int u = parentDictionary[vertex];
                    if (flow > residualCapacity[u, vertex])
                    {
                        flow = residualCapacity[u, vertex];
                    }
                    vertex = u;
                }

                augmentedPath.Add(source);
                augmentedPath.Reverse();
                augmentedPaths.Add(augmentedPath);

                // Add min capacity to max flow
                maxFlow += flow;

                // Decrease residual capacity by min capacity from u to v in augmented path
                // and increase residual capacity by min capacity from v to u
                vertex = sink;

                while (vertex != source)
                {
                    int u = parentDictionary[vertex];
                    residualCapacity[u, vertex] -= flow;
                    residualCapacity[vertex, u] += flow;
                    vertex = u;
                }
            }

            DisplayAugmentedPaths(augmentedPaths);
            return maxFlow;
        }

        private void DisplayAugmentedPaths(List<List<int>> augmentedPaths)
        {
            Console.WriteLine("Augmented paths");

            foreach (List<int> path in augmentedPaths)
            {
                foreach (int val in path)
                {
                    Console.WriteLine(val + "  ");
                }
                Console.WriteLine();
            }
        }

        private bool BFS(int[,] residualCapacity, Dictionary<int, int> parentDictionary, int source, int sink)
        {
            HashSet<int> visited = new HashSet<int>();
            Queue<int> queue = new Queue<int>();

            queue.Enqueue(source);
            visited.Add(source);

            bool foundAugmentedPath = false;

            // See if we can find augmented path from source to sink
            while (queue.Count > 0)
            {
                int vertexU = queue.Dequeue();

                for (int vertexV = 0; vertexV < residualCapacity.Length; vertexV++)
                {
                    // Explore the vertex only if it is not visited and its residual capacity is greater than 0
                    if (!visited.Contains(vertexV) && residualCapacity[vertexU, vertexV] > 0)
                    {
                        // Aadd in parent Dictionary saying vertexV got explored by vertexU
                        parentDictionary[vertexV] = vertexU;
                        
                        visited.Add(vertexV);
                        
                        // Add v to queue for BFS
                        queue.Enqueue(vertexV);
                        
                        //if sink is found then augmented path is found
                        if (vertexV == sink)
                        {
                            foundAugmentedPath = true;
                            break;
                        }
                    }
                }
            }

            //returns if augmented path is found from source to sink or not
            return foundAugmentedPath;
        }

        public void MaxFlowTest()
        {
            int[,] capacity = {{0, 3, 0, 3, 0, 0, 0},
                            {0, 0, 4, 0, 0, 0, 0},
                            {3, 0, 0, 1, 2, 0, 0},
                            {0, 0, 0, 0, 2, 6, 0},
                            {0, 1, 0, 0, 0, 0, 1},
                            {0, 0, 0, 0, 0, 0, 9},
                            {0, 0, 0, 0, 0, 0, 0}};

            Console.WriteLine("\nMaximum capacity " + MaxFlow(capacity, 0, 6));
        }
    }
}