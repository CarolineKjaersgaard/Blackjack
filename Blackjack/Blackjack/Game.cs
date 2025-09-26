using System;
using System.Runtime.Serialization;

namespace Blackjack
{
    class Game
    {
        public Deck deck; // deck of cards for game
        public List<Player> players; // list of players' hands containg their cards
        public Player dealer; // dealer's hand containg their cards
        public Game() // starts game of blackjack
        {
            deck = new Deck();
            players = new List<Player> { };
            bool runGame = true;

            MakePlayers(NumberOfPlayers(), StartingBank());

            while (runGame)
            {
                PlaceBets();
                dealer = MakeDealer();
                PlayTurns();
                RemoveLosers();
                runGame = PlayAgain();
            }
        }

        public int NumberOfPlayers()
        {
            int nOfPlayers = 0;
            while (nOfPlayers < 1 || nOfPlayers > 4)
            {
                Console.WriteLine("How many players are playing? (max. 4)"); // lets user input number of players in game
                try
                {
                    nOfPlayers = Convert.ToInt32(Console.ReadLine());
                    if (nOfPlayers < 1 || nOfPlayers > 4)
                    {
                        Console.WriteLine("Number of players need to be between 1 and 4");
                    }
                }
                catch
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }
            return nOfPlayers;
        }

        public int StartingBank()
        {
            int startingBank = 0;
            while (startingBank <= 0)
            {
                Console.WriteLine("How many chips should each player have to start?");
                try
                {
                    startingBank = Convert.ToInt32(Console.ReadLine());
                    if (startingBank <= 0)
                    {
                        Console.WriteLine("Amount of chips should be bigger than 0");
                    }
                }
                catch
                {
                    Console.WriteLine("Please enter a valid number");
                }

            }
            return startingBank;
        }

        public void MakePlayers(int nOfPlayers, int startingBank)
        {
            for (int i = 1; i <= nOfPlayers; i++)
            {
                string name = "";
                while (name == "")
                {
                    try
                    {
                        Console.WriteLine("Player " + i + " enter your name");
                        name = Console.ReadLine();
                    }
                    catch
                    {
                        Console.WriteLine("Please enter a valid input as name");
                    }

                }
                Card card1 = deck.DrawCard();
                Card card2 = deck.DrawCard();
                Hand hand = new Hand(card1, card2);
                players.Add(new Player(name, startingBank, hand));
            }
        }

        public void PlaceBets()
        {
            foreach (Player player in players)
            {
                player.SetBet();
            }
        }

        public Player MakeDealer()
        {
            Card card1 = deck.DrawCard();
            Card card2 = deck.DrawCard();
            Hand hand = new Hand(card1, card2);
            return new Player("the dealer", 0, hand); // creates dealer hand
        }

        public bool PlayersTurn(Player player) // runs given player's turn and reports if bust
        {
            bool isPlayersTurn = true;
            string name = player.GetName();
            player.GetHand().PrintHand(name);
            while (isPlayersTurn && !player.GetHand().HasBust())
            {
                Console.WriteLine("Do you choose to Hit (H) or Stand (S)?");
                string choice = Console.ReadLine();
                if (choice == "h" || choice == "H" || choice == "Hit" || choice == "hit")
                {
                    Card newCard = deck.DrawCard();
                    Console.WriteLine("You drew " + newCard.GetName());
                    player.GetHand().AddCard(newCard);
                    player.GetHand().PrintHand(name);
                }
                else if (choice == "s" || choice == "S" || choice == "Stand" || choice == "stand")
                {
                    isPlayersTurn = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid input");
                }
            }
            if (player.GetHand().HasBust())
            {
                Console.WriteLine("You have Bust and have lost");
            }
            return player.GetHand().HasBust();
        }

        public bool DealersTurn() // runs dealer's turn and reports if bust
        {
            int points = dealer.GetHand().GetPoints();
            Console.WriteLine("It is the dealer's turn");
            dealer.GetHand().PrintHand("the dealer");
            while (points < 17)
            {
                Console.WriteLine("The dealer Hits");
                Card newCard = deck.DrawCard();
                dealer.GetHand().AddCard(newCard);
                Console.WriteLine("The dealer now has");
                dealer.GetHand().PrintHand("the dealer");
                points = dealer.GetHand().GetPoints();
            }
            if (dealer.GetHand().HasBust())
            {
                Console.WriteLine("The dealer Bust");
            }
            else
            {
                Console.WriteLine("The dealer Stands");
            }
            return dealer.GetHand().HasBust();
        }

        public void PlayTurns() // lets players play their turns and afterward the dealer
        {
            bool somePlayersHasNotBust = false;
            bool dealerHasBust = false;
            foreach (Player player in players) // lets each player play their turn in game
            {
                Console.WriteLine(player.GetName() + " it is your turn");
                bool hasBust = PlayersTurn(player);
                if (!hasBust)
                {
                    somePlayersHasNotBust = true;
                }
            }

            if (somePlayersHasNotBust) // plays dealer's turn if not all players already has lost
            {
                dealerHasBust = DealersTurn();
            }
            FindWinner(somePlayersHasNotBust, dealerHasBust);
        }

        public void FindWinner(bool somePlayersHasNotBust, bool dealerHasBust) // writes winners of game to console and sets bet piles or writes push to console if no winners exist
        {
            int dealerPoints = dealer.GetHand().GetPoints();
            if (somePlayersHasNotBust)
            {
                int topSum = 0;
                foreach (Player player in players)
                {
                    int points = player.GetHand().GetPoints();
                    foreach (Player comparingPlayer in players)
                    {
                        int comparingPoints = comparingPlayer.GetHand().GetPoints();
                        if (points < comparingPoints)
                        {
                            player.SetTopPlayer(false);
                        }
                    }
                    if (player.IsTopPlayer())
                    {
                        topSum = points;
                    }
                }
                if (dealerPoints < topSum)
                {
                    foreach (Player player in players)
                    {
                        if (player.IsTopPlayer())
                        {
                            Console.WriteLine(player.GetName() + " won the round");
                            player.HasWon(true);
                        }
                        else
                        {
                            Console.WriteLine(player.GetName() + " lost the round");
                            player.HasWon(false);
                        }

                    }
                }
                else if (dealerPoints == topSum)
                {
                    Console.WriteLine("Push. No winners");
                    foreach (Player player in players)
                    {
                        player.ReturnBetToBank();
                    }
                }
            }
            else if (!dealerHasBust)
            {
                Console.WriteLine("The dealer won");
                foreach (Player player in players)
                {
                    Console.WriteLine(player.GetName() + " lost the round");
                    player.HasWon(false);
                }
            }
            else
            {
                Console.WriteLine("Push. No winners");
                foreach (Player player in players)
                {
                    player.ReturnBetToBank();
                }
            }
        }

        public void RemoveLosers()
        {
            foreach (Player player in players)
            {
                if (player.GetChipsInBank() <= 0)
                {
                    Console.WriteLine(player + " has no more chips left in bank");
                    players.Remove(player);
                }
                else
                {
                    Console.WriteLine(player + " has " + player.GetChipsInBank() + " chips left in bank");
                }
            }
        }

        public void ShuffleDeck()
        {
            deck = new Deck();
        }

        public bool PlayAgain()
        {
            bool runGame = true;
            if (players.Count != 0)
            {
                Console.WriteLine("Do you want to play a new round? (Y) yes or (N) no");
                string choice = Console.ReadLine();
                if (choice == "n" || choice == "N" || choice == "No" || choice == "no")
                {
                    runGame = false;
                }
                if (runGame)
                {
                    ShuffleDeck();
                }
            }
            else
            {
                runGame = false;
            }
            return runGame;
        }
    }
}