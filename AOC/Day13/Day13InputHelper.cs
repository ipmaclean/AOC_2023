namespace AOC_2023.Day13
{
    public class Day13InputHelper : InputHelper<List<GroundScan>>
    {
        public Day13InputHelper(string fileName) : base(fileName)
        {
        }

        public override List<GroundScan> Parse()
        {
            var output = new List<GroundScan>();
            using (var sr = new StreamReader(InputPath))
            {
                var ln = "abc";
                while (ln != null)
                {
                    var scan = new List<List<char>>();
                    while ((ln = sr.ReadLine()!) != string.Empty && ln != null)
                    {
                        scan.Add(ln.ToCharArray().ToList());
                    }
                    output.Add(new GroundScan(scan));
                }
            }
            return output;
        }
    }
}