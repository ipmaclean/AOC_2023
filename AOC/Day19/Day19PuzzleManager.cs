namespace AOC_2023.Day19
{
    public class Day19PuzzleManager : PuzzleManager
    {
        public Dictionary<string, Workflow> Workflows { get; set; }
        public List<MachinePart> MachineParts { get; set; }
        public Day19PuzzleManager()
        {
            var inputHelper = new Day19InputHelper(INPUT_FILE_NAME);
            (Workflows, MachineParts) = inputHelper.Parse();
        }
        public override Task SolveBothParts()
        {
            SolvePartOne();
            Console.WriteLine();
            SolvePartTwo();
            return Task.CompletedTask;
        }

        public override Task SolvePartOne()
        {
            var solution = 0L;
            foreach (var part in MachineParts)
            {
                var destination = "in";
                while (!(destination == "A" || destination == "R"))
                {
                    destination = Workflows[destination].ApplyRulesToMachinePart(part);
                }
                if (destination == "A")
                {
                    solution += part.Categories.Values.Sum();
                }
            }

            Console.WriteLine($"The solution to part one is '{solution}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var solution = 0L;
            foreach (var keyValuePair in Workflows)
            {
                for (var i = 0; i < keyValuePair.Value.Rules.Count; i++)
                {
                    var rule = keyValuePair.Value.Rules[i].Destination;
                    if (rule == "A")
                    {
                        // Each route to acceptance is its own unique path - therefore each path
                        // has a unique set of acceptable ranges for "xmas" that do not overlap
                        // with any other path.
                        // We can safely just find the sum of each individual route's total possible
                        // combinations without worrying about overlapping ranges.
                        solution += PathsToAcceptanceFromRule(keyValuePair.Key, i);
                    }
                }
            }
            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }

        private long PathsToAcceptanceFromRule(
            string workflowName,
            int ruleIndex)
        {
            var ranges = new Dictionary<char, (long min, long max)>()
            {
                { 'x', (1, 4000) },
                { 'm', (1, 4000) },
                { 'a', (1, 4000) },
                { 's', (1, 4000) }
            };
            TrimRanges(ranges, workflowName, ruleIndex);
            return ranges.Values.Aggregate(1L, (product, value) => product *= value.min <= value.max ? value.max - value.min + 1 : 0);
        }

        private void TrimRanges(
            Dictionary<char, (long min, long max)> ranges,
            string workflowName,
            int ruleIndex)
        {
            var currentRule = Workflows[workflowName].Rules[ruleIndex];
            if (currentRule.MachineProperty.HasValue && currentRule.ComparisonCharacter.HasValue)
            {
                (var comparisonValue, var comparisonOperator) = GetComparisonValueAndOperatorFromRule(currentRule);
                var range = ranges[currentRule.MachineProperty.Value];
                if (comparisonOperator == '<')
                {
                    var max = Math.Min(range.max, comparisonValue - 1);
                    ranges[currentRule.MachineProperty.Value] = (range.min, max);
                }
                else
                {
                    var min = Math.Max(range.min, comparisonValue + 1);
                    ranges[currentRule.MachineProperty.Value] = (min, range.max);
                }
            }
            for (var i = ruleIndex - 1; i >= 0; i--)
            {
                currentRule = Workflows[workflowName].Rules[i];
                (var comparisonValue, var comparisonOperator) = GetComparisonValueAndOperatorFromRule(currentRule);
                var range = ranges[currentRule.MachineProperty!.Value];
                if (comparisonOperator == '<')
                {
                    var min = Math.Max(range.min, comparisonValue);
                    ranges[currentRule.MachineProperty.Value] = (min, range.max);
                }
                else
                {
                    var max = Math.Min(range.max, comparisonValue);
                    ranges[currentRule.MachineProperty.Value] = (range.min, max);
                }
            }
            if (workflowName == "in")
            {
                return;
            }
            (var nextWorkflowName, var nextRuleIndex) = FindRuleSendingToWorkflow(workflowName);
            TrimRanges(ranges, nextWorkflowName, nextRuleIndex);
        }

        private (long comparisonValue, char comparisonOperator) GetComparisonValueAndOperatorFromRule(Rule currentRule)
        {
            var target = currentRule.Test!.Target!;
            var comparisonValue = (long)target.GetType().GetFields()[0].GetValue(target)!;
            var comparisonOperator = currentRule.ComparisonCharacter!.Value;
            return (comparisonValue, comparisonOperator);
        }

        private (string nextWorkflowName, int nextRuleIndex) FindRuleSendingToWorkflow(string workflowName)
        {
            foreach (var keyValuePair in Workflows)
            {
                var rules = keyValuePair.Value.Rules;
                for (var i = 0; i < rules.Count; i++)
                {
                    if (rules[i].Destination == workflowName)
                    {
                        return (keyValuePair.Key, i);
                    }
                }
            }
            throw new InvalidOperationException($"Could not find a workflow rule with a destination '{workflowName}'.");
        }
    }
}