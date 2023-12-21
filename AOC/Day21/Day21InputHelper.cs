namespace AOC_2023.Day21
{
    public class Day21InputHelper : InputHelper<char[,]>
    {
        public Day21InputHelper(string fileName) : base(fileName)
        {
        }

        public override char[,] Parse()
        {
            var output = new char[0, 0];
            using (var sr = new StreamReader(InputPath))
            {
                var ln = sr.ReadLine()!;
                output = new char[ln.Length, ln.Length];
                var y = 0;

                while (ln != null)
                {
                    for (var x = 0; x < output.GetLength(0); x++)
                    {
                        output[x, y] = ln[x];
                    }
                    y++;
                    ln = sr.ReadLine();
                }
            }
            return output;
        }
    }
}