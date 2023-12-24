namespace AOC_2023.Day24
{
    public class Day24PuzzleManager : PuzzleManager
    {
        public List<HailStone> Input { get; set; }
        public Day24PuzzleManager()
        {
            var inputHelper = new Day24InputHelper(INPUT_FILE_NAME);
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
            var testLow = 200000000000000m;
            var testHigh = 400000000000000m;
            var solution = 0;

            for (var i = 0; i < Input.Count - 1; i++)
            {
                for (var j = i + 1; j < Input.Count; j++)
                {
                    var hail1 = Input[i];
                    var hail2 = Input[j];

                    if (FutureXAndYIntersectionInTestArea(hail1, hail2, testLow, testHigh))
                    {
                        solution++;
                    }
                }
            }

            Console.WriteLine($"The solution to part one is '{solution}'.");
            return Task.CompletedTask;
        }

        public override Task SolvePartTwo()
        {
            var solution = 0;
            Console.WriteLine($"The solution to part two is '{solution}'.");
            return Task.CompletedTask;
        }

        private bool FutureXAndYIntersectionInTestArea(HailStone hail1, HailStone hail2, decimal testLow, decimal testHigh)
        {
            if (IsXAndYParallel(hail1.Velocity, hail2.Velocity))
            {
                return false;
            }
            (var alpha, var beta) = GetVelocityMultiplesForIntersection(hail1, hail2);
            if (alpha < 0 || beta < 0)
            {
                return false;
            }
            (decimal x, decimal y) intersection = (hail1.Position.x + alpha * hail1.Velocity.x, hail1.Position.y + alpha * hail1.Velocity.y);

            if (intersection.x >= testLow && intersection.x <= testHigh &&
                intersection.y >= testLow && intersection.y <= testHigh)
            {
                return true;
            }
            return false;
        }

        private bool IsXAndYParallel((decimal x, decimal y, decimal z) velocity1, (decimal x, decimal y, decimal z) velocity2)
            => (velocity1.y * velocity2.x / velocity2.y) - velocity1.x == 0;

        private (decimal alpha, decimal beta) GetVelocityMultiplesForIntersection(HailStone hail1, HailStone hail2)
        {
            var alpha = (hail1.Position.x - hail2.Position.x - (hail2.Velocity.x / hail2.Velocity.y) * (hail1.Position.y - hail2.Position.y)) / ((hail1.Velocity.y * hail2.Velocity.x / hail2.Velocity.y) - hail1.Velocity.x);
            var beta = (hail1.Position.x - hail2.Position.x - (hail1.Velocity.x / hail1.Velocity.y) * (hail1.Position.y - hail2.Position.y)) / (hail2.Velocity.x - (hail1.Velocity.x * hail2.Velocity.y / hail1.Velocity.y));
            return (alpha, beta);
        }
    }
}