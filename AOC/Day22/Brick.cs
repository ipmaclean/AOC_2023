namespace AOC_2023.Day22
{
    public class Brick
    {
        public (long X, long Y, long Z) Coord1 { get; private set; }
        public (long X, long Y, long Z) Coord2 { get; private set; }
        public List<Brick> Supporting { get; private set; } = [];
        public HashSet<Brick> SupportedBy { get; private set; } = [];

        public Brick(
            (long X, long Y, long Z) coord1,
            (long X, long Y, long Z) coord2,
            List<Brick>? supporting = null,
            HashSet<Brick>? supportedBy = null)
        {
            Coord1 = coord1;
            Coord2 = coord2;
            Supporting = supporting ?? [];
            SupportedBy = supportedBy ?? [];
        }

        public long LowX => Math.Min(Coord1.X, Coord2.X);
        public long HighX => Math.Max(Coord1.X, Coord2.X);
        public long LowY => Math.Min(Coord1.Y, Coord2.Y);
        public long HighY => Math.Max(Coord1.Y, Coord2.Y);
        public long LowZ => Math.Min(Coord1.Z, Coord2.Z);
        public long HighZ => Math.Max(Coord1.Z, Coord2.Z);

        public bool WillLandOn(Brick brick)
        {
            if (HighX < brick.LowX || LowX > brick.HighX)
            {
                return false;
            }
            if (HighY < brick.LowY || LowY > brick.HighY)
            {
                return false;
            }
            return true;
        }

        public void Drop(long newLowZ)
        {
            var difference = LowZ - newLowZ;
            Coord1 = (Coord1.X, Coord1.Y, Coord1.Z - difference);
            Coord2 = (Coord2.X, Coord2.Y, Coord2.Z - difference);
        }

        public Brick Clone() => new(Coord1, Coord2, new List<Brick>(Supporting), new HashSet<Brick>(SupportedBy));
    }
}
