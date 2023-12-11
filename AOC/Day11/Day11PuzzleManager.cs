namespace AOC_2023.Day11
{
    public class Day11PuzzleManager : PuzzleManager
    {
        public List<string> Input { get; set; }
        public Day11PuzzleManager()
        {
            var inputHelper = new Day11InputHelper(INPUT_FILE_NAME);
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
            var galaxies = FindGalaxies(Input);
            ExpandSpace(galaxies, Input, 2);
            Console.WriteLine($"The solution to part one is '{SumDistancesBetweenPairs(galaxies)}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var galaxies = FindGalaxies(Input);
            ExpandSpace(galaxies, Input, 1_000_000);
            Console.WriteLine($"The solution to part two is '{SumDistancesBetweenPairs(galaxies)}'.");
            return Task.CompletedTask;
        }

        private List<Galaxy> FindGalaxies(List<string> input)
        {
            var galaxies = new List<Galaxy>();
            for (var y = 0; y < input.Count; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        galaxies.Add(new Galaxy(x, y));
                    }
                }
            }
            return galaxies;
        }

        private void ExpandSpace(List<Galaxy> galaxies, List<string> input, int expansionReplacementAmount)
        {
            // rows
            for (var i = input.Count - 1; i >= 0; i--)
            {
                if (input[i].All(tile => tile == '.'))
                {
                    galaxies.Where(galaxy => galaxy.Y > i).ToList().ForEach(galaxy => galaxy.Y += expansionReplacementAmount - 1);
                }
            }
            // columns
            for (var i = input[0].Length - 1; i >= 0; i--)
            {
                if (input.All(tile => tile[i] == '.'))
                {
                    galaxies.Where(galaxy => galaxy.X > i).ToList().ForEach(galaxy => galaxy.X += expansionReplacementAmount - 1);
                }
            }
        }

        private long SumDistancesBetweenPairs(List<Galaxy> galaxies)
        {
            var sum = 0L;
            for (var i = 0; i < galaxies.Count - 1; i++)
            {
                for (var j = i + 1; j < galaxies.Count; j++)
                {
                    sum += Math.Abs(galaxies[i].Y - galaxies[j].Y) + Math.Abs(galaxies[i].X - galaxies[j].X);
                }
            }
            return sum;
        }
    }
}