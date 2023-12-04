namespace AOC_2023.Day4
{
    public class Ticket
    {
        public int CardNumber { get; set; }
        public HashSet<int> WinningNumbers { get; set; }
        public HashSet<int> OwnedNumbers { get; set; }

        public Ticket(int cardNumber, HashSet<int> winningNumbers, HashSet<int> ownedNumbers)
        {
            CardNumber = cardNumber;
            WinningNumbers = winningNumbers;
            OwnedNumbers = ownedNumbers;
        }
    }
}
