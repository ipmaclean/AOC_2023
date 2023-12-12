namespace AOC_2023.Day12
{
    public class Record
    {
        public string Springs { get; set; }
        public List<int> Groups { get; set; }

        public Record(string springs, List<int> record)
        {
            Springs = springs;
            Groups = record;
        }
    }
}
