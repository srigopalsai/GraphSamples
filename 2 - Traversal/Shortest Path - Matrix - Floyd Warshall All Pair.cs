using System;
using System.Collections.Generic;

namespace GraphSamples
{
    // Time - O(V^3)     Space - O(V^2)
    /* https://en.wikipedia.org/wiki/Floyd%E2%80%93Warshall_algorithm
       
    https://github.com/mission-peace/interview/blob/master/src/com/interview/graph/FloydWarshallAllPairShortestPath.java
    http://www.sanfoundry.com/cpp-program-find-all-pairs-shortest-path/
    http://www.geeksforgeeks.org/dynamic-programming-set-16-floyd-warshall-algorithm/
    http://community.topcoder.com/tc?module=Static&d1=tutorials&d2=graphsDataStrucs3
    http://www.algorithmist.com/index.php/Floyd-Warshall%27s_Algorithm

    Find out quickly whether one vertex is reachable from another vertex. 
    E.g. You want to fly from Athens to Murmansk on Hubris Airlines and you don’t care how many intermediate stops you need to make.
    Is this trip possible?

    You could examine the connectivity table.
    But then you would need to look through all the entries on a given row, which would take O(N) time.
    Where N is the average number of vertices reachable from a given vertex). But is there a faster way?

    It’s possible to construct a table that will tell you instantly (that is, in O(1) time) whether one vertex is reachable from another. 
    Such a table can be obtained by systematically modifying a graph’s adjacency matrix. 
    The graph represented by this revised adjacency matrix is called the transitive closure of the original graph.

    Remember that in an ordinary adjacency matrix the row number indicates where an edge starts and the column number indicates where it ends.
    This is similar to the arrangement in the connectivity table.

    A 1 at the intersection of row C and column D means there’s an edge from vertex C to vertex D. 
    You can get from one vertex to the other in one step. 
    (Of course, in a directed graph it does not follow that you can go the other way, from D to C.) 

    - A B C D E
    A 0 0 1 0 0
    B 1 0 0 0 1
    C 0 0 0 0 0
    D 0 0 0 0 1
    E 0 0 1 0 0

    You might wonder if this algorithm can find paths of more than two edges. 
    After all, the rule only talks about combining two one-edge paths into one two-edge path. 
    As it turns out, the algorithm will build on previously discovered multi-edge paths to create paths of arbitrary length.

    Row A
    We start with row A. There’s nothing in columns A and B, but there’s a 1 at column C, so we stop there. As it says there is a path from A to C. 
    If we knew there was a path from some vertex X to A, then we would know there was a path from X to C.
    Where are the edges (if any) that end at A? They’re in column A. 
    So we examine all the cells in column A, i.e. at row B. It says there’s an edge from B to A. From this we can get from B to C in two steps. 

    To record this result, we put a 1 at the intersection of row B and column C. 
        The result is shown in Figure 13.16a.
    
    The remaining cells of row A are blank.
    Rows B, C, and D
    We go to row B. The first cell, at column A, has a 1, indicating an edge from B to A.
    Are there any edges that end at B? We look in column B, but it’s empty, so we know
    that none of the 1s we find in row B will result in finding longer paths because no
    edges end at B.


    All-pairs shortest path with Floyd- Warshall Someteimes, we have a graph and want to find the shortest path from u to v for all pairs of vertices (u, v). 
    Note that we already know how to do this in  On3 time – we can run the quadratic version of Dijkstra's algorithm from each of the n vertices. 
    However, there is a 4­line algorithm that will do the same job.
     
    http://www.cs.cornell.edu/~wdtseng/icpc/notes/graph_part3.pdf

     */

    public partial class TraversalSamples
    {
        // https://algorithms.tutorialhorizon.com/dynamic-programming-count-all-paths-from-top-left-to-bottom-right-of-a-mxn-matrix/
        public int[,] AllPairShortestPath(int[,] srcMatrix)
        {
            int[,] minDistance = new int[srcMatrix.GetLength(0), srcMatrix.GetLength(0)];
            int[,] pathMatrix = new int[srcMatrix.GetLength(0), srcMatrix.GetLength(0)];

            for (int rIndx = 0; rIndx < srcMatrix.GetLength(0); rIndx++)
            {
                for (int cIndx = 0; cIndx < srcMatrix.GetLength(1); cIndx++)
                {
                    minDistance[rIndx, cIndx] = srcMatrix[rIndx, cIndx];

                    if (srcMatrix[rIndx, cIndx] != int.MaxValue && rIndx != cIndx)
                    {
                        pathMatrix[rIndx, cIndx] = rIndx;
                    }
                    else
                    {
                        pathMatrix[rIndx, cIndx] = -1;
                    }
                }
            }

            //-------------------------------------------------------------------------------------------------

            for (int k = 0; k < srcMatrix.GetLength(0); k++)
            {
                for (int rIndx = 0; rIndx < srcMatrix.GetLength(0); rIndx++)
                {
                    for (int cIndx = 0; cIndx < srcMatrix.GetLength(0); cIndx++)
                    {
                        if (minDistance[rIndx, k] == int.MaxValue || minDistance[k, cIndx] == int.MaxValue)
                        {
                            continue;
                        }

                        if (minDistance[rIndx, cIndx] > minDistance[rIndx, k] + minDistance[k, cIndx])
                        {
                            minDistance[rIndx, cIndx] = minDistance[rIndx, k] + minDistance[k, cIndx];
                            pathMatrix[rIndx, cIndx] = pathMatrix[k, cIndx];
                        }
                    }
                }
            }

            //Look for negative weight cycle in the graph if values on diagonal of distance matrix is negative
            //then there is negative weight cycle in the graph.
            for (int i = 0; i < minDistance.GetLength(0); i++)
            {
                if (minDistance[i, i] < 0)
                {
                    throw new Exception("NegativeWeightCycleException");
                }
            }

            int fromIndx = 3;
            int toIndx = 2;
            DisplayPath(pathMatrix, fromIndx, toIndx);
            return minDistance;
        }

        public void DisplayPath(int[,] path, int startIndx, int endIndx)
        {
            if (startIndx < 0 || endIndx < 0 || startIndx >= path.Length || endIndx >= path.Length)
            {
                return;
            }

            Console.WriteLine("Actual path - between " + startIndx + " " + endIndx);

            Stack<int> stack = new Stack<int>();
            stack.Push(endIndx);

            while (true)
            {
                endIndx = path[startIndx, endIndx];
                if (endIndx == -1)
                    return;

                stack.Push(endIndx);

                if (endIndx == startIndx)
                    break;
            }

            while (stack.Count > 0)
            {
                Console.Write(stack.Pop() + " ");
            }

            Console.WriteLine();
        }

        public void FWShortestPathTest()
        {
            int[,] graph = {
                {0,             3,              6,              15},
                {int.MaxValue,  0,              -2,             int.MaxValue},
                {int.MaxValue,  int.MaxValue,   0,              2},
                {1,             int.MaxValue,   int.MaxValue,   0}
            };

            TestData.Display(graph);

            int[,] minDistance = AllPairShortestPath(graph);
            TestData.Display(minDistance);

            Console.WriteLine("\nMinimum Distance Matrix : ");            
        }
    }
}