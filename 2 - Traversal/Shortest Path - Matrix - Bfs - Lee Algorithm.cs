using System.Collections.Generic;

namespace GraphSamples
{
     // Other Path Finding Algorithms to visit
     //   1. HadLock Algorithm
     //   2. Soukup's Algorithm
     //   3. Line-Search Algorithms ( Mikami-Tabuchi, Hightower) 

    public partial class TraversalSamples
    {
        public class VertexM
        {
            public Point Point { get; set; }

            public int Distance { get; set; }
        }

        public int ShortestPathByLeeBFS(int[,] matrix, Point srcPoint, Point destPoint, Stack<Point> resultPath)
        {
            if (matrix == null || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
                return -1;

            if (matrix[srcPoint.xPos, srcPoint.yPos] != 1 || matrix[destPoint.xPos, destPoint.yPos] != 1)
                return -1;

            bool[,] visited = new bool[matrix.GetLength(0), matrix.GetLength(1)];

            // Mark the source cell as visited
            visited[srcPoint.xPos, srcPoint.yPos] = true;

            Queue<VertexM> VertexMQueue = new Queue<VertexM>();

            VertexM s = new VertexM() { Point = srcPoint, Distance = 0 };

            VertexMQueue.Enqueue(s);  // Enqueue source cell
            resultPath.Push(srcPoint);

            // These arrays are used to get row and column numbers of 4 neighbours of a given cell
            int[] rowNum = { -1, 0, 0, 1 };
            int[] colNum = { 0, -1, 1, 0 };

            // Do a BFS starting from source cell
            while (VertexMQueue.Count > 0)
            {
                VertexM currVertexM = VertexMQueue.Dequeue();
                Point currPoint = currVertexM.Point;

                // Reached the destination cell, we are done
                if (currPoint.xPos == destPoint.xPos && currPoint.yPos == destPoint.yPos)
                    return currVertexM.Distance;

                for (int lpCnt = 0; lpCnt < 4; lpCnt++)
                {
                    int rowPos = currPoint.xPos + rowNum[lpCnt];
                    int colPos = currPoint.yPos + colNum[lpCnt];

                    // If adjacent cell is valid, has path and not visited yet, enqueue it.
                    if (Point.IsSafePoint(matrix, rowPos, colPos) == true && visited[rowPos, colPos] == false)
                    {
                        visited[rowPos, colPos] = true;
                        Point point = new Point() { xPos = rowPos, yPos = colPos };
                        VertexM Adjcell = new VertexM()
                        {
                            Point = point,
                            Distance = currVertexM.Distance + 1
                        };
                    }
                }
            }
            return -1;
        }

        //====================================================================================================================================
        //Approach 2 Retun list of locations

        public Point ShortestPathByLeeBFS(int[,] srcMaze, int xSrcPos, int ySrcPos, int xDestPos, int yDestPos)
        {
            Queue<Point> pointQueue = new Queue<Point>();
            pointQueue.Enqueue(new Point(xSrcPos, ySrcPos, null));

            while (pointQueue.Count > 0)
            {
                Point currPoint = pointQueue.Dequeue();

                // Reached Destination
                if (currPoint.xPos == xDestPos && currPoint.yPos == yDestPos)
                    return currPoint;

                // Visit Neighbours
                ProcessPoint(srcMaze, currPoint, pointQueue, currPoint.xPos + 1, currPoint.yPos);
                ProcessPoint(srcMaze, currPoint, pointQueue, currPoint.xPos - 1, currPoint.yPos);
                ProcessPoint(srcMaze, currPoint, pointQueue, currPoint.xPos, currPoint.yPos + 1);
                ProcessPoint(srcMaze, currPoint, pointQueue, currPoint.xPos, currPoint.yPos - 1);
            }
            return null;
        }

        private void ProcessPoint(int[,] srcMaze, Point currPoint, Queue<Point> pointQueue, int xPos, int yPos)
        {
            if (Point.IsSafePoint(srcMaze, xPos, yPos))
            {
                srcMaze[currPoint.xPos, currPoint.yPos] = -1;// Current point visited
                Point nextP = new Point(xPos, yPos, currPoint);
                pointQueue.Enqueue(nextP);
            }
        }
    }
}