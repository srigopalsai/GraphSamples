using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    /* Video link - https://youtu.be/ID00PMy0-vE
    https://en.wikipedia.org/wiki/Disjoint-set_data_structure
    https://msdn.microsoft.com/en-us/library/ms379575(v=vs.80).aspx
    Disjoint  (Non Overlapping)  sets using path compression and union by rank
    Supports 3 operations
    1) MakeSet
    2) Union
    3) FindSet

    For m operations and total n elements time complexity is O(m* f(n)) where f(n) is very slowly growing function. 
    For most cases f(n) <= 4 so effectively total time will be O(m). Proof in Coreman book.

            Average     Worst Case
    Space   O(n)[1]     O(n)[1]
    Search  O(α(n))[1]  O(α(n))[1]
    Merge   O(α(n))[1]  O(α(n))[1]
    [1] Refer wiki link
    */

    public class DisjointSet
    {
        private Dictionary<long, Node> NodeDictionary = new Dictionary<long, Node>();

        public class Node
        {
            public long Data { get; set; }

            public Node Parent { get; set; }

            public int Rank { get; set; }
        }

        // Create a set with only one element.
        public void MakeSet(long data)
        {
            Node node = new Node();
            node.Data = data;
            node.Parent = node;
            node.Rank = 0;
            NodeDictionary[data] = node;
        }

        // Combines two sets together to one. Does union by rank
        // @return true if data1 and data2 are in different set before union else false.

        public bool Union(long data1, long data2)
        {
            Node node1 = NodeDictionary[data1];
            Node node2 = NodeDictionary[data2];

            Node parent1 = FindSet(node1);
            Node parent2 = FindSet(node2);

            // If they are part of same set do nothing
            if (parent1.Data == parent2.Data)
            {
                return false;
            }

            // Else whoever's rank is higher becomes parent of other
            if (parent1.Rank >= parent2.Rank)
            {
                // Increment rank only if both sets have same rank
                parent1.Rank = (parent1.Rank == parent2.Rank) ? parent1.Rank + 1 : parent1.Rank;
                parent2.Parent = parent1;
            }
            else
            {
                parent1.Parent = parent2;
            }
            return true;
        }

        // Finds the representative of this set
        public long FindSet(long data)
        {
            return FindSet(NodeDictionary[data]).Data;
        }

        // Find the representative recursively and does path compression as well.
        private Node FindSet(Node node)
        {
            Node parent = node.Parent;
            if (parent == node)
            {
                return parent;
            }

            node.Parent = FindSet(node.Parent);
            return node.Parent;
        }

        public static void DisjointSetTest()
        {
            DisjointSet ds = new DisjointSet();

            ds.MakeSet(1);
            ds.MakeSet(2);
            ds.MakeSet(3);
            ds.MakeSet(4);
            ds.MakeSet(5);
            ds.MakeSet(6);
            ds.MakeSet(7);

            ds.Union(1, 2);
            ds.Union(2, 3);
            ds.Union(4, 5);
            ds.Union(6, 7);
            ds.Union(5, 6);
            ds.Union(3, 7);

            Console.WriteLine("DisjoinSet Demo :");

            Console.WriteLine(ds.FindSet(1));
            Console.WriteLine(ds.FindSet(2));
            Console.WriteLine(ds.FindSet(3));
            Console.WriteLine(ds.FindSet(4));
            Console.WriteLine(ds.FindSet(5));
            Console.WriteLine(ds.FindSet(6));
            Console.WriteLine(ds.FindSet(7));
        }
    }
}