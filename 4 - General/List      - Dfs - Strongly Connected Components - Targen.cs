using System;
using System.Collections.Generic;

namespace GraphSamples
{
    // Strongly Connected Components of directed graph.
    // Time O(E + V)  Space O(V)
    // https://en.wikipedia.org/wiki/Tarjan%27s_strongly_connected_components_algorithm
    //https://stackoverflow.com/questions/6643076/tarjan-cycle-detection-help-c-sharp#sca

    public partial class GeneralSamples
    {
        // Keeps Dictionary of vertex to time it was visited
        private Dictionary<Vertex, int> visitedTime;

        // Keeps Dictionary of vertex and time of first vertex visited in current DFS
        private Dictionary<Vertex, int> lowTime;

        // Tells if a vertex is in stack or not
        private HashSet<Vertex> onHashSet;

        // Stack of visited vertices
        private Stack<Vertex> stack;

        // Tells if vertex has ever been visited or not. This is for DFS purpose.
        private HashSet<Vertex> visited;

        // Stores the strongly connected components result;
        private List<HashSet<Vertex>> result;

        // Keeps the time when every vertex is visited
        private int time;

        public List<HashSet<Vertex>> TarganStronglyConnectedComponents(Graph graph)
        {
            time = 0;

            visitedTime = new Dictionary<Vertex, int>();            
            lowTime = new Dictionary<Vertex, int>();
            onHashSet = new HashSet<Vertex>();

            stack = new Stack<Vertex>();
            visited = new HashSet<Vertex>();
            result = new List<HashSet<Vertex>>();

            // Start from any vertex in the graph.
            foreach (KeyValuePair<long,Vertex> vertexPair in graph.Verticies)
            {
                if (visited.Contains(vertexPair.Value))
                    continue;

                TarganStronglyConnectedComponentsUtil(vertexPair.Value);
            }

            return result;
        }

        private void TarganStronglyConnectedComponentsUtil(Vertex vertex)
        {
            visited.Add(vertex);
            visitedTime[vertex] = time;
            lowTime[vertex] = time;
            time++;

            stack.Push(vertex);
            onHashSet.Add(vertex);

            foreach (Vertex child in vertex.Adjacents)
            {
                // If child is not visited then visit it and see if it has link back to vertex's ancestor. 
                // In that case update low time to ancestor's visit time
                if (!visited.Contains(child))
                {
                    TarganStronglyConnectedComponentsUtil(child);
                    lowTime[vertex] = Math.Min(lowTime[vertex], lowTime[child]);
                    //lowTime.Compute(vertex, (v, low)-> Math.min(low, lowTime[child]));
                }

                // If child is on stack then see if it was visited before vertex's low time. If yes then update vertex's low time to that.
                else if (onHashSet.Contains(child))
                {
                    lowTime[vertex] = Math.Min(lowTime[vertex], visitedTime[child]);
                    //lowTime.Compute(vertex, (v, low)->Math.min(low, visitedTime[child]));
                }
            }

            // If vertex low time is same as visited time then this is start vertex for strongly connected component.
            // keep popping vertices out of stack still you find current vertex. 
            // They are all part of one strongly connected component.

            if (visitedTime[vertex] == lowTime[vertex])
            {
                HashSet<Vertex> stronglyConnectedComponenet = new HashSet<Vertex>();
                Vertex _vertex;

                do
                {
                    _vertex = stack.Pop();
                    onHashSet.Remove(_vertex);
                    stronglyConnectedComponenet.Add(_vertex);

                } while (!vertex.Equals(_vertex));

                result.Add(stronglyConnectedComponenet);
            }
        }

        public void TarganStronglyConnectedComponentsTest()
        {
            Graph graph = new Graph(true);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 1);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 5);
            graph.AddEdge(5, 6);
            graph.AddEdge(6, 4);
            graph.AddEdge(7, 6);
            graph.AddEdge(7, 8);
            graph.AddEdge(8, 7);

            graph.Display();

            List<HashSet<Vertex>> result = TarganStronglyConnectedComponents(graph);

            foreach (HashSet<Vertex> vertexSet in result)
            {
                foreach (Vertex vertex in vertexSet)
                    Console.Write(vertex.Id + " ");
                Console.WriteLine();
            }
        }
    }
}
