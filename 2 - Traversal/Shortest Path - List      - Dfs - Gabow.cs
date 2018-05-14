using System;
using System.Collections.Generic;

namespace GraphSamples
{    
    // http://www.sanfoundry.com/java-program-gabow-algorithm/
    
    public partial class TraversalSamples
    {
        private int V;

        private int preCount;

        private int[] preorder;

        private bool[] visited;

        private bool[] chk;

        private List<int>[] graph;

        private List<List<int>> sccComp;

        private Stack<int> stack1;

        private Stack<int> stack2;

        public List<List<int>> GetSCComponents(List<int>[] graph)
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
    }
}