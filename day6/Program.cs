using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new int[] { 3,4,3,1,2 };
            int[] input = null;

            long[] fish_state = { 0,0,0,0,0,0,0,0,0 };

            using (var reader = File.OpenText("input.txt")) {
                input = reader.ReadLine().Split(',').Select(x => int.Parse(x)).ToArray();

            }
            
            for (int i=0 ; i< input.Length ; i++) {
                fish_state[input[i]]++;
            }

            var days = 256;

            for (int i=0 ; i<days ; i++) {
                var reset = fish_state[0];

                for (int j=1 ; j<fish_state.Length ; j++) {
                    fish_state[j-1] = fish_state[j];
                }

                fish_state[6] += reset;
                fish_state[8] = reset;
            }

            var total_fish = fish_state.Sum();
            Console.WriteLine(total_fish);


        }
    }
}
