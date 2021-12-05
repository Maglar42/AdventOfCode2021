using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    // https://adventofcode.com/2021/day/4
    public class Day04
    {
        public string Run()
        {
            //var drawStrings = "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1";
            var drawStrings = "13,47,64,52,60,69,80,85,57,1,2,6,30,81,86,40,27,26,97,77,70,92,43,94,8,78,3,88,93,17,55,49,32,59,51,28,33,41,83,67,11,91,53,36,96,7,34,79,98,72,39,56,31,75,82,62,99,66,29,58,9,50,54,12,45,68,4,46,38,21,24,18,44,48,16,61,19,0,90,35,65,37,73,20,22,89,42,23,15,87,74,10,71,25,14,76,84,5,63,95";

            var draws = drawStrings.Split(",").ToList();

            var data = InputHelper.ReadOutEachLine("Day04Input");

            var boards = new List<List<List<string>>>();
            var board = new List<List<string>>();
            foreach (var row in data)
            {
                if (string.IsNullOrEmpty(row))
                {
                    boards.Add(board);
                    board = new List<List<string>>();
                    continue;
                }

                var rowItems = System.Text.RegularExpressions.Regex.Split(row.Trim(), @"\s+").ToList();

                board.Add(rowItems);
            }

            // Add the last board
            boards.Add(board);

            return "Part1: " + this.Part1(draws, boards) + " ; Part2: " + this.Part2(draws, boards);
        }


        private string Part1(List<string> draws, List<List<List<string>>> boards)
        {
            var drawn = new List<string>();
            foreach(var draw in draws)
            {
                drawn.Add(draw);
                foreach(var board in boards)
                {
                    if (this.BoardWon(board, drawn))
                    {
                        return this.ScoreBoard(board, drawn).ToString();
                    }
                }
            }

            return null;
        }

        private string Part2(List<string> draws, List<List<List<string>>> boards)
        {
            var drawn = new List<string>();
            var lastWinner = new List<List<string>>();
            foreach (var draw in draws)
            {
                drawn.Add(draw);
                var toRemove = new List<int>();
                for (var boardIndex = 0; boardIndex < boards.Count(); boardIndex++)
                {
                    if (this.BoardWon(boards[boardIndex], drawn))
                    {
                        toRemove.Add(boardIndex);
                    }
                }

                var toWrite = new List<string> { draw };
                toWrite.Add("ToRemove: ");
                foreach(var toRemoveItem in toRemove)
                {
                    toWrite.Add(toRemoveItem.ToString());
                }

                Console.WriteLine(string.Join(" ", toWrite));

                toRemove = toRemove.OrderByDescending(item => item).ToList();
                foreach (var toRemoveItem in toRemove)
                {
                    lastWinner = boards[toRemoveItem];
                    boards.RemoveAt(toRemoveItem);
                }

                if(boards.Count() == 0)
                {
                    return this.ScoreBoard(lastWinner, drawn).ToString();
                }
            }

            return null;            
        }

        private bool BoardWon(List<List<string>> boardToCheck, List<string> drawn)
        {
            foreach(var row in boardToCheck)
            {
                if(this.CheckSet(row, drawn))
                {
                    return true;
                }
            }

            for (var x = 0; x < 5; x++)
            {
                var columnToCheck = new List<string>();
                foreach (var row in boardToCheck)
                {
                    columnToCheck.Add(row[x]);
                }

                if (this.CheckSet(columnToCheck, drawn))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckSet(List<string> tocheck, List<string> drawn)
        {
            var missingOne = false;
            foreach(var item in tocheck)
            {
                if (!drawn.Contains(item))
                {
                    missingOne = true;
                }
            }

            return !missingOne;
        }

        private int ScoreBoard(List<List<string>> boardToScore, List<string> drawn)
        {
            var score = 0;
            foreach(var row in boardToScore)
            {
                foreach(var num in row)
                {
                    if (!drawn.Contains(num))
                    {
                        score += int.Parse(num);
                    }
                }
            }

            return score * int.Parse(drawn.Last());
        }


    }
}
