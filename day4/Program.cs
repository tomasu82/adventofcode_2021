using System;
using System.IO;                                                                                                    
using System.Collections.Generic;  
using System.Linq;                                                                                         

namespace day4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] draws;
            var allBoards = new List<bingoBoard>();

            using (var reader = File.OpenText("input.txt")) {
                var line = reader.ReadLine();
                draws = line.Split(',').Select(x => int.Parse(x)).ToArray();

                while (reader.Peek() > -1) {
                    // empty
                    line = reader.ReadLine();

                    // line 2-6: bingo board
                    List<string[]> b = new List<string[]>();
                    
                    line = reader.ReadLine();
                    b.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries));
                    line = reader.ReadLine();
                    b.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries));
                    line = reader.ReadLine();
                    b.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries));
                    line = reader.ReadLine();
                    b.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries));
                    line = reader.ReadLine();
                    b.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries));
                    

                    allBoards.Add(new bingoBoard() {board = 
                        b.Select(
                            x => x.Select(
                                y => (int.Parse(y), false)).ToArray()
                        ).ToArray() 
                    });
                }
            }

            var winningBoards = new List<int>();
            var lastDraw = 0;
            foreach (var n in draws) {
                var newWinners = new List<(bingoBoard, int)>();

                //foreach (var b in allBoards) {
                for (int i=0 ; i< allBoards.Count ; i++) {
                    var b = allBoards[i];
                    if (!winningBoards.Contains(i) && b.MarkSpace(n)) {
                        winningBoards.Add(i);
                        lastDraw = n;
                    }
                }
            }
            var lastWinner = allBoards[winningBoards[winningBoards.Count -1]].Sum() * lastDraw;

            Console.WriteLine(lastWinner);
            Console.ReadKey();
        }
    }

    class bingoBoard {
        public (int number, bool marked)[][] board;

        public int Sum() {
            var total = 0;
            for (int i=0 ; i<board.Length ; i++) {
                for (int j=0 ; j<board[0].Length ; j++) {
                    if (!board[i][j].marked)
                        total += board[i][j].number;
                }
            }

            return total;
        }

        public bool CheckWinner(int row, int column) {
            // check row
            var count = 0;
            for (int i=0 ; i< board[0].Length ; i++) {
                if (board[row][i].marked == true)
                    count++;
            }
            if (count == 5) return true;

            // check column
            count = 0;
            for (int i=0 ; i< board.Length ; i++) {
                if (board[i][column].marked == true)
                    count++;
            }
            if (count == 5) return true;

            // if row == column, check diagonal

            count = 0;
            for (int i=0 ; i<board.Length ; i++) {
                if (board[i][i].marked == true)
                    count++;
            }
            if (count == 5) return true;

            return false;
        }

        public bool MarkSpace(int number) {
            var space = FindNumber(number);

            if (space == (-1, -1))
                return false;
            
            board[space.row][space.column].marked = true;
            return CheckWinner(space.row, space.column);
        }

        public (int row, int column) FindNumber(int number) {
            for (int i=0 ; i<board.Length ; i++) {
                for (int j=0 ; j<board[0].Length ; j++) {
                    if (board[i][j].number == number)
                        return (i,j);
                }
            }

            return (-1, -1);
        }
    }
}
