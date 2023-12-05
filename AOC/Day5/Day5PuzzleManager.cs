namespace AOC_2023.Day5
{
    public class Day5PuzzleManager : PuzzleManager
    {
        public Almanac Almanac { get; set; }
        public Day5PuzzleManager()
        {
            var inputHelper = new Day5InputHelper(INPUT_FILE_NAME);
            Almanac = inputHelper.Parse();
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
            var currentLocations = new List<long>(Almanac.Seeds);
            foreach (var maps in Almanac.MapsInOrder)
            {
                var nextLocations = new List<long>();
                foreach (var location in currentLocations)
                {
                    var locationMapped = false;
                    foreach (var map in maps)
                    {
                        if (location >= map.SourceRangeStart && location < map.SourceRangeStart + map.RangeLength)
                        {
                            nextLocations.Add(map.DestinationRangeStart + (location - map.SourceRangeStart));
                            locationMapped = true;
                            break;
                        }
                    }
                    if (!locationMapped)
                    {
                        nextLocations.Add(location);
                    }
                }
                currentLocations = nextLocations;
            }
            Console.WriteLine($"The solution to part one is '{currentLocations.Min()}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var currentLocations = new Queue<(long start, long range)>();
            for (var i = 0; i < Almanac.Seeds.Count; i += 2)
            {
                currentLocations.Enqueue((Almanac.Seeds[i], Almanac.Seeds[i + 1]));
            }
            foreach (var maps in Almanac.MapsInOrder)
            {
                var nextLocations = new Queue<(long start, long range)>();
                while (currentLocations.Count > 0)
                {
                    var location = currentLocations.Dequeue();
                    foreach (var map in maps)
                    {
                        // can map the full range
                        if (location.start >= map.SourceRangeStart && location.start < map.SourceRangeStart + map.RangeLength &&
                            location.start + location.range - 1 >= map.SourceRangeStart && location.start + location.range - 1 < map.SourceRangeStart + map.RangeLength)
                        {
                            nextLocations.Enqueue(
                                (map.DestinationRangeStart + (location.start - map.SourceRangeStart),
                                location.range));

                            location = (-1, -1); // nothing left to map
                            break;
                        }
                        // range extends either side of map
                        else if (location.start < map.SourceRangeStart &&
                             location.start + location.range - 1 >= map.SourceRangeStart + map.RangeLength)
                        {
                            nextLocations.Enqueue(
                                (map.DestinationRangeStart,
                                map.RangeLength));

                            location =
                                (location.start,
                                map.SourceRangeStart - location.start);
                            currentLocations.Enqueue(
                                (map.SourceRangeStart + map.RangeLength,
                                (map.SourceRangeStart + map.RangeLength) - (location.start + location.range)));
                        }
                        // range begins in map but extends out
                        else if (location.start >= map.SourceRangeStart && location.start < map.SourceRangeStart + map.RangeLength &&
                            location.start + location.range - 1 >= map.SourceRangeStart + map.RangeLength)
                        {
                            nextLocations.Enqueue(
                                (map.DestinationRangeStart + (location.start - map.SourceRangeStart),
                                (map.SourceRangeStart + map.RangeLength) - location.start));

                            location = 
                                (map.SourceRangeStart + map.RangeLength,
                                (location.start + location.range) - (map.SourceRangeStart + map.RangeLength));
                        }
                        // range begins before map but extends in
                        else if (location.start < map.SourceRangeStart &&
                            location.start + location.range - 1 >= map.SourceRangeStart && location.start + location.range - 1 < map.SourceRangeStart + map.RangeLength)
                        {
                            nextLocations.Enqueue(
                                (map.DestinationRangeStart,
                                (location.start + location.range) - map.SourceRangeStart));

                            location =
                                (location.start,
                                map.SourceRangeStart - location.start);
                        }
                    }

                    if (location != (-1, -1))
                    {
                        nextLocations.Enqueue(location);
                    }
                }
                currentLocations = nextLocations;
            }
            Console.WriteLine($"The solution to part two is '{currentLocations.Min(x => x.start)}'.");
            return Task.CompletedTask;
        }
    }
}