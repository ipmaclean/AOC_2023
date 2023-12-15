namespace AOC_2023.Day10
{
    public class Day10PuzzleManager : PuzzleManager
    {
        public List<string> Input { get; set; }
        public Day10PuzzleManager()
        {
            var inputHelper = new Day10InputHelper(INPUT_FILE_NAME);
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
            (var distance, _) = Solve();
            Console.WriteLine($"The solution to part one is '{distance / 2}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            (var distance, var corners) = Solve();
            var area = CalculateArea(corners);
            // Pick's theorem
            Console.WriteLine($"The solution to part two is '{1 + area - distance / 2}'.");
            return Task.CompletedTask;
        }

        private (int distance, List<(int x, int y)> corners) Solve()
        {
            (var x, var y) = FindStartPosition(Input);
            var direction = FindStartDirection(Input, x, y);
            var distance = 0;
            var corners = new List<(int x, int y)>() { (x, y) };
            do
            {
                (x, y, direction) = TravelOnPipe(x, y, direction, corners);
                distance++;
            }
            while (Input[y][x] != 'S');
            return (distance, corners);
        }

        private int CalculateArea(List<(int x, int y)> corners)
        {
            // Shoelace formula
            var sum = 0;
            for (var i = 0; i < corners.Count; i++)
            {
                sum += corners[i].x * corners[(i + 1) % corners.Count].y;
                sum -= corners[i].y * corners[(i + 1) % corners.Count].x;
            }
            return Math.Abs(sum / 2);
        }

        private (int x, int y, Direction direction) TravelOnPipe(int x, int y, Direction direction, List<(int x, int y)> corners)
        {
            (x, y) = direction switch
            {
                Direction.North => (x, y - 1),
                Direction.East => (x + 1, y),
                Direction.South => (x, y + 1),
                Direction.West => (x - 1, y),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), $"Unexpected direction value: {direction}"),
            };

            if (direction == Direction.North)
            {
                if (Input[y][x] == '7')
                {
                    direction = Direction.West;
                    corners.Add((x, y));
                }
                if (Input[y][x] == 'F')
                {
                    direction = Direction.East;
                    corners.Add((x, y));
                }
            }
            else if (direction == Direction.East)
            {
                if (Input[y][x] == 'J')
                {
                    direction = Direction.North;
                    corners.Add((x, y));
                }
                if (Input[y][x] == '7')
                {
                    direction = Direction.South;
                    corners.Add((x, y));
                }
            }
            else if (direction == Direction.South)
            {
                if (Input[y][x] == 'J')
                {
                    direction = Direction.West;
                    corners.Add((x, y));
                }
                if (Input[y][x] == 'L')
                {
                    direction = Direction.East;
                    corners.Add((x, y));
                }
            }
            else
            {
                if (Input[y][x] == 'L')
                {
                    direction = Direction.North;
                    corners.Add((x, y));
                }
                if (Input[y][x] == 'F')
                {
                    direction = Direction.South;
                    corners.Add((x, y));
                }
            }
            return (x, y, direction);
        }

        private Direction FindStartDirection(List<string> input, int x, int y)
        {
            if (y - 1 >= 0 && (input[y - 1][x] == '|' || input[y - 1][x] == '7' || input[y - 1][x] == 'F'))
            {
                return Direction.North;
            }
            if (x + 1 < input[y].Length && (input[y][x + 1] == '-' || input[y][x + 1] == 'J' || input[y][x + 1] == '7'))
            {
                return Direction.East;
            }
            if (y + 1 < input.Count && (input[y + 1][x] == '|' || input[y + 1][x] == 'J' || input[y + 1][x] == 'L'))
            {
                return Direction.South;
            }
            if (x - 1 >= 0 && (input[y][x - 1] == '-' || input[y][x - 1] == 'L' || input[y][x - 1] == 'F'))
            {
                return Direction.West;
            }
            throw new ArgumentException("Invalid input - could not find start direction.");
        }

        private (int x, int y) FindStartPosition(List<string> input)
        {
            for (var i = 0; i < input.Count; i++)
            {
                var startIndex = input[i].IndexOf("S");
                if (startIndex != -1)
                {
                    return (startIndex, i);
                }
            }
            throw new ArgumentException("Invalid input - could not find start position.");
        }
    }
}