using System;

namespace Blackjack
{
    class Hand // represents players' or dealer's cards on hand durring the game
    {
        public List<Card> cardsOnHand; // list of Card objects on hand
        public int points;
        public bool hasBust;

        public Hand(Card card1, Card card2) // creates a initial hand with two random cards drawn from deck
        {
            cardsOnHand = new List<Card> { card1, card2 };
            hasBust = false;
            points = GetSummedValue();
        }

        public int GetPoints()
        {
            return points;
        }

        public bool HasBust()
        {
            return hasBust;
        }

        public void AddCard(Card card) // adds a card object to hand
        {
            cardsOnHand.Add(card);
            points = GetSummedValue();
            if (points > 21)
            {
                hasBust = true;
            }
        }

        public int GetSummedValue() // returns the to number of points according to cards on hand
        {
            int valueSum = 0;
            int aces = 0;
            foreach (Card card in cardsOnHand) // counts number of aces and sums the other cards
            {
                if (card.GetValue() != 0)
                {
                    valueSum = valueSum + card.GetValue();
                }
                else
                {
                    aces++;
                }
            }

            if (aces > 0) // finds the total sum with aces
            {
                for (int a = 1; a <= aces; a++)
                {
                    if (valueSum + aces - a + 11 <= 21)
                    {
                        valueSum = valueSum + 11;
                    }
                    else
                    {
                        valueSum = valueSum + 1;
                    }

                }
            }
            return valueSum;
        }
        
        public void PrintHand(string name) // prints the names of cards on hand to console
        {
            Console.WriteLine(name + "'s hand is:");
            foreach (Card card in cardsOnHand)
            {
                if (name == "the dealer")
                {
                    Console.WriteLine("  * Hidden Card");
                    name = "";
                }
                else
                {
                    Console.WriteLine("  * " + card.GetName());
                }
            }
        }

    }
}