namespace AOC_2023.Day20
{
    public abstract class Module
    {
        public string Name { get; private set; }
        public List<string> Outputs { get; private set; }
        public List<string> Inputs  { get; set; } = new List<string>();

        public Module(string name, List<string> outputs)
        {
            Name = name;
            Outputs = outputs;
        }

        public abstract List<(string to, string from, Pulse pulse)> ProcessPulse(string from, Pulse pulse);

        public virtual void AddInput(string name)
        {
            Inputs.Add(name);
        }
    }
}
