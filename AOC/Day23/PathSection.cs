namespace AOC_2023.Day23
{
    public class PathSection
    {
        public long X1 { get; private set; }
        public long Y1 { get; private set; }
        public long X2 { get; private set; }
        public long Y2 { get; private set; }
        public long Length { get; private set; }

        public PathSection(long x1, long y1, long x2, long y2, long length)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            Length = length;
        }
    }
}
