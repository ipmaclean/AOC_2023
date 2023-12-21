using BfsStateNoDistance = (int x, int y);
using BfsState = (int x, int y, int distanceTravelled);

// This solution only works with a square input

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
            // Simple BFS, but we'll only log states with even steps taken for our solution.

            var totalSteps = 64;
            var statesToVisit = new Queue<BfsState>();
            var startCoords = FindStart(Input);
            statesToVisit.Enqueue((startCoords.x, startCoords.y, 0));
            var visitedStates = new HashSet<BfsStateNoDistance>();

            var solution = 0;

            while (statesToVisit.Count > 0)
            {
                var currentState = statesToVisit.Dequeue();
                if (visitedStates.Contains((currentState.x, currentState.y)))
                {
                    continue;
                }
                visitedStates.Add((currentState.x, currentState.y));
                if (currentState.distanceTravelled % 2 == 0)
                {
                    solution++;
                }
                if (currentState.distanceTravelled + 1 > totalSteps)
                {
                    continue;
                }
                var directions = new List<(int x, int y)>() { (1, 0), (-1, 0), (0, 1), (0, -1) };
                foreach (var direction in directions)
                {
                    var nextX = currentState.x + direction.x;
                    var nextY = currentState.y + direction.y;
                    if (nextX < 0 || nextX >= Input.GetLength(0) ||
                        nextY < 0 || nextY >= Input.GetLength(1) ||
                        Input[nextX, nextY] == '#')
                    {
                        continue;
                    }
                    if (visitedStates.Contains((nextX, nextY)))
                    {
                        continue;
                    }
                    statesToVisit.Enqueue((nextX, nextY, currentState.distanceTravelled + 1));
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

        private (int x, int y) FindStart(char[,] input)
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
    }
}