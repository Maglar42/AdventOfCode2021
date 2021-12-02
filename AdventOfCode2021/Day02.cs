using System.Collections.Generic;

namespace AdventOfCode2021
{
    // https://adventofcode.com/2021/day/2
    public class Day02
    {
        private const string forward = "forward";
        private const string down = "down";
        private const string up = "up";

        public string Run()
        {
            var data = InputHelper.ReadOutEachLine("Day02Input");
            var mungedData = new List<KeyValuePair<string, int>>();
            foreach (var item in data)
            {
                var split = item.Split(" ");
                var action = split[0];
                var distance = int.Parse(split[1]);

                mungedData.Add(new KeyValuePair<string, int>(action, distance));
            }

            return "Part1: " + this.Part1(mungedData) + "; Part2: " + this.Part2(mungedData);
        }

        private string Part1(IList<KeyValuePair<string, int>> inputData)
        {
            int horizontal = 0;
            int depth = 0;

            foreach(var input in inputData)
            {
                switch (input.Key)
                {
                    case forward:
                        horizontal += input.Value;
                        break;
                    case down:
                        depth += input.Value;
                        break;
                    case up:
                        depth -= input.Value;
                        break;
                }
            }

            return (horizontal * depth).ToString();
        }

        private string Part2(IList<KeyValuePair<string, int>> inputData)
        {
            int horizontal = 0;
            int depth = 0;
            int aim = 0;

            foreach (var input in inputData)
            {
                switch (input.Key)
                {
                    case forward:
                        horizontal += input.Value;
                        depth += input.Value * aim;
                        break;
                    case down:
                        aim += input.Value;
                        break;
                    case up:
                        aim -= input.Value;
                        break;
                }
            }

            return (horizontal * depth).ToString();
        }
    }
}
