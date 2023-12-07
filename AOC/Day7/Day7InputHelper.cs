namespace AOC_2023.Day7
{
    public class Day7InputHelper : InputHelper<List<Hand>>
    {
        public Day7InputHelper(string fileName) : base(fileName)
        {
        }

        public override List<Hand> Parse()
        {
            var output = new List<Hand>();
            using (var sr = new StreamReader(InputPath))
            {
                string ln;
                while ((ln = sr.ReadLine()!) != null)
                {
                    var split = ln.Split(' ');
                    output.Add(new Hand(split[0], long.Parse(split[1])));
                }
            }
            return output;
        }

        public List<HandPartTwo> ParsePartTwo()
        {
            var output = new List<HandPartTwo>();
            using (var sr = new StreamReader(InputPath))
            {
                string ln;
                while ((ln = sr.ReadLine()!) != null)
                {
                    var split = ln.Split(' ');
                    output.Add(new HandPartTwo(split[0], long.Parse(split[1])));
                }
            }
            return output;
        }
    }
}