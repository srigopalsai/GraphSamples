using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    /*  http://www.sanfoundry.com/java-program-use-boruvkas-algorithm-find-minimum-spanning-tree/

        References        https://en.wikipedia.org/wiki/Bor%C5%AFvka%27s_algorithm
    */

    //public partial class MSTSamples
    //{

    //    public class BoruvkasMST
    //    {
    //        private List<Edge> mst = new List<Edge>();    // Edge in MST

    //        private double weight;                      // weight of MST

    //        public BoruvkasMST(EdgeWeightedGraph G)
    //        {
    //            UF uf = new UF(G.V());
    //            // repeat at most log V times or until we have V-1 Edge

    //            for (int t = 1; t < G.V() && mst.Count < G.V() - 1; t = t + t)
    //            {
    //                // foreach tree in forest, find closest edge
    //                // if edge weights are equal, ties are broken in favor of first edge
    //                // in G.Edge()
    //                Edge[] closest = new Edge[G.V()];

    //                foreach (Edge e in G.Edge())
    //                {
    //                    int v = e.either(), w = e.other(v);
    //                    int i = uf.find(v), j = uf.find(w);

    //                    if (i == j)
    //                        continue;   // same tree

    //                    if (closest[i] == null || less(e, closest[i]))
    //                        closest[i] = e;

    //                    if (closest[j] == null || less(e, closest[j]))
    //                        closest[j] = e;
    //                }
    //                // add newly discovered Edge to MST

    //                for (int i = 0; i < G.V(); i++)
    //                {
    //                    Edge e = closest[i];

    //                    if (e != null)
    //                    {
    //                        int v = e.either(), w = e.other(v);
    //                        // don't add the same edge twice

    //                        if (!uf.connected(v, w))
    //                        {
    //                            mst.Add(e);
    //                            Weight += e.Weight();
    //                            uf.union(v, w);
    //                        }
    //                    }
    //                }
    //            }
    //            // check optimality conditions
    //            //TODO: assert check(G);
    //        }

    //        public IEnumerable<Edge> Edge()
    //        {
    //            return mst;
    //        }

    //        public double weight()
    //        {
    //            return weight;
    //        }

    //        // is the weight of edge e strictly less than that of edge f?

    //        private static bool less(Edge e, Edge f)
    //        {
    //            return e.Weight() < f.Weight();
    //        }

    //        // check optimality conditions (takes time proportional to E V lg* V)

    //        private bool check(EdgeWeightedGraph G)
    //        {
    //            // check weight
    //            double totalWeight = 0.0;

    //            foreach (Edge e in Edge())
    //            {
    //                totalWeight += e.Weight();
    //            }
    //            double EPSILON = 1E-12;

    //            if (Math.Abs(totalWeight - weight()) > EPSILON)
    //            {
    //                System.err.Printf(
    //                        "Weight of Edge does not equal weight(): %f vs. %f\n",
    //                        totalWeight, weight());
    //                return false;
    //            }
    //            // check that it is acyclic
    //            UF uf = new UF(G.V());

    //            foreach (Edge e in Edge())
    //            {
    //                int v = e.Either(), w = e.other(v);

    //                if (uf.Connected(v, w))
    //                {
    //                    System.err.Println("Not a forest");
    //                    return false;
    //                }
    //                uf.Union(v, w);
    //            }
    //            // check that it is a spanning forest

    //            foreach (Edge e in G.Edge())
    //            {
    //                int v = e.Either(), w = e.other(v);

    //                if (!uf.Connected(v, w))
    //                {
    //                    System.err.Println("Not a spanning forest");
    //                    return false;
    //                }
    //            }
    //            // check that it is a minimal spanning forest (cut optimality
    //            // conditions)

    //            foreach (Edge e in Edge())
    //            {
    //                // all Edge in MST except e
    //                uf = new UF(G.V());

    //                foreach (Edge f in mst)
    //                {
    //                    int x = f.Either(), y = f.other(x);

    //                    if (f != e)
    //                        uf.Union(x, y);
    //                }
    //                // check that e is min weight edge in crossing cut

    //                foreach (Edge f in G.Edge())
    //                {
    //                    int x = f.Either(), y = f.other(x);

    //                    if (!uf.Connected(x, y))
    //                    {
    //                        if (f.Weight() < e.Weight())
    //                        {
    //                            System.err.Println("Edge " + f
    //                                    + " violates cut optimality conditions");
    //                            return false;
    //                        }
    //                    }
    //                }
    //            }
    //            return true;
    //        }

    //        public static void main(String[] args)
    //        {
    //            Console.WriteLine("Enter the number of verties: ");
    //            EdgeWeightedGraph G = new EdgeWeightedGraph(Convert.ToInt32(Console.ReadLine()));
    //            BoruvkasMST mst = new BoruvkasMST(G);
    //            Console.WriteLine("MST: ");

    //            foreach (Edge e in mst.Edge())
    //            {
    //                Console.WriteLine(e);
    //            }
    //            System.Console.Write("Total Weight of MST: %.5f\n", mst.Weight());
    //        }
    //    }

    //    public class ListOfItems<Item> : IEnumerable<Item>
    //    {
    //        private int N;               // number of elements in bag

    //        private Node<Item> first;    // beginning of bag
    //                                     // helper linked list class

    //        private static class Node<Item>
    //        {
    //            private Item item;

    //            private Node<Item> next;
    //        }

    //        public ListOfItems()
    //        {
    //            first = null;
    //            N = 0;
    //        }

    //        public bool Count() > 0
    //{
    //    return first == null;
    //}

    //    public int size()
    //    {
    //        return N;
    //    }

    //    public void add(Item item)
    //    {
    //        Node<Item> oldfirst = first;
    //        first = new Node<Item>();
    //        first.item = item;
    //        first.next = oldfirst;
    //        N++;
    //    }

    //    public IEnumerable<Item> iterator()
    //    {
    //        return new ListIEnumerable<Item>(first);
    //    }

    //    // an iterator, doesn't implement remove() since it's optional
    //    //TODO: @SuppressWarnings("hiding")

    //    private class ListIEnumerable<Item> : IEnumerable<Item>
    //    {
    //        private Node<Item> current;

    //        public ListIEnumerable(Node<Item> first)
    //        {
    //            current = first;
    //        }

    //        public bool hasNext()
    //        {
    //            return current != null;
    //        }

    //        public void remove()
    //        {
    //            throw new UnsupportedOperationException();
    //        }

    //        public Item next()
    //        {
    //            if (!hasNext())
    //                throw new NoSuchElementException();
    //            Item item = current.item;
    //            current = current.next;
    //            return item;
    //        }
    //    }
    //}

    //public class EdgeWeightedGraph
    //{
    //    private readonly int V;

    //    private readonly int E;

    //    private List<Edge>[] adj;
    //    //TODO: @SuppressWarnings("unchecked")

    //    public EdgeWeightedGraph(int V)
    //    {

    //        if (V < 0)
    //        {
    //            throw new Exception(
    //                    "Number of vertices must be nonnegative");
    //        }
    //        this.V = V;
    //        adj =[](List<Edge>) new List[V];

    //        for (int v = 0; v < V; v++)
    //        {
    //            adj[v] = new List<Edge>();
    //        }
    //        Console.WriteLine("Enter the number of Edge: ");
    //        E = Convert.ToInt32(Console.ReadLine());

    //        if (E < 0)
    //        {
    //            throw new Exception(
    //                    "Number of Edge must be nonnegative");
    //        }
    //        Console.WriteLine("Enter the Edge: <from> <to>");

    //        for (int i = 0; i < E; i++)
    //        {
    //            int v = Convert.ToInt32(Console.ReadLine());
    //            int w = Convert.ToInt32(Console.ReadLine());
    //            double weight = Math.Round(100 * Math.random()) / 100.0;
    //            Console.WriteLine(weight);
    //            Edge e = new Edge(v, w, weight);
    //            addEdge(e);
    //        }
    //    }

    //    public int V()
    //    {
    //        return V;
    //    }

    //    public int E()
    //    {
    //        return E;
    //    }

    //    public void addEdge(Edge e)
    //    {
    //        int v = e.Either();
    //        int w = e.Other(v);

    //        if (v < 0 || v >= V)
    //            throw new Exception("vertex " + v
    //                    + " is not between 0 and " + (V - 1));

    //        if (w < 0 || w >= V)
    //            throw new Exception("vertex " + w
    //                    + " is not between 0 and " + (V - 1));
    //        adj[v].Add(e);
    //        adj[w].Add(e);
    //    }

    //    public IEnumerable<Edge> adj(int v)
    //    {
    //        if (v < 0 || v >= V)
    //            throw new Exception("vertex " + v
    //                    + " is not between 0 and " + (V - 1));
    //        return adj[v];
    //    }

    //    public IEnumerable<Edge> Edge()
    //    {
    //        List<Edge> list = new List<Edge>();

    //        for (int v = 0; v < V; v++)
    //        {
    //            int selfLoops = 0;

    //            foreach (Edge e in adj(v))
    //            {
    //                if (e.Other(v) > v)
    //                {
    //                    list.Add(e);
    //                }
    //                // only add one copy of each self loop (self loops will be
    //                // consecutive)
    //                else if (e.Other(v) == v)
    //                {
    //                    if (selfLoops % 2 == 0)
    //                        list.Add(e);
    //                    selfLoops++;
    //                }
    //            }
    //        }
    //        return list;
    //    }

    //    public String toString()
    //    {
    //        String NEWLINE = System.getProperty("line.separator");
    //        StringBuilder s = new StringBuilder();
    //        s.Append(V + " " + E + NEWLINE);

    //        for (int v = 0; v < V; v++)
    //        {
    //            s.Append(v + ": ");

    //            foreach (Edge e in adj[v])
    //            {
    //                s.Append(e + "  ");
    //            }
    //            s.Append(NEWLINE);
    //        }
    //        return s.toString();
    //    }
    //}

    //public class Edge : Comparable<Edge>
    //{
    //    private readonly int v;

    //    private readonly int w;

    //    private readonly double weight;

    //    public Edge(int v, int w, double weight)
    //    {
    //        if (v < 0)
    //            throw new Exception(
    //                    "Vertex name must be a nonnegative integer");

    //        if (w < 0)
    //            throw new Exception(
    //                    "Vertex name must be a nonnegative integer");

    //        if (Double.isNaN(weight))
    //            throw new Exception("Weight is NaN");
    //        this.v = v;
    //        this.w = w;
    //        this.weight = weight;
    //    }

    //    public double weight()
    //    {
    //        return weight;
    //    }

    //    public int either()
    //    {
    //        return v;
    //    }

    //    public int other(int vertex)
    //    {
    //        if (vertex == v)
    //            return w;
    //        else if (vertex == w)
    //            return v;
    //        else
    //            throw new Exception("Illegal endpoint");
    //    }

    //    public int compareTo(Edge that)
    //    {
    //        if (this.Weight() < that.Weight())
    //            return -1;
    //        else if (this.Weight() > that.Weight())
    //            return +1;
    //        else
    //            return 0;
    //    }

    //    public String toString()
    //    {
    //        return String.Format("%d-%d %.5f", v, w, weight);
    //    }
    //}

    //public class UF
    //{
    //    private int[] id;     // id[i] = parent of i

    //    private byte[] rank;  // rank[i] = rank of subtree rooted at i (cannot be
    //                          // more than 31)

    //    private int count;    // number of components

    //    public UF(int N)
    //    {
    //        if (N < 0)
    //            throw new Exception();
    //        count = N;
    //        id = new int[N];
    //        rank = new byte[N];

    //        for (int i = 0; i < N; i++)
    //        {
    //            id[i] = i;
    //            rank[i] = 0;
    //        }
    //    }

    //    public int find(int p)
    //    {
    //        if (p < 0 || p >= id.Length)
    //            throw new Exception();

    //        while (p != id[p])
    //        {
    //            id[p] = id[id[p]];    // path compression by halving
    //            p = id[p];
    //        }
    //        return p;
    //    }

    //    public int count()
    //    {
    //        return count;
    //    }

    //    public bool connected(int p, int q)
    //    {
    //        return find(p) == find(q);
    //    }

    //    public void union(int p, int q)
    //    {
    //        int i = find(p);
    //        int j = find(q);

    //        if (i == j)
    //            return;
    //        // make root of smaller rank point to root of larger rank

    //        if (rank[i] < rank[j])
    //            id[i] = j;
    //        else if (rank[i] > rank[j])
    //            id[j] = i;
    //        else
    //        {
    //            id[j] = i;
    //            rank[i]++;
    //        }
    //        count--;
    //    }
    //}

}