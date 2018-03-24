using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace GraphSamples
{
    public class Graph2
    {
        private ConcurrentDictionary<int, List<int>> adjacencyList;

        public Graph2(int v)
        {
            adjacencyList = new ConcurrentDictionary<int, List<int>>();

            for (int i = 1; i <= v; i++)
                adjacencyList[i] = new List<int>();
        }

        public void AddEdge(int from, int to)
        {
            if (to > adjacencyList.Count() || from > adjacencyList.Count())
                Console.WriteLine("The vertices does not exists");
            /*
            * List<int> sls = adjacencyList[to];
            * sls.Add(from);
            */
            List<int> dls = adjacencyList[from];
            dls.Add(to);
        }

        public List<int> getEdge(int to)
        {
            if (to > adjacencyList.Count())
            {
                Console.WriteLine("The vertices does not exists");
                return null;
            }

            return adjacencyList[to];
        }

        public bool IsDirectedAcyclicGraph()
        {
            int count = 0;
            int size = adjacencyList.Count() - 1;

            IEnumerator<int> vertexIterator = adjacencyList.Keys.GetEnumerator();

            while (vertexIterator.MoveNext())
            {
                int currentVertex = vertexIterator.Current;
                List<int> adjList = adjacencyList[currentVertex];

                if (count == size)
                {
                    Console.WriteLine("Result:  Given graph is DAG (Directed Acyclic Graph).\n");
                    return true;
                }

                if (adjList.Count() == 0)
                {
                    count++;

                    Console.WriteLine("Target Node - " + currentVertex);

                    IEnumerator<int> adjacentIterator = adjacencyList.Keys.GetEnumerator();

                    while (adjacentIterator.MoveNext())
                    {
                        int adjcentCurrVertex = adjacentIterator.Current;
                        List<int> adjcentsList = adjacencyList[adjcentCurrVertex];

                        if (adjcentsList.Contains(currentVertex))
                        {
                            adjcentsList.Remove(currentVertex);
                            Console.WriteLine("  Deleting edge between target node " + currentVertex + " - " + adjcentCurrVertex + " ");
                        }
                    }

                    List<int> verticies;
                    bool result = adjacencyList.TryRemove(currentVertex, out verticies);

                    if (result == false)
                    {
                        Console.Error.WriteLine("Unable to remove the item");
                    }

                    vertexIterator = adjacencyList.Keys.GetEnumerator();
                }
            }

            Console.WriteLine("Result:  Given graph is NOT DAG (Directed Acyclic Graph).\n");
            return false;
        }

        public Graph2 CheckDirectedAcyclicGraph()
        {
            IsDirectedAcyclicGraph();
            return this;
        }

        public bool FindGoodFeedbackSet(int v, bool edgeSet=false)
        {
            CheckDirectedAcyclicGraph();

            bool flag = false;
            int[] visited = new int[v + 1];

            IEnumerator<int> iterator = adjacencyList.Keys.GetEnumerator();

            if (edgeSet == false)
                Console.Write("The set of Vertices in feedback vertex set: \n");
            else
                Console.Write("The set of Edges in feedback arc set: \n");

            while (iterator.MoveNext())
            {
                int i = iterator.Current;
                List<int> list = adjacencyList[i];
                visited[i] = 1;

                if (list.Count != 0)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (visited[list[j]] == 1)
                        {
                            flag = true;
                            if (edgeSet == true)
                                Console.Write(i + " - ");

                            Console.WriteLine(list[j]);
                        }
                        else
                            visited[list[j]] = 1;
                    }
                }
            }

            if (flag == false)
                Console.WriteLine("None");

            return flag;
        }

        public void DisplayGraph()
        {
            Console.WriteLine("\nThe Graph is: ");

            for (int vertex = 1; vertex <= adjacencyList.Count(); vertex++)
            {
                List<int> edgeList = getEdge(vertex);

                if (edgeList.Count() != 0)
                {
                    Console.Write(vertex);

                    for (int j = 0; j < edgeList.Count(); j++)
                    {
                        Console.Write(" -> " + edgeList[j]);
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}