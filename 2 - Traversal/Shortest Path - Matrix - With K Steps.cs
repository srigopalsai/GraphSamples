using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    public partial class TraversalSamples
    {
        //// https://www.geeksforgeeks.org/count-possible-paths-source-destination-exactly-k-edges/
        //// O(V^k)
        //// Number of vertices
        //static int V = 4;

        //// A naive recursive function to 
        //// count walks from u to v with 
        //// k edges
        //static int countwalks(int[,] graph, int u,   int v, int k)
        //{

        //    // Base cases
        //    if (k == 0 && u == v)
        //        return 1;
        //    if (k == 1 && graph[u, v] == 1)
        //        return 1;
        //    if (k <= 0)
        //        return 0;

        //    // Initialize result
        //    int count = 0;

        //    // Go to all adjacents of u and recur
        //    for (int i = 0; i < V; i++)

        //        // Check if is adjacent of u
        //        if (graph[u, i] == 1)
        //            count +=
        //            countwalks(graph, i, v, k - 1);

        //    return count;
        //}

        //// Driver method
        //public static void Main()
        //{

        //    /* Let us create the graph shown 
        //    in above diagram*/
        //    int[,] graph =
        //       new int[,] { {0, 1, 1, 1},
        //                {0, 0, 0, 1},
        //                {0, 0, 0, 1},
        //                {0, 0, 0, 0} };

        //    int u = 0, v = 3, k = 2;

        //    Console.Write(
        //         countwalks(graph, u, v, k));
        //}

        //static int V = 4; //Number of vertices

        //// A Dynamic programming based function
        //// to count walks from u to v with k edges
        //static int countwalks(int[,] graph, int u,
        //                             int v, int k)
        //{
        //    // Table to be filled up using DP. The
        //    // value count[i][j][e] will/ store 
        //    // count of possible walks from i to 
        //    // j with exactly k edges
        //    int[,,] count = new int[V, V, k + 1];

        //    // Loop for number of edges from 0 to k
        //    for (int e = 0; e <= k; e++)
        //    {

        //        // for source
        //        for (int i = 0; i < V; i++)
        //        {

        //            // for destination
        //            for (int j = 0; j < V; j++)
        //            {
        //                // initialize value
        //                count[i, j, e] = 0;

        //                // from base cases
        //                if (e == 0 && i == j)
        //                    count[i, j, e] = 1;
        //                if (e == 1 && graph[i, j] != 0)
        //                    count[i, j, e] = 1;

        //                // go to adjacent only when
        //                // number of edges
        //                // is more than 1
        //                if (e > 1)
        //                {
        //                    // adjacent of i
        //                    for (int a = 0; a < V; a++)
        //                        if (graph[i, a] != 0)
        //                            count[i, j, e] +=
        //                                 count[a, j, e - 1];
        //                }
        //            }
        //        }
        //    }

        //    return count[u, v, k];
        //}

        //// Driver method
        //public static void Main()
        //{
        //    /* Let us create the graph shown in 
        //    above diagram*/
        //    int[,] graph = { {0, 1, 1, 1},
        //                 {0, 0, 0, 1},
        //                 {0, 0, 0, 1},
        //                 {0, 0, 0, 0} };
        //    int u = 0, v = 3, k = 2;

        //    Console.WriteLine(countwalks(graph, u, v, k));
        //}

        //// Shortest path with by traversing k (current cell value) steps
        //// http://www.techiedelight.com/find-shortest-path-source-destination-matrix-satisfies-given-constraints/
        //// N x N matrix
        //int N = 10;

        //// queue node used in BFS
        //Dictionary<int, int> Node;
        //// Below arrays details all 4 possible movements from a cell
        //int[] row = { -1, 0, 0, 1 };
        //int[] col = { 0, -1, 1, 0 };
        //// Function to check if it is possible to go to position pt
        //// from current position. The function returns false if pt is
        //// not a valid position or it is already visited
        //bool isValid(Node pt, Dictionary<Node, int> visited)
        //{
        //    return (pt.first >= 0) && (pt.first < N) &&
        //        (pt.second >= 0) && (pt.second < N) && !visited.count(pt);
        //}
        //// Find shortest path Length in the matrix from source cell (x, y) to
        //// destination cell (N - 1, N - 1)
        //int findPath(int[,] mat, int x, int y)
        //{
        //    // create a queue and enqueue first node
        //    queue<Node> Q;
        //    Node src = { x, y };
        //    Q.Push(src);
        //    // map to check if matrix cell is visited before or not. It also
        //    // stores the shortest distance info i.e. the value corresponding
        //    // to a node in map represents its shortest distance from the source
        //    Dictionary<Node, int> visited;

        //    visited[src] = 0;
        //    // run till queue is not empty

        //    while (!Q.empty())
        //    {
        //        // pop front node from queue and process it
        //        Node node = Q.front();
        //        Q.Pop();
        //        int i = node.first;
        //        int j = node.second;
        //        int dist = visited[node];
        //        // if destination is found, return true

        //        if (i == N - 1 && j == N - 1)
        //            return dist;
        //        // value of current cell
        //        int n = mat[i, j];
        //        // check all 4 possible movements from current cell
        //        // and recurse for each valid movement

        //        for (int k = 0; k < 4; k++)
        //        {
        //            // get next position using value of current cell
        //            Node next = { (i + row[k] * n), (j + col[k] * n) };
        //            // check if it is possible to go to position (x, y)
        //            // from current position

        //            if (isValid(next, visited))
        //            {
        //                Q.Push(next);
        //                visited[next] = dist + 1;
        //            }
        //        }
        //    }

        //    // return INFINITY if path is not possible
        //    return INT_MAX;
        //}
        //// main function
        //int main()
        //{
        //    int[,] matrix = {   { 7, 1, 3, 5, 3, 6, 1, 1, 7, 5 },
        //                        { 2, 3, 6, 1, 1, 6, 6, 6, 1, 2 },
        //                        { 6, 1, 7, 2, 1, 4, 7, 6, 6, 2 },
        //                        { 6, 6, 7, 1, 3, 3, 5, 1, 3, 4 },
        //                        { 5, 5, 6, 1, 5, 4, 6, 1, 7, 4 },
        //                        { 3, 5, 5, 2, 7, 5, 3, 4, 3, 6 },
        //                        { 4, 1, 4, 3, 6, 4, 5, 3, 2, 6 },
        //                        { 4, 4, 1, 7, 4, 3, 3, 1, 4, 2 },
        //                        { 4, 4, 5, 1, 5, 2, 3, 5, 3, 5 },
        //                        { 3, 6, 3, 5, 2, 2, 6, 4, 2, 1 } };

        //    // Find a route in the matrix from source cell (0, 0) to
        //    // destination cell (N - 1, N - 1)
        //    int len = findPath(matrix, 0, 0);

        //    if (len != INT_MAX)
        //    {
        //        cout << "Shortest Path Length is " << len;
        //    }
        //    return 0;
        //}
    }
}
