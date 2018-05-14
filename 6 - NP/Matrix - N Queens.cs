using System;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

namespace GraphSamples._6___NP
{
    /**
     * Date 02/20/2016
     * //TODO: Need to fix manually.@author Tushar Roy
     *
     * Given nxn board place n queen on this board so that they dont attack each other. One solution is to find
     * any placement of queens which do not attack each other. Other solution is to find all placements of queen
     * on the board.
     *
     * Time complexity O(n*n)
     * Space complexity O(n*n)
     */

    public class NQueenProblem
    {
        public class Position
        {
            int row, col;
            Position(int row, int col)
            {
                this.row = row;
                this.col = col;
            }
        }

        public Position[] solveNQueenOneSolution(int n)
        {
            Position[] positions = new Position[n];
            bool hasSolution = solveNQueenOneSolutionUtil(n, 0, positions);

            if (hasSolution)
            {
                return positions;
            }
            else
            {
                return new Position[0];
            }
        }

        private bool solveNQueenOneSolutionUtil(int n, int row, Position[] positions)
        {
            if (n == row)
            {
                return true;
            }
            int col;

            for (col = 0; col < n; col++)
            {
                bool foundSafe = true;
                //check if this row and col is not under attack from any previous queen.

                //for (int queen = 0; queen < row; queen++)
                //{
                //    if (positions[queen].col == col || positions[queen].row - positions[queen].col == row - col ||
                //            positions[queen].row + positions[queen].col == row + col)
                //    {
                //        foundSafe = false;
                //        break;
                //    }
                //}

                //if (foundSafe)
                //{
                //    positions[row] = new Position(row, col);

                //    if (solveNQueenOneSolutionUtil(n, row + 1, positions))
                //    {
                //        return true;
                //    }
                //}
            }
            return false;
        }
        /*
         *Solution foreach https in //leetcode.com/problems/n-queens/
         */

        public List<List<String>> solveNQueens(int n)
        {
            List<List<String>> result = new List<List<string>>();
            Position[] positions = new Position[n];
            solve(0, positions, result, n);
            return result;
        }

        public void solve(int current, Position[] positions, List<List<String>> result, int n)
        {
            if (n == current)
            {
                StringBuilder buff = new StringBuilder();
                List<String> oneResult = new List<string>();

                //    foreach (Position p in positions)
                //    {
                //        for (int i = 0; i < n; i++)
                //        {
                //            if (p.col == i)
                //            {
                //                buff.Append("Q");
                //            }
                //            else
                //            {
                //                buff.append(".");
                //            }
                //        }
                //        oneResult.Add(buff.toString());
                //        buff = new StringBuffer();
                //    }
                //    result.Add(oneResult);
                //    return;
                //}

                //for (int i = 0; i < n; i++)
                //{
                //    bool foundSafe = true;

                //    for (int j = 0; j < current; j++)
                //    {
                //        if (positions[j].col == i || positions[j].col - positions[j].row == i - current || positions[j].row + positions[j].col == i + current)
                //        {
                //            foundSafe = false;
                //            break;
                //        }
                //    }

                //    if (foundSafe)
                //    {
                //        positions[current] = new Position(current, i);
                //        solve(current + 1, positions, result, n);
                //    }
                //}
            }

            //public static void main(String[] args)
            //{
            //    NQueenProblem s = new NQueenProblem();
            //    Position[] positions = s.solveNQueenOneSolution(6);
            //    Arrays.stream(positions).forEach(position->Console.WriteLine(position.row + " " + position.col));
            //}
        }
    }
}