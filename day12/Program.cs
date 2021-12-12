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

            var paths = findEnd(matrix, "start", node_idx, node_name, new List<string>());
            Console.WriteLine(paths);
        }
        
        static int findEnd(bool[][] matrix, string node, 
                            Dictionary<string, int> node_idx, 
                            Dictionary<int, string> node_name,
                            List<string> banned,
                            string path = "",
                            bool double_dip = false) {
                
                path += "," + node;
                if (node == "end") {
                    //Console.WriteLine(path);
                    return 1;
                }

                var ni = node_idx[node];
                var result = 0;

                var dd = double_dip;

                if (node_name[ni][0] > (int)'Z' && 
                    node_name[ni] != "start" && 
                    node_name[ni] != "end") {
                    
                    if (banned.Contains(node_name[ni])) {
                        if (double_dip)
                            return 0;

                        dd = true;
                    }
                    else 
                        banned.Add(node_name[ni]);
                }
// to improve speed, ignore dead paths. 
/*
    to do this correctly, my guess is to look ahead. if you are double_dip, the next node you go to, it must have capital
    children. otherwise, if it has none, it is a dead path.
*/
                for (int i=0 ; i<matrix.Length ; i++) {
                    if (matrix[ni][i] && 
                        node_name[i] != "start")
                    {
                        result += findEnd(matrix, node_name[i], node_idx, node_name, new List<string>(banned), path, dd);
                    }
                }

                return result;
            }
    }

}
