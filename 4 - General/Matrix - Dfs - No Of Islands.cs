using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    /*
     http://www.geeksforgeeks.org/find-number-of-islands/
     */
    public partial class GeneralSamples
    {
        public int NumberOfIslands(int[,] graph)
        {
            bool[,] visited = new bool[graph.GetLength(0), graph.GetLength(1)];
            int count = 0;

            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    if (visited[i, j] == false && graph[i, j] == 1)
                    {
                        count++;
                        DFS(graph, visited, i, j);
                    }
                }
            }
            return count;
        }

        private void DFS(int[,] graph, bool[,] visited, int i, int j)
        {
            if (i < 0 || j < 0 || i == graph.GetLength(0) || j == graph.GetLength(1))
                return;

            visited[i, j] = true;

            if (graph[i, j] == 0)
                return;

            DFS(graph, visited, i, j + 1);
            DFS(graph, visited, i + 1, j);
            DFS(graph, visited, i + 1, j + 1);
            DFS(graph, visited, i - 1, j + 1);
        }

        // =======================================================
        /* A 2d grid map of m rows and n columns is initially filled with water.
           We may perform an addLand operation which turns the water at position (row, col) into a land. 
           Given a list of positions to operate, count the number of islands after each addLand operation. 
           An island is surrounded by water and is formed by connecting adjacent lands horizontally or vertically. 
           You may assume all four edges of the grid are all surrounded by water.
           https://leetcode.com/problems/number-of-islands-ii/
        */

        public List<int> NumberOfIslands2(int rowSize, int colSize, int[,] positions)
        {
            List<int> result = new List<int>();

            if (positions == null || positions.GetLength(0) == 0 || positions.GetLength(1) == 0)
                return result;

            int count = 0;
            DisjointSet dsSet = new DisjointSet();
            HashSet<int> landHashSet = new HashSet<int>();

            for (int lpRIndx = 0; lpRIndx < positions.GetLength(0); lpRIndx++)
            {
                int index = positions[lpRIndx, 0] * colSize + positions[lpRIndx, 1];

                landHashSet.Add(index);
                dsSet.MakeSet(index);

                count++;

                // Find the four neighbors;
                int n1 = (positions[lpRIndx, 0] - 1) * colSize + positions[lpRIndx, 1];
                int n2 = (positions[lpRIndx, 0] + 1) * colSize + positions[lpRIndx, 1];
                int n3 = (positions[lpRIndx, 0])     * colSize + positions[lpRIndx, 1] + 1;
                int n4 = (positions[lpRIndx, 0])     * colSize + positions[lpRIndx, 1] - 1;

                if (positions[lpRIndx, 0] - 1 >= 0 && landHashSet.Contains(n1) && dsSet.Union(index, n1))
                    count--;

                if (positions[lpRIndx, 0] + 1 < rowSize && landHashSet.Contains(n2) && dsSet.Union(index, n2))
                    count--;

                if (positions[lpRIndx, 1] + 1 < colSize && landHashSet.Contains(n3) && dsSet.Union(index, n3))
                    count--;

                if (positions[lpRIndx, 1] - 1 >= 0 && landHashSet.Contains(n4) && dsSet.Union(index, n4))
                    count--;

                result.Add(count);
            }
            return result;
        }

        public void NumberOfIslandsTest()
        {
            int[,] matrix = {{1,1,0,1,0},
                          {1,0,0,1,1},
                          {0,0,0,0,0},
                          {1,0,1,0,1},
                          {1,0,0,0,0}
                        };

            //            int count = NumberOfIslands(matrix);
            //Console.WriteLine("No of Islands " + count);
            List<int> noOfIslands = NumberOfIslands2(matrix.GetLength(0), matrix.GetLength(1), matrix);

            foreach(int i in noOfIslands)
                Console.WriteLine(" " + i);
        }
    }
}