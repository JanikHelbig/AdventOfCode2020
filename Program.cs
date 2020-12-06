using System;
using System.Collections.Generic;

namespace AdventOfCode2020
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 | @JanikHelbig ");
            
            var days = new List<(string, IDay)>
            {
                ("Day  1", new Day01()),
                ("Day  2", new Day02()),
                ("Day  3", new Day03()),
                ("Day  4", new Day04()),
                ("Day  5", new Day05()),
                ("Day  6", new Day06())
            };

            foreach ((string name, IDay day) in days)
            {
                (string resultOne, string resultTwo) = day.GetResults();
                Console.WriteLine($"{name} ┬ 1: {resultOne}");
                Console.WriteLine($"       └ 2: {resultTwo}");
            }
        }
    }
}
