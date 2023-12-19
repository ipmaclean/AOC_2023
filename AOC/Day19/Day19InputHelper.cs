using System.Text.RegularExpressions;

namespace AOC_2023.Day19
{
    public class Day19InputHelper : InputHelper<(Dictionary<string, Workflow>, List<MachinePart>)>
    {
        public Day19InputHelper(string fileName) : base(fileName)
        {
        }

        public override (Dictionary<string, Workflow>, List<MachinePart>) Parse()
        {
            var workflows = new Dictionary<string, Workflow>();
            var parts = new List<MachinePart>();
            using (var sr = new StreamReader(InputPath))
            {
                string ln;
                while ((ln = sr.ReadLine()!) != string.Empty)
                {
                    var split = ln.Replace("}", string.Empty).Split('{');
                    var workflowName = split[0];
                    var rulesStrings = split[1].Split(',');

                    workflows.Add(workflowName, new Workflow(GetRules(rulesStrings)));
                }

                var numberRegex = new Regex(@"\d+");
                while ((ln = sr.ReadLine()!) != null)
                {
                    var matches = numberRegex.Matches(ln);
                    var machinePart = new MachinePart();
                    var xmas = "xmas";
                    for (int i = 0; i < xmas.Length; i++)
                    {
                        machinePart.Categories[xmas[i]] = long.Parse(matches[i].Value);
                    }
                    parts.Add(machinePart);
                }
            }
            return (workflows, parts);
        }

        private List<Rule> GetRules(string[] rulesStrings)
        {
            var rules = new List<Rule>();

            foreach (var rule in rulesStrings)
            {
                var split = rule.Split(":");
                if (split.Length == 1)
                {
                    rules.Add(new Rule(split[0]));
                }
                else
                {
                    var machineProperty = split[0].ToCharArray()[0];
                    var valueToCompare = long.Parse(split[0][2..]);
                    Func<long, bool> test;
                    if (split[0].Contains("<"))
                    {
                        test = (long category) => category < valueToCompare;
                        rules.Add(new Rule(split[1], machineProperty, test, '<'));
                    }
                    else
                    {
                        test = (long category) => category > valueToCompare;
                        rules.Add(new Rule(split[1], machineProperty, test, '>'));
                    }
                }
            }

            return rules;
        }
    }
}