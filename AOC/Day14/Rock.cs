namespace AOC_2023.Day14
{
    public class Rock
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Rock(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Rock Clone()
        {
            return new Rock(X, Y);
        }
    }
}
