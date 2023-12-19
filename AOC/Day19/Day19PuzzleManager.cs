namespace AOC_2023.Day19
{
    public class Day19PuzzleManager : PuzzleManager
    {
        public Dictionary<string, Workflow> Workflows { get; set; }
        public List<MachinePart> MachineParts { get; set; }
        public Day19PuzzleManager()
        {
            var inputHelper = new Day19InputHelper(INPUT_FILE_NAME);
            (Workflows, MachineParts) = inputHelper.Parse();
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
            foreach (var part in MachineParts)
            {
                var destination = "in";
                while (!(destination == "A" || destination == "R"))
                {
                    destination = Workflows[destination].ApplyRulesToMachinePart(part);
                }
                if (destination == "A")
                {
                    solution += part.Categories.Values.Sum();
                }
            }

            Console.WriteLine($"The solution to part one is '{solution}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var solution = 0;
            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }
    }
}