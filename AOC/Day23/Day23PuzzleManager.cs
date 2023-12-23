using BfsStateNoDistance = (long x, long y);
using BfsState = (long x, long y, long distanceTravelled, System.Collections.Generic.HashSet<(long x, long y)> visitedStates);

// Not very proud of this one! Runs terribly and I had a simple bug with part two that held me up for ages.

namespace AOC_2023.Day23
{
    public class Day23PuzzleManager : PuzzleManager
    {
        public char[,] Input { get; set; }
        public Day23PuzzleManager()
        {
            var inputHelper = new Day23InputHelper(INPUT_FILE_NAME);
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
            var statesToVisit = new Queue<BfsState>();
            statesToVisit.Enqueue((1, 0, 0, new HashSet<BfsStateNoDistance>()));
            var solution = 0L;

            while (statesToVisit.Count > 0)
            {
                var currentState = statesToVisit.Dequeue();
                currentState.visitedStates.Add((currentState.x, currentState.y));
                solution = Math.Max(solution, currentState.distanceTravelled);

                var directions = new List<(long x, long y)>();
                if (Input[currentState.x, currentState.y] == '.')
                {
                    directions.AddRange(new List<(long x, long y)>() { (1, 0), (-1, 0), (0, 1), (0, -1) });
                }
                else if (Input[currentState.x, currentState.y] == '>')
                {
                    directions.Add((1, 0));
                }
                else
                {
                    directions.Add((0, 1));
                }

                foreach (var direction in directions)
                {
                    var nextX = currentState.x + direction.x;
                    var nextY = currentState.y + direction.y;
                    if (nextX < 0 || nextX >= Input.GetLength(0) ||
                        nextY < 0 || nextY >= Input.GetLength(1))
                    {
                        continue;
                    }
                    var nextTile = Input[nextX, nextY];
                    if (nextTile == '#')
                    {
                        continue;
                    }
                    if (currentState.visitedStates.Contains((nextX, nextY)))
                    {
                        continue;
                    }
                    statesToVisit.Enqueue((nextX, nextY, currentState.distanceTravelled + 1, new HashSet<BfsStateNoDistance>(currentState.visitedStates)));
                }
            }
            Console.WriteLine($"The solution to part one is '{solution}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var pathSections = GeneratePathSections();

            var statesToVisit = new Queue<(long x, long y, long distanceTravelled, HashSet<PathSection> visitedPaths, HashSet<BfsStateNoDistance> visitedCoords)>();
            statesToVisit.Enqueue((1, 0, 0, new HashSet<PathSection>(), new HashSet<BfsStateNoDistance>()));
            var solution = 0L;

            while (statesToVisit.Count > 0)
            {
                var currentState = statesToVisit.Dequeue();
                if (currentState.visitedCoords.Contains((currentState.x, currentState.y)))
                {
                    continue;
                }
                currentState.visitedCoords.Add((currentState.x, currentState.y));
                if ((currentState.x, currentState.y) == (Input.GetLength(0) - 2, Input.GetLength(1) - 1))
                {
                    solution = Math.Max(solution, currentState.distanceTravelled);
                    continue;
                }

                var currentPathSections = pathSections.Where(section => (section.X1, section.Y1) == (currentState.x, currentState.y) || (section.X2, section.Y2) == (currentState.x, currentState.y));
                
                foreach (var pathSection in currentPathSections)
                {
                    if (currentState.visitedPaths.Contains(pathSection))
                    {
                        continue;
                    }
                    (var nextX, var nextY) = (pathSection.X1, pathSection.Y1) == (currentState.x, currentState.y) ?
                        (pathSection.X2, pathSection.Y2) :
                        (pathSection.X1, pathSection.Y1);

                    if (currentState.visitedCoords.Contains((nextX, nextY)))
                    {
                        continue;
                    }
                    var visitedPaths = new HashSet<PathSection>(currentState.visitedPaths)
                    {
                        pathSection
                    };
                    statesToVisit.Enqueue((nextX, nextY, currentState.distanceTravelled + pathSection.Length, visitedPaths, new HashSet<BfsStateNoDistance>(currentState.visitedCoords)));
                }
            }
            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }

        private List<PathSection> GeneratePathSections()
        {
            var pathSections = new List<PathSection>();
            var statesToVisit = new Queue<(long x, long y, long startX, long startY, long distanceInPath)>();
            var visitedStates = new HashSet<BfsStateNoDistance>();
            statesToVisit.Enqueue((1, 0, 1, 0, 0));
            while (statesToVisit.Count > 0)
            {
                var currentState = statesToVisit.Dequeue();
                if (visitedStates.Contains((currentState.x, currentState.y)))
                {
                    continue;
                }
                visitedStates.Add((currentState.x, currentState.y));

                var directions = new List<(long x, long y)>();
                var currentTile = Input[currentState.x, currentState.y];
                if (currentTile == '.')
                {
                    directions.AddRange(new List<(long x, long y)>() { (1, 0), (-1, 0), (0, 1), (0, -1) });
                }
                else if (currentTile == '>')
                {
                    directions.Add((1, 0));
                }
                else
                {
                    directions.Add((0, 1));
                }

                foreach (var direction in directions)
                {
                    var nextX = currentState.x + direction.x;
                    var nextY = currentState.y + direction.y;
                    if (nextX < 0 || nextX >= Input.GetLength(0) ||
                        nextY < 0 || nextY >= Input.GetLength(1))
                    {
                        continue;
                    }
                    var nextTile = Input[nextX, nextY];
                    if (nextTile == '#')
                    {
                        continue;
                    }
                    if (
                        direction != (0, 1) && nextTile == 'v' ||
                        direction != (1, 0) && nextTile == '>')
                    {
                        continue;
                    }
                    var startX = currentState.startX;
                    var startY = currentState.startY;
                    var distanceInPath = currentState.distanceInPath;
                    if (IsAtEndOfPath((nextX, nextY)) &&
                        (nextX, nextY) != (1, 0))
                    {
                        pathSections.Add(
                            new PathSection(
                                currentState.startX,
                                currentState.startY,
                                nextX,
                                nextY,
                                currentState.distanceInPath + 1
                                )
                            );
                        distanceInPath = -1;
                        startX = nextX;
                        startY = nextY;
                    }
                    if (visitedStates.Contains((nextX, nextY)))
                    {
                        continue;
                    }
                    statesToVisit.Enqueue((nextX, nextY, startX, startY, distanceInPath + 1));
                }
            }
            return pathSections;
        }

        private bool IsAtEndOfPath(
            (long x, long y) coords)
        {
            var directions = new List<(long x, long y)>() { (1, 0), (-1, 0), (0, 1), (0, -1) };
            var arrowCount = 0;
            foreach (var direction in directions)
            {
                var nextX = coords.x + direction.x;
                var nextY = coords.y + direction.y;
                if (nextX < 0 || nextX >= Input.GetLength(0) ||
                    nextY < 0 || nextY >= Input.GetLength(1))
                {
                    return true;
                }
                var nextTile = Input[nextX, nextY];
                if (">v".Contains(nextTile))
                {
                    arrowCount++;
                }
            }
            if (arrowCount >= 3)
            {
                return true;
            }
            return false;
        }
    }
}