namespace AOC_2023.Day16
{
    public class Tile
    {
        public char Type { get; set; }
        public HashSet<Direction> LightDirections { get; set; } = new HashSet<Direction>();

        public Tile(char type)
        {
            Type = type;
        }
    }
}
