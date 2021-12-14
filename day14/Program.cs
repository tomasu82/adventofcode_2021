using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day14
{
    class Program
    {
        static void Main(string[] args)
        {
            var template = "";
            var patterns = new Dictionary<string, string>();
            using (var reader = File.OpenText("input.txt"))
            {
                template = reader.ReadLine();
                reader.ReadLine();

                while (reader.Peek() > -1)
                {
                    var input = reader.ReadLine().Split(" -> ");

                    patterns.Add(input[0], input[1]);
                }
            }

            var fiveMap = new Dictionary<string, string>();
            var occurences = new Dictionary<string, Dictionary<char, long>>();
            foreach (var kvp in patterns) {
                fiveMap.Add(kvp.Key, PartOne(patterns, kvp.Key, 2));
                // occurences.Add(kvp.Key, ComputeOccurance(patterns, kvp.Key, 10));
            }

            var b = computeRecurse(fiveMap, patterns, template, new Dictionary<(int, int), Dictionary<char, long>>(), 2, 20);
            Console.WriteLine("fast 20 iterations: ");
            printFrequency(b);

            var max = long.MinValue;
            var min = long.MaxValue;
            foreach (var kvp in b) {
                max = Math.Max(kvp.Value, max);
                min = Math.Min(kvp.Value, min);
            }
        }

        static Dictionary<char, long> computeRecurse(Dictionary<string, string> fiveMap, 
                                    Dictionary<string, string> patterns,
                                    string input, 
                                    Dictionary<(int, int), Dictionary<char, long>> occur, 
                                    int length,
                                    int level) 
        {
            if (level == 0) {
                return computeFrequency(input);
            }

            Dictionary<char, long> results = null;

            for (int i=0 ; i< input.Length - (length - 1) ; i+= (length -1)) {
                var curr = input.Substring(i, length);
                if (!fiveMap.ContainsKey(curr))
                    fiveMap.Add(curr, PartOne(patterns, curr, 2));
                
                
                Dictionary<char, long> right = null;
                if (occur.ContainsKey((curr.GetHashCode(), level))) {
                    right = occur[(curr.GetHashCode(), level)];
                }
                else
                    right = computeRecurse(fiveMap, patterns, fiveMap[curr], occur, fiveMap[curr].Length / 2, level - 1);

                if (!occur.ContainsKey((curr.GetHashCode(), level))) {
                    occur.Add((curr.GetHashCode(), level), right);                    
                }

                if (results == null) {
                    results = right;
                    continue;
                }

                results = combineOccurance(results, right, input[i]);
            }
            

            return results;
        }

        static Dictionary<char, long> combineOccurance(Dictionary<char, long> left, Dictionary<char, long> right, char minus) {
            Dictionary<char, long> result = new Dictionary<char, long>(left);
            foreach (var kvp in right) {
                if (!result.ContainsKey(kvp.Key)) {
                    result.Add(kvp.Key, 0);
                }

                if (kvp.Key == minus) {
                    result[kvp.Key] += kvp.Value - 1;
                }
                else 
                {
                    result[kvp.Key] += kvp.Value;
                }
            }
            return result;
        }

        static Dictionary<char, long> computeFrequency(string input) {
            Dictionary<char, long> results = new Dictionary<char, long>();

            foreach (var c in input) {
                if (!results.ContainsKey(c))
                    results.Add(c,0);

                results[c]++;
            }

            return results;
        }
        
        static void printFrequency(Dictionary<char, long> results) {

            foreach (var kvp in results) {
                Console.Write(kvp.Key + ": " + kvp.Value + " ");
            }
            Console.WriteLine();

        }

        static string PartOne(Dictionary<string, string> patterns, string template, int steps, int length = 2) {
            var counter = 0;

            var pol = template;
            while (counter < steps) {
                var output = "";

                for (int i=0 ; i<pol.Length-(length - 1) ; i+= (length - 1)) {
                    string lookup = pol.Substring(i,length);

                    output += lookup[0] + patterns[lookup];
                }

                output += pol[pol.Length -1];

                pol = output;

                counter++;
            }

            return pol;
        }
    }
}
