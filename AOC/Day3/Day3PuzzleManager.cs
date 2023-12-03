namespace AOC_2023.Day3
{
    public class Day3PuzzleManager : PuzzleManager
    {
        public List<List<char>> Input { get; set; }
        public readonly List<char> _numbers = new List<char>()
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
            };
        public Day3PuzzleManager()
        {
            var inputHelper = new Day3InputHelper(INPUT_FILE_NAME);
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
            var solution = 0;

            for (var i = 0; i < Input.Count; i++)
            {
                var row = Input[i];
                var currentNumberString = string.Empty;
                var numberIsPart = false;
                for (var j = 0; j < row.Count; j++)
                {
                    var tile = row[j];
                    if (_numbers.Contains(tile))
                    {
                        currentNumberString += tile;
                        numberIsPart = numberIsPart || IsTilePart(i, j);
                        if (j == row.Count - 1 && numberIsPart)
                        {
                            numberIsPart = false;
                            solution += int.Parse(currentNumberString);
                            currentNumberString = string.Empty;
                        }
                    }
                    else
                    {
                        if (numberIsPart)
                        {
                            numberIsPart = false;
                            solution += int.Parse(currentNumberString);
                        }
                        currentNumberString = string.Empty;
                    }
                }
            }

            Console.WriteLine($"The solution to part one is '{solution}'.");
            return Task.CompletedTask;
        }

        private bool IsTilePart(int row, int col)
        {
            var adjacentTiles = new List<(int x, int y)>()
            {
                (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)
            };
            foreach (var tile in adjacentTiles)
            {
                if (row + tile.x >= 0 &&
                    row + tile.x < Input.Count &&
                    col + tile.y >= 0 &&
                    col + tile.y < Input[0].Count)
                {
                    var tileToCheck = Input[row + tile.x][col + tile.y];
                    if (!_numbers.Contains(tileToCheck) && tileToCheck != '.')
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override Task SolvePartTwo()
        {
            // I've assumed that gears are only ever adjacent to a max of two numbers
            // and that numbers are only ever adjacent to a max of one gear.

            var solution = 0;
            var gearDict = new Dictionary<(int row, int col), int>();

            for (var i = 0; i < Input.Count; i++)
            {
                var row = Input[i];
                var currentNumberString = string.Empty;
                (int row, int col) adjacentGearCoords = (-1, -1);
                for (var j = 0; j < row.Count; j++)
                {
                    var tile = row[j];
                    if (_numbers.Contains(tile))
                    {
                        currentNumberString += tile;
                        var currentAdjacentGearCoords = GiveCoordOfAdjacentGear(i, j);
                        adjacentGearCoords = currentAdjacentGearCoords == (-1, -1) ? adjacentGearCoords : currentAdjacentGearCoords;
                        if (j == row.Count - 1 && adjacentGearCoords != (-1, -1))
                        {

                            var number = int.Parse(currentNumberString);
                            if (gearDict.ContainsKey(adjacentGearCoords))
                            {
                                solution += gearDict[adjacentGearCoords] * number;
                            }
                            else
                            {
                                gearDict[adjacentGearCoords] = number;
                            }
                            adjacentGearCoords = (-1, -1);
                            currentNumberString = string.Empty;
                        }
                    }
                    else
                    {
                        if (adjacentGearCoords != (-1, -1))
                        {
                            var number = int.Parse(currentNumberString);
                            if (gearDict.ContainsKey(adjacentGearCoords))
                            {
                                solution += gearDict[adjacentGearCoords] * number;
                            }
                            else
                            {
                                gearDict[adjacentGearCoords] = number;
                            }
                            adjacentGearCoords = (-1, -1);
                        }
                        currentNumberString = string.Empty;
                    }
                }
            }
            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }

        private (int row, int col) GiveCoordOfAdjacentGear(int row, int col)
        {
            var adjacentTiles = new List<(int x, int y)>()
            {
                (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)
            };
            foreach (var tile in adjacentTiles)
            {
                if (row + tile.x >= 0 &&
                    row + tile.x < Input.Count &&
                    col + tile.y >= 0 &&
                    col + tile.y < Input[0].Count)
                {
                    var tileToCheck = Input[row + tile.x][col + tile.y];
                    if (tileToCheck == '*')
                    {
                        return (row + tile.x, col + tile.y);
                    }
                }
            }
            return (-1, -1);
        }
    }
}