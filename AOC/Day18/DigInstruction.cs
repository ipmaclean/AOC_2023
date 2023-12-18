namespace AOC_2023.Day18
{
    public class DigInstruction
    {
        public char Direction { get; set; }
        public long Distance { get; set; }
        public string ColourHex { get; set; }

        public DigInstruction(char direction, long amount, string colourHex)
        {
            Direction = direction;
            Distance = amount;
            ColourHex = colourHex;
        }
    }
}
