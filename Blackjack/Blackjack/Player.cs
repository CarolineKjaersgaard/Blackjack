using System;

namespace Blackjack
{
    class Player
    {
        public string name; // name of player
        public int chipsInBank; // amount of chips player has in bank
        public int bet; // amount player has betted on current game
        public Hand cardsOnHand; // player's hand of cards
        public bool isTopPlayer; // is true if player has more or equal points compared to others

        public Player(string nameOfPlayer, int chips, Hand hand) // creates a player with name and initial chips and hand
        {
            name = nameOfPlayer;
            chipsInBank = chips;
            cardsOnHand = hand;
            bet = 0;
            isTopPlayer = true;
        }

        public int GetChipsInBank() // returns current amount of chips for player
        {
            return chipsInBank;
        }

        public string GetName() // returns player name
        {
            return name;
        }

        public Hand GetHand() // returns current hand of player
        {
            return cardsOnHand;
        }

        public bool IsTopPlayer() // returns if player has most points
        {
            return isTopPlayer;
        }

        public void SetHand(Hand hand) // repleces the current hand of player
        {
            cardsOnHand = hand;
        }

        public void SetTopPlayer(bool isTop) // sets players position
        {
            isTopPlayer = isTop;
        }


        public void SetBet() // allows player to place a new bet
        {
            Console.WriteLine(name + " you have " + chipsInBank + " left");
            while (1 > bet || bet > chipsInBank)
            {
                Console.WriteLine("Place your bet");
                try
                {
                    bet = Convert.ToInt32(Console.ReadLine());
                    if (1 > bet || bet > chipsInBank)
                    {
                        Console.WriteLine("Your bet can not be less than 0 or more than you have");
                    }
                }
                catch
                {
                    Console.WriteLine("You need to enter a number");
                }

            }
        }

        public void HasWon(bool hasWon) // sets amount of chips to be returned to player from bet pile
        {
            if (hasWon)
            {
                bet = bet * 2;
            }
            else
            {
                bet = 0;
            }
            ReturnBetToBank();
        }

        public void ReturnBetToBank() // returns bet pile to player's bank of chips
        {
            chipsInBank = chipsInBank + bet;
            bet = 0;
        }

    }
}