namespace AOC_2023.Day7
{
    public class Hand: IComparable<Hand>
    {
        public string Cards { get; set; }
        public long Bid { get; set; }

        public Hand(string cards, long bid)
        {
            Cards = cards;
            Bid = bid;
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

            var cardOrder = "23456789TJQKA";

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
            if (Cards.Distinct().Count() == 1) // 5 of a kind
            {
                return 6;
            }
            if (Cards.Any(x => Cards.Count(y => y == x) == 4)) // 4 of a kind
            {
                return 5;
            }
            if (Cards.Any(x => Cards.Count(y => y == x) == 3) && Cards.Distinct().Count() == 2) // Full house
            {
                return 4;
            }
            if (Cards.Any(x => Cards.Count(y => y == x) == 3)) // 3 of a kind
            {
                return 3;
            }
            if (Cards.Count(x => Cards.Count(y => y == x) == 2) == 4) // 2 pair
            {
                return 2;
            }
            if (Cards.Any(x => Cards.Count(y => y == x) == 2)) // Pair
            {
                return 1;
            }
            return 0;
        }
    }
}
