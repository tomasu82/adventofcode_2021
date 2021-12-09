using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day7
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = {16,1,2,0,4,2,7,1,2,14};

            using (var reader = File.OpenText("input.txt")) {
                while (reader.Peek() > -1) {
                    input = reader.ReadLine().Split(',').Select(x => int.Parse(x)).ToArray();
                }
            }

            // position, count collection
            Dictionary<int, int> crabs = new Dictionary<int, int>();
            foreach (var i in input) {
                if (!crabs.ContainsKey(i))
                    crabs.Add(i, 0);

                crabs[i]++;
            }

            // start from most common position

            (int position, int count) crab = (0,0);

            foreach (var kvp in crabs) {
                if (kvp.Value > crab.count)
                    crab = (kvp.Key, kvp.Value);
            }

            var destination = crab.position;
            var total_fuel = 0;

            foreach (var kvp in crabs) {
                total_fuel += SumTo(Math.Abs(kvp.Key - destination)) * kvp.Value;    
            }

            var fuel_right = 0;

            var counter = 1;

            var times_large = 0;
            var position = 0;
            
            while (counter < crabs.Count) {
                var r_dest = destination + counter;
                fuel_right = 0;
                foreach (var kvp in crabs) {
                    fuel_right += SumTo(Math.Abs(kvp.Key - r_dest)) * kvp.Value;    
                }
                if (fuel_right < total_fuel) {
                    total_fuel = fuel_right;
                    position = r_dest;
                }
                else {
                    times_large++;

                }

                counter++;
            }
            //part 1: 344535
            // part 2: 95581659

            Console.WriteLine(total_fuel);

        }

        public static int SumTo(int top) {
            var result = 0;
            for (int i=1 ; i<= top ; i++) {
                result += i;
            }
            return result;
        }
    }
}
