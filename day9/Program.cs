using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day9
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<int[]>();

            using (var reader = File.OpenText("input.txt"))
            {
                while (reader.Peek() > -1)
                {
                    var line = reader.ReadLine();
                    var carray = new int[line.Length];
                    for (int i = 0; i < line.Length; i++)
                    {
                        carray[i] = int.Parse(line[i].ToString());
                    }
                    input.Add(carray);
                }
            }

            var flow = new List<bool[]>();

            for (int i = 0; i < input.Count; i++)
            {
                flow.Add(new bool[input[i].Length]);
                for (int j = 0; j < input[0].Length; j++)
                {
                    flow[i][j] = canFlowDown(input, i, j);
                }
            }

            var result = 0; // part 1
            List<(int x, int y)> lowest = new List<(int x, int y)>();
            for (int i = 0; i < flow.Count; i++)
            {
                for (int j = 0; j < flow[0].Length; j++)
                {
                    if (!flow[i][j]) { 
                        result += input[i][j] + 1;
                        lowest.Add((i, j));
                    }
                }
            }

            var basins = new List<int>();
            foreach (var l in lowest) {
                basins.Add(computeBasin(input, l.x, l.y));

            }
            basins.Sort();

            Console.WriteLine(basins[basins.Count -1] * 
                    basins[basins.Count -2] * 
                    basins[basins.Count -3]);
        }

        static int computeBasin(List<int[]> input, int x, int y) {
            // spread out counting. use a queue for neighbours to check.
            // if no more neighbours, compute the sum.

            Queue<(int x, int y)> neighbours = new Queue<(int x, int y)>();
            var startX = x;
            var startY = y;
            List<int> basin = new List<int>();
            HashSet<(int x, int y)> visited = new HashSet<(int x, int y)>();

            var result = 0;

            neighbours.Enqueue((x, y));
            while (neighbours.Count > 0) {
                var current = neighbours.Dequeue();
                
                if (visited.Contains(current))
                    continue;

                if (current.x < 0 || current.x >= input.Count ||
                    current.y < 0 || current.y >= input[0].Length)
                    continue;
                
                if (input[current.x][current.y] == 9)
                    continue;

                visited.Add(current);
                result++;
                

                neighbours.Enqueue((current.x-1, current.y));
                neighbours.Enqueue((current.x+1, current.y));
                neighbours.Enqueue((current.x, current.y-1));
                neighbours.Enqueue((current.x, current.y+1));
            }

            return result;
        }
        static bool canFlowDown(List<int[]> input, int x, int y)
        {
            // because of the way I am traversing the array, i dont think I need to scan these coordinates.
            if (x-1 >= 0)
            {
                if (input[x][y] >= input[x - 1][y])
                    return true;
            }

            if (y-1 >= 0)
            {
                if (input[x][y] >= input[x][y - 1])
                    return true;
            }

            if (x + 1 < input.Count)
            {
                if (input[x][y] >= input[x + 1][y])
                    return true;
            }

            if (y + 1 < input[0].Length)
            {
                if (input[x][y] >= input[x][y + 1])
                    return true;
            }

            return false;
        }
    }
}