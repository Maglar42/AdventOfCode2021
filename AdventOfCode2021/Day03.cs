using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    // https://adventofcode.com/2021/day/3
    public class Day03
    {
        public string Run()
        {
            var data = InputHelper.ReadOutEachLine("Day03Input");
            var numRows = data.Count();
            var numNumbers = data[0].Length;

            var inputData = new List<int[]>();
            int rowCounter = 0;
            foreach (var row in data)
            {
                int itemCounter = 0;
                var rowArray = new int[numNumbers];
                foreach (var item in row)
                {
                    rowArray[itemCounter] =  int.Parse(item.ToString());
                    itemCounter++;
                }

                inputData.Add(rowArray);

                rowCounter++;
            }


            return "Part1: " + this.Part1(inputData, numRows, numNumbers) + "; Part2: " + this.Part2(inputData, numRows, numNumbers);
        }

        private string Part1(List<int[]> inputData, int numRows, int numNumbers)
        {
            var sums = new int[numNumbers];

            foreach (var row in inputData)
            {
                for (var y = 0; y < numNumbers; y++)
                {
                    sums[y] += row[y];
                }
            }

            var gammaRateBinary = "";
            var epsilonRateBinary = "";
            foreach (var item in sums)
            {
                if(item >= numRows/2)
                {
                    gammaRateBinary += "1";
                    epsilonRateBinary += "0";
                }
                else
                {
                    gammaRateBinary += "0";
                    epsilonRateBinary += "1";
                }
            }

            var gammaRate = Convert.ToInt32(gammaRateBinary,2);
            var epsilonRate = Convert.ToInt32(epsilonRateBinary,2); ;

            return (gammaRate * epsilonRate).ToString();
        }

        private string Part2(List<int[]> inputData, int numRows, int numNumbers)
        {
            var bitCompare = new int[numNumbers];
            var oxygenRateInputs = inputData.ToList();
            var co2RateInputs = inputData.ToList();

            int counter = 0;
            do
            {
                var sum = oxygenRateInputs.Select(x => x[counter]).Sum();
                //var average = Math.Round(oxygenRateInputs.Count() / 2.0, MidpointRounding.AwayFromZero);
                var average = oxygenRateInputs.Count() / 2.0;
                var mostCommon = sum >= average ? 1 : 0;

                oxygenRateInputs.RemoveAll(x => x[counter] != mostCommon);
                counter++;
            } while (oxygenRateInputs.Count() > 1);


             counter = 0;
            do
            {
                var sum = co2RateInputs.Select(x => x[counter]).Sum(); 
                var average = co2RateInputs.Count() / 2.0;
                var leastCommon = sum >= average ? 0 : 1;

                co2RateInputs.RemoveAll(x => x[counter] != leastCommon);
                counter++;
            } while (co2RateInputs.Count() > 1);



            var oxygenRate = Convert.ToInt32(string.Join("", oxygenRateInputs.Single()), 2);
            var co2Rate = Convert.ToInt32(string.Join("",co2RateInputs.Single()), 2); 

            return (oxygenRate * co2Rate).ToString();
        }
    }
}
