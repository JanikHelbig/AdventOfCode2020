using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
	public class Day07 : IDay
	{
		private const string InputFile = "Inputs/input_07.txt";

		private static int GetNumberOfBagsContainingGoldBag()
		{
			var innerToOuter = new Dictionary<string, HashSet<string>>();

			foreach (string line in File.ReadLines(InputFile))
			{
				string outer = Regex.Match(line, @"^\w+\s\w+").Value;
				List<string> innerBags = Regex.Matches(line, @"\d\s\w+\s\w+")
					.Select(match => match.Value.Substring(2))
					.ToList();

				foreach (string inner in innerBags)
				{
					if (!innerToOuter.ContainsKey(inner))
						innerToOuter.Add(inner, new HashSet<string>());

					innerToOuter[inner].Add(outer);
				}
			}
			
			var bagsContainingGoldBag = new HashSet<string>();
			AddContainingBags("shiny gold");
			return bagsContainingGoldBag.Count;
			
			void AddContainingBags(string inner)
			{
				if (!innerToOuter.TryGetValue(inner, out HashSet<string> outerBags)) return;
				
				foreach (string outer in outerBags)
				{
					bagsContainingGoldBag.Add(outer);
					AddContainingBags(outer);
				}
			}
		}

		private static int GetTotalNumberBagsInGoldenBag()
		{
			var bagLookup = new Dictionary<string, List<(int count, string bags)>>();

			foreach (string line in File.ReadLines(InputFile))
			{
				string bag = Regex.Match(line, @"^\w+\s\w+").Value;
				List<(int, string)> innerBags = Regex.Matches(line, @"\d\s\w+\s\w+")
					.Select(match =>
					{
						int count = int.Parse(match.Value.Substring(0, 1));
						string innerBag = match.Value.Substring(2);
						return (count, innerBag);
					})
					.ToList();
				
				bagLookup.Add(bag, innerBags);
			}
			
			int CountBags(string bag) => bagLookup[bag]
				.Aggregate(0, (total, inner) => total + inner.count + inner.count * CountBags(inner.bags));

			return CountBags("shiny gold");
		}

		public (string resultOne, string resultTwo) GetResults()
		{
			var resultOne = $"The number of bags eventually containing a golden bag is {GetNumberOfBagsContainingGoldBag()}.";
			var resultTwo = $"The total number of bags inside the golden bag is {GetTotalNumberBagsInGoldenBag()}.";

			return (resultOne, resultTwo);
		}
	}
}