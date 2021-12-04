using System;
using System.IO;                                                                                                    
using System.Collections.Generic;  
using System.Linq;

namespace day3
{
    class Program
    {
        static void Main(string[] args)
        {
            // var input = new List<string>() {
            //     "00100",
            //     "11110",
            //     "10110",
            //     "10111",
            //     "10101",
            //     "01111",
            //     "00111",
            //     "11100",
            //     "10000",
            //     "11001",
            //     "00010",
            //     "01010"
            // };       
            
            var input = new List<double>();                                                   
                                                                                                                    
            using (var reader = File.OpenText("input.txt")) {                                                      
                while (reader.Peek() > -1) {                                                                        
                    var line = reader.ReadLine();
                    input.Add(decode(line));                                            
                }                                                                                                   
            }
            //var inp = new List<double>(input.Select(x => decode(x)));
            Console.WriteLine(PartTwo(input));

        }

        public static double PartTwo(List<double> input) {
            // special numbers.
            /*
                2^11 = 2048
                1024
                512
                256
                128
                64
                32
                16
                8
                4
                2
                1
            */
            IEnumerable<double> result = input;
            double target = 0.0;
            int start = 0;
            int end = input.Count();
            input.Sort();

            var s1 = 0;
            var e1 = input.Count();
            var t1 = 0.0;


            for (int i=11 ; i>=0 ; i--) {
                target += Math.Pow(2,i);
                t1 += Math.Pow(2,i);

                for (int j = start ; j < end ; j++) {
                    if (input[j] >= target) {
                        // hit
                        if (j-start >  end - j) {
                            target -= Math.Pow(2,i);
                            end = j;
                        }
                        else
                            start = j;

                        break;
                    }
                }

                for (int j = s1 ; j < e1 ; j++) {
                    if (input[j] >= t1) {
                        // hit
                        if (j-s1 >  e1 - j) {
                            s1 = j; 
                        }
                        else {
                            t1 -= Math.Pow(2,i);
                            e1 = j;
                        }
                        break;
                    }
                }

            }

            return input[start] * input[s1];
        }

        public static double decode(string input) {
            return decode(input.ToCharArray().Select(x => (x == '1' ? 1 : 0)).ToArray<int>());
        }
        public static double decode(int[] number) {
            var result = 0.0;

            for (int i= 0 ; i<number.Length ; i++) {
                var exp = number.Length - 1 - i;
                if (number[i] == 0)
                    continue;

                result += Math.Pow(2, exp);
            }

            return result;

        }

        public static double PartOne(List<string> input) {
            var counts = new int[] {0,0,0,0,0,0,0,0,0,0,0,0};
            var inputCount = input.Count;

            foreach (var l in input) {
                for (int i=0 ; i<l.Length ; i++) {
                    if (l[i] == '1')
                        counts[i]++;
                }
            }
            var gamma = new int[] {
                (counts[0] > inputCount/2 ? 1 : 0),
                (counts[1] > inputCount/2 ? 1 : 0),
                (counts[2] > inputCount/2 ? 1 : 0),
                (counts[3] > inputCount/2 ? 1 : 0),
                (counts[4] > inputCount/2 ? 1 : 0),
                (counts[5] > inputCount/2 ? 1 : 0),
                (counts[6] > inputCount/2 ? 1 : 0),
                (counts[7] > inputCount/2 ? 1 : 0),
                (counts[8] > inputCount/2 ? 1 : 0),
                (counts[9] > inputCount/2 ? 1 : 0),
                (counts[10] > inputCount/2 ? 1 : 0),
                (counts[11] > inputCount/2 ? 1 : 0)
            };

            var epsilon = new int[] {
                gamma[0] == 1 ? 0 : 1,
                gamma[1] == 1 ? 0 : 1,
                gamma[2] == 1 ? 0 : 1,
                gamma[3] == 1 ? 0 : 1,
                gamma[4] == 1 ? 0 : 1,
                gamma[5] == 1 ? 0 : 1,
                gamma[6] == 1 ? 0 : 1,
                gamma[7] == 1 ? 0 : 1,
                gamma[8] == 1 ? 0 : 1,
                gamma[9] == 1 ? 0 : 1,
                gamma[10] == 1 ? 0 : 1,
                gamma[11] == 1 ? 0 : 1

            };

            return decode(gamma) *decode(epsilon);
        }
    }
}
