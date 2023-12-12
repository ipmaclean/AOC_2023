namespace AOC_2023.Day12
{
    public class Day12InputHelper : InputHelper<List<Record>>
    {
        public Day12InputHelper(string fileName) : base(fileName)
        {
        }

        public override List<Record> Parse()
        {
            var output = new List<Record>();
            using (var sr = new StreamReader(InputPath))
            {
                string ln;
                while ((ln = sr.ReadLine()!) != null)
                {
                    var springsAndGroups = ln.Split(' ');
                    var groups = springsAndGroups[1].Split(",");
                    output.Add(new Record(springsAndGroups[0], groups.Select(x => int.Parse(x)).ToList()));
                }
            }
            return output;
        }
    }
}