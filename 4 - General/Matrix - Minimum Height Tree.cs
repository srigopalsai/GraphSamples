using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples._4___General
{
    //Given a graph representing a tree. Find all minimum height trees.
    //Time complexity O(n)
    //Space complexity O(n)

    //https://leetcode.com/problems/minimum-height-trees/

    public class MinimumHeightTree
    {
        public List<int> FindMinHeightTrees(int n, int[,] matrix)
        {
            List<int> leaves = new List<int>();

            if (n == 1)
                return leaves;

            List<HashSet<int>> adj = new List<HashSet<int>>();

            for (int i = 0; i < n; i++)
                adj.Add(new HashSet<int>());

            for (int rIndx = 0; rIndx < matrix.GetLength(0); rIndx++)
            {
                for (int cIndx = 0; cIndx < matrix.GetLength(0); cIndx++)
                {
//                    adj[edge[0]].Add(edge[1]);
                }
            }

            for (int rIndx = 0; rIndx < matrix.GetLength(0); rIndx++)
            {
                adj[matrix[rIndx, 0]].Add(matrix[rIndx, 1]);
                adj[matrix[rIndx, 1]].Add(matrix[rIndx, 0]);
            }

            for (int i = 0; i < n; i++)
            {
                if (adj[i].Count() == 1)
                {
                    leaves.Add(i);
                }
            }

            while (n > 2)
            {
                n -= leaves.Count();
                List<int> newLeaves = new List<int>();

                foreach (int leaf in leaves)
                {
                    int node = adj[leaf].First();

                    adj[node].Remove(leaf);
                    if (adj[node].Count() == 1)
                    {
                        newLeaves.Add(node);
                    }
                }
                leaves = newLeaves;
            }

            return leaves;
        }

        public void FindMinHeightTreesTest()
        {
            int[,] matrix = { { 1, 0 },
                              { 1, 2 },
                              { 1, 3 } };
            int n = 4;

            Console.WriteLine("Find Minimum Height Trees: ");

            List<int> result = FindMinHeightTrees(n, matrix);

            foreach (int item in result)
                Console.Write(n + " to " + item);
        }
    }
}