namespace AOC_2023.Day3
{
    internal class Day3InputHelper : InputHelper<List<List<char>>>
    {
        public Day3InputHelper(string fileName) : base(fileName)
        {
        }

        public override List<List<char>> Parse()
        {
            var output = new List<List<char>>();
            using (var sr = new StreamReader(InputPath))
            {
                string ln;
                while ((ln = sr.ReadLine()!) != null)
                {
                    output.Add(ln.ToCharArray().ToList());
                }
            }
            return output;
        }
    }
}