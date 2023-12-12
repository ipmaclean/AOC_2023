// Heavy inpsiration (read: cheating) drawn from https://github.com/encse/adventofcode/blob/master/2023/Day12/Solution.cs for part 2
// I found the memoisation for this one really difficult to get my head around

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
            var solution = 0L;
            foreach (var record in Input)
            {
                var cache = new Dictionary<(string springs, int countOfGroups), long>();
                solution += Solve(record.Springs, 0, record.Groups, cache);
            }
            Console.WriteLine($"The solution to part one is '{solution}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var solution = 0L;
            foreach (var record in Input)
            {
                var springs = string.Empty;
                var groups = new List<int>();
                var delimiter = "";
                for (var i = 0; i < 5; i++)
                {
                    springs += delimiter;
                    springs += record.Springs;
                    delimiter = "?";
                    groups.AddRange(record.Groups);
                }
                var cache = new Dictionary<(string springs, int countOfGroups), long>();
                solution += Solve(springs, 0, groups, cache);
            }
            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }

        private long Solve(
            string springs,
            int countOfGroups,
            List<int> groups,
            Dictionary<(string springs, int countOfGroups), long> cache
            )
        {
            if (!cache.ContainsKey((springs, countOfGroups)))
            {
                cache[(springs, countOfGroups)] = CountValidConfigurations(springs, countOfGroups, groups, cache);
            }
            return cache[(springs, countOfGroups)];
        }

        private long CountValidConfigurations(
            string springs,
            int countOfGroups,
            List<int> groups,
            Dictionary<(string springs, int countOfGroups), long> cache
            )
            => springs.FirstOrDefault() switch
            {
                '.' => ProcessDot(springs, countOfGroups, groups, cache),
                '#' => ProcessHash(springs, countOfGroups, groups, cache),
                '?' => ProcessQuestionMark(springs, countOfGroups, groups, cache),
                _ => countOfGroups == groups.Count ? 1 : 0,
            };

        private long ProcessDot(
            string springs,
            int countOfGroups,
            List<int> groups,
            Dictionary<(string springs, int countOfGroups), long> cache)
            => Solve(springs[1..], countOfGroups, groups, cache);

        private long ProcessHash(
            string springs,
            int countOfGroups,
            List<int> groups,
            Dictionary<(string springs, int countOfGroups), long> cache)
        {
            if (groups.Count == countOfGroups)
            {
                return 0;
            }

            var potentiallyHash = springs.TakeWhile(s => s == '#' || s == '?').Count();

            if (potentiallyHash < groups[countOfGroups])
            {
                return 0; // Not enough dead springs 
            }
            else if (springs.Length == groups[countOfGroups])
            {
                return Solve(string.Empty, countOfGroups + 1, groups, cache); // We've reached the end of the springs
            }
            else if (springs[groups[countOfGroups]] == '#')
            {
                return 0; // Dead spring follows the range -> too many for the current group
            }
            else
            {
                return Solve(springs[(groups[countOfGroups] + 1)..], countOfGroups + 1, groups, cache); // Cut off the group plus 1 more (will be '.' or '?') and add to the group count
            }
        }

        private long ProcessQuestionMark(
            string springs,
            int countOfGroups,
            List<int> groups,
            Dictionary<(string springs, int countOfGroups), long> cache)
            => Solve("." + springs[1..], countOfGroups, groups, cache) + Solve("#" + springs[1..], countOfGroups, groups, cache);
    }
}