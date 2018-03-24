using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    /*
        http://www.sanfoundry.com/java-program-implement-traveling-salesman-problem-using-nearest-neighbour-algorithm/
        https://en.wikipedia.org/wiki/Travelling_salesman_problem

        000  374  200  223  108  178  252  285  240  356
        374  000  255  166  433  199  135  095  136  017
        200  255  000  128  277  128  180  160  131  247
        223  166  128  000  430  047  052  084  040  155
        108  433  277  430  000  453  478  344  389  423
        178  199  128  047  453  000  091  110  064  181
        252  135  180  052  478  091  000  114  083  117
        285  095  160  084  344  110  114  000  047  078
        240  136  131  040  389  064  083  047  000  118
        356  017  247  155  423  181  117  078  118  000

        The citys are visited as follows  1	   5	3	2	9	7	4
    */

    public partial class NPSamples
    {
        public void TSPUsingNNA(int[,] matrix)
        {
            // If index zero not considered for vertex ids then make startVertex as 0.
            int startVertex = 0;

            for (int i = startVertex; i < matrix.GetLength(0); i++)
            {
                for (int j = startVertex; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 1 && matrix[j, i] == 0)
                    {
                        matrix[j, i] = 1;
                    }
                }
            }

            Stack<int> stack = new Stack<int>();

            int noOfNodes = matrix.GetLength(0) - 1;
            int[] visited = new int[noOfNodes + 1];

            visited[1] = startVertex;
            stack.Push(startVertex);

            int element;
            int destination = 0;
            int curPos;
            int min = int.MaxValue;
            bool minFlag = false;

            Console.Write(startVertex + "\t");

            while (stack.Count() > 0)
            {
                element = stack.Peek();
                curPos = startVertex;
                min = int.MaxValue;

                while (curPos <= noOfNodes)
                {
                    if (matrix[element, curPos] > 1 && visited[curPos] == 0)
                    {
                        if (min > matrix[element, curPos])
                        {
                            min = matrix[element, curPos];
                            destination = curPos;
                            minFlag = true;
                        }
                    }
                    curPos++;
                }

                if (minFlag)
                {
                    visited[destination] = 1;
                    stack.Push(destination);

                    Console.Write(destination + "\t");

                    minFlag = false;
                    continue;
                }
                stack.Pop();
            }
        }

        public void TSPUsingNNATest()
        {
            try
            {
                Console.WriteLine("Travelling Salesman - Using Nearest Neighbour Algorithm - Matrix");

                int[,] matrix = TestData.BinaryMatrices[TestData.Keys.TenByTen3];

                TestData.Display(matrix);
                Console.WriteLine("The citys are visited as follows");

                TSPUsingNNA(matrix);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wrong Input format" + ex.Message);
            }
        }
    }
}