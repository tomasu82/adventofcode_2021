using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day8
{
    class Program
    {
        static void Main(string[] args)
        {
            List<(string pattern, string output)> input = new List<(string pattern, string output)>();

            using (var reader = File.OpenText("input.txt"))
            {
                while (reader.Peek() > -1)
                {
                    var line =reader.ReadLine().Split('|');

                    input.Add((line[0], line[1]));
                }
            }
            var total = 0;
            foreach (var i in input) {
                decoder d = new decoder();
                d.AddSegments(i.pattern);

                string result = "";

                foreach (var o in i.output.Split(' ', StringSplitOptions.RemoveEmptyEntries)) {
                    result += d.decode(o).ToString();
                }

                total += int.Parse(result);
            }

            Console.WriteLine(total);
        }

        static void PartOne(List<(string pattern, string output)> input)
        {
            var count = 0;
            foreach (var i in input)
            {
                var nums = i.output.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Where(x => x.Length == 2 || x.Length == 4 || x.Length == 3 || x.Length == 7);

                count += nums.Count();
            }

            Console.WriteLine(count);
        }
    }

    class decoder {
        string[] numbers;

        public decoder() {
            numbers = new string[10];
        }
        public int decode(string num) {
            var sorted = num.ToCharArray();
            Array.Sort(sorted);

            var n = new string(sorted);

            for (int i=0 ; i<numbers.Length ; i++) {
                if (numbers[i] == n)
                    return i;
            }

            return -1;
        }
        public void AddSegments(string segments) {
            var segs = segments.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                /*
                    0: 6 -- 
                    1: 2 -- unique
                    2: 5 --  left over
                    3: 5 -- contains 1
                    4: 4 -- unique
                    5: 5 --  one awway from a 6
                    6: 6 -- does not contain 4
                    7: 3 -- unique
                    8: 7 -- unique
                    9: 6 -- contains 4


                */

            var leftOver = new Stack<string>();

            foreach (var s in segs) {
                var sorted = s.ToCharArray();
                Array.Sort(sorted);
                var seg = new string(sorted);

                switch (seg.Length) {
                    case 7:
                    numbers[8] = seg;
                    break;
                    case 3:
                    numbers[7] = seg;
                    break;
                    case 4:
                    numbers[4] = seg;
                    break;
                    case 2:
                    numbers[1] = seg;
                    break;
                    default:
                    leftOver.Push(seg);
                    break;
                }
            }
            while (leftOver.Count > 0) {

                var s = leftOver.Pop();

                if (s.Length == 6) {
                    // 0, 6, 9
                    if (CountMatches(s, 4) == numbers[4].Length) {
                        numbers[9] = s;
                    }
                    else if (CountMatches(s, 1) == numbers[1].Length) {
                        numbers[0] = s;
                    }
                    else {
                        numbers[6] = s;
                    }
                }
                else if (s.Length == 5) {
                    // 2, 3, 5
                    if (CountMatches(s, 1) == numbers[1].Length) {
                        numbers[3] = s;
                    }
                    else {
                        // 5 matches 3/4 characters in 4
                        var matches = 0;
                        for (int i=0 ; i<s.Length ; i++) {
                            for (int j=0 ; j<numbers[4].Length ; j++) {
                                if (s[i] == numbers[4][j]) {
                                    matches++;
                                    break;
                                }
                            }
                        }

                        if (matches == 3)
                            numbers[5] = s;
                        else 
                            numbers[2] = s;
                    }
                }
            }
        }

        private int CountMatches(string s, int n) {
            var matches = 0;
            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 0; j < numbers[n].Length; j++)
                {
                    if (s[i] == numbers[n][j])
                    {
                        matches++;
                        break;
                    }
                }
            }

            return matches;
        }
    }
}
