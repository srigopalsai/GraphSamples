using System;

namespace GraphSamples
{
    /**
    Find Hamiltonian Cycle in an UnWeighted Graph.     NP-complete.

    A Hamiltonian cycle (or Hamiltonian circuit) is a Hamiltonian path (or a traceable path) that is a cycle. 
    Is a path in an undirected or directed graph that visits each vertex exactly once.
    Determining whether such paths and cycles exist in graphs is the Hamiltonian path problem.

    http://www.sanfoundry.com/java-program-check-if-given-graph-contain-hamiltonian-cycle-not/
    */

    public partial class NPSamples
    {
        int V;
        int pathCount;

        int[] path;
        int[,] matrixGlobal;

        public void FindHamiltonianCycle(int[,] matrix)
        {
            V = matrix.GetLength(0);
            path = new int[V];

            Array.ForEach(path, item=> item = -1);

            matrixGlobal = matrix;

            try
            {
                path[0] = 0;
                pathCount = 1;
                FindPathRecursive(0);

                Console.WriteLine("No solution");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                // TODO Check
                DisplayPath();
            }
        }

        private void FindPathRecursive(int vertex)
        {
            if (matrixGlobal[vertex, 0] == 1 && pathCount == V)
                throw new Exception("Solution found");

            // All vertices selected but last vertex not linked to 0
            if (pathCount == V)
                return;

            for (int neighbour = 0; neighbour < V; neighbour++)
            {
                // If connected
                if (matrixGlobal[vertex, neighbour] == 1)
                {
                    path[pathCount++] = neighbour;

                    matrixGlobal[vertex, neighbour] = 0;
                    matrixGlobal[neighbour, vertex] = 0;

                    // If vertex not already selected solve recursively
                    if (!IsPresent(neighbour))
                        FindPathRecursive(neighbour);

                    // Restore connection                    
                    matrixGlobal[vertex, neighbour] = 1;
                    matrixGlobal[neighbour, vertex] = 1;

                    // Remove path
                    path[--pathCount] = -1;
                }
            }
        }

        private bool IsPresent(int vertex)
        {
            for (int index = 0; index < pathCount - 1; index++)
            {
                if (path[index] == vertex)
                    return true;
            }

            return false;
        }

        public void DisplayPath()
        {
            Console.Write("\nPath : ");

            for (int index = 0; index <= V; index++)
                Console.Write(path[index % V] + " ");

            Console.WriteLine();
        }

        public void HamiltonianCycleMatrixTest()
        {
            // No path found.
            //int[,] matrix = TestData.BinaryMatrices[TestData.Keys.SixBySix4];

            // Path found.
            int[,] matrix = TestData.BinaryMatrices[TestData.Keys.SixBySix5];

            TestData.Display(matrix, "Hamiltonian Path - Matrix Test");
            FindHamiltonianCycle(matrix);
        }
    }
}