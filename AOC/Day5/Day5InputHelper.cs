using System.Text.RegularExpressions;

namespace AOC_2023.Day5
{
    public class Day5InputHelper : InputHelper<Almanac>
    {
        public Day5InputHelper(string fileName) : base(fileName)
        {
        }

        public override Almanac Parse()
        {
            var seeds = new List<long>();
            var mapsInOrder = new List<List<Map>>();
            using (var sr = new StreamReader(InputPath))
            {
                string ln;
                var numberRegex = new Regex(@"\d+");
                while ((ln = sr.ReadLine()!) != string.Empty)
                {
                    seeds = numberRegex.Matches(ln).Select(x => long.Parse(x.Value)).ToList();
                }

                while ((ln = sr.ReadLine()!) != null)
                {
                    var maps = new List<Map>();
                    while ((ln = sr.ReadLine()!) != string.Empty && ln != null)
                    {
                        var mapMatches = numberRegex.Matches(ln).Select(x => long.Parse(x.Value)).ToList();
                        maps.Add(new Map(mapMatches[0], mapMatches[1], mapMatches[2]));
                    }
                    mapsInOrder.Add(maps);
                }
            }
            return new Almanac(seeds, mapsInOrder);
        }
    }
}