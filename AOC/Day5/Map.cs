namespace AOC_2023.Day5
{
    public class Map
    {
        public long DestinationRangeStart { get; set; }
        public long SourceRangeStart { get; set; }
        public long RangeLength { get; set; }

        public Map(long destinationRangeStart, long sourceRangeStart, long rangeLength)
        {
            DestinationRangeStart = destinationRangeStart;
            SourceRangeStart = sourceRangeStart;
            RangeLength = rangeLength;
        }
    }
}
