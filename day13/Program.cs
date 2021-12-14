using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day13
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<(int x, int y)> newPage = new HashSet<(int x, int y)>();
            List<(char dir, int fold)> folds = new List<(char x, int f)>();

            using (var reader = File.OpenText("input.txt"))
            {
                while (reader.Peek() > -1)
                {
                    var line = reader.ReadLine();

                    if (line == "") 
                        continue;
                    
                    if (line.Contains("fold along")) {
                        var ans = line.Substring(11).Split('=');
                        folds.Add((ans[0][0], int.Parse(ans[1])));
                    }
                    else {
                        var l = line.Split(',');
                        newPage.Add((int.Parse(l[0]), int.Parse(l[1])));
                    }                   
                }
            }

            foreach (var f in folds) {
                 if (f.dir == 'y') {
                     var up = new HashSet<(int, int)>(newPage.Where(dot => dot.y < f.fold));
                     var down = newPage.Where(dot => dot.y > f.fold);
                     up.UnionWith(down.Select(d => (d.x, ((f.fold * 2) - d.y))));

                     newPage = up;
                 }
                 else {
                     var left = new HashSet<(int,int)>(newPage.Where(dot => dot.x < f.fold));
                     var right = newPage.Where(dot => dot.x > f.fold);
                     left.UnionWith(right.Select(d=> ((f.fold * 2) - d.x, d.y)));

                     newPage = left;
                     }
            }

            Console.WriteLine(newPage.Count);

            printGrid(newPage);
            Console.WriteLine();
        }

        static void printGrid(HashSet<(int x, int y)> newPage) {
            var mmax_x = newPage.Max(dot => dot.x);
            var mmax_y = newPage.Max(dot => dot.y);

            bool[][] grid = new bool[mmax_x+1][];
            for (int i=0 ; i< mmax_x+1 ; i++) {
                grid[i] = new bool[mmax_y+1];
            }

            foreach (var dot in newPage) {
                grid[dot.x][dot.y] = true;
            }

            for (int j=0 ; j<grid[0].Length ; j++) {
                for (int i=0 ; i< grid.Length ; i++) {
                    if (grid[i][j]) Console.Write('#');
                    else Console.Write(' ');
                }
                Console.WriteLine();
            }
        }
    }
}
