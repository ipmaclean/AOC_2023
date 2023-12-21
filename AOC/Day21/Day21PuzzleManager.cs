using BfsStateNoDistance = (long x, long y);
using BfsState = (long x, long y, long distanceTravelled);

// This solution only works with a square input
// and possibly with some special properties of the input!

namespace AOC_2023.Day21
{
    public class Day21PuzzleManager : PuzzleManager
    {
        public char[,] Input { get; set; }
        public Day21PuzzleManager()
        {
            var inputHelper = new Day21InputHelper(INPUT_FILE_NAME);
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
            Console.WriteLine($"The solution to part one is '{Solve(64)}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            // When increasing the steps by the width of the input the solutions
            // form a sequence - namely a quadratic one.
            var stepsToTake = 26501365L;
            var remainder = stepsToTake % Input.GetLength(0);
            var quotient = (stepsToTake - remainder) / Input.GetLength(0);
            var sequence = new List<long>();
            for (var i = 1; i < 4; i++)
            {
                sequence.Add(Solve(remainder + i * Input.GetLength(0)));
            }
            var lastValue = sequence.Last();
            var firstDifference = GetLatestFirstDifference(sequence);
            var secondDifference = GetSecondDifference(sequence);
            for (var i = 0; i < quotient - 3; i++)
            {
                firstDifference += secondDifference;
                lastValue += firstDifference;
            }
            Console.WriteLine($"The solution to part two is '{lastValue}'.");

            return Task.CompletedTask;
        }

        private long Solve(long totalSteps)
        {
            var statesToVisit = new Queue<BfsState>();
            var startCoords = FindStart(Input);
            statesToVisit.Enqueue((startCoords.x, startCoords.y, 0));
            var visitedStates = new Dictionary<BfsStateNoDistance, long>();

            var solution = 0L;

            while (statesToVisit.Count > 0)
            {
                var currentState = statesToVisit.Dequeue();
                if (visitedStates.ContainsKey((currentState.x, currentState.y)))
                {
                    continue;
                }
                visitedStates.Add((currentState.x, currentState.y), currentState.distanceTravelled);
                if (totalSteps % 2 == 0 && currentState.distanceTravelled % 2 == 0 ||
                    totalSteps % 2 == 1 && currentState.distanceTravelled % 2 == 1)
                {
                    solution++;
                }
                if (currentState.distanceTravelled + 1 > totalSteps)
                {
                    continue;
                }
                var directions = new List<(long x, long y)>() { (1, 0), (-1, 0), (0, 1), (0, -1) };
                foreach (var direction in directions)
                {
                    var nextX = currentState.x + direction.x;
                    var nextY = currentState.y + direction.y;
                    var nextTile = Input[Modulo(nextX, Input.GetLength(0)), Modulo(nextY, Input.GetLength(1))];
                    if (nextTile == '#')
                    {
                        continue;
                    }
                    if (visitedStates.ContainsKey((nextX, nextY)))
                    {
                        continue;
                    }
                    statesToVisit.Enqueue((nextX, nextY, currentState.distanceTravelled + 1));
                }
            }
            return solution;
        }

        private (long x, long y) FindStart(char[,] input)
        {
            for (var y = 0; y < input.GetLength(1); y++)
            {
                for (var x = 0; x < input.GetLength(0); x++)
                {
                    if (input[x, y] == 'S')
                    {
                        return (x, y);
                    }
                }
            }
            throw new InvalidOperationException("Could not find start coords.");
        }

        private static long Modulo(long x, long m)
        {
            long r = x % m;
            return r < 0 ? r + m : r;
        }

        private long GetLatestFirstDifference(List<long> sequence)
            => sequence.Last() - sequence[sequence.Count - 2];

        private long GetSecondDifference(List<long> sequence)
        {
            var differences = new List<long>();
            for (var i = 0; i < sequence.Count - 1; i++)
            {
                differences.Add(sequence[i + 1] - sequence[i]);
            }
            return differences[1] - differences[0];
        }
    }
}