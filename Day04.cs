using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdventOfCode2020
{
	public class Day04 : IDay
	{
		private class Passport
		{
			public string byr;
			public string iyr;
			public string eyr;
			public string hgt;
			public string hcl;
			public string ecl;
			public string pid;
			public string cid;
		}

		private const string InputFile = "Inputs/input_04.txt";

		private delegate bool ValidatePassportDelegate(Passport passport);

		private static int ValidatePassports(ValidatePassportDelegate validatePassport)
		{
			List<string> lines = File.ReadLines(InputFile).ToList();
			var passports = new List<Passport> { new() };

			foreach (string line in lines)
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					passports.Add(new Passport());
				}
				else
				{
					Passport passport = passports[^1];
					foreach (string[] element in line.Split(' ').Select(value => value.Split(':')))
					{
						switch (element[0])
						{
							case "byr": passport.byr = element[1]; break;
							case "iyr": passport.iyr = element[1]; break;
							case "eyr": passport.eyr = element[1]; break;
							case "hgt": passport.hgt = element[1]; break;
							case "hcl": passport.hcl = element[1]; break;
							case "ecl": passport.ecl = element[1]; break;
							case "pid": passport.pid = element[1]; break;
							case "cid": passport.cid = element[1]; break;
						}
					}
				}
			}

			return passports.Count(x => validatePassport(x));
		}

		private static bool HasRequiredFields(Passport passport)
		{
			return passport.byr != null &&
			       passport.iyr != null &&
			       passport.eyr != null && 
			       passport.hgt != null && 
			       passport.hcl != null && 
			       passport.ecl != null && 
			       passport.pid != null;
		}

		private static bool HasRequiredFieldsAndValidData(Passport passport)
		{
			const string hgtNumberPattern = @"^[0-9]{2,3}";
			const string hgtUnitPattern = @"(cm|in)$";
			const string hgtPattern = hgtNumberPattern + hgtUnitPattern;

			return HasRequiredFields(passport) &&
			       int.TryParse(passport.byr, out int byr) && byr is >= 1920 and <= 2002 &&
			       int.TryParse(passport.iyr, out int iyr) && iyr is >= 2010 and <= 2020 &&
			       int.TryParse(passport.eyr, out int eyr) && eyr is >= 2020 and <= 2030 &&
			       Regex.IsMatch(passport.hgt, hgtPattern) &&
			       Regex.Match(passport.hgt, hgtNumberPattern) is {Success: true, Value: { } hgtNum} && int.TryParse(hgtNum, out int hgtNumber) &&
			       Regex.Match(passport.hgt, hgtUnitPattern) is {Success: true, Value: { } hgtUnit} &&
			       (hgtNumber is >= 150 and <= 193 && hgtUnit is "cm" || (hgtNumber is >= 59 and <= 76 && hgtUnit is "in")) &&
			       Regex.IsMatch(passport.hcl, @"^#[0-9a-f]{6}$") &&
			       Regex.IsMatch(passport.ecl, @"^(amb|blu|brn|gry|grn|hzl|oth)$") &&
			       Regex.IsMatch(passport.pid, @"^[0-9]{9}$");
		}

		public (string resultOne, string resultTwo) GetResults()
		{
			var resultOne = $"The valid number of passports is {ValidatePassports(HasRequiredFields)}.";
			var resultTwo = $"The valid number of passports is {ValidatePassports(HasRequiredFieldsAndValidData)}.";
			return (resultOne, resultTwo);
		}
	}
}