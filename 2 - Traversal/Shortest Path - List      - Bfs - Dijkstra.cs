using System;
using System.Collections.Generic;

namespace GraphSamples
{
     //Find single source shortest path using Dijkstra's algorithm 
     //Space Complexity - O(E + V)
     //Time  Complexity - O(E log V)
 
     //https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm

    public partial class TraversalSamples
    {
        // Approach 1 - Using Min Heap
        public Dictionary<Vertex, int> ShortestPathUsingMinHeap(Graph graph, Vertex srcVertex)
        {
            // Using MinHeap instead of regular queue.
            BinaryMinHeap<Vertex> minHeap = new BinaryMinHeap<Vertex>();

            // Stores shortest distance from root to every vertex
            Dictionary<Vertex, int> distanceDict = new Dictionary<Vertex, int>();

            // Fill heap by initializing all vertex with infinite distance from source vertex
            foreach (KeyValuePair<long, Vertex> heapItem in graph.Verticies)
            {
                minHeap.Add(heapItem.Value, int.MaxValue);
            }

            // Set distance of source vertex to 0
            minHeap.DecreaseWeight(srcVertex, 0);

            // Put it in Dictionary/Map
            distanceDict[srcVertex] = 0;

            // Iterate till heap is not empty
            while (!minHeap.IsEmpty())
            {
                // Get the min value from heap node which has vertex and distance of that vertex from source vertex.
                BinaryMinHeap<Vertex>.Node heapNode = minHeap.DeQueueMinNode();
                Vertex currentVertex = heapNode.key;

                // Update shortest distance of current vertex from source vertex
                distanceDict[currentVertex] = heapNode.Weight;

                // Iterate through all edges of current vertex
                foreach (Edge edge in currentVertex.Edges)
                {
                    // Get the adjacent vertex
                    Vertex adjacent = edge.Vertex1.Equals(currentVertex) ? edge.Vertex2 : edge.Vertex1;

                    // If heap does not contain adjacent vertex means adjacent vertex already has shortest distance from source vertex
                    if (!minHeap.Contains(adjacent))
                        continue;

                    // Add distance of current vertex to edge weight to get distance of adjacent vertex from source vertex
                    // When it goes through current vertex
                    int newDistance = distanceDict[currentVertex] + edge.Weight;

                    // See if this above calculated distance is less than current distance stored for adjacent vertex from source vertex
                    if (minHeap.GetWeightByKey(adjacent) > newDistance)
                    {
                        minHeap.DecreaseWeight(adjacent, newDistance);
                    }
                }
            }

            return distanceDict;
        }

        // Approach 2 - Using Queue
        public void ShortestPathUsingQueue(Vertex srcVertex)
        {
            srcVertex.MinDistance = 0;

            Queue<Vertex> vertexQueue = new Queue<Vertex>();
            vertexQueue.Enqueue(srcVertex);

            while (vertexQueue.Count > 0)
            {
                Vertex curVertex = vertexQueue.Dequeue();

                foreach (Edge edge in curVertex.Edges)
                {
                    Vertex adjVertex = (edge.Vertex1 == srcVertex) ? edge.Vertex2 : edge.Vertex1;

                    double distanceToAdjecent = curVertex.MinDistance + edge.Weight;

                    if (distanceToAdjecent < adjVertex.MinDistance)
                    {
                        adjVertex.MinDistance = distanceToAdjecent;
                        adjVertex.Previous = curVertex;
                        vertexQueue.Enqueue(adjVertex);
                    }
                }
            }
        }

        public void DijkstraShortestPathTest()
        {
            Graph graph = new Graph(false);

            /*graph.addEdge(0, 1, 4);
            graph.addEdge(1, 2, 8);
            graph.addEdge(2, 3, 7);
            graph.addEdge(3, 4, 9);
            graph.addEdge(4, 5, 10);
            graph.addEdge(2, 5, 4);
            graph.addEdge(1, 7, 11);
            graph.addEdge(0, 7, 8);
            graph.addEdge(2, 8, 2);
            graph.addEdge(3, 5, 14);
            graph.addEdge(5, 6, 2);
            graph.addEdge(6, 8, 6);
            graph.addEdge(6, 7, 1);
            graph.addEdge(7, 8, 7);*/

            graph.AddEdge(10, 20, 5);
            graph.AddEdge(20, 30, 2);
            graph.AddEdge(10, 40, 4);
            graph.AddEdge(10, 50, 3);
            graph.AddEdge(50, 60, 2);
            graph.AddEdge(60, 40, 2);
            graph.AddEdge(30, 40, 3);
            graph.AddEdge(70, 10, 8);

            graph.Display();

            int srcVal = 10;

            Console.WriteLine("\nDijkstra Shortest Path - From " + srcVal + " to all neighbors\n");
            Vertex sourceVertex = graph.GetVertexById(srcVal);
            Dictionary<Vertex, int> pathVerticies = ShortestPathUsingMinHeap(graph, sourceVertex);

            Console.WriteLine("\nUsing MinHeap:\n");
            
            foreach (var item in pathVerticies)
            {
                Console.WriteLine(srcVal + " to " + item.Key.Id + " " + item.Value);
            }

            Console.WriteLine("\nUsing Queue:\n");

            ShortestPathUsingQueue(sourceVertex);

            foreach (KeyValuePair<long,Vertex> vertex in graph.Verticies)
            {
                Console.WriteLine(srcVal + " to " + vertex.Value.Id + " " + vertex.Value.MinDistance);
            }
        }
    }
}