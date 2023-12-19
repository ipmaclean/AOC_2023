namespace AOC_2023.Day19
{
    public class Rule
    {
        public char? MachineProperty { get; set; }
        public Func<long, bool>? Test { get; set; }
        public char? ComparisonCharacter { get; set; }
        public string Destination { get; set; }

        public Rule(
            string destination, 
            char? machineProperty = null, 
            Func<long, bool>? test = null,
            char? comparisonCharacter = null)
        {
            MachineProperty = machineProperty;
            Test = test;
            Destination = destination;
            ComparisonCharacter = comparisonCharacter;
        }
    }
}
