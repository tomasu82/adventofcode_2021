using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day12
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> node_idx = new Dictionary<string, int>();
            Dictionary<int, string> node_name = new Dictionary<int, string>();
            var edges = new List<string>();

            using (var reader = File.OpenText("input.txt"))
            {
                var idx = 0;
                while (reader.Peek() > -1)
                {
                    var input = reader.ReadLine();
                    edges.Add(input);
                    var nodes = input.Split('-');
                    
                    if (!node_idx.ContainsKey(nodes[0])) {
                        node_idx.Add(nodes[0], idx);
                        node_name.Add(idx, nodes[0]);
                        idx++;
                    }

                    if (!node_idx.ContainsKey(nodes[1])) {
                        node_idx.Add(nodes[1], idx);
                        node_name.Add(idx, nodes[1]);
                        idx++;
                    }

                }
            }

            bool[][] matrix = new bool[node_idx.Keys.Count][];
            for (int i=0 ; i<matrix.Length ; i++) 
                matrix[i] = new bool[node_idx.Keys.Count];

            foreach (var edge in edges) {
                var e = edge.Split('-');

                matrix[node_idx[e[0]]][node_idx[e[1]]] = true;
                matrix[node_idx[e[1]]][node_idx[e[0]]] = true;
            }
            
            var paths = findEnd(matrix, "start", node_idx, node_name);

            Console.WriteLine(paths);
        }
        
        static int findEnd(bool[][] matrix, string node, 
                            Dictionary<string, int> node_idx, 
                            Dictionary<int, string> node_name,
                            string path = "",
                            bool double_dip = false) {
                if (node == "end") {
                    //Console.WriteLine(path + ",end");
                    return 1;
                }

                var result = 0;
                var dd = double_dip;

                if (node[0] > (int)'Z') {
                    if (path.Contains(node)) {
                        if (double_dip)
                            return 0;

                        dd = true;
                    }
                    else {
                        path += "," + node;
                    }
                }

                for (int i=0 ; i<matrix.Length ; i++) {
                    if (matrix[node_idx[node]][i] && node_name[i] != "start")
                        result += findEnd(matrix, node_name[i], node_idx, node_name, path, dd);
                }

                return result;
            }
    }

}
