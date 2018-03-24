using System;
using System.Linq;
using System.Collections.Generic;

namespace GraphSamples
{
    // DFS, IDDFS & BFS : Time O(V + E)
    // The Space required by DFS & IDDFS is O(d) where d is depth of tree.
    // But Space required by BFS is O(V) where V is number of verticies.
    
    // BFS is faster, Dfs takes less Space, IDDFS is faster and takes less space.

    // Why? Note that the last level of tree can have around n/2 nodes and second last level n/4 nodes and in BFS we need to have every level one by one in queue.
    // DFS Non-recursive implementation is similar to breadth-first search but differs from it in two ways:
    // https://stackoverflow.com/questions/11468621/why-is-the-time-complexity-of-both-dfs-and-bfs-o-v-e

    // Every edge is considered exactly twice, and every node is processed exactly once, so the complexity has to be a constant multiple of the number of edges as well as the number of vertices.

    // 1. It uses a stack instead of a queue.
    // 2. It delays checking whether a vertex has been discovered until the vertex is popped from the stack. 
    //    Rather than making this check before adding the vertex.

    // Applications of BFS :

    //1. Copying garbage collection, Cheney's algorithm
    //2. Finding the shortest path between two nodes u and v, with path length measured by number of edges(an advantage over depth-first search)[10]
    //3. (Reverse) Cuthill–McKee mesh numbering
    //4. Ford–Fulkerson method for computing the maximum flow in a flow network
    //5. Serialization/Deserialization of a binary tree vs serialization in sorted order, allows the tree to be re-constructed in an efficient manner.
    //6. Construction of the failure function of the Aho-Corasick pattern matcher.
    //7. Testing bipartiteness of a graph.

    // Applications of DFS :

    //  1. Finding connected components.
    //  2. Topological sorting.
    //  3. Finding 2-(edge or vertex)-connected components.
    //  4. Finding 3-(edge or vertex)-connected components.
    //  5. Finding the bridges of a graph.
    //  6. Generating words in order to plot the Limit Set of a Group.
    //  7. Finding strongly connected components.
    //  8. Planarity testing[7][8]
    //  9. Solving puzzles with only one solution, such as mazes. 
    //    (DFS can be adapted to find all solutions to a maze by only including nodes on the current path in the visited set.)
    // 10. Maze generation may use a randomized depth-first search.
    // 11.Finding biconnectivity in graphs.

    // Iterative Deepening Search(IDS) or Iterative Deepening Depth First Search(IDDFS)
    // IDDFS combines DFS space-efficiency and BFS fast search(for nodes closer to root).

    // How does IDDFS work?
    // IDDFS calls DFS for different depths starting from an initial value.
    // In every call, DFS is restricted from going beyond given depth. So basically we do DFS in a BFS fashion.

    public partial class TraversalSamples
    {
        // Arrays used to get row and column numbers of 8 neighbours of a given cell
        // 0, 0 can be skipped as it is the current pointer.
        int[] rowNbr = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] colNbr = { -1, 0, 1, -1, 1, -1, 0, 1 };

        // For White Gray Black Approach 

        public int UNVISITED = 0; // White
        public int VISITING = 1;  // Gray
        public int FINISHED = 2;  // Black

        // 1 ---------------------------------------------------------------------------------------------------------------------------------

        public void DfsRecursiveWGBMatrix(int[,] srcMatrix)
        {
            int[] visited = new int[srcMatrix.GetLength(0)];

            //Loops over each node in the graph in case we haven't visited it yet 
            for (int vertex = 0; vertex < srcMatrix.GetLength(0); ++vertex)
            {
                DfsRecursiveWGBMatrixHelper(srcMatrix, vertex, visited);
            }
        }

        private void DfsRecursiveWGBMatrixHelper(int[,] srcMatrix, int vetex, int[] visitMatrix)
        {
            if (visitMatrix[vetex] != UNVISITED)
                return;

            //Console.WriteLine("Visiting Vertex : " + vetex);
            visitMatrix[vetex] = VISITING;

            for (int neighbour = 0; neighbour < srcMatrix.GetLength(1); ++neighbour)
            {
                if (srcMatrix[vetex, neighbour] == 1)
                {
                    Console.WriteLine(" Found an Edge between : " + vetex + ", " + neighbour);
                    DfsRecursiveWGBMatrixHelper(srcMatrix, neighbour, visitMatrix);
                }
            }

            //Console.WriteLine("Finishing Vertex : " + vetex);
            visitMatrix[vetex] = FINISHED;
        }

        // 2 ---------------------------------------------------------------------------------------------------------------------------------

        public bool[] DftRecursiveMatrix(int[,] matrix, int vertex)
        {
            bool[] visitArray = new bool[matrix.GetLongLength(0)];
            DftRecursiveMatrix(matrix, visitArray, vertex);
            return visitArray;
        }

        public void DftRecursiveMatrix(int[,] srcMatrix, bool[] visitArray, int vertex)
        {
            visitArray[vertex] = true;
            Console.Write(vertex + "  ");

            for (int neighbour = 0; neighbour < srcMatrix.GetLength(0); neighbour++)
            {
                if (visitArray[neighbour] == false && srcMatrix[vertex, neighbour] == 1)
                {
                    DftRecursiveMatrix(srcMatrix, visitArray, neighbour);
                }
            }
        }

        // 3 ---------------------------------------------------------------------------------------------------------------------------------

        public void DfsRecursive8Neighbours(int[,] srcMatrix, bool[,] visitMatrix, int row, int col, ref int visitCount)
        {
            // Mark current cell as visited
            visitMatrix[row, col] = true;

            // Recursive call for all connected neighbours
            for (int cellPos = 0; cellPos < 8; ++cellPos)
            {
                if (IsSafePosition(srcMatrix, visitMatrix, row + rowNbr[cellPos], col + colNbr[cellPos]))
                {
                    visitCount++;
                    DfsRecursive8Neighbours(srcMatrix, visitMatrix, row + rowNbr[cellPos], col + colNbr[cellPos], ref visitCount);
                }
            }
        }

        public static bool IsSafePosition(int[,] matrix, bool[,] visitMatrix, int xPos, int yPos, int safeVal = 1)
        {
            if (xPos >= 0 && xPos < matrix.GetLength(0) &&
                yPos >= 0 && yPos < matrix.GetLength(1) &&
                matrix[xPos, yPos] == safeVal && visitMatrix[xPos, yPos] == false)
            {
                return true;
            }
            return false;
        }

        // 4 ---------------------------------------------------------------------------------------------------------------------------------
        // Directed and Undirected

        public static bool[] DfsIterativeMatrix(int[,] srcMatrix, int srcVertex)
        {
            bool[] visited = new bool[srcMatrix.GetLength(0)];

            Stack<int> vertexStack = new Stack<int>();
            vertexStack.Push(srcVertex);

            Console.WriteLine("Visiting verticies by DFS Iterative : ");
            while (vertexStack.Count > 0)
            {
                int vertex = vertexStack.Pop();

                if (visited[vertex] == true)
                    continue;

                Console.Write(vertex + " ");
                visited[vertex] = true;

                for (int neighbour = 0; neighbour < srcMatrix.GetLength(0); neighbour++)
                //for (int neighbour = srcMatrix.GetLength(0) - 1; neighbour >= 0; neighbour--)
                {
                    if (srcMatrix[vertex, neighbour] == 1 && visited[neighbour] == false)
                    {
                        vertexStack.Push(neighbour);
                    }
                }
            }

            Console.WriteLine("Given source vertex connected to ");

            for (int vertex = 0; vertex < srcMatrix.GetLength(0); vertex++)
            {
                if (visited[vertex] == true)
                {
                    Console.Write(vertex + " ");
                }
            }

            Console.WriteLine("");
            return visited;
        }

        // 5 ---------------------------------------------------------------------------------------------------------------------------------

        public static bool[] BftIterativeMatrix(int[,] srcMatrix, int srcVertex)
        {
            bool[] visited = new bool[srcMatrix.GetLength(0)];

            Queue<int> vertexQueue = new Queue<int>();
            vertexQueue.Enqueue(srcVertex);

            while (vertexQueue.Count > 0)
            {
                int vertex = vertexQueue.Dequeue();

                if (visited[vertex] == true)
                    continue;

                Console.Write(vertex + "  ");
                visited[vertex] = true;

                for (int neighbour = 0; neighbour < srcMatrix.GetLength(1); neighbour++)
                {
                    if (srcMatrix[vertex, neighbour] == 1 && visited[neighbour] == false)
                    {
                        vertexQueue.Enqueue(neighbour);
                    }
                }
            }
            return visited;
        }

        // 6 ---------------------------------------------------------------------------------------------------------------------------------
        // Combination of Dfs & Bfs

        // An important thing to note is, we visit top level nodes multiple times.
        // The last(or max depth) level is visited once, second last level is visited twice, and so on.
        // It may seem expensive, but it turns out to be not so costly, since in a tree most of the nodes are in the bottom level.
        // So it does not matter much if the upper levels are visited multiple times.

        public void IterativeDeepingDfs(int[,] matrix, int source, int destination)
        {
            int depth = 0;
            int maxDepth = 0;
            bool goalFound = false;
            Stack<int> stack = new Stack<int>();

            while (!goalFound)
            {
                DepthLimitedSearch(stack, matrix, source, destination, ref goalFound, ref depth, ref maxDepth);
                maxDepth++;
            }

            Console.WriteLine("\nGoal Found at depth " + depth);
        }

        private void DepthLimitedSearch(Stack<int> stack, int[,] matrix, int srcVertex, int destVertex, 
                                        ref bool goalFound, ref int depth, ref int maxDepth)
        {
            int curSrcVertex;
            int curDestVertex = 1;

            int[] visited = new int[matrix.GetLength(0) + 1];

            stack.Push(srcVertex);
            depth = 0;

            Console.WriteLine("\nAt Depth " + maxDepth);
            Console.Write(srcVertex + "\t");

            while (stack.Count() > 0)
            {
                curSrcVertex = stack.Peek();

                while (curDestVertex <= matrix.GetLength(0))
                {
                    if (depth >= maxDepth)
                        break;

                    if (matrix[curSrcVertex, curDestVertex] == 1)
                    {
                        stack.Push(curDestVertex);

                        visited[curDestVertex] = 1;

                        depth++;

                        Console.Write(curDestVertex + "\t");

                        if (curDestVertex == destVertex)
                        {
                            goalFound = true;
                            return;
                        }

                        curSrcVertex = curDestVertex;
                        curDestVertex = 1;

                        continue;
                    }

                    curDestVertex++;
                }

                curDestVertex = stack.Pop() + 1;
                depth--;
            }
        }

        public void IterativeDeepingDfsTest()
        {
            try
            {
                int[,] matrix = {   { 0, 1, 1, 0, 0, 0, 0},
                                    { 0, 0, 0, 1, 1, 0, 0},
                                    { 0, 0, 0, 0, 0, 1, 1},
                                    { 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0},
                                    { 0, 0, 0, 0, 0, 0, 0} };

                IterativeDeepingDfs(matrix, 1, 7);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wrong Input format" + ex.Message);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------

        public void MatrixTraversalTest()
        {
            int[,] srcMatrix = TestData.BinaryMatrices[TestData.Keys.SevenBySeven1];
            bool[] visitMatrix = new bool[srcMatrix.GetLength(0)];

            Console.Clear();

            Console.Write("DFS Recursive : ");
            DftRecursiveMatrix(srcMatrix, visitMatrix, 0);

            Console.Write("DFS Recursive WhiteGrayBlack : ");
            DfsRecursiveWGBMatrix(srcMatrix);

            Console.Write("\nDFS Iterative : ");
            DfsIterativeMatrix(srcMatrix, 0);

            Console.Write("\nBFS Iterative : ");
            BftIterativeMatrix(srcMatrix, 0);
        }
    }
}