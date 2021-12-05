using System;
using System.IO;                                                                                                    
using System.Collections.Generic;                                                                                   
using System.Linq;

namespace day5
{
    class Program
    {
        static void Main(string[] args)
        {
            List<((int x, int y) p1, (int x, int y) p2)> lines = new List<((int x, int y) p1, (int x, int y) p2)>();

            using (var reader = File.OpenText("input.txt")) {
                while (reader.Peek() != -1) {
                var line = reader.ReadLine().Split(" -> ");
                var p1 = line[0].Split(',');
                var p2 = line[1].Split(',');
                lines.Add((
                            (int.Parse(p1[0]), int.Parse(p1[1])),
                            (int.Parse(p2[0]), int.Parse(p2[1]))
                            ));
                }
            }

            Dictionary<(int x, int y), int> allPoints = new Dictionary<(int x, int y), int>();

            foreach (var l in lines) {
                if (l.p1.x == l.p2.x) {
                    // vertical line
                    // need start and end y
                    var mag = l.p1.y - l.p2.y;
                    var startY = (mag < 0 ? l.p1.y : l.p2.y);
                    var startX = l.p1.x;

                    for (int i=0 ; i<= Math.Abs(mag) ; i++) {
                        var newPoint = (startX, startY + i); 
                        if (!allPoints.ContainsKey(newPoint))
                            allPoints.Add(newPoint, 0);
                        
                        allPoints[newPoint]++;
                    }
                }
                else if (l.p1.y == l.p2.y) {
                    // horizontal line
                    var mag = l.p1.x - l.p2.x;
                    var startY = l.p1.y;
                    var startX = (mag < 0 ? l.p1.x : l.p2.x);

                    for (int i=0 ; i<= Math.Abs(mag) ; i++) {
                        var newPoint = (startX + i , startY); 
                        if (!allPoints.ContainsKey(newPoint))
                            allPoints.Add(newPoint, 0);
                        
                        allPoints[newPoint]++;
                    }
                }
                else {
                    // diagonal line

                    // get left most point
                    var leftPoint = (l.p1.x < l.p2.x ? l.p1 : l.p2);
                    var rightPoint = (l.p1.x > l.p2.x ? l.p1 : l.p2);
                    // go up or downn?
                    var mag = (leftPoint.y - rightPoint.y);

                    if (mag < 0) {
                        for (int i=0 ; i<=Math.Abs(mag) ; i++) {
                            var newPoint = (leftPoint.x + i, leftPoint.y + i);

                            if (!allPoints.ContainsKey(newPoint))
                                allPoints.Add(newPoint, 0);
                            
                            allPoints[newPoint]++;
                        }
                    }
                    else {
                        for (int i=0 ; i<=Math.Abs(mag) ; i++) {
                            var newPoint = (leftPoint.x + i, leftPoint.y - i);
                            
                            if (!allPoints.ContainsKey(newPoint))
                                allPoints.Add(newPoint, 0);
                            
                            allPoints[newPoint]++;
                        }
                    }
                    
                }
            }

            var result = allPoints.Where(x => x.Value > 1);
            Console.WriteLine(result.Count());
        }
    }
}
