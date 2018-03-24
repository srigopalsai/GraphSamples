using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples._2___Traversal
{
    // https://algorithms.tutorialhorizon.com/snake-and-ladder-problem/
    public class SnakeAndLadder
    {
        class Vertex
        {
            public int cell;
            public int moves;
        }

        public int FindMinMoves(int[] board)
        {
            int boardLen = board.Length;

            bool[] visited = new bool[boardLen];

            Queue<Vertex> queue = new Queue<Vertex>();
            
            //start from position 1 (index 0) and it is already visited
            Vertex vertex = new Vertex();
            vertex.cell = 0;
            vertex.moves = 0;

            queue.Enqueue(vertex);
            visited[0] = true;

            while (queue.Count > 0)
            {
                vertex = queue.Dequeue();

                int cellNum = vertex.cell;

                //check if reached to the end
                if (cellNum == boardLen - 1)
                    break;

                //if not reached to the end then add the reachable adjacent cells from the current cells
                //Since dice can be controlled and max value can be achieved 6
                //so from the current cell, you can reach to next 6 cells so add next 6 cells
                for (int i = cellNum + 1; i < (cellNum + 6) && i < boardLen; i++)
                {
                    //check if cell is already not visited
                    if (visited[i] != true)
                    {
                        //add it to the queue, update moves and mak visited
                        Vertex currentVertex = new Vertex();
                        currentVertex.moves = vertex.moves + 1; //can be reached by throwing dice one more time
                        visited[i] = true;

                        //now fill the cell can be reached (might be snake or ladder)
                        if (board[i] == -1)
                        {
                            //means can be reached by throwing dice at that cell
                            currentVertex.cell = i;
                        }
                        else
                        {
                            //might be snake OR ladder at this cell 'i'
                            //then tail of the snake or top of the ladder will be achieved
                            // by reaching at cell 'i'
                            currentVertex.cell = board[i];
                        }

                        queue.Enqueue(currentVertex);
                    }
                }
            }
            return vertex.moves;
        }

        public void FindMinMovesTest()
        {
            int size = 36;

            int[] board = new int[size];

            for (int indx = 0; indx < size; indx++)
            {
                board[indx] = -1;
            }

            //ladders
            board[2] = 15;
            board[14] = 24;
            board[20] = 31;

            // Snakes
            board[11] = 1;
            board[29] = 3;
            board[34] = 21;

            SnakeAndLadder s = new SnakeAndLadder();
            Console.WriteLine ("Minimum Dice throws needed to reach to end: " + s.FindMinMoves(board));
        }
    }
}