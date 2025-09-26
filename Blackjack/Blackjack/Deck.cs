using System;

namespace Blackjack {
    class Deck // represents a standard deck of 52 cards
    {
        public List<Card> deck; // list of Card object objects in deck

        public Deck() // creates a full deck of 52 Card objects
        {
            deck = new List<Card>
        {
            new Card("Ace of Hearts", 0), new Card("Ace of Spades", 0), new Card("Ace of Clubs", 0), new Card("Ace of Diamonds", 0),
            new Card("2 of Hearts", 2), new Card("2 of Spades", 2), new Card("2 of Clubs", 2), new Card("2 of Diamonds", 2),
            new Card("3 of Hearts", 3), new Card("3 of Spades", 3), new Card("3 of Clubs", 3), new Card("3 of Diamonds", 3),
            new Card("4 of Hearts", 4), new Card("4 of Spades", 4), new Card("4 of Clubs", 4), new Card("4 of Diamonds", 4),
            new Card("5 of Hearts", 5), new Card("5 of Spades", 5), new Card("5 of Clubs", 5), new Card("5 of Diamonds", 5),
            new Card("6 of Hearts", 6), new Card("6 of Spades", 6), new Card("6 of Clubs", 6), new Card("6 of Diamonds", 6),
            new Card("7 of Hearts", 7), new Card("7 of Spades", 7), new Card("7 of Clubs", 7), new Card("7 of Diamonds", 7),
            new Card("8 of Hearts", 8), new Card("8 of Spades", 8), new Card("8 of Clubs", 8), new Card("8 of Diamonds", 8),
            new Card("9 of Hearts", 9), new Card("9 of Spades", 9), new Card("9 of Clubs", 9), new Card("9 of Diamonds", 9),
            new Card("10 of Hearts", 10), new Card("10 of Spades", 10), new Card("10 of Clubs", 10), new Card("10 of Diamonds", 10),
            new Card("Jack of Hearts", 10), new Card("Jack of Spades", 10), new Card("Jack of Clubs", 10), new Card("Jack of Diamonds", 10),
            new Card("Queen of Hearts", 10), new Card("Queen of Spades", 10), new Card("Queen of Clubs", 10), new Card("Queen of Diamonds", 10),
            new Card("King of Hearts", 10), new Card("King of Spades", 10), new Card("King of Clubs", 10), new Card("King of Diamonds", 10),
        };
        }

        public Card DrawCard() // returns a random Card from remaing objects in Deck and removes it from Deck
        {
            Random random = new Random();
            int upperBound = deck.Count - 1;
            int randomInt = random.Next(0, upperBound);
            Card card = deck[randomInt];
            deck.RemoveAt(randomInt);
            return card;
        }


    }
}