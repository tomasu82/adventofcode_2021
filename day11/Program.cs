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
            int[][] input = new int[10][];


            using (var reader = File.OpenText("input.txt"))
            {
                var currLine = 0;
                while (reader.Peek() > -1)
                {
                    var line = reader.ReadLine();

                    input[currLine] = new int[10];

                    for (int i = 0; i < line.Length; i++)
                    {
                        input[currLine][i] = int.Parse(line[i].ToString());
                    }

                    currLine++;
                }
            }

            var count = 0;
            var total_flashes = 0;
            while (true)
            {
                if (checkSync(input))
                    break;
                incEnergy(input);


                Queue<(int x, int y)> octs = new Queue<(int x, int y)>();

                for (int i = 0; i < input.Length; i++)
                {
                    for (int j = 0; j < input[0].Length; j++)
                    {
                        if (input[i][j] >= 10)
                            total_flashes++;

                        checkFlash(input, i, j, octs);
                    }
                }


                while (octs.Count > 0)
                {
                    var oct = octs.Dequeue();
                    if (input[oct.x][oct.y] >= 10)
                        total_flashes++;

                    checkFlash(input, oct.x, oct.y, octs);
                }

                count++;
            }
            // print output
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    Console.Write(input[i][j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("step: " + count);
            Console.WriteLine("flashes: " + total_flashes);
        }

        static void incEnergy(int[][] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    input[i][j]++;
                }
            }
        }

        static bool checkSync(int[][] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    if (input[i][j] != 0)
                        return false;
                }
            }

            return true;
        }
        static void checkFlash(int[][] input, int x, int y, Queue<(int x, int y)> octs)
        {
            if (input[x][y] >= 10)
            {
                // flash
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (i == 0 && j == 0)
                            continue;
                        if (x + i >= 10 || y + j >= 10 ||
                            x + i < 0 || y + j < 0)
                            continue;

                        if (input[x + i][y + j] != 0)
                        {
                            input[x + i][y + j]++;

                            octs.Enqueue((x + i, y + j));
                        }
                    }
                }

                input[x][y] = 0;
            }
        }
    }
}
