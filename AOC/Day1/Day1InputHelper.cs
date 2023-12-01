namespace AOC_2023.Day1
{
    internal class Day1InputHelper : InputHelper<List<int>>
    {
        public Day1InputHelper(string fileName) : base(fileName)
        {
        }

        public override List<int> Parse()
        {
            var output = new List<int>();
            using (var sr = new StreamReader(InputPath))
            {
                string ln;
                var elf = new List<int>();
                while ((ln = sr.ReadLine()!) != null)
                {
                    output.Add(int.Parse(ln));
                }
            }
            return output;
        }
    }
}
