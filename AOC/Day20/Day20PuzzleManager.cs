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
            Console.WriteLine($"The solution to part one is '{Solve(1)}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            Console.WriteLine($"The solution to part two is '{Solve(2)}'.");
            return Task.CompletedTask;
        }

        private long Solve(int puzzlePart)
        {
            var inputHelper = new Day20InputHelper(INPUT_FILE_NAME);
            Modules = inputHelper.Parse();

            var lowPulseCount = 0L;
            var highPulseCount = 0L;
            var pulseQueue = new Queue<(string to, string from, Pulse pulse)>();
            var timesToPushButton = puzzlePart == 1 ? 1000L : -1L;

            // The below were found from using Graphviz to analyse the input.
            // These are the end of four closed loops and when each of these
            // emit a high pulse to 'nc' on the same button press then this
            // is the solution.
            // Therefore we work out when each of these loop and find the
            // lcm of the four loop lengths.
            var penultimates = new List<string>() { "hh", "fh", "lk", "fn" };
            var loops = new Dictionary<string, long>();
            while (timesToPushButton-- != 0)
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

                        if (output.to == "nc" && penultimates.Contains(output.from) && output.pulse == Pulse.High)
                        {
                            if (!loops.ContainsKey(output.from))
                            {
                                loops.Add(output.from, Math.Abs(timesToPushButton + 1));
                            }
                            if (loops.Count == 4)
                            {
                                return loops.Values.Aggregate(1L, (acc, val) => Lcm(acc, val));
                            }
                        }
                        pulseQueue.Enqueue(output);
                    }
                }
            }
            return lowPulseCount * highPulseCount;
        }

        static long Gcf(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static long Lcm(long a, long b)
        {
            return (a / Gcf(a, b)) * b;
        }
    }
}