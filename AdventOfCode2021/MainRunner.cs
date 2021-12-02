using System;

namespace AdventOfCode2021
{
    public class MainRunner
    {
        static void Main()
        {
            var dayToRun = new Day02();
            var answer = dayToRun.Run();

            Console.WriteLine($"Answer for {dayToRun.GetType().Name} is {answer}");
            Console.ReadLine();
        }
    }
}
