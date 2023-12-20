namespace AOC_2023.Day20
{
    public class FlipFlopModule : Module
    {
        public bool IsOn { get; set; } = false;

        public FlipFlopModule(string name, List<string> outputs) : base(name, outputs)
        {
        }

        public override List<(string to, string from, Pulse pulse)> ProcessPulse(string from, Pulse pulse)
        {
            var output = new List<(string to, string from, Pulse pulse)>();
            if (pulse == Pulse.Low)
            {
                IsOn = !IsOn;
                foreach (var outputName in Outputs)
                {
                    output.Add((outputName, Name, IsOn ? Pulse.High : Pulse.Low));
                }
            }
            return output;
        }
    }
}
