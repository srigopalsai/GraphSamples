using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphSamples
{
    //In graph theory, a biconnected graph is a connected and “nonseparable” graph.
    //Meaning that if any vertex were to be removed, the graph will remain connected. 
    //Therefore a biconnected graph has no articulation vertices.

    //A vertex in an undirected connected graph is an articulation point (or cut vertex) iff removing it disconnects the graph.  
    //Articulation points represent vulnerabilities in a connected network – single points whose failure would split the network into 2 or more disconnected components. 
    //Finding them is useful for designing reliable networks by avoiding single point failures.

    // https://stackoverflow.com/questions/15873153/explanation-of-algorithm-for-finding-articulation-points-or-cut-vertices-of-a-gr
    // https://www.geeksforgeeks.org/articulation-points-or-cut-vertices-in-a-graph/
    // http://www.sanfoundry.com/java-program-check-whether-graph-biconnected/

    public class BiconnectedGraph
    {
        private HashSet<int> articulationPoints { get; set; }

        private int[] Visited { get; set; }

        public BiconnectedGraph(int noOfNodes)
        {
            this.articulationPoints = new HashSet<int>();
            this.Visited = new int[noOfNodes + 1];
        }

        private int NoOfArticulationPoint(int[,] matrix, int source)
        {
            int noOfNodes = matrix.GetLength(0);
            int[] Parent = new int[noOfNodes + 1];
            int[,] tempMatrix = new int[noOfNodes + 1, noOfNodes + 1];

            int noOfChildren = 0;

            Stack<int> stack = new Stack<int>();
            stack.Push(source);

            Visited[source] = 1;

            for (int vertex = 1; vertex <= matrix.GetLength(0); vertex++)
            {
                for (int neighbor = 1; neighbor <= matrix.GetLength(0); neighbor++)
                {
                    tempMatrix[vertex, neighbor] = matrix[vertex, neighbor];
                }
            }

            while (stack.Count() > 0)
            {
                int element = stack.Peek();
                int destination = element;

                while (destination <= matrix.GetLength(0))
                {
                    if (tempMatrix[element, destination] == 1 && Visited[destination] == 0)
                    {
                        stack.Push(destination);

                        Visited[destination] = 1;
                        Parent[destination] = element;

                        if (element == source)
                        {
                            noOfChildren++;
                        }

                        if (!IsLeaf(tempMatrix, destination))
                        {
                            if (noOfChildren > 1)
                            {
                                articulationPoints.Add(source);
                            }

                            if (IsArticulationPointDfsTree(tempMatrix, destination, Parent))
                            {
                                articulationPoints.Add(destination);
                            }
                        }

                        element = destination;
                        destination = 1;

                        continue;
                    }

                    destination++;
                }
                stack.Pop();
            }
            return articulationPoints.Count;
        }

        //In DFS tree, a vertex u is parent of another vertex v, if v is discovered by u (obviously v is an adjacent of u in graph). 
        //In DFS tree, a vertex u is articulation point if one of the following two conditions is true.
        //1) u is root of DFS tree and it has at least two children.
        //2) u is not root of DFS tree and it has a child v 
        //   such that no vertex in subtree rooted with v has a back edge to one of the ancestors (in DFS tree) of u.

        public bool IsArticulationPointDfsTree(int[,] matrix, int srcVertex, int[] Parent)
        {
            bool[] visited = new bool[matrix.GetLength(0) + 1];

            Stack<int> stack = new Stack<int>();
            stack.Push(srcVertex);

            while (stack.Count() > 0)
            {
                int curVertex = stack.Peek();

                for (int neighbor = 1; neighbor <= matrix.GetLength(0); neighbor++)
                {
                    if (curVertex != srcVertex)
                    {
                        if (matrix[curVertex, neighbor] == 1 && Visited[neighbor] == 1)
                        {
                            if (stack.Contains(neighbor))
                            {
                                if (neighbor <= Parent[srcVertex])
                                {
                                    return false;
                                }
                                return true;
                            }
                        }
                    }

                    if ((matrix[curVertex, neighbor] == 1 && visited[neighbor] == true) && Visited[neighbor] == 0)
                    {
                        stack.Push(neighbor);
                        visited[neighbor] = true;

                        matrix[neighbor, curVertex] = 0;
                        curVertex = neighbor;
                        neighbor = 1;

                        continue;
                    }
                }

                stack.Pop();
            }
            return true;
        }

        private bool IsLeaf(int[,] matrix, int srcVertex)
        {
            for (int curVertex = 1; curVertex <= matrix.GetLength(0); curVertex++)
            {
                if (matrix[srcVertex, curVertex] == 1 && Visited[curVertex] == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsBiconnected(int[,] matrix, int source)
        {
            bool biConnected = false;
            
            if (TraversalSamples.IsConnectedBfsIterativeMatrix(matrix, source) && NoOfArticulationPoint(matrix, source) == 0)
            {
                biConnected = true;
            }
            return biConnected;
        }

        public static void IsBiconnectedTest()
        {
            int noOfNodes;
            int source;

            try
            {
                Console.WriteLine("Enter the number of nodes in the graph");
                noOfNodes = Convert.ToInt32(Console.ReadLine());

                int[,] matrix = new int[noOfNodes + 1, noOfNodes + 1];
                Console.WriteLine("Enter the adjacency matrix");

                for (int rIndx = 1; rIndx <= noOfNodes; rIndx++)
                    for (int cIndx = 1; cIndx <= noOfNodes; cIndx++)
                        matrix[rIndx, cIndx] = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter the source for the graph");
                source = Convert.ToInt32(Console.ReadLine());
                BiconnectedGraph biconnectedGraph = new BiconnectedGraph(noOfNodes);

                if (biconnectedGraph.IsBiconnected(matrix, source))
                {
                    Console.WriteLine("The Given Graph is BiConnected");
                }
                else
                {
                    Console.WriteLine("The Given Graph is Not BiConnected");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Wrong Input format");
            }
        }

        // https://www.geeksforgeeks.org/articulation-points-or-cut-vertices-in-a-graph/
        public static void ArticulationPointsListTest()
        {
            Console.WriteLine("Articulation points in first graph "); // 0, 3
            Graph g1 = new Graph(false);
            g1.AddEdge(1, 0);
            g1.AddEdge(0, 2);
            g1.AddEdge(2, 1);
            g1.AddEdge(0, 3);
            g1.AddEdge(3, 4);
            //g1.AP();
            Console.WriteLine();

            Console.WriteLine("Articulation points in Second graph"); // 1, 2
            Graph g2 = new Graph(false);
            g2.AddEdge(0, 1);
            g2.AddEdge(1, 2);
            g2.AddEdge(2, 3);
            //g2.AP();
            Console.WriteLine();

            Console.WriteLine("Articulation points in Third graph "); // 1
            Graph g3 = new Graph(false);
            g3.AddEdge(0, 1);
            g3.AddEdge(1, 2);
            g3.AddEdge(2, 0);
            g3.AddEdge(1, 3);
            g3.AddEdge(1, 4);
            g3.AddEdge(1, 6);
            g3.AddEdge(3, 5);
            g3.AddEdge(4, 5);
            //g3.AP();
        }
    }
}