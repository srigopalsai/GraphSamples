using System;
using System.Collections.Generic;

namespace GraphSamples
{
    /*
     http://www.sanfoundry.com/java-program-gabow-algorithm/
    */
    public class Gabow
    {
        /** number of vertices **/

        private int V;
        /** preorder number counter **/

        private int preCount;

        private int[] preorder;
        /** to check if v is visited **/

        private bool[] visited;
        /** check strong componenet containing v **/

        private bool[] chk;
        /** to store given graph **/

        private List<int>[] graph;
        /** to store all scc **/

        private List<List<int>> sccComp;

        private Stack<int> stack1;

        private Stack<int> stack2;
        /** function to get all strongly connected components **/

        public List<List<int>> getSCComponents(List<int>[] graph)
        {
            V = graph.Length;
            this.graph = graph;
            preorder = new int[V];
            chk = new bool[V];
            visited = new bool[V];
            stack1 = new Stack<int>();
            stack2 = new Stack<int>();
            sccComp = new List<List<int>>();

            for (int v = 0; v < V; v++)

                if (!visited[v])
                    dfs(v);
            return sccComp;
        }
        /** function dfs **/

        public void dfs(int v)
        {
            preorder[v] = preCount++;
            visited[v] = true;
            stack1.Push(v);
            stack2.Push(v);

            foreach (int w in graph[v])
            {
                if (!visited[w])
                    dfs(w);
                else if (!chk[w])

                    while (preorder[stack2.Peek()] > preorder[w])
                        stack2.Pop();
            }

            if (stack2.Peek() == v)
            {
                stack2.Pop();
                List<int> component = new List<int>();
                int w;

                do
                {
                    w = stack1.Pop();
                    component.Add(w);
                    chk[w] = true;
                } while (w != v);

                sccComp.Add(component);
            }
        }
        /** main **/

        public static void GabowShortestPathTest()
        {
            Console.WriteLine("Gabow algorithm Test\n");
            Console.WriteLine("Enter number of Vertices");

            /** number of vertices **/
            int V = Convert.ToInt32(Console.ReadLine());
            /** make graph **/
            List<int>[] g = new List<int>[V];

            for (int i = 0; i < V; i++)
                g[i] = new List<int>();

            /** accpet all edges **/
            Console.WriteLine("\nEnter number of edges");
            int E = Convert.ToInt32(Console.ReadLine());
            /** all edges **/
            Console.WriteLine("Enter " + E + " x, y coordinates");

            for (int i = 0; i < E; i++)
            {
                int x = Convert.ToInt32(Console.ReadLine());
                int y = Convert.ToInt32(Console.ReadLine());
                g[x].Add(y);
            }

            Gabow gab = new Gabow();
            Console.WriteLine("\nSCC : ");
            /** print all strongly connected components **/
            List<List<int>> scComponents = gab.getSCComponents(g);
            Console.WriteLine(scComponents);
        }
    }
}