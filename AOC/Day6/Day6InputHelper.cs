using System.Text.RegularExpressions;

namespace AOC_2023.Day6
{
    public class Day6InputHelper : InputHelper<List<Race>>
    {
        public Day6InputHelper(string fileName) : base(fileName)
        {
        }

        public override List<Race> Parse()
        {
            var numberRegex = new Regex(@"\d+");
            var times = new List<double>();
            var distances = new List<double>();
            using (var sr = new StreamReader(InputPath))
            {
                times = numberRegex.Matches(sr.ReadLine()!).Select(x => double.Parse(x.Value)).ToList();
                distances = numberRegex.Matches(sr.ReadLine()!).Select(x => double.Parse(x.Value)).ToList();
            }
            var output = new List<Race>();
            for (var i = 0; i < times.Count; i++)
            {
                output.Add(new Race(times[i], distances[i]));
            }
            return output;
        }

        public List<Race> ParsePartTwo()
        {
            var numberRegex = new Regex(@"\d+");
            var output = new List<Race>();
            using (var sr = new StreamReader(InputPath))
            {
                var time = double.Parse(numberRegex.Matches(sr.ReadLine()!).Select(x => x.Value).ToList().Aggregate(string.Empty, (x, y) => x + y));
                var distance = double.Parse(numberRegex.Matches(sr.ReadLine()!).Select(x => x.Value).ToList().Aggregate(string.Empty, (x, y) => x + y));
                output.Add(new Race(time, distance));
            }
            return output;
        }
    }
}