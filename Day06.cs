using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day06 : IDay
	{
		private const string InputFile = "Inputs/input_06.txt";

		private delegate int EvaluateAnswers(List<List<string>> answers);
		
		private static int EvaluateAnswerGroups(EvaluateAnswers evaluateAnswers)
		{
			var answersPerGroup = new List<List<string>>{ new() };

			foreach (string line in File.ReadLines(InputFile))
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					answersPerGroup.Add(new List<string>());
				}
				else
				{
					answersPerGroup.Last().Add(line);
				}
			}

			return evaluateAnswers(answersPerGroup);
		}

		private static int GetAnswerCountByAnyone(List<List<string>> answerGroups)
		{
			return answerGroups
				.Sum(answers => answers
					.SelectMany(answer => answer)
					.Distinct()
					.Count());
		}

		private static int GetAnswerCountByEveryone(List<List<string>> answerGroups)
		{
			return answerGroups
				.Sum(answers => answers
					.SelectMany(answer => answer)
					.GroupBy(character => character)
					.Count(group => group
						.Count() == answers.Count));
		}

		public (string resultOne, string resultTwo) GetResults()
		{
			var resultOne = $"The sum of any 'yes' answers per group is {EvaluateAnswerGroups(GetAnswerCountByAnyone)}.";
			var resultTwo = $"The sum of all 'yes' answers per group is {EvaluateAnswerGroups(GetAnswerCountByEveryone)}.";

			return (resultOne, resultTwo);
		}
	}
}