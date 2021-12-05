using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    // https://adventofcode.com/2021/day/5
    public class Day05
    {
        public string Run()
        {
            var data = InputHelper.ReadOutEachLine("Day05Input");

            var ventData = new List<VentData>();

            foreach(var item in data)
            {
                var startAndEnd = item.Split(" -> ");
                var start = startAndEnd[0].Split(",");
                var end = startAndEnd[1].Split(",");

                var newVentData = new VentData
                {
                    x1 = int.Parse(start[0]),
                    x2 = int.Parse(end[0]),
                    y1 = int.Parse(start[1]),
                    y2 = int.Parse(end[1]),
                };
                ventData.Add(newVentData);
            }

            foreach(var vent in ventData)
            {
                Console.WriteLine(vent);
            }


            return "Part1: " + this.Part1(ventData) + " ; Part2: " + this.Part2(ventData);
        }



        private string Part1(List<VentData> ventData)
        {
            var xMaxStart = ventData.Max(item => item.x1);
            var xMaxEnd = ventData.Max(item => item.x2);
            var xMax = xMaxStart > xMaxEnd ? xMaxStart : xMaxEnd;

            var yMaxStart = ventData.Max(item => item.y1);
            var yMaxEnd = ventData.Max(item => item.y2);
            var yMax = yMaxStart > yMaxEnd ? yMaxStart : yMaxEnd;

            var oceanFloor = new int[xMax + 1, yMax + 1];

            foreach (var vent in ventData)
            {
                if (vent.IsVertical)
                {
                    var yCounter = vent.yLower;

                    do
                    {
                        oceanFloor[vent.x1, yCounter] += 1;
                        yCounter++;
                    } while (yCounter <= vent.yHigher);

                }
                else if (vent.IsHorizonal)
                {
                    var xCounter = vent.xLower;

                    do
                    {
                        oceanFloor[xCounter, vent.y1] += 1;
                        xCounter++;
                    } while (xCounter <= vent.xHigher);
                }                
            }



            var dangerCounter = 0;
            for (var y = 0; y <= yMax; y++)
            {
                for (var x = 0; x <= xMax; x++)
                {
                    if(oceanFloor[x, y] >= 2)
                    {
                        dangerCounter++;
                    }

                    //Console.Write(oceanFloor[x, y]);
                }

                //Console.WriteLine();
            }


            return dangerCounter.ToString();
        }

        private string Part2(List<VentData> ventData)
        {
            var xMaxStart = ventData.Max(item => item.x1);
            var xMaxEnd = ventData.Max(item => item.x2);
            var xMax = xMaxStart > xMaxEnd ? xMaxStart : xMaxEnd;

            var yMaxStart = ventData.Max(item => item.y1);
            var yMaxEnd = ventData.Max(item => item.y2);
            var yMax = yMaxStart > yMaxEnd ? yMaxStart : yMaxEnd;

            var oceanFloor = new int[xMax + 1, yMax + 1];

            foreach (var vent in ventData)
            {
                if (vent.IsVertical)
                {
                    var yCounter = vent.yLower;

                    do
                    {
                        oceanFloor[vent.x1, yCounter] += 1;
                        yCounter++;
                    } while (yCounter <= vent.yHigher);

                }
                else if (vent.IsHorizonal)
                {
                    var xCounter = vent.xLower;

                    do
                    {
                        oceanFloor[xCounter, vent.y1] += 1;
                        xCounter++;
                    } while (xCounter <= vent.xHigher);
                }
                else if(vent.x1 < vent.x2 && vent.y1 < vent.y2)
                {
                    var xCounter = vent.x1;
                    var yCounter = vent.y1;

                    var steps = vent.xHigher - vent.xLower + 1;
                    for (var step = 0; step < steps; step++)
                    {
                        oceanFloor[xCounter, yCounter] += 1;
                        xCounter++;
                        yCounter++;
                    }
                }
                else if (vent.x1 > vent.x2 && vent.y1 > vent.y2)
                {
                    var xCounter = vent.x1;
                    var yCounter = vent.y1;

                    var steps = vent.xHigher - vent.xLower + 1;
                    for (var step = 0; step < steps; step++)
                    {
                        oceanFloor[xCounter, yCounter] += 1;
                        xCounter--;
                        yCounter--;
                    }
                }
                else if (vent.x1 < vent.x2 && vent.y1 > vent.y2)
                {
                    var xCounter = vent.x1;
                    var yCounter = vent.y1;

                    var steps = vent.xHigher - vent.xLower + 1;
                    for (var step = 0; step < steps; step++)
                    {
                        oceanFloor[xCounter, yCounter] += 1;
                        xCounter++;
                        yCounter--;
                    }
                }
                else if (vent.x1 > vent.x2 && vent.y1 < vent.y2)
                {
                    var xCounter = vent.x1;
                    var yCounter = vent.y1;

                    var steps = vent.xHigher - vent.xLower + 1;
                    for (var step = 0; step < steps; step++)
                    {
                        oceanFloor[xCounter, yCounter] += 1;
                        xCounter--;
                        yCounter++;
                    }
                }
                else
                {
                    throw new Exception();
                }
            }



            var dangerCounter = 0;
            for (var y = 0; y <= yMax; y++)
            {
                for (var x = 0; x <= xMax; x++)
                {
                    if (oceanFloor[x, y] >= 2)
                    {
                        dangerCounter++;
                    }

                    // Console.Write(oceanFloor[x, y]);
                }

                // Console.WriteLine();
            }


            return dangerCounter.ToString();
        }


        private class VentData
        {
            public int x1 { get; set; }

            public int y1 { get; set; }

            public int x2 { get; set; }

            public int y2 { get; set; }

            public int yLower => Math.Min(y1, y2);

            public int yHigher => Math.Max(y1, y2);

            public int xLower => Math.Min(x1, x2);

            public int xHigher => Math.Max(x1, x2);

            public bool IsHorizonal => this.y1 == this.y2;

            public bool IsVertical => this.x1 == this.x2;

            public bool IsVerticalOrHorizonal => this.IsHorizonal || this.IsVertical;

            public override string ToString()
            {
                return x1 + "," + y1 +  " -> " + x2 + "," + y2 + " " + this.IsVerticalOrHorizonal;
            }
        }
    }
}
