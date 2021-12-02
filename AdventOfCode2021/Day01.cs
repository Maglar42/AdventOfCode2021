using System.Collections.Generic;

namespace AdventOfCode2021
{
    // https://adventofcode.com/2021/day/1
    public class Day01
    {
        public string Run()
        {
            var data = InputHelper.ReadOutEachLine("Day01Input");
            var dataAsInts = new List<int>();
            foreach(var item in data)
            {
                dataAsInts.Add(int.Parse(item));
            }

            return "Part1: " + this.Part1(dataAsInts) + "; Part2: " + this.Part2(dataAsInts);
        }

        private string Part1(IList<int> data)
        {
            int? priorValue = null;
            int increases = 0;

            foreach (var dataItem in data)
            {
                if (priorValue != null && dataItem > priorValue)
                {
                    increases++;
                }

                priorValue = dataItem;
            }

            return increases.ToString();
        }

        private string Part2(IList<int> data)
        {
            int? firstValue = null;
            int? secondValue = null;
            int? priorValue = null;
            int increases = 0;

            foreach (var dataItem in data)
            {
                if(firstValue == null)
                {
                    firstValue = dataItem;
                    continue;
                }

                if (secondValue == null)
                {
                    secondValue = dataItem;
                    continue;
                }

                var sum = firstValue + secondValue + dataItem;
                Helper.WriteLine(new List<int?> { firstValue, secondValue, dataItem, priorValue, sum });

                firstValue = secondValue;
                secondValue = dataItem;


                if (priorValue == null)
                {
                    priorValue = sum;
                    continue;
                }

                if (sum > priorValue)
                {
                    increases++;
                }

                priorValue = sum;

            }

            return increases.ToString();
        }


    }
}
