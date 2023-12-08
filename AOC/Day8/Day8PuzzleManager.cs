namespace AOC_2023.Day8
{
    public class Day8PuzzleManager : PuzzleManager
    {
        public string Directions { get; set; }
        public List<Node> Nodes { get; set; }
        public Day8PuzzleManager()
        {
            var inputHelper = new Day8InputHelper(INPUT_FILE_NAME);
            (Directions, Nodes) = inputHelper.Parse();
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
            var startNodeName = "AAA";
            var endNodeName = "ZZZ";
            var currentNode = Nodes.First(x => x.Name == startNodeName);
            var directionIndex = 0;
            var atEndNode = false;
            var solution = 0;
            while (!atEndNode)
            {
                solution++;

                if (Directions[directionIndex] == 'L')
                {
                    currentNode = currentNode!.Left;
                }
                else
                {
                    currentNode = currentNode!.Right;
                }

                directionIndex = (directionIndex + 1) % Directions.Length;

                if (currentNode!.Name == endNodeName)
                {
                    atEndNode = true;
                }
            }
            Console.WriteLine($"The solution to part one is '{solution}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var startNodes = Nodes.Where(x => x.Name[2] == 'A').ToList();
            var currentNodes = new List<Node>(startNodes);
            var endNodes = Nodes.Where(x => x.Name[2] == 'Z');
            var endingLengths = new List<long>();

            for (var i = 0; i < currentNodes.Count; i++)
            {
                var directionIndex = 0;
                var atEndNode = false;
                var pathLength = 0;
                while (!atEndNode)
                {
                    pathLength++;

                    if (Directions[directionIndex] == 'L')
                    {
                        currentNodes[i] = currentNodes[i]!.Left!;
                    }
                    else
                    {
                        currentNodes[i] = currentNodes[i]!.Right!;
                    }

                    directionIndex = (directionIndex + 1) % Directions.Length;

                    if (endNodes.Contains(currentNodes[i]) && directionIndex == 0)
                    {
                        atEndNode = true;
                        endingLengths.Add(pathLength);
                    }
                }
            }
            var solution = 1L;
            foreach (var endingLength in endingLengths)
            {
                solution = Lcm(solution, endingLength);
            }

            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }

        static long Gcf(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static long Lcm(long a, long b)
        {
            return (a / Gcf(a, b)) * b;
        }
    }
}