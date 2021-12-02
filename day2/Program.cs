using System;
using System.IO;
using System.Collections.Generic;

namespace day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<(string dir, int mag)>();

            using (var reader = File.OpenText("input1.txt")) {
                while (reader.Peek() > -1) {
                    var line = reader.ReadLine().Split(' ');
                    input.Add((line[0], int.Parse(line[1])));
                }
            }

            var test = new List<(string dir, int mag)>();
            test.Add(("forward", 5));
            test.Add(("down", 5));
            test.Add(("forward", 8));
            test.Add(("up", 3));
            test.Add(("down", 8));
            test.Add(("forward", 2));


            Console.WriteLine(Part2(input));
        }

        static int Part2(List<(string dir, int mag)> input) {
            (int horizontal, int depth, int aim) result = (0,0,0);
            
            foreach (var i in input) {
                if (i.dir == "up") {
                    result.aim -= i.mag;
                }
                else if (i.dir == "down") {
                    result.aim += i.mag;
                }
                else {
                    result.horizontal += i.mag;
                    result.depth += result.aim * i.mag;
                }
            }

            return result.horizontal * result.depth;
        }
        static int Part1(List<(string dir, int mag)> input) {
            (int horizontal, int depth) result = (0,0);
            
            foreach (var i in input) {
                if (i.dir == "up") {
                    result.depth -= i.mag;
                }
                else if (i.dir == "down") {
                    result.depth += i.mag;
                }
                else {
                    result.horizontal += i.mag;
                }
            }

            return result.horizontal * result.depth;
        }
    }
}
