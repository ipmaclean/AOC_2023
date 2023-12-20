namespace AOC_2023.Day20
{
    public class ConjunctionModule : Module
    {
        public Dictionary<string, Pulse> LatestInputPulses { get; set; } = new Dictionary<string, Pulse>();

        public ConjunctionModule(string name, List<string> outputs) : base(name, outputs)
        {
        }

        public override List<(string to, string from, Pulse pulse)> ProcessPulse(string from, Pulse pulse)
        {
            LatestInputPulses[from] = pulse;
            var output = new List<(string to, string from, Pulse pulse)>();
            foreach (var outputName in Outputs)
            {
                output.Add((outputName, Name, LatestInputPulses.Values.All(x => x == Pulse.High) ? Pulse.Low : Pulse.High));
            }
            return output;
        }

        public override void AddInput(string name)
        {
            base.AddInput(name);
            LatestInputPulses.Add(name, Pulse.Low);
        }
    }
}
