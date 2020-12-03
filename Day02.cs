using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day02 : IDay
	{
		private const string InputFile = "Inputs/input_02.txt";
		
		private delegate bool ValidatePasswordDelegate(int a, int b, char character, string password);

		private static int ValidatePasswords(ValidatePasswordDelegate validatePassword)
		{
			List<string> lines = File.ReadLines(InputFile)
				.Where(x => !string.IsNullOrWhiteSpace(x))
				.ToList();
			
			var splitSeparators = new[] { '-', ':', ' ' };
			var splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
			
			IEnumerable<string> validPasswords =
				from line in lines
				select line.Split(splitSeparators, splitOptions)
				into parts
				let a = int.Parse(parts[0])
				let b = int.Parse(parts[1])
				let character = char.Parse(parts[2])
				let password = parts[3]
				where validatePassword(a, b, character, password)
				select password;
			
			return validPasswords.Count();
		}
		
		private static bool CheckNumberOfOccurrences(int min, int max, char character, string password)
		{
			int occurrences = password.Count(x => x == character);
			return min <= occurrences && max >= occurrences;
		}

		private static bool CheckPositionOfOccurrences(int a, int b, char character, string password)
		{
			return password[a - 1] == character ^ password[b - 1] == character;
		}

		public (string resultOne, string resultTwo) GetResults()
		{
			int validPasswordsOne = ValidatePasswords(CheckNumberOfOccurrences);
			var resultOne = $"Input contains {validPasswordsOne} valid passwords.";
			
			int validPasswordsTwo = ValidatePasswords(CheckPositionOfOccurrences);
			var resultTwo = $"Input contains {validPasswordsTwo} valid passwords.";

			return (resultOne, resultTwo);
		}
	}
}