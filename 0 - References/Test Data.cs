using System;
using System.Collections.Generic;

namespace GraphSamples
{
    public class TestData
    {
        public static IDictionary<string, Graph> DirectedGraphs = new Dictionary<string, Graph>();
        public static IDictionary<string, Graph> UnDirectedGraphs = new Dictionary<string, Graph>();
        public static IDictionary<string, int[,]> BinaryMatrices = new Dictionary<string, int[,]>();

        public class Keys
        {
            public const string Null = "Null";
            public const string Empty = "Empty";
            public const string Zero = "Zero";
            public const string OneElement = "OneElement";
            public const string OneAndOne = "OneAndOne";
            public const string OneElementMinus = "OneElementMinus";
            public const string Graph2v = "Graph2v";
            public const string Graph2e = "Graph2e";
            public const string Graph3e = "Graph3e";
            public const string DGraph9e = "DGraph9e";

            public const string FourByFour1 = "FourByFour1";
            public const string FourByFour2 = "FourByFour2";
            public const string FiveByFive1 = "FiveByFive1";
            public const string FiveByFive2 = "FiveByFive2";
            public const string FiveByFive3 = "FiveByFive3";
            public const string FiveByFive4 = "FiveByFive4";
            public const string FiveByFive5 = "FiveByFive5";

            public const string SixBySix1 = "SixBySix1";
            public const string SixBySix2 = "SixBySix2";
            public const string SixBySix3 = "SixBySix3";
            public const string SixBySix4 = "SixBySix4";
            public const string SixBySix5 = "SixBySix5";
            
            public const string SevenBySeven1 = "SevenBySeven1";
            public const string SevenBySeven2 = "SevenBySeven2";
            public const string TenByTen1 = "TenByTen1";
            public const string TenByTen2 = "TenByTen2";
            public const string TenByTen3 = "TenByTen3";
        }
        static TestData()
        {
            try
            {
                DirectedGraphAdjList();
                UnDirectedGraphAdjList();
                DirectedGraphAdjMatrix();
                UnDirectedGraphAdjMatrix();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loading data failed. " + ex.Message);
            }
        }

        private static void DirectedGraphAdjList()
        {
            DirectedGraphs.Add(Keys.Null, null);
            DirectedGraphs.Add(Keys.Empty, new Graph(true));

            Graph graph0 = new Graph(true);
            graph0.AddVertex(new Vertex(0));
            DirectedGraphs.Add(Keys.Zero, graph0);

            Graph graph1 = new Graph(true);
            graph1.AddVertex(new Vertex(1));
            DirectedGraphs.Add(Keys.OneElement, graph1);

            Graph graph2v = new Graph(true);
            graph2v.AddVertex(new Vertex(1));
            graph2v.AddVertex(new Vertex(2));
            DirectedGraphs.Add(Keys.Graph2v, graph2v);

            Graph graph2e = new Graph(true);
            graph2e.AddEdge(1, 2);
            DirectedGraphs.Add(Keys.Graph2e, graph2e);

            Graph graph3e = new Graph(true);
            graph3e.AddEdge(1, 2);
            graph3e.AddEdge(1, 2);
            DirectedGraphs.Add(Keys.Graph3e, graph3e);

            Graph dGraph9 = new Graph(true);
            dGraph9.AddEdge(1, 2);
            dGraph9.AddEdge(1, 4);
            dGraph9.AddEdge(2, 3);
            dGraph9.AddEdge(2, 7);
            dGraph9.AddEdge(4, 5);
            dGraph9.AddEdge(4, 6);
            dGraph9.AddEdge(4, 9);
            dGraph9.AddEdge(5, 8);
            dGraph9.AddEdge(5, 9);
            dGraph9.AddEdge(6, 7);
            dGraph9.AddEdge(7, 8);
            DirectedGraphs.Add(Keys.DGraph9e, graph3e);
        }

        private static void UnDirectedGraphAdjMatrix()
        {
        }

        private static void DirectedGraphAdjMatrix()
        {

            BinaryMatrices.Add(Keys.Null, null);
            BinaryMatrices.Add(Keys.Empty, new int[,] { { } });
            BinaryMatrices.Add(Keys.Zero, new int[,] { { 0 }, { 0 } });
            BinaryMatrices.Add(Keys.OneElement, new int[,] { { 1 } });
            BinaryMatrices.Add(Keys.OneAndOne, new int[,] { { 1 }, { 1 } });
            BinaryMatrices.Add(Keys.OneElementMinus, new int[,] { { -1 } });

            BinaryMatrices.Add(Keys.FourByFour2, new int[,] {  { 1, 1, 0, 0 },
                                                                { 1, 1, 0, 0 },
                                                                { 0, 0, 1, 0 },
                                                                { 0, 1, 1, 0 },
                                                                { 1, 0, 1, 1 }});

            BinaryMatrices.Add(Keys.FiveByFive1, new int[,] {   { 1, 1, 1, 0, 0 },
                                                                { 0, 0, 1, 1, 0 },
                                                                { 0, 0, 0, 1, 0 },
                                                                { 0, 0, 0, 1, 1 },
                                                                { 0, 0, 0, 0, 1 }});

            BinaryMatrices.Add(Keys.FiveByFive2, new int[,]{    { 1, 1, 0, 0, 0 },
                                                                { 1, 0, 1, 1, 1 },
                                                                { 1, 1, 1, 0, 1 },
                                                                { 1, 0, 0, 0, 1 },
                                                                { 1, 0, 0, 0, 1 }});

            //int[,] srcMatrix = new int[,] { { 0, 1, 0, 1, 0 },
            //                                { 1, 0, 1, 0, 0 },
            //                                { 0, 1, 0, 1, 1 },
            //                                { 0, 1, 0, 1, 1 },
            //                                { 1, 0, 0, 1, 1 } };

            int[,] adjMatrix5by5 = new int[,] {// 0, 1, 2, 3, 4  DFS 0 4 3 2 1 http://wikistack.com/depth-first-traversal-of-graph/
                                                { 0, 1, 1, 0, 1 },
                                                { 0, 0, 0, 0, 0 },
                                                { 0, 0, 0, 1, 0 },
                                                { 0, 0, 0, 0, 0 },
                                                { 0, 0, 1, 1, 0 } };

            // Connected - Refer ..\0 - References\Graph Data Reference.pptx
            BinaryMatrices.Add(Keys.SixBySix1, new int[,]   {   {0, 1, 1, 0, 0, 0 },
                                                                {0, 0, 0, 0, 0, 0 },
                                                                {0, 0, 0, 1, 0, 0 },
                                                                {0, 1, 0, 0, 1, 0 },
                                                                {0, 0, 0, 0, 0, 1 },
                                                                {0, 0, 0, 0, 0, 0 } });

            // Disconnected - Refer ..\0 - References\Graph Data Reference.pptx
            BinaryMatrices.Add(Keys.SixBySix2, new int[,]   {   {0, 1, 1, 0, 0, 0 },
                                                                {0, 0, 0, 0, 0, 0 },
                                                                {0, 0, 0, 1, 0, 0 },
                                                                {0, 0, 0, 0, 0, 0 },
                                                                {0, 0, 0, 0, 0, 1 },
                                                                {0, 0, 0, 0, 0, 0 } });

            //0  1  2  3  4  5
            // DFS 0 1 2 3 5 4 http://wikistack.com/a-recursive-implementation-of-dfs/
            BinaryMatrices.Add(Keys.SixBySix3, new int[,] {{ 0, 1, 1, 0, 0, 0},     //0
                                                            {0, 0, 1, 1, 0, 0},     //1
                                                            {0, 0, 0, 1, 0, 0},     //2
                                                            {0, 0, 0, 0, 0, 1},     //3
                                                            {0, 0, 1, 0, 0, 0},     //4
                                                            {0, 0, 0, 0, 0, 0}});   //5

            BinaryMatrices.Add(Keys.SixBySix4, new int[,] { { 0, 1, 0, 0, 0, 0 },
                                                            { 1, 0, 1, 1, 0, 0 },
                                                            { 0, 1, 0, 0, 0, 1 },
                                                            { 0, 1, 0, 0, 1, 1 },
                                                            { 0, 0, 0, 1, 0, 1 },
                                                            { 0, 0, 1, 1, 1, 0 } });

            BinaryMatrices.Add(Keys.SixBySix5, new int[,] { { 0, 1, 0, 0, 0, 0 },
                                                            { 0, 0, 1, 0, 0, 0 },
                                                            { 0, 0, 0, 1, 0, 0 },
                                                            { 0, 0, 0, 0, 1, 0 },
                                                            { 0, 0, 0, 0, 0, 1 },
                                                            { 1, 0, 0, 0, 0, 0 } });

            BinaryMatrices.Add(Keys.SevenBySeven1, new int[,] {// 0, 1, 2, 3, 4, 5, 6  DFS    0, 1, 3, 6, 2, 4, 5
                                                                { 0, 1, 1, 0, 0, 0, 0},
                                                                { 1, 0, 0, 1, 1, 1, 0},
                                                                { 1, 0, 0, 0, 0, 0, 1},
                                                                { 0, 1, 0, 0, 0, 0, 1},
                                                                { 0, 1, 0, 0, 0, 0, 1},
                                                                { 0, 1, 0, 0, 0, 0 ,0},
                                                                { 0, 0, 1, 1, 1, 0, 0}
                                                            });

            //0, 1, 2, 3, 4, 5, 6 Refer here http://wikistack.com/c-program-for-bfs-using-adjacency-matrix/
            BinaryMatrices.Add(Keys.SevenBySeven2, new int[,] { { 0, 1, 1, 0, 0, 0, 0 }, // 0
                                                                { 0, 0, 0, 0, 0, 0, 1 }, // 1
                                                                { 0, 0, 0, 1, 0, 0, 1 }, // 2
                                                                { 0, 0, 0, 0, 1, 0, 0 }, // 3
                                                                { 0, 0, 0, 0, 0, 0, 1 }, // 4
                                                                { 1, 0, 0, 1, 0, 0, 0 }, // 5
                                                                { 0, 0, 0, 0, 0, 0, 0 }, // 6
                                                                });

            BinaryMatrices.Add(Keys.TenByTen1, new int[,] {     { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1 },
                                                                { 1, 0, 1, 0, 1, 1, 1, 0, 1, 1 },
                                                                { 1, 1, 1, 0, 1, 1, 0, 1, 0, 1 },
                                                                { 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
                                                                { 1, 1, 1, 0, 1, 1, 1, 0, 1, 0 },
                                                                { 1, 0, 1, 1, 1, 1, 0, 1, 0, 0 },
                                                                { 1, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
                                                                { 1, 0, 1, 1, 1, 1, 0, 1, 1, 1 },
                                                                { 1, 1, 0, 0, 0, 0, 1, 0, 0, 1 },
                                                                { 1, 1, 0, 0, 0, 0, 1, 0, 0, 1 }});

            //BinaryMatrices.Add(Keys.TenByTen2, new int[,] {     { 000, 000, 000,  000,  000,  000,  000,  000,  000,  000,  000 },
            //                                                    { 000, 000, 374,  200,  223,  108,  178,  252,  285,  240,  356 },
            //                                                    { 000, 374, 000,  255,  166,  433,  199,  135,  095,  136,  017 },
            //                                                    { 000, 200, 255,  000,  128,  277,  128,  180,  160,  131,  247 },
            //                                                    { 000, 223, 166,  128,  000,  430,  047,  052,  084,  040,  155 },
            //                                                    { 000, 108, 433,  277,  430,  000,  453,  478,  344,  389,  423 },
            //                                                    { 000, 178, 199,  128,  047,  453,  000,  091,  110,  064,  181 },
            //                                                    { 000, 252, 135,  180,  052,  478,  091,  000,  114,  083,  117 },
            //                                                    { 000, 285, 095,  160,  084,  344,  110,  114,  000,  047,  078 },
            //                                                    { 000, 240, 136,  131,  040,  389,  064,  083,  047,  000,  118 },
            //                                                    { 000, 356, 017,  247,  155,  423,  181,  117,  078,  118,  000 }});

            BinaryMatrices.Add(Keys.TenByTen2, new int[,] {     { 01, 002, 003, 004, 005, 006, 007, 008, 009, 010 },
                                                                { 02, 000, 374, 200, 223, 108, 178, 252, 285, 240 },
                                                                { 03, 356, 374, 000, 255, 166, 433, 199, 135, 095 },
                                                                { 04, 136, 017, 200, 255, 000, 128, 277, 128, 180 },
                                                                { 05, 160, 131, 247, 223, 166, 128, 000, 430, 047 },
                                                                { 06, 052, 084, 040, 155, 108, 433, 277, 430, 000 },
                                                                { 07, 453, 478, 344, 389, 423, 178, 199, 128, 047 },
                                                                { 08, 453, 000, 091, 110, 064, 181, 252, 135, 180 },
                                                                { 09, 052, 488, 091, 000, 114, 083, 117, 285, 095 },
                                                                { 10, 160, 084, 344, 110, 114, 000, 047, 078, 240 } });

            BinaryMatrices.Add(Keys.TenByTen3, new int[,] {     { 000, 374, 200, 223, 108, 178, 252, 285, 240 },
                                                                { 356, 374, 000, 255, 166, 433, 199, 135, 095 },
                                                                { 136, 017, 200, 255, 000, 128, 277, 128, 180 },
                                                                { 160, 131, 247, 223, 166, 128, 000, 430, 047 },
                                                                { 052, 084, 040, 155, 108, 433, 277, 430, 000 },
                                                                { 453, 478, 344, 389, 423, 178, 199, 128, 047 },
                                                                { 453, 000, 091, 110, 064, 181, 252, 135, 180 },
                                                                { 052, 488, 091, 000, 114, 083, 117, 285, 095 },
                                                                { 160, 084, 344, 110, 114, 000, 047, 078, 240 } });


        }
        private static void UnDirectedGraphAdjList()
        {
        }

        public static void Display(int[,] matrix, string message="")
        {
            if (!string.IsNullOrEmpty(message))
                Console.WriteLine(message);

            Console.WriteLine("Matrix :");

            for (int rowIndx = 0; rowIndx < matrix.GetLength(0); rowIndx++)
            {
                for (int colIndx = 0; colIndx < matrix.GetLength(1); colIndx++)
                {
                    Console.Write(matrix[rowIndx,colIndx] + " ");
                }
                Console.WriteLine("");
            }
        }
    }
}