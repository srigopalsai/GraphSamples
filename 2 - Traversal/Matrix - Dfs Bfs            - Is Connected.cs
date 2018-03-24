using System;
using System.Collections.Generic;

namespace GraphSamples
{
    public partial class TraversalSamples
    {
        public void IsConnectedDfsIterativeMatrix(int[,] matrix, int srcVertex)
        {
            Console.WriteLine("IsConnected - Dfs Iterative Matrix");

            bool connected = true;
            bool[] visited = DfsIterativeMatrix(matrix, srcVertex);

            for (int vertex = 0; vertex < matrix.GetLength(0); vertex++)
            {
                if (visited[vertex] != true)
                {
                    connected = false;
                    break;
                }
            }

            if (connected)
                Console.WriteLine("The graph is connected");
            else
                Console.WriteLine("The graph is disconnected");
        }

        public static bool IsConnectedBfsIterativeMatrix(int[,] matrix, int srcVertex)
        {
            Console.WriteLine("IsConnected - Directed/UnDirected Bfs Iterative Matrix");

            bool connected = true;
            bool[] visited = BftIterativeMatrix(matrix, srcVertex);

            for (int vertex = 0; vertex < matrix.GetLength(0); vertex++)
            {
                if (visited[vertex] != true)
                {
                    connected = false;
                    break;
                }
            }
            
            if (connected)
                Console.WriteLine("The graph is connected");
            else
                Console.WriteLine("The graph is disconnected");

            return connected;
        }

        public void IsConnectedByDfsIterativeTest()
        {
            try
            {
                //Connected
                //int[,] matrix = TestData.BinaryMatrices[TestData.Keys.SixBySix1];

                //Disconnected
                int[,] matrix = TestData.BinaryMatrices[TestData.Keys.SixBySix2];

                TestData.Display(matrix);
                Console.WriteLine("Is Graph Connected By Dfs Iterative ");

                IsConnectedDfsIterativeMatrix(matrix, 0);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Wrong Input format" + exception.Message);
            }
        }
    }
}