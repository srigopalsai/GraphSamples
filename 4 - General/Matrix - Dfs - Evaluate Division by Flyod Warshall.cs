using System;
using System.Collections.Generic;

namespace GraphSamples
{
    /*
    Equations are given in the format A / B = k, where A and B are variables represented as strings, and k is a real number(floating point number).
    Given some queries, return the answers.If the answer does not exist, return -1.0.

    Solution
    Do Flyod warshall algorithm initialized as values between equations.Do Flyod Warshall to create
    all possible paths b/w two strings.

    Time complexity O(n* n * n) + O(m)
    where n is total number of strings in equations and m is total number of queries
    https://github.com/mission-peace/interview/blob/master/src/com/interview/graph/EvaluateDivison.java
    https://leetcode.com/problems/evaluate-division/
    */

    public partial class GeneralSamples
    {
        public double[] calcEquation(String[,] equations, double[] values, String[,] queries)
        {
            if (equations.Length == 0)
                return new double[0];

            Dictionary<String, int> indexDictionary = new Dictionary<string, int>();

            int count = 0;
            for (int lpIndx = 0; lpIndx < equations.Length; lpIndx++)
            {
                String first = equations[lpIndx, 0];
                String second = equations[lpIndx, 1];

                if (!indexDictionary.ContainsKey(first))
                {
                    indexDictionary[first] = count++;
                }
                if (!indexDictionary.ContainsKey(second))
                {
                    indexDictionary[second] = count++;
                }
            }

            double[,] graph = new double[count, count];

            for (int lpRIndx = 0; lpRIndx < graph.Length; lpRIndx++)
            {
                for (int lpCIndx = 0; lpCIndx < graph.GetLength(lpRIndx); lpCIndx++)
                {
                    graph[lpRIndx, lpCIndx] = -1;
                }
            }

            for (int lpIndx = 0; lpIndx < equations.Length; lpIndx++)
            {
                String first = equations[lpIndx, 0];
                String second = equations[lpIndx, 1];

                int i1 = indexDictionary[first];
                int i2 = indexDictionary[second];

                graph[i1, i1] = graph[i2, i2] = 1.0;
                graph[i1, i2] = values[lpIndx];
                graph[i2, i1] = 1 / values[lpIndx];
            }

            for (int lpRIndx = 0; lpRIndx < graph.Length; lpRIndx++)
            {
                for (int lpCIndx = 0; lpCIndx < graph.Length; lpCIndx++)
                {
                    if (graph[lpRIndx, lpCIndx] != -1)
                        continue;

                    for (int k = 0; k < graph.Length; k++)
                    {
                        if (graph[lpRIndx, k] == -1 || graph[k, lpCIndx] == -1)
                            continue;

                        graph[lpRIndx, lpCIndx] = graph[lpRIndx, k] * graph[k, lpCIndx];
                    }
                }
            }

            double[] result = new double[queries.Length];

            for (int lpIndx = 0; lpIndx < queries.Length; lpIndx++)
            {
                String first = queries[lpIndx, 0];
                String second = queries[lpIndx, 1];

                if (!indexDictionary.ContainsKey(first) || !indexDictionary.ContainsKey(second))
                    result[lpIndx] = -1;
                else
                {
                    int i1 = indexDictionary[first];
                    int i2 = indexDictionary[second];
                    result[lpIndx] = graph[i1, i2];
                }
            }

            return result;
        }
    }
}