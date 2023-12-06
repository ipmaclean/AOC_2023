namespace AOC_2023.Day6
{
    public class Day6PuzzleManager : PuzzleManager
    {
        public List<Race> Input { get; set; }
        public List<Race> InputPartTwo { get; set; }
        public Day6PuzzleManager()
        {
            var inputHelper = new Day6InputHelper(INPUT_FILE_NAME);
            Input = inputHelper.Parse();
            InputPartTwo = inputHelper.ParsePartTwo();
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
            Console.WriteLine($"The solution to part one is '{Solve(Input)}'.");
            return Task.CompletedTask;
        }


        public override Task SolvePartTwo()
        {
            Console.WriteLine($"The solution to part two is '{Solve(InputPartTwo)}'.");
            return Task.CompletedTask;
        }
        private int Solve(List<Race> input)
        {
            var solution = 1;
            foreach (var race in input)
            {
                var discrim = Math.Sqrt(((race.Time * race.Time) / 4) - race.Distance);
                var a = (race.Time / 2) + discrim;
                var b = (race.Time / 2) - discrim;
                var waysOfWinning = (int)Math.Floor(a) - (int)Math.Ceiling(b) + 1;
                if (IsInteger(a))
                {
                    waysOfWinning--;
                }
                if (IsInteger(b))
                {
                    waysOfWinning--;
                }
                solution *= waysOfWinning;
            }

            return solution;
        }

        private bool IsInteger(double d)
            => Math.Abs(d % 1) <= (double.Epsilon * 100);
    }
}