using System.Text.RegularExpressions;
namespace AOC_2023.Day2
{
    internal class Day2InputHelper : InputHelper<Dictionary<int, List<(int r, int g, int b)>>>
    {
        public Day2InputHelper(string fileName) : base(fileName)
        {
        }

        public override Dictionary<int, List<(int r, int g, int b)>> Parse()
        {
            var output = new Dictionary<int, List<(int r, int g, int b)>>();
            using (var sr = new StreamReader(InputPath))
            {
                string ln;
                var counter = 1;
                var redRegex = new Regex(@"\d+(?= red)");
                var greenRegex = new Regex(@"\d+(?= green)");
                var blueRegex = new Regex(@"\d+(?= blue)");
                while ((ln = sr.ReadLine()!) != null)
                {
                    var rounds = new List<(int r, int g, int b)>();
                    var roundsRaw = ln.Split(':')[1].Split(';');
                    foreach (var roundRaw in roundsRaw)
                    {
                        var rMatch = redRegex.Match(roundRaw);
                        var gMatch = greenRegex.Match(roundRaw);
                        var bMatch = blueRegex.Match(roundRaw);

                        var r = rMatch.Success ? int.Parse(redRegex.Match(roundRaw).Value) : 0;
                        var g = gMatch.Success ? int.Parse(greenRegex.Match(roundRaw).Value) : 0;
                        var b = bMatch.Success ? int.Parse(blueRegex.Match(roundRaw).Value) : 0;

                        rounds.Add((r, g, b));
                    }
                    output.Add(counter++, rounds);
                }
            }
            return output;
        }
    }
}