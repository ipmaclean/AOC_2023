namespace AOC_2023.Day7
{
    public class Hand: IComparable<Hand>
    {
        public string Cards { get; set; }
        public long Bid { get; set; }
        private int PuzzlePart { get; set; }

        public Hand(string cards, long bid, int puzzlePart)
        {
            Cards = cards;
            Bid = bid;
            PuzzlePart = puzzlePart;
        }

        public int CompareTo(Hand? otherHand)
        {
            if (this is null && otherHand is not null)
            {
                return -1;
            }
            if (this is not null && otherHand is null)
            {
                return 1;
            }
            if (this is null && otherHand is null)
            {
                return 0;
            }

            var xRank = GetCardsRank();
            var otherHandRank = otherHand!.GetCardsRank();

            if (xRank < otherHandRank)
            {
                return -1;
            }
            if (xRank > otherHandRank)
            {
                return 1;
            }

            var cardOrder = PuzzlePart == 1 ? "23456789TJQKA" : "J23456789TQKA";

            for (var i = 0; i < Cards.Length; i++)
            {
                if (cardOrder.IndexOf(Cards[i]) < cardOrder.IndexOf(otherHand.Cards[i]))
                {
                    return -1;
                }
                if (cardOrder.IndexOf(Cards[i]) > cardOrder.IndexOf(otherHand.Cards[i]))
                {
                    return 1;
                }
            }
            return 0;
        }

        public int GetCardsRank()
        {
            var cardsNoJokers = PuzzlePart == 1 ? Cards : Cards.Replace("J", string.Empty);
            var jokerCount = 5 - cardsNoJokers.Length;
            if (cardsNoJokers.Any(x => cardsNoJokers.Count(y => y == x) == 5)) // 5 of a kind
            {
                return 6;
            }
            if (cardsNoJokers.Any(x => cardsNoJokers.Count(y => y == x) == 4)) // 4 of a kind
            {
                if (jokerCount == 0)
                {
                    return 5; // 4 of a kind
                }
                if (jokerCount == 1)
                {
                    return 6; // 5 of a kind
                }
                throw new Exception("Unhandled card rank.");
            }
            if (cardsNoJokers.Any(x => cardsNoJokers.Count(y => y == x) == 3) && cardsNoJokers.Distinct().Count() == 2 && jokerCount == 0) // Full house
            {
                return 4;
            }
            if (cardsNoJokers.Any(x => cardsNoJokers.Count(y => y == x) == 3))
            {
                if (jokerCount == 0)
                {
                    return 3; // 3 of a kind
                }
                if (jokerCount == 1)
                {
                    return 5; // 4 of a kind
                }
                if (jokerCount == 2)
                {
                    return 6; // 5 of a kind
                }
                throw new Exception("Unhandled card rank.");
            }
            if (cardsNoJokers.Count(x => cardsNoJokers.Count(y => y == x) == 2) == 4)
            {
                if (jokerCount == 0)
                {
                    return 2; // 2 pair
                }
                if (jokerCount == 1)
                {
                    return 4; // Full house
                }
                throw new Exception("Unhandled card rank.");
            }
            if (cardsNoJokers.Any(x => cardsNoJokers.Count(y => y == x) == 2)) // Pair
            {
                if (jokerCount == 0)
                {
                    return 1; // Pair
                }
                if (jokerCount == 1)
                {
                    return 3; // 3 of a kind
                }
                if (jokerCount == 2)
                {
                    return 5; // 4 of a kind
                }
                if (jokerCount == 3)
                {
                    return 6; // 5 of a kind
                }
                throw new Exception("Unhandled card rank.");
            }
            if (jokerCount == 0)
            {
                return 0; // High card
            }
            if (jokerCount == 1)
            {
                return 1; // Pair
            }
            if (jokerCount == 2)
            {
                return 3; // 3 of a kind
            }
            if (jokerCount == 3)
            {
                return 5; // 4 of a kind
            }
            if (jokerCount == 4 || jokerCount == 5)
            {
                return 6; // 5 of a kind
            }
            throw new Exception("Unhandled card rank.");
        }
    }
}
