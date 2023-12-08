namespace AOC_2023.Day7
{
    public class Day7PuzzleManager : PuzzleManager
    {
        public List<Hand> InputPartOne { get; set; }
        public List<Hand> InputPartTwo { get; set; }
        public Day7PuzzleManager()
        {
            var inputHelper = new Day7InputHelper(INPUT_FILE_NAME);
            InputPartOne = inputHelper.Parse();
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
            var hands = new List<Hand>(InputPartOne);
            Console.WriteLine($"The solution to part one is '{Solve(hands)}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var hands = new List<Hand>(InputPartTwo);
            Console.WriteLine($"The solution to part two is '{Solve(hands)}'.");
            return Task.CompletedTask;
        }

        private static long Solve(List<Hand> hands)
        {
            hands.Sort();
            var solution = 0L;
            for (var i = 0; i < hands.Count; i++)
            {
                solution += hands[i].Bid * ((long)i + 1);
            }
            return solution;
        }
    }
}