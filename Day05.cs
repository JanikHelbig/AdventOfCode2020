using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
	public class Day05 : IDay
	{
		private const string InputFile = "Inputs/input_05.txt";

		private const string RowPattern = "^(F|B){7}";
		private const string ColPattern = "(L|R){3}$";

		private static IEnumerable<int> GetSeatIDs()
		{
			return from line in File.ReadLines(InputFile)
			       let rowChars = Regex.Match(line, RowPattern).Value
			       let colChars = Regex.Match(line, ColPattern).Value
			       let row = EvaluateSequence(rowChars, 'F')
			       let col = EvaluateSequence(colChars, 'L')
			       select row * 8 + col;
		}
		
		private static int EvaluateSequence(string sequence, char lower)
		{
			int min = 0, max = (int) Math.Pow(2, sequence.Length);
			
			foreach (char element in sequence)
			{
				int newBound = min + (max - min) / 2;
				if (element == lower) max = newBound;
				else min = newBound;
			}

			return min;
		}

		private static int FindHighestSeatID() => GetSeatIDs().Max();

		private static int FindEmptySeatID()
		{
			List<int> sortedIDs = GetSeatIDs().ToList();
			sortedIDs.Sort();

			int previousID = sortedIDs[0];
			foreach (int seatID in sortedIDs)
			{
				if (previousID < seatID - 1)
					return seatID - 1;
				previousID = seatID;
			}

			return -1;
		}

		public (string resultOne, string resultTwo) GetResults()
		{
			int highestSeatID = FindHighestSeatID();
			var resultOne = $"The highest seat ID is {highestSeatID}.";

			int emptySeatID = FindEmptySeatID();
			var resultTwo = $"The empty seat ID is {emptySeatID}.";
			
			return (resultOne, resultTwo);
		}
	}
}