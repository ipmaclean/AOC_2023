namespace AOC_2023.Day4
{
    public class Day4PuzzleManager : PuzzleManager
    {
        public List<Ticket> Input { get; set; }
        public Day4PuzzleManager()
        {
            var inputHelper = new Day4InputHelper(INPUT_FILE_NAME);
            Input = inputHelper.Parse();
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
            var solution = 0;
            foreach (var ticket in Input)
            {
                var intersection = ticket.WinningNumbers.Intersect(ticket.OwnedNumbers);
                solution += (int)Math.Pow(2, intersection.Count() - 1);
            }
            Console.WriteLine($"The solution to part one is '{solution}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var ticketCounts = new Dictionary<int, int>();
            for (var i = 1; i <= Input.Count; i++)
            {
                ticketCounts.Add(i, 1);
            }

            foreach (var ticketCountPair in ticketCounts)
            {
                if (ticketCountPair.Value == 0)
                {
                    continue;
                }

                var ticket = Input.First(x => x.CardNumber == ticketCountPair.Key);
                var winCount = ticket.WinningNumbers.Intersect(ticket.OwnedNumbers).Count();

                for (var i = 1; i <= winCount; i++)
                {
                    ticketCounts[ticketCountPair.Key + i] += ticketCountPair.Value;
                }
            }

            Console.WriteLine($"The solution to part two is '{ticketCounts.Values.Sum()}'.");
            return Task.CompletedTask;
        }
    }
}