namespace AOC_2023.Day20
{
    public class BroadcastModule : Module
    {
        public BroadcastModule(string name, List<string> outputs) : base(name, outputs)
        {
        }

        public override List<(string to, string from, Pulse pulse)> ProcessPulse(string from, Pulse pulse)
        {
            var output = new List<(string to, string from, Pulse pulse)>();
            foreach (var outputName in Outputs)
            {
                output.Add((outputName, Name, pulse));
            }
            return output;
        }
    }
}
