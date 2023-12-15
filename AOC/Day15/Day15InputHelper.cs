namespace AOC_2023.Day15
{
    public class Day15InputHelper : InputHelper<List<string>>
    {
        public Day15InputHelper(string fileName) : base(fileName)
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
                    output = ln.Split(',').ToList();
                }
            }
            return output;
        }
    }
}