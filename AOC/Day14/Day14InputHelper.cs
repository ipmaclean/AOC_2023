namespace AOC_2023.Day14
{
    public class Day14InputHelper : InputHelper<(List<Rock> roundRocks, List<Rock> cubeRocks, int sideLength)>
    {
        public Day14InputHelper(string fileName) : base(fileName)
        {
        }

        public override (List<Rock> roundRocks, List<Rock> cubeRocks, int sideLength) Parse()
        {
            var roundRocks = new List<Rock>();
            var cubeRocks = new List<Rock>();
            var sideLength = 0;
            using (var sr = new StreamReader(InputPath))
            {
                string ln;
                var y = 0;
                while ((ln = sr.ReadLine()!) != null)
                {
                    for (var x = 0; x < ln.Length; x++)
                    {
                        if (ln[x] == 'O')
                        {
                            roundRocks.Add(new Rock(x, y));
                        }
                        else if (ln[x] == '#')
                        {
                            cubeRocks.Add(new Rock(x, y));
                        }
                    }
                    y++;
                    sideLength++;
                }
            }
            return (roundRocks, cubeRocks, sideLength);
        }
    }
}