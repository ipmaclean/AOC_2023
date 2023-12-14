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
                var verticalReflection = FindVerticalReflection(scan, 1);
                if (verticalReflection != -1)
                {
                    solution += verticalReflection;
                    continue;
                }
                var horizontalReflection = FindVerticalReflection(scan.CloneAndRotate(), 1);
                if (horizontalReflection != -1)
                {
                    solution += 100 * horizontalReflection;
                    continue;
                }
                throw new InvalidOperationException($"Did not find a reflection for input '{counter}'.");
            }
            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }

        private int FindVerticalReflection(GroundScan scan, int differencesWanted = 0)
        {
            var reflectionIndex = -1;
            var totalWidth = scan.Rows.First().Count;
            for (var i = 1; i < totalWidth; i++)
            {
                var isValidReflection = true;
                var differencesFound = 0;
                var widthToCheck = Math.Min(i, totalWidth - i);
                foreach (var row in scan.Rows)
                {
                    for (var j = 0; j < widthToCheck; j++)
                    {
                        if (row[i - j - 1] != row[i + j])
                        {
                            if (++differencesFound > differencesWanted)
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
                }
                if (differencesFound == differencesWanted)
                {
                    reflectionIndex = i;
                    return reflectionIndex;
                }
            }
            return reflectionIndex;
        }
    }
}