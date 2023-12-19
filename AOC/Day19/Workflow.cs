using System.Xml.Linq;

namespace AOC_2023.Day19
{
    public class Workflow
    {
        public List<Rule> Rules { get; set; }

        public Workflow(List<Rule> rules)
        {
            Rules = rules;
        }

        public string ApplyRulesToMachinePart(MachinePart part)
        {
            foreach (var rule in Rules)
            {
                if ((rule.MachineProperty.HasValue && rule.Test!(part.Categories[rule.MachineProperty.Value])) ||
                    !rule.MachineProperty.HasValue)
                {
                    return rule.Destination;
                }
            }
            throw new InvalidOperationException("Did not return a Destination from Workflow.");
        }
    }
}
