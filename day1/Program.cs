using System;
using System.IO;
using System.Collections.Generic;

namespace day1
{
    class Program
    {
        static void Main(string[] args)
        {
            PartTwo();
        }

        static void PartTwo() {
            var depths = new List<int>();
            var count = 0;

            using (var reader = File.OpenText("input1.txt")) {
                while (reader.Peek() > -1) {
                    depths.Add(int.Parse(reader.ReadLine()));
                }
            }

            for (int i = 3 ; i< depths.Count ; i++) {
                var prev = depths[i-1] + depths[i-2] + depths[i-3];
                var curr = depths[i] + depths[i-1] + depths[i-2];
                if (prev < curr)
                    count++;
            }

            Console.WriteLine(count);
        }
        static void PartOne() {
            var depths = new List<int>();
            using (var reader = File.OpenText("input1.txt")) {
                while (reader.Peek() > -1)
                    depths.Add(int.Parse(reader.ReadLine()));
            }

            var count = 0;
            for (int i=1 ; i< depths.Count ; i++) {
                if (depths[i-1] < depths[i])
                    count++;
            }

            Console.WriteLine(count);
        }
    }
}
