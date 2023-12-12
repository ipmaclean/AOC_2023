using System.Text.RegularExpressions;

namespace AOC_2023.Day12
{
    public class Day12PuzzleManager : PuzzleManager
    {
        public List<Record> Input { get; set; }
        public Day12PuzzleManager()
        {
            var inputHelper = new Day12InputHelper(INPUT_FILE_NAME);
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
            var solution = 0;
            foreach (var record in Input)
            {
                solution += CountValidConfigurations(record);
            }
            Console.WriteLine($"The solution to part one is '{solution}'.");
            return Task.CompletedTask;
        }

        private int CountValidConfigurations(Record record)
        {
            var indicesOfSprings = FindIndicesOfCharInString(record.Springs, '#');
            var springsToPlace = record.Groups.Sum() - indicesOfSprings.Count;
            return springsToPlace > 0 ? PermutateAndCheck(record.Springs, record.Groups, springsToPlace, 0, 0) : 1;
        }

        private int PermutateAndCheck(string springs, List<int> groups, int springsToPlace, int startingIndexForPlacement, int totalValidPermutations)
        {
            var indicesOfUnknown = FindIndicesOfCharInString(springs, '?');
            for (var i = startingIndexForPlacement; i < indicesOfUnknown.Count - springsToPlace + 1; i++)
            {
                var newSprings = springs.Remove(indicesOfUnknown[i], 1).Insert(indicesOfUnknown[i], "#");
                if (springsToPlace - 1 > 0)
                {
                    totalValidPermutations = PermutateAndCheck(newSprings, groups, springsToPlace - 1, i, totalValidPermutations);
                }
                else
                {
                    totalValidPermutations = IsValidPermutation(newSprings, groups) ? totalValidPermutations + 1 : totalValidPermutations;
                }
            }
            return totalValidPermutations;
        }

        private bool IsValidPermutation(string springs, List<int> groups)
        {
            var regex = new Regex(@"#+");
            var matches = regex.Matches(springs);
            if (matches.Count != groups.Count)
            {
                return false;
            }
            for (var i = 0; i < groups.Count; i++)
            {
                if (matches[i].Value.Length != groups[i])
                {
                    return false;
                }
            }
            return true;
        }

        private List<int> FindIndicesOfCharInString(string springs, char c)
        {
            var indices = new List<int>();
            for (var i = 0; i < springs.Length; i++)
            {
                if (springs[i] == c)
                {
                    indices.Add(i);
                }
            }
            return indices;
        }

        public override Task SolvePartTwo()
        {
            var solution = 0;
            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }
    }
}