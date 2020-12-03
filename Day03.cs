using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
	public class Day03 : IDay
	{
		private const string InputFile = "Inputs/input_03.txt";

		private static bool[,] ConstructGrid()
		{
			List<string> lines = File.ReadLines(InputFile)
				.Where(line => !string.IsNullOrWhiteSpace(line))
				.ToList();
			
			int cols = lines[0].Length;
			int rows = lines.Count;
			var grid = new bool[cols, rows];

			for (var x = 0; x < cols; x++)
			for (var y = 0; y < rows; y++)
				grid[x, y] = lines[y][x] == '#';

			return grid;
		}

		private static int CountTreesHit()
		{
			bool[,] grid = ConstructGrid();
			return CalculateTreesHit(grid, 3, 1);
		}
		
		private static long CountAndMultiplyTreesHit()
		{
			bool[,] grid = ConstructGrid();
			var slopes = new List<(int x, int y)> { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };

			return slopes
				.Select(slope => (long) CalculateTreesHit(grid, slope.x, slope.y))
				.Aggregate((a, b) => a * b);
		}
		
		private static int CalculateTreesHit(bool[,] grid, int slopeX, int slopeY)
		{
			var treeCount = 0;

			for (var step = 0; step * slopeY < grid.GetLength(1); step++)
			{
				int x = slopeX * step % grid.GetLength(0);
				int y = slopeY * step;

				if (grid[x, y]) treeCount++;
			}
			
			return treeCount;
		}

		public (string resultOne, string resultTwo) GetResults()
		{
			int treesHit = CountTreesHit();
			var resultOne = $"The number of trees hit is {treesHit}.";
			
			long treesHitMultiplied = CountAndMultiplyTreesHit();
			var resultTwo = $"Multiplying the number of trees for all slopes results in {treesHitMultiplied}.";
			
			return (resultOne, resultTwo);
		}
	}
}