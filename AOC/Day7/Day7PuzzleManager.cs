namespace AOC_2023.Day7
{
    public class Day7PuzzleManager : PuzzleManager
    {
        public List<Hand> InputPartOne { get; set; }
        public List<HandPartTwo> InputPartTwo { get; set; }
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
            hands.Sort();
            var solution = 0L;
            for (var i = 0; i < hands.Count; i++)
            {
                solution += hands[i].Bid * ((long)i + 1);
            }
            Console.WriteLine($"The solution to part one is '{solution}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var hands = new List<HandPartTwo>(InputPartTwo);
            hands.Sort();
            var solution = 0L;
            for (var i = 0; i < hands.Count; i++)
            {
                solution += hands[i].Bid * ((long)i + 1);
            }
            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }
    }
}