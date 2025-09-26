using System;

namespace Blackjack
{
    class Player
    {
        public string name;
        public int chipsInBank;
        public int bet;
        public Hand cardsOnHand;
        public bool isTopPlayer;

        public Player(string nameOfPlayer, int chips, Hand hand)
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

        public Hand GetHand()
        {
            return cardsOnHand;
        }

        public bool IsTopPlayer()
        {
            return isTopPlayer;
        }

        public void SetHand(Hand hand)
        {
            cardsOnHand = hand;
        }

        public void SetTopPlayer(bool isTop)
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