namespace AOC_2023.Day16
{
    public class Day16InputHelper : InputHelper<Tile[,]>
    {
        public Day16InputHelper(string fileName) : base(fileName)
        {
        }

        public override Tile[,] Parse()
        {
            var output = new Tile[0, 0];
            using (var sr = new StreamReader(InputPath))
            {
                var ln = sr.ReadLine()!;
                output = new Tile[ln.Length, ln.Length];
                var y = 0;

                while (ln != null)
                {
                    for (var x = 0; x < output.GetLength(0); x++)
                    {
                        output[x, y] = new Tile(ln[x]);
                    }
                    y++;
                    ln = sr.ReadLine();
                }
            }
            return output;
        }


    }
}