using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    /*
    A knight's tour is a sequence of moves of a knight on a chessboard such that the knight visits every square only once. 
    If the knight ends on a square that is one knight's move from the beginning square (so that it could tour the board again immediately, following the same path), the tour is closed, otherwise it is open.

    The knight's tour problem is an instance of the more general Hamiltonian path problem in graph theory. 
    The problem of finding a closed knight's tour is similarly an instance of the Hamiltonian cycle problem. 
    Unlike the general Hamiltonian path problem, the knight's tour problem can be solved in linear time.[
    https://en.wikipedia.org/wiki/Knight%27s_tour
    */

    public partial class GeneralSamples        
    {
        private static readonly int N = 8;

        private int[,] soln;

        public GeneralSamples()
        {
            soln = new int[N, N];
        }

        private bool isSafe(int x, int y)
        {
            if (x >= 0 && x < N && y >= 0 && y < N && soln[x, y] == -1)
                return true;
            return false;
        }

        private void printSolution()
        {
            for (int x = 0; x < N; x++)
            {
                for (int y = 0; y < N; y++)
                {
                    Console.Write("  " + soln[x, y]);
                }
                Console.WriteLine();
            }
        }

        private bool solveKTUtil(int x, int y, int movei, int[] xMove, int[] yMove)
        {
            int k, next_x, next_y;

            if (movei == N * N)
                return true;

            for (k = 0; k < N; k++)
            {
                next_x = x + xMove[k];
                next_y = y + yMove[k];

                if (isSafe(next_x, next_y))
                {
                    soln[next_x, next_y] = movei;

                    if (solveKTUtil(next_x, next_y, movei + 1, xMove, yMove))
                        return true;
                    else
                        soln[next_x, next_y] = -1;
                }
            }
            return false;
        }

        public bool solveKnightTour()
        {
            for (int x = 0; x < N; x++)
            {
                for (int y = 0; y < N; y++)
                {
                    soln[x, y] = -1;
                }
            }

            int[] xMove = { 2, 1, -1, -2, -2, -1, 1, 2 };
            int[] yMove = { 1, 2, 2, 1, -1, -2, -2, -1 };

            soln[0, 0] = 0;

            if (!solveKTUtil(0, 0, 1, xMove, yMove))
            {
                Console.WriteLine("the solution does not exist");
                return false;
            }
            else
            {
                printSolution();
            }
            return true;
        }

        public static void KnightTourTest(String arg)
        {
            GeneralSamples knightTour = new GeneralSamples();
            Console.WriteLine("the solution is");

            knightTour.solveKnightTour();
        }
    }

}
