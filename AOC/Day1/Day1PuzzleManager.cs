using System.Text.RegularExpressions;

namespace AOC_2023.Day1
{
    public class Day1PuzzleManager : PuzzleManager
    {
        public List<string> Input { get; set; }
        public Day1PuzzleManager()
        {
            var inputHelper = new Day1InputHelper(INPUT_FILE_NAME);
            Input = inputHelper.Parse();
        }
        public override Task SolveBothParts()
        {
            SolvePartOne();
            Console.WriteLine();
            SolvePartTwo();
            return Task.CompletedTask;
        }

        public override Task SolvePartOne()
        {
            var numberRegexFirst = new Regex(@"\d");
            var solution = 0;
            foreach (var line in Input)
            {
                var matches = numberRegexFirst.Matches(line);
                solution += int.Parse(matches.First().Value + matches.Last().Value);
            }
            Console.WriteLine($"The solution to part one is '{solution}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var dict = new Dictionary<string, string>()
            {
                { "one", "1" },
                { "two", "2" },
                { "three", "3" },
                { "four", "4" },
                { "five", "5" },
                { "six", "6" },
                { "seven", "7" },
                { "eight", "8" },
                { "nine", "9" },
                { "1", "1" },
                { "2", "2" },
                { "3", "3" },
                { "4", "4" },
                { "5", "5" },
                { "6", "6" },
                { "7", "7" },
                { "8", "8" },
                { "9", "9" },
            };
            var pattern = string.Join("|", dict.Select(x => x.Key));
            var numberRegexFirst = new Regex(pattern);
            var numberRegexLast = new Regex(pattern, RegexOptions.RightToLeft);
            var solution = 0;
            foreach (var line in Input)
            {
                var firstMatch = numberRegexFirst.Match(line);
                var lastMatch = numberRegexLast.Match(line);
                solution += int.Parse(dict[firstMatch.Value] + dict[lastMatch.Value]);
            }
            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }
    }
}
