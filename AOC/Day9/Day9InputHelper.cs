using System.Text.RegularExpressions;

namespace AOC_2023.Day9
{
    public class Day9InputHelper : InputHelper<List<List<long>>>
    {
        public Day9InputHelper(string fileName) : base(fileName)
        {
        }

        public override List<List<long>> Parse()
        {
            var redRegex = new Regex(@"-*\d+");
            var output = new List<List<long>>();
            using (var sr = new StreamReader(InputPath))
            {
                string ln;
                while ((ln = sr.ReadLine()!) != null)
                {
                    output.Add(redRegex.Matches(ln).Select(x => long.Parse(x.Value)).ToList());
                }
            }
            return output;
        }
    }
}