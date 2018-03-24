using System;
using System.Collections.Generic;

namespace GraphSamples
{

     //Given a 2 D floor plan with empty space, block and multiple exits. 
     //Find distance of every empty space from nearest exits in case of fire emergency.

     //Idea is to start from every exit position and do BFS search in all 4 directions and maintain the distance of every space from exit. 
     //If another exit in future iterator is closer than already calculated exit then update the distance.

     //Space complexity is O(n*m)
     //Time complexity is O(number of exits * m * n);

    public partial class TraversalSamples
    {
        public int[,] FindShortestPathFromExit(CellType[,] input)
        {
            int[,] distance = new int[input.GetLength(0), input.GetLength(1)];

            for (int rIndx = 0; rIndx < input.GetLength(0); rIndx++)
            {
                for (int cIndx = 0; cIndx < input.GetLength(1); cIndx++)
                {
                    distance[rIndx, cIndx] = int.MaxValue;
                }
            }

            for (int rIndx = 0; rIndx < input.GetLength(0); rIndx++)
            {
                for (int cIndx = 0; cIndx < input.GetLength(1); cIndx++)
                {
                    // For every exit location do a BFS starting with this exit as the origin
                    if (input[rIndx, cIndx] == CellType.GUARD)
                    {
                        distance[rIndx, cIndx] = 0;

                        SetDistance(input, rIndx, cIndx, distance);
                    }
                }
            }

            return distance;
        }

        private void SetDistance(CellType[,] input, int x, int y, int[,] distance)
        {

            bool[,] visited = new bool[input.GetLength(0), input.GetLength(1)];
            Queue<Point> q = new Queue<Point>();

            q.Enqueue(new Point(x, y));

            //Do a BFS at keep updating distance.
            while (q.Count > 0)
            {
                Point p = q.Dequeue();

                SetDistanceUtil(q, input, p, GetNeighbor(input, p.xPos + 1, p.yPos), distance, visited);
                SetDistanceUtil(q, input, p, GetNeighbor(input, p.xPos, p.yPos + 1), distance, visited);
                SetDistanceUtil(q, input, p, GetNeighbor(input, p.xPos - 1, p.yPos), distance, visited);
                SetDistanceUtil(q, input, p, GetNeighbor(input, p.xPos, p.yPos - 1), distance, visited);
            }
        }

        private void SetDistanceUtil(Queue<Point> q, CellType[,] input, Point p, Point newPoint, int[,] distance, bool[,] visited)
        {
            if (newPoint != null && !visited[newPoint.xPos, newPoint.yPos])
            {
                if (input[newPoint.xPos, newPoint.yPos] != CellType.GUARD &&
                    input[newPoint.xPos, newPoint.yPos] != CellType.BLOCK)
                {
                    distance[newPoint.xPos, newPoint.yPos] = Math.Min(distance[newPoint.xPos, newPoint.yPos], 1 + distance[p.xPos, p.yPos]);
                    visited[newPoint.xPos, newPoint.yPos] = true;
                    q.Enqueue(newPoint);
                }
            }
        }

        private Point GetNeighbor(CellType[,] input, int xPos, int yPos)
        {
            if (xPos < 0 || xPos >= input.Length || yPos < 0 || yPos >= input.GetLength(1))
                return null;

            return new Point(xPos, yPos);
        }

        public void ShortestPathFromExitTest()
        {
            CellType[,] input = { {CellType.SPACE, CellType.SPACE, CellType.BLOCK, CellType.BLOCK},
                                  {CellType.SPACE, CellType.SPACE, CellType.GUARD, CellType.SPACE},
                                  {CellType.SPACE, CellType.SPACE, CellType.BLOCK, CellType.SPACE},
                                  {CellType.SPACE, CellType.GUARD, CellType.BLOCK, CellType.SPACE}
                                 };

            int[,] result = FindShortestPathFromExit(input);

            for (int rIndx = 0; rIndx < result.GetLength(0); rIndx++)
            {
                for (int cIndx = 0; cIndx < result.GetLength(1); cIndx++)
                {
                    Console.Write(result[rIndx, cIndx] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}