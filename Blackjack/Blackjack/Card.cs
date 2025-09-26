using System;

namespace Blackjack {
    class Card // represents a standard playing card
    {
        public string name; // name of color and type of card
        public int value; // the initial point value according to type
        public Card(string cardName, int cardValue) // creates a Card object with a name and value
        {
            name = cardName;
            value = cardValue;
        }

        public string GetName() // returns printable card name
        {
            return name;
        }

        public int GetValue() // returns point value of card
        {
            return value;
        }

    }
}