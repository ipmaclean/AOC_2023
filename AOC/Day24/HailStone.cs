namespace AOC_2023.Day24
{
    public class HailStone
    {
        public (decimal x, decimal y, decimal z) Position { get; set; }
        public (decimal x, decimal y, decimal z) Velocity { get; set; }

        public HailStone(decimal x, decimal y, decimal z, decimal a, decimal b, decimal c)
        {
            Position = (x, y, z);
            Velocity = (a, b, c);
        }
    }
}
