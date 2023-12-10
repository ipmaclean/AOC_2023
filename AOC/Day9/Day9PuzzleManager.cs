namespace AOC_2023.Day9
{
    public class Day9PuzzleManager : PuzzleManager
    {
        public List<List<long>> Input { get; set; }
        public Day9PuzzleManager()
        {
            var inputHelper = new Day9InputHelper(INPUT_FILE_NAME);
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
            foreach (var sequence in Input)
            {
                solution += FindNextValueInSequence(sequence);
            }
            Console.WriteLine($"The solution to part one is '{solution}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var solution = 0L;
            foreach (var sequence in Input)
            {
                sequence.Reverse();
                solution += FindNextValueInSequence(sequence);
                sequence.Reverse();
            }
            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }

        private long FindNextValueInSequence(List<long> sequence)
        {
            var differences = new List<long>();
            for (var i = 0; i < sequence.Count - 1; i++)
            {
                differences.Add(sequence[i + 1] - sequence[i]);
            }
            if (differences.All(x => x == 0))
            {
                return sequence.Last();
            }
            return sequence.Last() + FindNextValueInSequence(differences);
        }
    }
}