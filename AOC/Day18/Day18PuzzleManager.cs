namespace AOC_2023.Day18
{
    public class Day18PuzzleManager : PuzzleManager
    {
        public List<DigInstruction> Input { get; set; }

        private readonly Dictionary<char, (long x, long y)> _directionDict = new Dictionary<char, (long x, long y)>()
        {
            { 'U', (0, -1) },
            { 'R', (1, 0) },
            { 'D', (0, 1) },
            { 'L', (-1, 0) },
            { '3', (0, -1) },
            { '0', (1, 0) },
            { '1', (0, 1) },
            { '2', (-1, 0) }
        };

        public Day18PuzzleManager()
        {
            var inputHelper = new Day18InputHelper(INPUT_FILE_NAME);
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
            var cornerCoords = new List<(long x, long y)>() { (0, 0) };
            var perimeter = 0L;
            var currentCoord = cornerCoords.First();
            foreach (var digInstruction in Input)
            {
                currentCoord = (
                    currentCoord.x + _directionDict[digInstruction.Direction].x * digInstruction.Distance,
                    currentCoord.y + _directionDict[digInstruction.Direction].y * digInstruction.Distance
                    );
                cornerCoords.Add(currentCoord);
                perimeter += digInstruction.Distance;
            }
            // Pick's Theorem
            Console.WriteLine($"The solution to part one is '{1 + CalculateArea(cornerCoords) + perimeter / 2}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var cornerCoords = new List<(long x, long y)>() { (0, 0) };
            var perimeter = 0L;
            var currentCoord = cornerCoords.First();
            foreach (var digInstruction in Input)
            {
                var distance = Convert.ToInt64(digInstruction.ColourHex[..5], 16);
                currentCoord = (
                    currentCoord.x + _directionDict[digInstruction.ColourHex[5]].x * distance,
                    currentCoord.y + _directionDict[digInstruction.ColourHex[5]].y * distance
                    );
                cornerCoords.Add(currentCoord);
                perimeter += distance;
            }
            // Pick's Theorem
            Console.WriteLine($"The solution to part two is '{1 + CalculateArea(cornerCoords) + perimeter / 2}'.");
            return Task.CompletedTask;
        }

        private long CalculateArea(List<(long x, long y)> corners)
        {
            // Shoelace formula
            var sum = 0L;
            for (var i = 0; i < corners.Count - 1; i++)
            {
                sum += corners[i].x * corners[i + 1].y;
                sum -= corners[i].y * corners[i + 1].x;
            }
            return Math.Abs(sum / 2);
        }
    }
}