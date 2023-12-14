
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace AOC_2023.Day14
{
    public class Day14PuzzleManager : PuzzleManager
    {
        public List<Rock> RoundRocks { get; set; } //(List<Rock> roundRocks, List<Rock> cubeRocks, int maxX, int maxY)
        public List<Rock> CubeRocks { get; set; }

        private readonly int _sideLength;
        public Day14PuzzleManager()
        {
            var inputHelper = new Day14InputHelper(INPUT_FILE_NAME);
            (RoundRocks, CubeRocks, _sideLength) = inputHelper.Parse();
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
            var roundRocks = RoundRocks.Select(x => x.Clone()).ToList();
            var cubeRocks = CubeRocks.Select(x => x.Clone()).ToList();
            TiltNorth(roundRocks, cubeRocks, _sideLength);
            Console.WriteLine($"The solution to part one is '{SumTotalLoad(roundRocks, _sideLength)}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var roundRocks = RoundRocks.Select(x => x.Clone()).ToList();
            var cubeRocks = CubeRocks.Select(x => x.Clone()).ToList();

            var states = new Dictionary<string, int>();
            var cycle = 0;
            string state = GetState(roundRocks);
            states.Add(state, cycle++);
            var cyclesToRun = 1_000_000_000;

            while (cycle <= cyclesToRun)
            {
                CompleteTiltCycle(roundRocks, cubeRocks, _sideLength);
                state = GetState(roundRocks);
                if (states.ContainsKey(state))
                {
                    var counterWhenStateFirstSeen = states[state];
                    var cyclesRemaining = (cyclesToRun - cycle) % (cycle - counterWhenStateFirstSeen);
                    for (var i = 0; i < cyclesRemaining; i++)
                    {
                        CompleteTiltCycle(roundRocks, cubeRocks, _sideLength);
                    }
                    break;
                }
                states.Add(state, cycle++);
            }
            Console.WriteLine($"The solution to part two is '{SumTotalLoad(roundRocks, _sideLength)}'.");
            return Task.CompletedTask;
        }

        private void CompleteTiltCycle(List<Rock> roundRocks, List<Rock> cubeRocks, int sideLength)
        {
            for (var i = 0; i < 4; i++)
            {
                TiltNorth(roundRocks, cubeRocks, sideLength);
                RotateQuarterClockwise(roundRocks, cubeRocks, sideLength);
            }
        }

        private void PrintPretty(List<Rock> roundRocks, List<Rock> cubeRocks, int sideLength)
        {
            var array = new char[sideLength, sideLength];
            for (var y = 0; y < sideLength; y++)
            {
                for (var x = 0; x < sideLength; x++)
                {
                    array[x, y] = '.';
                }
            }
            foreach (var rock in roundRocks)
            {
                array[rock.X, rock.Y] = 'O';
            }
            foreach (var rock in cubeRocks)
            {
                array[rock.X, rock.Y] = '#';
            }
            var sb = new StringBuilder();
            for (var y = 0; y < sideLength; y++)
            {
                for (var x = 0; x < sideLength; x++)
                {
                    sb.Append(array[x, y]);
                }
                sb.Append(Environment.NewLine);
            }
            Console.WriteLine(sb.ToString());
        }

        private void TiltNorth(List<Rock> roundRocks, List<Rock> cubeRocks, int sideLength)
        {
            for (var x = 0; x < sideLength; x++)
            {
                var cubeRocksInCol = cubeRocks.Where(rock => rock.X == x).OrderBy(rock => rock.Y);
                var previousBlockingY = -1;

                foreach (var cubeRockInCol in cubeRocksInCol)
                {
                    var nextBlockingY = cubeRockInCol.Y;
                    previousBlockingY = PlaceRoundRocksInNorthSection(roundRocks, x, previousBlockingY, nextBlockingY);
                }

                PlaceRoundRocksInNorthSection(roundRocks, x, previousBlockingY, sideLength);
            }
        }

        private int PlaceRoundRocksInNorthSection(List<Rock> roundRocks, int x, int previousBlockingY, int nextBlockingY)
        {
            var counter = 0;
            foreach (var roundRockInSection in roundRocks.Where(rock => rock.X == x && rock.Y > previousBlockingY && rock.Y < nextBlockingY))
            {
                roundRockInSection.Y = previousBlockingY + 1 + counter++;
            }
            previousBlockingY = nextBlockingY;
            return previousBlockingY;
        }

        private void RotateQuarterClockwise(List<Rock> roundRocks, List<Rock> cubeRocks, int sideLength)
        {
            var allRocks = roundRocks.Union(cubeRocks);
            foreach (var rock in allRocks)
            {
                var translatedX = rock.X - ((sideLength / 2) - 0.5);
                var translatedY = rock.Y - ((sideLength / 2) - 0.5);
                var newX = -translatedY + ((sideLength / 2) - 0.5);
                var newY = translatedX + ((sideLength / 2) - 0.5);
                rock.X = (int)newX;
                rock.Y = (int)newY;
            }
        }

        private int SumTotalLoad(List<Rock> roundRocks, int sideLength)
        {
            var sum = 0;
            foreach (var roundRock in roundRocks)
            {
                sum += sideLength - roundRock.Y;
            }
            return sum;
        }

        private string GetState(List<Rock> roundRocks)
        {
            var sb = new StringBuilder();
            foreach (var rock in roundRocks.OrderBy(rock => rock.X).ThenBy(rock => rock.Y))
            {
                sb.Append($"({rock.X},{rock.Y})");
            }
            return sb.ToString();
        }
    }
}