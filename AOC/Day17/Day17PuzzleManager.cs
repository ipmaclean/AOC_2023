using BfsStateNoDistance = (int x, int y, AOC_2023.Day17.Direction direction, int distanceTravelledStraight);
using BfsState = (int x, int y, AOC_2023.Day17.Direction direction, int distanceTravelledStraight, int totalDistance);

// This puzzle only works with a square input - notably it will fail on the second part 2 example

namespace AOC_2023.Day17
{
    public class Day17PuzzleManager : PuzzleManager
    {
        public int[,] Input { get; set; }
        public Day17PuzzleManager()
        {
            var inputHelper = new Day17InputHelper(INPUT_FILE_NAME);
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
            Console.WriteLine($"The solution to part one is '{FindShortestPath(Input, 0, 3)}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            Console.WriteLine($"The solution to part two is '{FindShortestPath(Input, 4, 10)}'.");
            return Task.CompletedTask;
        }

        private int FindShortestPath(int[,] input, int minimumStraightBeforeTurn, int maximumStraightBeforeTurn)
        {
            // Because we can't discount visited nodes, we can't use Dijkstra.
            // We'll instead use a priority queue BFS with a state including
            // distance travelled straight and direction (along with total distance).
            var states = new PriorityQueue<BfsState, int>();
            states.Enqueue((0, 0, Direction.East, 0, 0), 0);
            states.Enqueue((0, 0, Direction.South, 0, 0), 0);
            var visitedStates = new HashSet<BfsStateNoDistance>();

            while (states.Count > 0)
            {
                var currentState = states.Dequeue();

                // Enqueue next states if we have not yet seen them
                List<BfsStateNoDistance> nextStates = currentState.direction switch
                {
                    Direction.North => new List<BfsStateNoDistance>() {
                        (currentState.x - 1, currentState.y, Direction.West, 1),
                        (currentState.x, currentState.y - 1, Direction.North, currentState.distanceTravelledStraight + 1),
                        (currentState.x + 1, currentState.y, Direction.East, 1)
                    },
                    Direction.East => new List<BfsStateNoDistance>() {
                        (currentState.x, currentState.y - 1, Direction.North, 1),
                        (currentState.x + 1, currentState.y, Direction.East, currentState.distanceTravelledStraight + 1),
                        (currentState.x, currentState.y + 1, Direction.South, 1)
                    },
                    Direction.South => new List<BfsStateNoDistance>() {
                        (currentState.x - 1, currentState.y, Direction.West, 1),
                        (currentState.x, currentState.y + 1, Direction.South, currentState.distanceTravelledStraight + 1),
                        (currentState.x + 1, currentState.y, Direction.East, 1)
                    },
                    Direction.West => new List<BfsStateNoDistance>() {
                        (currentState.x, currentState.y - 1, Direction.North, 1),
                        (currentState.x - 1, currentState.y, Direction.West, currentState.distanceTravelledStraight + 1),
                        (currentState.x, currentState.y + 1, Direction.South, 1)
                    },
                    _ => throw new ArgumentOutOfRangeException("Unexpected Light Direction found.")
                };

                // Check that states are valid and not already visited then
                // add the the queue of states to visit.
                foreach (var nextState in nextStates)
                {
                    if (nextState.distanceTravelledStraight > maximumStraightBeforeTurn)
                    {
                        continue;
                    }
                    if (nextState.direction != currentState.direction &&
                        currentState.distanceTravelledStraight < minimumStraightBeforeTurn)
                    {
                        continue;
                    }
                    if (nextState.x < 0 || nextState.x >= input.GetLength(0) ||
                        nextState.y < 0 || nextState.y >= input.GetLength(0))
                    {
                        continue;
                    }
                    if (visitedStates.Contains(nextState))
                    {
                        continue;
                    }
                    if (nextState.x == input.GetLength(0) - 1 && nextState.y == input.GetLength(0) - 1 &&
                        nextState.distanceTravelledStraight < minimumStraightBeforeTurn)
                    {
                        continue;
                    }
                    else if (nextState.x == input.GetLength(0) - 1 && nextState.y == input.GetLength(0) - 1)
                    {
                        return currentState.totalDistance + input[nextState.x, nextState.y];
                    }
                    visitedStates.Add(nextState);
                    states.Enqueue(
                        (nextState.x, nextState.y, nextState.direction, nextState.distanceTravelledStraight, currentState.totalDistance + input[nextState.x, nextState.y]),
                        currentState.totalDistance + input[nextState.x, nextState.y]
                        );
                }
            }
            return -1;
        }
    }
}