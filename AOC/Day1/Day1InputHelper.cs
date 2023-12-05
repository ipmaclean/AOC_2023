namespace AOC_2023.Day1
{
    public class Day1InputHelper : InputHelper<List<string>>
    {
        public Day1InputHelper(string fileName) : base(fileName)
        {
        }

        public override List<string> Parse()
        {
            var output = new List<string>();
            using (var sr = new StreamReader(InputPath))
            {
                string ln;
                while ((ln = sr.ReadLine()!) != null)
                {
                    output.Add(ln);
                }
            }
            return output;
        }
    }
}