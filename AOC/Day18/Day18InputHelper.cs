namespace AOC_2023.Day18
{
    public class Day18InputHelper : InputHelper<List<DigInstruction>>
    {
        public Day18InputHelper(string fileName) : base(fileName)
        {
        }

        public override List<DigInstruction> Parse()
        {
            var output = new List<DigInstruction>();
            using (var sr = new StreamReader(InputPath))
            {
                string ln;
                while ((ln = sr.ReadLine()!) != null)
                {
                    var split = ln.Split(' ');
                    output.Add(new DigInstruction(split[0][0], long.Parse(split[1]), split[2].Substring(2, 6)));
                }
            }
            return output;
        }
    }
}