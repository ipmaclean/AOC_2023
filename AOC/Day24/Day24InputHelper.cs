using System.Text.RegularExpressions;

namespace AOC_2023.Day24
{
    public class Day24InputHelper : InputHelper<List<HailStone>>
    {
        public Day24InputHelper(string fileName) : base(fileName)
        {
        }

        public override List<HailStone> Parse()
        {
            var output = new List<HailStone>();
            using (var sr = new StreamReader(InputPath))
            {
                var numberRegex = new Regex(@"-?\d+");
                string ln;
                while ((ln = sr.ReadLine()!) != null)
                {
                    var matches = numberRegex.Matches(ln);
                    output.Add(new HailStone(
                        decimal.Parse(matches[0].Value),
                        decimal.Parse(matches[1].Value),
                        decimal.Parse(matches[2].Value), 
                        decimal.Parse(matches[3].Value),
                        decimal.Parse(matches[4].Value),
                        decimal.Parse(matches[5].Value))
                        );
                }
            }
            return output;
        }
    }
}