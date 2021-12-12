using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<string>();

            using (var reader = File.OpenText("input.txt"))
            {

                while (reader.Peek() > -1)
                {
                    input.Add(reader.ReadLine());
                }


            }

            var results = new List<long>();

            foreach (var i in input)
            {
                var p = parse(i);
                if (p.expected == 'i') {
                    // complete the string
                    
                    results.Add(completeScore(complete(i)));
                }
            }
            results.Sort();

            Console.WriteLine(results[results.Count / 2]);
        }

        static long completeScore(string brackets) {
            long score = 0;

            foreach (var c in brackets) {
                score = score * 5 + computeScore2(c);
            }

            return score;
        }

        static int computeScore2(char bracket) {
            if (bracket == ')')
                return 1;
            else if (bracket == ']')
                return 2;
            else if (bracket == '}')
                return 3;
            else
                return 4;
        }

        static int computeScore(char bracket) {
            if (bracket == ')')
                return 3;
            else if (bracket == ']')
                return 57;
            else if (bracket == '}')
                return 1197;
            else
                return 25137;
        }

        static string complete(string input) {
            var chunk_key = new Dictionary<char, char>()
            {
                { '(' , ')' },
                { '[' , ']' },
                { '{' , '}' },
                { '<' , '>' }
            };

            var result = "";

            Stack<char> bracket_stack = new Stack<char>();
            
            foreach (var c in input)
            {
                // if open, push
                if (chunk_key.ContainsKey(c))
                    bracket_stack.Push(c);
                else
                {
                    // close bracket. needs to match
                    var bracket = bracket_stack.Pop();
                }
            }

            while (bracket_stack.Count > 0) {
                result += chunk_key[bracket_stack.Pop()];
            }

            return result;
        }
        static (char expected, char found) parse(string input)
        {
            var chunk_key = new Dictionary<char, char>()
            {
                { '(' , ')' },
                { '[' , ']' },
                { '{' , '}' },
                { '<' , '>' }
            };

            Stack<char> bracket_stack = new Stack<char>();

            foreach (var c in input)
            {
                // if open, push
                if (chunk_key.ContainsKey(c))
                    bracket_stack.Push(c);
                else
                {
                    // close bracket. needs to match
                    var bracket = bracket_stack.Pop();
                    if (chunk_key[bracket] != c)
                        return (chunk_key[bracket], c);
                }

            }

            if (bracket_stack.Count > 0)
                return ('i','i');

            return ('x', 'x');
        }
    }
}
