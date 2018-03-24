using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    /*
     The Held–Karp algorithm is a dynamic programming algorithm, and an extension of the Hamiltonian circuit problem.  
     Time  Complexity - Exponential O(2^n * n^2)
     Space Complexity - O(2^n * n)
 
     https://en.wikipedia.org/wiki/Held%E2%80%93Karp_algorithm
     https://en.wikipedia.org/wiki/Travelling_salesman_problem
    */

    public partial class NPSamples
    {
        private int INFINITY = 100000000;

        private class Index
        {
            int CurrentVertex { get; set; }

            HashSet<int> VertexHashSet { get; set; }

            public override bool Equals(Object obj)
            {
                if (this == obj)
                    return true;

                if (obj == null || this.GetType() != obj.GetType())
                    return false;

                Index index = (Index)obj;

                if (CurrentVertex != index.CurrentVertex)
                    return false;

                return !(VertexHashSet != null ? !VertexHashSet.Equals(index.VertexHashSet) : index.VertexHashSet != null);
            }

            public override int GetHashCode()
            {
                int result = CurrentVertex;
                result = 31 * result + (VertexHashSet != null ? VertexHashSet.GetHashCode() : 0);
                return result;
            }

            public static Index CreateIndex(int vertex, HashSet<int> vertexHashSet)
            {
                Index index = new Index();
                index.CurrentVertex = vertex;
                index.VertexHashSet = vertexHashSet;
                return index;
            }
        }

        private class HashSetSizeComparator : IComparer<HashSet<int>>
        {
            public int Compare(HashSet<int> o1, HashSet<int> o2)
            {
                return o1.Count - o2.Count;
            }
        }

        public int MinCost(int[,] distance)
        {
            //stores intermediate values in Dictionary
            Dictionary<Index, int> minCostDP = new Dictionary<Index, int>();
            Dictionary<Index, int> parent = new Dictionary<Index, int>();

            List<HashSet<int>> allHashSets = GenerateCombination(distance.Length - 1);

            foreach (HashSet<int> curHashSet in allHashSets)
            {
                for (int currentVertex = 1; currentVertex < distance.Length; currentVertex++)
                {
                    if (curHashSet.Contains(currentVertex))
                        continue;

                    Index index = Index.CreateIndex(currentVertex, curHashSet);

                    int minCost = INFINITY;
                    int minPrevVertex = 0;

                    //to avoid ConcurrentModificationException copy HashSet into another HashSet while iterating
                    HashSet<int> copyHashSet1 = new HashSet<int>(curHashSet);

                    foreach (int prevVertex1 in curHashSet)
                    {
                        int cost = distance[prevVertex1,currentVertex] + GetCost(copyHashSet1, prevVertex1, minCostDP);
                        if (cost < minCost)
                        {
                            minCost = cost;
                            minPrevVertex = prevVertex1;
                        }
                    }

                    //this happens for empty subHashSet
                    if (curHashSet.Count == 0)
                    {
                        minCost = distance[0,currentVertex];
                    }

                    minCostDP[index]= minCost;
                    parent[index] =minPrevVertex;
                }
            }

            HashSet<int> hashSet = new HashSet<int>();

            for (int lpIndx = 1; lpIndx < distance.Length; lpIndx++)
                hashSet.Add(lpIndx);

            int min = int.MaxValue;
            int prevVertex = -1;

            // To avoid ConcurrentModificationException copy HashSet into another HashSet while iterating
            HashSet<int> copyHashSet = new HashSet<int>(hashSet);

            foreach (int k in hashSet)
            {
                int cost = distance[k,0] + GetCost(copyHashSet, k, minCostDP);
                if (cost < min)
                {
                    min = cost;
                    prevVertex = k;
                }
            }

            parent[Index.CreateIndex(0, hashSet)] = prevVertex;
            PrintTour(parent, distance.Length);

            return min;
        }

        private void PrintTour(Dictionary<Index, int> parent, int totalVertices)
        {
            HashSet<int> hashSet = new HashSet<int>();

            for (int lpIndx = 0; lpIndx < totalVertices; lpIndx++)
                hashSet.Add(lpIndx);

            int start = 0;

            Console.WriteLine("\nTSP tour");

            while (true)
            {
                hashSet.Remove(start);

                Console.Write(start + " ");
                start = parent[Index.CreateIndex(start, hashSet)];

                if (start == 0)
                    break;
            }
        }

        private int GetCost(HashSet<int> hashSet, int prevVertex, Dictionary<Index, int> minCostDict)
        {
            hashSet.Remove(prevVertex);

            Index index = Index.CreateIndex(prevVertex, hashSet);

            int cost = minCostDict[index];
            hashSet.Add(prevVertex);
            return cost;
        }

        private List<HashSet<int>> GenerateCombination(int n)
        {
            int[] inputArray = new int[n];

            for (int lpIndx = 0; lpIndx < inputArray.Length; lpIndx++)
            {
                inputArray[lpIndx] = lpIndx + 1;
            }

            List<HashSet<int>> allHashSets = new List<HashSet<int>>();
            int[] resultArray = new int[inputArray.Length];

            GenerateCombination(inputArray, 0, 0, allHashSets, resultArray);

            allHashSets.Sort(new HashSetSizeComparator());
            return allHashSets;
        }

        private void GenerateCombination(int[] input, int start, int position, List<HashSet<int>> allHashSets, int[] resultArray)
        {
            if (position == input.Length)
                return;

            HashSet<int> HashSet = CreateHashSet(resultArray, position);
            allHashSets.Add(HashSet);

            for (int lpIndx = start; lpIndx < input.Length; lpIndx++)
            {
                resultArray[position] = input[lpIndx];
                GenerateCombination(input, lpIndx + 1, position + 1, allHashSets, resultArray);
            }
        }

        private static HashSet<int> CreateHashSet(int[] input, int position)
        {
            HashSet<int> hashSet = new HashSet<int>();
      
            for (int lpIndx = 0; lpIndx < position; lpIndx++)
            {
                hashSet.Add(input[lpIndx]);
            }

            return hashSet;
        }
    }
}