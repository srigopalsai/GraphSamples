using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples._6___NP
{
    class KnightTourDemo
    {
        int[,] moves = { { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 }, { 2, -1 }, { 2, 1 }, { -2, -1 }, { -2, 1 } };

        public void KnightTourDfsColoring(int[,] board)
        {
        //    def knightTour(n, path, u, limit):
        //u.setColor('gray')
        //path.append(u)
        //if n < limit:
        //    nbrList = list(u.getConnections())
        //    i = 0
        //    done = False
        //    while i < len(nbrList) and not done:
        //    if nbrList[i].getColor() == 'white':
        //            done = knightTour(n + 1, path, nbrList[i], limit)
        //        i = i + 1
        //    if not done:  # prepare to backtrack
        //        path.pop()
        //        u.setColor('white')
        //else:
        //    done = True
        //return done
        }

        public void KnightTourDfs(int[,] board)
        {
            if (board == null || board.Length == 0)
                return;

            int visitCnt = 0;

            Stack<Tuple<int, int>> cellStack = new Stack<Tuple<int, int>>();

            cellStack.Push(new Tuple<int, int>(0, 0));

            while (cellStack.Count > 0)
            {
                Tuple<int, int> cell = cellStack.Pop();
                board[cell.Item1, cell.Item2] = ++visitCnt;

                for (int mIndx = 0; mIndx < 8; mIndx++)
                {
                    int trRIndx = cell.Item1 + moves[mIndx, 0];
                    int trCIndx = cell.Item2 + moves[mIndx, 1];

                    if (IsValidCell(board, trRIndx, trCIndx) == true)
                    {
                        cellStack.Push(new Tuple<int, int>(trRIndx, trCIndx));
                    }
                }
            }
        }

        public void DispayTour(int[,] board)
        {
            for (int rIndx = 0; rIndx < board.GetLength(0); rIndx++)
            {
                for (int cIndx = 0; cIndx < board.GetLength(0); cIndx++)
                {
                    Console.Write(board[rIndx, cIndx] + " ");
                }
                Console.WriteLine();
            }
        }

        bool IsValidCell(int[,] board, int rIndx, int cIndx)
        {
            int rLen = board.GetLength(0);
            int cLen = board.GetLength(1);

            return rIndx >= 0 && rIndx < rLen && cIndx >= 0 && cIndx < cLen && board[rIndx, cIndx] == 0;
        }

        public void KnightTourTest()
        {
            int[,] board = new int[4, 4];

            KnightTourDemo tourDemo = new KnightTourDemo();
            tourDemo.KnightTourDfs(board);
            tourDemo.DispayTour(board);

            Console.ReadLine();
        }
    }

    // http://rosettacode.org/wiki/Knight%27s_tour
    // https://www.geeksforgeeks.org/backtracking-set-3-n-queen-problem/
    // https://www.geeksforgeeks.org/backtracking-set-1-the-knights-tour-problem/
    class Solution
    {
        int[,] moves = { { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 }, { 2, -1 }, { 2, 1 }, { -2, -1 }, { -2, 1 } };
        public double knightProbability(int N, int K, int r, int c)
        {
            double[,,] dp = new double[K + 1,N,N];
            return helper(dp, N, K, r, c) / Math.Pow(8.0, K);
        }

        private double helper(double[,,] dp, int N, int k, int r, int c)
        {
            if (r < 0 || r >= N || c < 0 || c >= N)
                return 0.0;

            if (k == 0)
                return 1.0;

            if (dp[k,r,c] != 0.0)
                return dp[k,r,c];

            for (int i = 0; i < 8; i++)
            {
                dp[k, r, c] += helper(dp, N, k - 1, r + moves[i, 0], c + moves[i, 1]);
            }
            return dp[k,r,c];
        }
    }

    class MainClass
    {
        public double knightProbability(int N, int K, int sr, int sc)
        {
            double[,] dp = new double[N,N];
            int[] dr = new int[] { 2, 2, 1, 1, -1, -1, -2, -2 };
            int[] dc = new int[] { 1, -1, 2, -2, 2, -2, 1, -1 };

            dp[sr,sc] = 1;

            for (; K > 0; K--)
            {
                double[,] dp2 = new double[N,N];

                for (int rIndx = 0; rIndx < N; rIndx++)
                {
                    for (int cIndx = 0; cIndx < N; cIndx++)
                    {
                        for (int mIndx = 0; mIndx < 8; mIndx++)// Move Indx
                        {
                            int cr = rIndx + dr[mIndx];
                            int cc = cIndx + dc[mIndx];

                            if (0 <= cr && cr < N && 0 <= cc && cc < N)
                            {
                                dp2[cr,cc] += dp[rIndx,cIndx] / 8.0;
                            }
                        }
                    }
                }
                dp = dp2;
            }

            double ans = 0.0;

            for (double[] row: dp)
            {
                for (double x: row)
                    ans += x;
            }

            return ans;
        }


        int[] xMove = { 2, 1, -1, -2, -2, -1,  1,  2 };
        int[] yMove = { 1, 2,  2,  1, -1, -2, -2, -1 };

        readonly static int[,] moves = { {+2,-1},
                                         {+2,+1},
                                         {-2,+1},
                                         {-2,-1},
                                         {+1,-2},
                                         {+1,+2},
                                         {-1,+2},                                         
                                         {-1,-2}};
        class Cell
        {
            public int x;
            public int y;

            public Cell(int _x, int _y)
            {
                x = _x;
                y = _y;
            }
        }

        public void Main(string[] args)
        {
            int N = 4;
            int[,] board = new int[N, N];
            board.Initialize();

            int x = 0,  // starting position
                y = 0;

            List<Cell> list = new List<Cell>(N * N);
            list.Add(new Cell(x, y));

            do
            {
                if (Move_Possible(board, x, y))
                {
                    int move = board[x, y];
                    board[x, y]++;

                    x += moves[move, 0];
                    y += moves[move, 1];

                    list.Add(new Cell(x, y));
                }
                else
                {
                    if (board[x, y] >= 8)
                    {
                        board[x, y] = 0;
                        list.RemoveAt(list.Count - 1);

                        if (list.Count == 0)
                        {
                            Console.WriteLine("No solution found.");
                            return;
                        }

                        x = list[list.Count - 1].x;
                        y = list[list.Count - 1].y;
                    }
                    board[x, y]++;
                }
            }

            while (list.Count < N * N);

            int last_x = list[0].x,
                last_y = list[0].y;

            string letters = "ABCDEFGH";

            for (int i = 1; i < list.Count; i++)
            {
                Console.WriteLine(string.Format("{0,2}:  ", i) + letters[last_x] + (last_y + 1) + " - " + letters[list[i].x] + (list[i].y + 1));

                last_x = list[i].x;
                last_y = list[i].y;
            }
        }

        bool Move_Possible(int[,] board, int cur_x, int cur_y)
        {
            if (board[cur_x, cur_y] >= 8)
            {
                return false;
            }

            int new_x = cur_x + moves[board[cur_x, cur_y], 0],
                new_y = cur_y + moves[board[cur_x, cur_y], 1];

            if (new_x >= 0 && new_x < board.GetLength(0) && new_y >= 0 && new_y < board.GetLength(0) && board[new_x, new_y] == 0)
            {
                return true;
            }

            return false;
        }
    }
}