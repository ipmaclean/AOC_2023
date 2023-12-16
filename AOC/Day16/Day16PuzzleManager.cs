namespace AOC_2023.Day16
{
    public class Day16PuzzleManager : PuzzleManager
    {
        public Tile[,] Input { get; set; }
        public Day16PuzzleManager()
        {
            var inputHelper = new Day16InputHelper(INPUT_FILE_NAME);
            Input = inputHelper.Parse();
        }
        public override Task SolveBothParts()
        {
            SolvePartOne();
            Console.WriteLine();
            Reset();
            SolvePartTwo();
            return Task.CompletedTask;
        }

        public override void Reset()
        {
            var inputHelper = new Day16InputHelper(INPUT_FILE_NAME);
            Input = inputHelper.Parse();
        }

        public override Task SolvePartOne()
        {
            Console.WriteLine($"The solution to part one is '{Solve((0, 0, Direction.Right))}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var solution = 0;
            var sideLength = Input.GetLength(0);
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                for (var i = 0; i < sideLength; i++)
                {
                    solution = direction switch
                    {
                        Direction.Up => Math.Max(solution, Solve((i, sideLength - 1, direction))),
                        Direction.Right => Math.Max(solution, Solve((0, i, direction))),
                        Direction.Down => Math.Max(solution, Solve((i, 0, direction))),
                        Direction.Left => Math.Max(solution, Solve((sideLength - 1, i, direction))),
                        _ => throw new ArgumentOutOfRangeException("Unexpected Light Direction found.")
                    };
                    Reset();
                }
            }
            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }

        private int Solve((int x, int y, Direction direction) startingState)
        {
            var statesToProcess = new Queue<(int x, int y, Direction direction)>();
            statesToProcess.Enqueue((startingState.x, startingState.y, startingState.direction));
            while (statesToProcess.Count > 0)
            {
                var nextStates = ProcessBeam(Input, statesToProcess.Dequeue());
                foreach (var state in nextStates)
                {
                    statesToProcess.Enqueue(state);
                }
            }
            return CountEnergisedTiles(Input);
        }

        private List<(int x, int y, Direction direction)> ProcessBeam(
            Tile[,] tiles,
            (int x, int y, Direction lightDirection) state)
        {
            var currentTile = tiles[state.x, state.y];
            if (currentTile.LightDirections.Contains(state.lightDirection))
            {
                return new List<(int x, int y, Direction direction)>();
            }

            currentTile.LightDirections.Add(state.lightDirection);
            var nextCoordsAndDirections = new List<(int x, int y, Direction direction)>();

            switch (currentTile.Type)
            {
                case '.':
                    nextCoordsAndDirections.Add(state.lightDirection switch
                    {
                        Direction.Up => (state.x, state.y - 1, Direction.Up),
                        Direction.Right => (state.x + 1, state.y, Direction.Right),
                        Direction.Down => (state.x, state.y + 1, Direction.Down),
                        Direction.Left => (state.x - 1, state.y, Direction.Left),
                        _ => throw new ArgumentOutOfRangeException("Unexpected Light Direction found.")
                    });
                    break;
                case '/':
                    nextCoordsAndDirections.Add(state.lightDirection switch
                    {
                        Direction.Up => (state.x + 1, state.y, Direction.Right),
                        Direction.Right => (state.x, state.y - 1, Direction.Up),
                        Direction.Down => (state.x - 1, state.y, Direction.Left),
                        Direction.Left => (state.x, state.y + 1, Direction.Down),
                        _ => throw new ArgumentOutOfRangeException("Unexpected Light Direction found.")
                    });
                    break;
                case '\\':
                    nextCoordsAndDirections.Add(state.lightDirection switch
                    {
                        Direction.Up => (state.x - 1, state.y, Direction.Left),
                        Direction.Right => (state.x, state.y + 1, Direction.Down),
                        Direction.Down => (state.x + 1, state.y, Direction.Right),
                        Direction.Left => (state.x, state.y - 1, Direction.Up),
                        _ => throw new ArgumentOutOfRangeException("Unexpected Light Direction found.")
                    });
                    break;
                case '-':
                    nextCoordsAndDirections.AddRange(state.lightDirection switch
                    {
                        Direction.Up => new List<(int x, int y, Direction direction)>() { (state.x - 1, state.y, Direction.Left), (state.x + 1, state.y, Direction.Right) },
                        Direction.Right => new List<(int x, int y, Direction direction)>() { (state.x + 1, state.y, Direction.Right) },
                        Direction.Down => new List<(int x, int y, Direction direction)>() { (state.x - 1, state.y, Direction.Left), (state.x + 1, state.y, Direction.Right) },
                        Direction.Left => new List<(int x, int y, Direction direction)>() { (state.x - 1, state.y, Direction.Left) },
                        _ => throw new ArgumentOutOfRangeException("Unexpected Light Direction found.")
                    });
                    break;
                case '|':
                    nextCoordsAndDirections.AddRange(state.lightDirection switch
                    {
                        Direction.Up => new List<(int x, int y, Direction direction)>() { (state.x, state.y - 1, Direction.Up) },
                        Direction.Right => new List<(int x, int y, Direction direction)>() { (state.x, state.y - 1, Direction.Up), (state.x, state.y + 1, Direction.Down) },
                        Direction.Down => new List<(int x, int y, Direction direction)>() { (state.x, state.y + 1, Direction.Down) },
                        Direction.Left => new List<(int x, int y, Direction direction)>() { (state.x, state.y - 1, Direction.Up), (state.x, state.y + 1, Direction.Down) },
                        _ => throw new ArgumentOutOfRangeException("Unexpected Light Direction found.")
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unexpected Tile Type found.");
            }

            var output = new List<(int x, int y, Direction direction)>();
            foreach (var nextCoordAndDirection in nextCoordsAndDirections) {
                if (nextCoordAndDirection.x < 0 || nextCoordAndDirection.x >= tiles.GetLength(0) || nextCoordAndDirection.y < 0 || nextCoordAndDirection.y >= tiles.GetLength(0))
                {
                    continue;
                }
                output.Add(nextCoordAndDirection);
            }
            return output;
        }

        private int CountEnergisedTiles(Tile[,] input)
        {
            var count = 0;
            foreach (var tile in input)
            {
                count += tile.LightDirections.Any() ? 1 : 0;
            }
            return count;
        }
    }
}