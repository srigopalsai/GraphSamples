using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphSamples
{
    /**
     Data structure to support following operations
     extracMin - O(logn)
     addToHeap - O(logn)
     ContainsKey - O(1)
     decreaseKey - O(logn)
     getKeyWeight - O(1)

     It is a combination of binary heap and hash map

     */
    public class BinaryMinHeap<T>
    {
        private List<Node> allNodes = new List<Node>();
        private Dictionary<T, int> nodePositions = new Dictionary<T, int>();

        public class Node
        {
            public int Weight;
            public T key;
        }

        // Checks where the key exists in heap or not
        public bool Contains(T key)
        {
            return nodePositions.ContainsKey(key);
        }

        // Add key and its Weight to they heap
        public void Add(T key, int Weight)
        {
            Node node = new Node();
            node.Weight = Weight;
            node.key = key;
            allNodes.Add(node);

            int size = allNodes.Count;
            int current = size - 1;
            int parentIndex = (current - 1) / 2;
            nodePositions[node.key] = current;

            while (parentIndex >= 0)
            {
                Node parentNode = allNodes[parentIndex];
                Node currentNode = allNodes[current];

                if (parentNode.Weight > currentNode.Weight)
                {
                    Swap(parentNode, currentNode);
                    UpdatePositionMap(parentNode.key, currentNode.key, parentIndex, current);
                    current = parentIndex;
                    parentIndex = (parentIndex - 1) / 2;
                }
                else
                {
                    break;
                }
            }
        }

        // Get the heap min without extracting the key 
        public T Min()
        {
            return allNodes[0].key;
        }

        // Checks with heap is empty or not
        public bool IsEmpty()
        {
            return allNodes.Count == 0;
        }

        // Decreases the Weight of given key to newWeight and re heaify the tree
        public void DecreaseWeight(T data, int newWeight)
        {
            int position = nodePositions[data];
            allNodes[position].Weight = newWeight;
            int parent = (position - 1) / 2;

            while (parent >= 0)
            {
                if (allNodes[parent].Weight > allNodes[position].Weight)
                {
                    Swap(allNodes[parent], allNodes[position]);

                    UpdatePositionMap(allNodes[parent].key, allNodes[position].key, parent, position);
                    position = parent;
                    parent = (parent - 1) / 2;
                }
                else
                {
                    break;
                }
            }
        }

        // Get the Weight of given key
        public int GetWeightByKey(T key)
        {
            int position = nodePositions[key];
            if (position == 0)
            {
                return 0;
            }
            else
            {
                return allNodes[position].Weight;
            }
        }

        // Remove and Returns the min node of the heap and re heapify
        public Node DeQueueMinNode()
        {
            int size = allNodes.Count - 1;
            Node minNode = new Node();
            minNode.key = allNodes[0].key;
            minNode.Weight = allNodes[0].Weight;

            int lastNodeWeight = allNodes[size].Weight;
            allNodes[0].Weight = lastNodeWeight;
            allNodes[0].key = allNodes[size].key;

            nodePositions.Remove(minNode.key);
            nodePositions.Remove(allNodes[0].key);

            nodePositions[allNodes[0].key] = 0;
            allNodes.RemoveAt(size);

            int currentIndex = 0;
            size--;

            while (true)
            {
                int left = 2 * currentIndex + 1;
                int right = 2 * currentIndex + 2;

                if (left > size)
                {
                    break;
                }
                if (right > size)
                {
                    right = left;
                }

                int smallerIndex = allNodes[left].Weight <= allNodes[right].Weight ? left : right;

                if (allNodes[currentIndex].Weight > allNodes[smallerIndex].Weight)
                {
                    Swap(allNodes[currentIndex], allNodes[smallerIndex]);
                    UpdatePositionMap(allNodes[currentIndex].key, allNodes[smallerIndex].key, currentIndex, smallerIndex);
                    currentIndex = smallerIndex;
                }
                else
                {
                    break;
                }
            }
            return minNode;
        }

        // Extract min value key from the heap
        public T ExtractMin()
        {
            Node node = DeQueueMinNode();
            return node.key;
        }

        private void PrintPositionMap()
        {
            Console.WriteLine(nodePositions);
        }

        private void Swap(Node node1, Node node2)
        {
            int Weight = node1.Weight;
            T data = node1.key;

            node1.key = node2.key;
            node1.Weight = node2.Weight;

            node2.key = data;
            node2.Weight = Weight;
        }

        private void UpdatePositionMap(T data1, T data2, int pos1, int pos2)
        {
            nodePositions.Remove(data1);
            nodePositions.Remove(data2);

            nodePositions[data1] = pos1;
            nodePositions[data2] = pos2;
        }

        public void PrintHeap()
        {
            foreach (Node n in allNodes)
            {
                Console.WriteLine(n.Weight + " " + n.key);
            }
        }

        public static void BinaryMinHeapTest()
        {
            BinaryMinHeap<String> heap = new BinaryMinHeap<String>();

            heap.Add("Sai", 3);
            heap.Add("Sri", 4);
            heap.Add("Mahi", 8);
            heap.Add("Aishu", 10);
            heap.Add("SaiSri", 5);
            heap.Add("SriMahi", 2);
            heap.Add("SriMahiAishu", 1);

            heap.DecreaseWeight("Aishu", 1);

            heap.PrintHeap();
            heap.PrintPositionMap();
        }
    }
}