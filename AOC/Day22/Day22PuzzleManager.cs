namespace AOC_2023.Day22
{
    public class Day22PuzzleManager : PuzzleManager
    {
        public List<Brick> Input { get; set; }
        public Day22PuzzleManager()
        {
            var inputHelper = new Day22InputHelper(INPUT_FILE_NAME);
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
            var landedBricksByHighZ = JengaUpThoseBricks();
            var solution = 0;
            foreach (var landedBrick in landedBricksByHighZ.SelectMany(x => x.Value))
            {
                if (landedBrick.Supporting.All(x => x.SupportedBy.Count > 1))
                {
                    solution++;
                }
            }
            Console.WriteLine($"The solution to part one is '{solution}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var landedBricksByHighZ = JengaUpThoseBricks();
            var solution = 0;
            var brickToFallsDict = new Dictionary<Brick, long>();
            var landedBricksByLowZ = landedBricksByHighZ.SelectMany(x => x.Value).OrderBy(x => x.LowZ).ToList(); // order is important!
            for (var i = 0; i < landedBricksByLowZ.Count; i++)
            {
                solution += CountFallingBricksAfterDisintegration(landedBricksByLowZ, i, [landedBricksByLowZ[i]]);
            }
            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }

        private static int CountFallingBricksAfterDisintegration(
            List<Brick> landedBricksByLowZ, 
            int landedBricksByLowZIndex,
            HashSet<Brick> fallenBricks)
        {
            for (var i = landedBricksByLowZIndex; i < landedBricksByLowZ.Count; i++)
            {
                var fallenBrick = landedBricksByLowZ[i];
                foreach (var supportedBrick in fallenBrick.Supporting)
                {
                    if (supportedBrick.SupportedBy.All(fallenBricks.Contains))
                    {
                        fallenBricks.Add(supportedBrick);
                    }
                }
            }
            return fallenBricks.Count - 1;
        }

        private SortedDictionary<long, List<Brick>> JengaUpThoseBricks()
        {
            var fallingBricks = Input.Select(brick => brick.Clone()).OrderByDescending(brick => brick.LowZ).ToList();
            var landedBricksByHighZ = new SortedDictionary<long, List<Brick>>();

            for (var i = fallingBricks.Count - 1; i >= 0; i--)
            {
                var fallingBrick = fallingBricks[i];
                // Check if it lands on the brick(s) with the largest HighZ
                // If not, check the brick(s) with the next largest HighZ etc
                // Settle 1 above the bricks and update those bricks.
                var willLandAtThisZ = false;
                for (var j = landedBricksByHighZ.Count - 1; j >= 0; j--)
                {
                    foreach (var landedBrick in landedBricksByHighZ.ElementAt(j).Value)
                    {
                        if (fallingBrick.WillLandOn(landedBrick))
                        {
                            willLandAtThisZ = true;
                            landedBrick.Supporting.Add(fallingBrick);
                            fallingBrick.SupportedBy.Add(landedBrick);
                        }
                    }
                    if (willLandAtThisZ)
                    {
                        var landingLevel = landedBricksByHighZ.ElementAt(j).Key + 1;
                        DropBrickToZ(fallingBricks, i, fallingBrick, landedBricksByHighZ, landingLevel);
                        break;
                    }
                }
                if (!willLandAtThisZ)
                {
                    DropBrickToZ(fallingBricks, i, fallingBrick, landedBricksByHighZ, 1);
                }
            }
            return landedBricksByHighZ;
        }

        private static void DropBrickToZ(
            List<Brick> fallingBricks,
            int fallingBricksIndex,
            Brick fallingBrick,
            SortedDictionary<long, List<Brick>> landedBricksByHighZ,
            long landingLevel)
        {
            fallingBricks.RemoveAt(fallingBricksIndex);
            fallingBrick.Drop(landingLevel);
            if (!landedBricksByHighZ.ContainsKey(fallingBrick.HighZ))
            {
                landedBricksByHighZ[fallingBrick.HighZ] = new List<Brick>();
            }
            landedBricksByHighZ[fallingBrick.HighZ].Add(fallingBrick);
        }
    }
}