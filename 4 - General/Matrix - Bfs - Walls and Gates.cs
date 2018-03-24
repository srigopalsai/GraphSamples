using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    /**
    You are given a m x n 2D grid initialized with these three possible values.
         -1 - A wall or an obstacle.
         0 - A gate.
         INF - Infinity means an empty room. We use the value 231 - 1 = 2147483647 to represent INF as
         you may assume that the distance to a gate is less than 2147483647.

    Fill each empty room with the distance to its nearest gate. If it is impossible to reach a gate, it should be filled with INF

    Time complexity O(n*m)
    Space complexity O(n*m)

    https://leetcode.com/problems/walls-and-gates/
        https://github.com/mission-peace/interview/blob/master/src/com/interview/graph/WallsAndGates.java
    */
    
    public partial class GeneralSamples
    {
        int[,] matrix = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
        int INF = int.MaxValue;

        public void wallsAndGates(int[,] rooms)
        {
            Queue<Cell> queue = new Queue<Cell>();
            Gates(rooms, queue);

            while (queue.Count > 0)
            {
                Cell cell = queue.Dequeue();
                addNeighbors(rooms, cell.Row, cell.Col, queue);
            }
        }

        private void addNeighbors(int[,] rooms, int row, int col, Queue<Cell> queue)
        {
            for(int rIndx =0; rIndx < rooms.GetLength(0); rIndx++)
            {
                int r1 = row + rooms[rIndx, 0];
                int c1 = col + rooms[rIndx, 1];

                if (r1 < 0 || c1 < 0 || r1 >= rooms.GetLength(0) || c1 >= rooms.GetLength(1) || rooms[r1,c1] != INF)
                    continue;

                rooms[r1,c1] = 1 + rooms[row,col];

                queue.Enqueue(new Cell(r1, c1));
            }
        }

        private void Gates(int[,] rooms, Queue<Cell> queue)
        {
            for (int lpRIndx = 0; lpRIndx < rooms.GetLength(0); lpRIndx++)
            {
                for (int lpCIndx = 0; lpCIndx < rooms.GetLength(1); lpCIndx++)
                {
                    if (rooms[lpRIndx,lpCIndx] == 0)
                    {
                        queue.Enqueue(new Cell(lpRIndx, lpCIndx));
                    }
                }
            }
        }
    }
}