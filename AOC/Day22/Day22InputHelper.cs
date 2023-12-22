using System.Text.RegularExpressions;

namespace AOC_2023.Day22
{
    public class Day22InputHelper : InputHelper<List<Brick>>
    {
        public Day22InputHelper(string fileName) : base(fileName)
        {
        }

        public override List<Brick> Parse()
        {
            var output = new List<Brick>();
            var numberRegex = new Regex(@"\d+");
            using (var sr = new StreamReader(InputPath))
            {
                string ln;
                while ((ln = sr.ReadLine()!) != null)
                {
                    var matches = numberRegex.Matches(ln);
                    var brick = new Brick(
                        (long.Parse(matches[0].Value), long.Parse(matches[1].Value), long.Parse(matches[2].Value)), 
                        (long.Parse(matches[3].Value), long.Parse(matches[4].Value), long.Parse(matches[5].Value))
                        );
                    output.Add(brick);
                }
            }
            return output;
        }
    }
}