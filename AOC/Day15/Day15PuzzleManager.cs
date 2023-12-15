namespace AOC_2023.Day15
{
    public class Day15PuzzleManager : PuzzleManager
    {
        public List<string> Input { get; set; }
        public Day15PuzzleManager()
        {
            var inputHelper = new Day15InputHelper(INPUT_FILE_NAME);
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
            foreach (var line in Input)
            {
                solution += RunHash(line);
            }
            Console.WriteLine($"The solution to part one is '{solution}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var boxes = new Dictionary<long, List<(string, long)>>();
            foreach (var line in Input)
            {
                var insert = line.Split('=');
                if (insert.Length > 1)
                {
                    var boxIndex = RunHash(insert[0]);
                    if (!boxes.ContainsKey(boxIndex))
                    {
                        boxes[boxIndex] = new List<(string, long)>() { (insert[0], long.Parse(insert[1])) };
                    }
                    else
                    {
                        var lensToReplace = boxes[boxIndex].FirstOrDefault(x => x.Item1 == insert[0]);
                        if (lensToReplace != (null, 0L))
                        {
                            boxes[boxIndex][boxes[boxIndex].IndexOf(lensToReplace)] = (insert[0], long.Parse(insert[1]));
                        }
                        else
                        {
                            boxes[boxIndex].Add((insert[0], long.Parse(insert[1])));
                        }
                    }
                }
                var remove = line.Split('-');
                if (remove.Length > 1)
                {
                    var boxIndex = RunHash(remove[0]);
                    if (boxes.ContainsKey(boxIndex))
                    {
                        var lensToRemove = boxes[boxIndex].FirstOrDefault(x => x.Item1 == remove[0]);
                        if (lensToRemove != (null, 0L))
                        {
                            boxes[boxIndex].Remove(lensToRemove);
                        }
                    }
                }
            }
            var solution = 0L;
            foreach (var keyValuePair in boxes)
            {
                for (var i = 0; i < keyValuePair.Value.Count; i++)
                {
                    solution += (keyValuePair.Key + 1) * (i + 1) * (keyValuePair.Value[i].Item2);
                }
            }
            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }

        private int RunHash(string line)
        {
            var currentValue = 0;
            foreach (var character in line)
            {
                currentValue += character;
                currentValue *= 17;
                currentValue %= 256;
            }
            return currentValue;
        }
    }
}