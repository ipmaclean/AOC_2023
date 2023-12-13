namespace AOC_2023.Day13
{
    public class Day13PuzzleManager : PuzzleManager
    {
        public List<GroundScan> Input { get; set; }
        public Day13PuzzleManager()
        {
            var inputHelper = new Day13InputHelper(INPUT_FILE_NAME);
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
            var counter = 0;
            foreach (var scan in Input)
            {
                counter++;
                var verticalReflection = FindVerticalReflection(scan);
                if (verticalReflection != -1)
                {
                    solution += verticalReflection;
                    continue;
                }
                var horizontalReflection = FindVerticalReflection(scan.CloneAndRotate());
                if (horizontalReflection != -1)
                {
                    solution += 100 * horizontalReflection;
                    continue;
                }
                throw new InvalidOperationException($"Did not find a reflection for input '{counter}'.");
            }
            Console.WriteLine($"The solution to part one is '{solution}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var solution = 0;
            var counter = 0;
            foreach (var scan in Input)
            {
                counter++;
                // In many cases, the part one solutions are still valid -
                // we will not check these lines of reflection
                int? verticalSkip = null;
                var originalVerticalReflection = FindVerticalReflection(scan);
                if (originalVerticalReflection != -1)
                {
                    verticalSkip = originalVerticalReflection;
                }
                int? horizontalSkip = null;
                var originalHorizontalReflection = FindVerticalReflection(scan.CloneAndRotate());
                if (originalHorizontalReflection != -1)
                {
                    horizontalSkip = originalHorizontalReflection;
                }
                if (verticalSkip is null && horizontalSkip is null)
                {
                    throw new InvalidOperationException($"Did not find a reflection for input '{counter}'.");
                }

                var foundReflection = false;
                for (var x = 0; x < scan.Rows[0].Count; x++)
                {
                    for (var y = 0; y < scan.Rows.Count; y++)
                    {
                        var scanClone = scan.Clone();
                        scanClone.Rows[y][x] = scanClone.Rows[y][x] == '#' ? '.' : '#';
                        var verticalReflection = FindVerticalReflection(scanClone, verticalSkip);
                        if (verticalReflection != -1)
                        {
                            solution += verticalReflection;
                            foundReflection = true;
                            break;
                        }
                        var horizontalReflection = FindVerticalReflection(scanClone.CloneAndRotate(), horizontalSkip);
                        if (horizontalReflection != -1)
                        {
                            solution += 100 * horizontalReflection;
                            foundReflection = true;
                            break;
                        }
                    }
                    if (foundReflection)
                    {
                        break;
                    }
                }
            }
            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }

        private int FindVerticalReflection(GroundScan scan, int? skipColumn = null)
        {
            var reflectionIndex = -1;
            var totalWidth = scan.Rows.First().Count;
            for (var i = 1; i < totalWidth; i++)
            {
                if (skipColumn is not null && skipColumn == i)
                {
                    continue;
                }
                var isValidReflection = true;
                var widthToCheck = Math.Min(i, totalWidth - i);
                foreach (var row in scan.Rows)
                {
                    for (var j = 0; j < widthToCheck; j++)
                    {
                        if (row[i - j - 1] != row[i + j])
                        {
                            isValidReflection = false;
                            break;
                        }
                    }
                    if (!isValidReflection)
                    {
                        break;
                    }
                }
                if (isValidReflection)
                {
                    reflectionIndex = i;
                    return reflectionIndex;
                }
            }
            return reflectionIndex;
        }
    }
}