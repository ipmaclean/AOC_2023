namespace AOC_2023.Day5
{
    public class Almanac
    {
        public List<long> Seeds { get; set; }
        public List<List<Map>> MapsInOrder { get; set; }

        public Almanac(List<long> seeds, List<List<Map>> mapsInOrder)
        {
            Seeds = seeds;
            MapsInOrder = mapsInOrder;
        }
    }
}
