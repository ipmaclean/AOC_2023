namespace AOC_2023.Day2
{
    public class Day2PuzzleManager : PuzzleManager
    {
        public Dictionary<int, List<(int r, int g, int b)>> Input { get; set; }
        public Day2PuzzleManager()
        {
            var inputHelper = new Day2InputHelper(INPUT_FILE_NAME);
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
            (int r, int g, int b) maxAllowed = (12, 13, 14);
            var solution = 0;

            for (var gameNumber = 1; gameNumber <= Input.Count; gameNumber++)
            {
                if (Input[gameNumber].All(x => 
                    x.r <= maxAllowed.r &&
                    x.g <= maxAllowed.g &
                    x.b <= maxAllowed.b))
                {
                    solution += gameNumber;
                }
            }
            Console.WriteLine($"The solution to part one is '{solution}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var solution = 0;

            foreach (var game in Input.Values)
            {
                var maxR = game.Max(x => x.r);
                var maxG = game.Max(x => x.g);
                var maxB = game.Max(x => x.b);

                solution += maxR * maxG * maxB;
            }
            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }
    }
}