using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day01 : IDay
	{
		private const string InputFile = "Inputs/input_01.txt";
		
		private static (int a, int b) FindTwoMatchingNumbers()
		{
			List<int> numbers = File.ReadLines(InputFile).Select(int.Parse).ToList();
			numbers.Sort();
			
			int min = 2020 - numbers[^1];
			int max = 2020 - numbers[0];
			
			foreach (int number in numbers)
			{
				if (number < min || number > max) continue;

				int matchingNumber = 2020 - number;
				int index = numbers.BinarySearch(matchingNumber);
				
				if (index < 0) continue;
				
				return (number, matchingNumber);
			}
			
			return (0, 0);
		}
		
		private static (int a, int b, int c) FindThreeMatchingNumbers()
		{
			List<int> numbers = File.ReadLines(InputFile).Select(int.Parse).ToList();
			numbers.Sort();
			
			int minA = 2020 - numbers[^1];
			int maxA = 2020 - (numbers[0] + numbers[1]);

			foreach (int numberA in numbers)
			{
				if (numberA <= minA || numberA > maxA) continue;

				int minB = numberA;
				int maxB = 2020 - numberA;
				
				foreach (int numberB in numbers)
				{
					if (numberB <= minB || numberB > maxB) continue;
					
					int matchingNumber = 2020 - numberA - numberB;
					int index = numbers.BinarySearch(matchingNumber);
					
					if (index < 0) continue;
					
					return (numberA, numberB, matchingNumber);
				}
			}

			return (0, 0, 0);
		}

		public (string resultOne, string resultTwo) GetResults()
		{
			(int a, int b) = FindTwoMatchingNumbers();	
			var resultOne = $"The product of the two matching numbers {a} and {b} is {a * b}.";

			(int x, int y, int z) = FindThreeMatchingNumbers();
			var resultTwo = $"The product of the three matching numbers {x}, {y} and {z} is {x * y * z}.";
			
			return (resultOne, resultTwo);
		}
	}
}