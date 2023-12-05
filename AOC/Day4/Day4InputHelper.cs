using System.Text.RegularExpressions;

namespace AOC_2023.Day4
{
    public class Day4InputHelper : InputHelper<List<Ticket>>
    {
        public Day4InputHelper(string fileName) : base(fileName)
        {
        }

        public override List<Ticket> Parse()
        {
            var output = new List<Ticket>();
            using (var sr = new StreamReader(InputPath))
            {
                string ln;
                var regex = new Regex(@"\d+");
                while ((ln = sr.ReadLine()!) != null)
                {
                    var splitLine = ln.Split(':')[1].Split('|');
                    
                    var winningMatches = regex.Matches(splitLine[0]);
                    var ownedMatches = regex.Matches(splitLine[1]);

                    var cardNumber = int.Parse(regex.Match(ln).Value);
                    var winningNumbers = winningMatches.Select(x => int.Parse(x.Value)).ToHashSet();
                    var ownedNumbers = ownedMatches.Select(x => int.Parse(x.Value)).ToHashSet();

                    output.Add(new Ticket(cardNumber, winningNumbers, ownedNumbers));
                }
            }
            return output;
        }
    }
}