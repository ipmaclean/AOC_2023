namespace AOC_2023.Day10
{
    public class Day10InputHelper : InputHelper<List<string>>
    {
        public Day10InputHelper(string fileName) : base(fileName)
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