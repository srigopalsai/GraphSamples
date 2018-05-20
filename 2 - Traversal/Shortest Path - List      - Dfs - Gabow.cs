using System;
using System.Collections.Generic;

namespace GraphSamples
{    
    // http://www.sanfoundry.com/java-program-gabow-algorithm/
    
    public partial class TraversalSamples
    {
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
            int graphLen = graph.Length;
            this.graph = graph;
            preorder = new int[graphLen];
            chk = new bool[graphLen];
            visited = new bool[graphLen];

            stack1 = new Stack<int>();
            stack2 = new Stack<int>();
            sccComp = new List<List<int>>();

            for (int v = 0; v < graphLen; v++)

                if (!visited[v])
                    dfs(v);
            return sccComp;
        }

        public void dfs(int vertex)
        {
            preorder[vertex] = preCount++;
            visited[vertex] = true;
            stack1.Push(vertex);
            stack2.Push(vertex);

            foreach (int w in graph[vertex])
            {
                if (!visited[w])
                {
                    dfs(w);
                }
                else if (!chk[w])
                {
                    while (preorder[stack2.Peek()] > preorder[w])
                    {
                        stack2.Pop();
                    }
                }
            }

            if (stack2.Peek() == vertex)
            {
                stack2.Pop();
                List<int> component = new List<int>();
                int w;

                do
                {
                    w = stack1.Pop();
                    component.Add(w);
                    chk[w] = true;
                } while (w != vertex);

                sccComp.Add(component);
            }
        }
    }
}