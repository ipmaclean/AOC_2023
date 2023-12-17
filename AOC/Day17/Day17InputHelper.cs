namespace AOC_2023.Day17
{
    public class Day17InputHelper : InputHelper<int[,]>
    {
        public Day17InputHelper(string fileName) : base(fileName)
        {
        }

        public override int[,] Parse()
        {
            var output = new int[0, 0];
            using (var sr = new StreamReader(InputPath))
            {
                var ln = sr.ReadLine()!;
                output = new int[ln.Length, ln.Length];
                var y = 0;

                while (ln != null)
                {
                    for (var x = 0; x < output.GetLength(0); x++)
                    {
                        output[x, y] = ln[x] - '0';
                    }
                    y++;
                    ln = sr.ReadLine();
                }
            }
            return output;
        }
    }
}