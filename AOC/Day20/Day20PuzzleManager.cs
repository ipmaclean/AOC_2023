namespace AOC_2023.Day20
{
    public class Day20PuzzleManager : PuzzleManager
    {
        public Dictionary<string, Module> Modules { get; set; } = new Dictionary<string, Module>();
        public Day20PuzzleManager()
        {
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
            var inputHelper = new Day20InputHelper(INPUT_FILE_NAME);
            Modules = inputHelper.Parse();

            var lowPulseCount = 0L;
            var highPulseCount = 0L;
            var pulseQueue = new Queue<(string to, string from, Pulse pulse)>();
            var timesToPushButton = 1000;
            for (var i = 0; i < timesToPushButton; i++)
            {
                pulseQueue.Enqueue(("broadcaster", "button", Pulse.Low));
                while (pulseQueue.Any())
                {
                    var instruction = pulseQueue.Dequeue();
                    _ = instruction.pulse == Pulse.Low ? lowPulseCount++ : highPulseCount++;
                    if (!Modules.ContainsKey(instruction.to))
                    {
                        continue;
                    }
                    var outputs = Modules[instruction.to].ProcessPulse(instruction.from, instruction.pulse);
                    foreach (var output in outputs)
                    {
                        pulseQueue.Enqueue(output);
                    }
                }
            }
            Console.WriteLine($"The solution to part one is '{lowPulseCount * highPulseCount}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var inputHelper = new Day20InputHelper(INPUT_FILE_NAME);
            Modules = inputHelper.Parse();



            var solution = 0;
            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }
    }
}