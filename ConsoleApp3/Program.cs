using System;
using System.Collections.Generic;
using System.Threading;


namespace BlackJack
{
    public enum Suit
    {
        Heart,
        Diamond,
        Spade,
        Club
    }

    public enum Face //By using enum this means that the values can not be changed and can only be read
    {
        Ace,//1 to 11
        Two,//2
        Three,//3
        Four,//4
        Five,//5
        Six,//6
        Seven,//7
        Eight,//8
        Nine,//9
        Ten,//10
        Jack,//10
        Queen,//10
        King,//10
    }

    public class Card
    {
        public Suit Suit { get; set; }
        public Face Face { get; set; }
        public int Value { get; set; }
    }

    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            this.Initialize();
        }

        public void Initialize()
        {
            cards = new List<Card>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    cards.Add(new Card() { Suit = (Suit)i, Face = (Face)j });

                    if (j <= 8)
                        cards[cards.Count - 1].Value = j + 1;
                    else
                        cards[cards.Count - 1].Value = 10;
                }
            }
        }

        public void Shuffle()
        {
            Random rng = new Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card card = cards[k];
                cards[k] = cards[n];
                cards[n] = card;
            }
        }

        public Card DrawACard()
        {
            if (cards.Count <= 0)
            {
                this.Initialize();
                this.Shuffle();
            }

            Card cardToReturn = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            return cardToReturn;
        }

        public int GetAmountOfRemainingCrads()
        {
            return cards.Count;
        }


        public static void FAQ()
        {
            Console.WriteLine(@"
            1. Blackjack is a card game where the player is given two cards.
               The aim of the game is to get as close to 21 as possible without going over
                
               The player can stick or hit
               Stick means to finsih the game with the cards they got
               Hit means the player gets another card.
                
               
");

        }




        class Program
        {
            //variables and lists       
            static Deck deck;
            static List<Card> userHand;
            static List<Card> dealerHand;

            static void Main(string[] args)
            {
                //Title and background colour
                Console.BackgroundColor = ConsoleColor.White;
                //Sets text colour to red
                Console.ForegroundColor = ConsoleColor.Black;
                //Clears console so that all colours are set properly and not just to the line of text 
                Console.Clear();
                Console.Title = "BlackJack";

                //makes a deck and shuffles
                deck = new Deck();
                deck.Shuffle();

                Console.WriteLine("BlackJack");

                Console.WriteLine("Do you need help Y/N");
                ConsoleKeyInfo nHelp = Console.ReadKey(true);
                while (nHelp.Key != ConsoleKey.Y && nHelp.Key != ConsoleKey.N)
                {
                    Console.WriteLine("Invalid input please choose an option Y or N: ");
                    nHelp = Console.ReadKey(true);
                }


                Console.Clear();
                //This determins whats happens if the key is Y or N
                switch (nHelp.Key)
                {
                    // if key is y then it shall run this line of code but if it's N  
                    case ConsoleKey.Y:
                        Console.WriteLine("Here's the rules and FAQ .");
                        Thread.Sleep(2000);
                        Console.Clear();
                        FAQ();
                        Console.WriteLine(@"     Please press a key to continue.");
                        Console.ReadKey(true);

                        break;
                    case ConsoleKey.N:



                        break;
                }



                // should have used a do loop but already done as while but runs through progam until they press n 
                do
                {
                    //Clears up console from last game  
                    Console.Clear();

                    //Outputs name of the game and ask if you would like to play

                    Console.WriteLine("BlackJack");

                    Console.WriteLine("Do you want to play blackjack Y/N");

                    //
                    ConsoleKeyInfo play = Console.ReadKey(true);
                    while (play.Key != ConsoleKey.Y && play.Key != ConsoleKey.N)
                    {
                        Console.WriteLine("Invalid input please choose an option Y or N: ");
                        play = Console.ReadKey(true);
                    }


                    Console.Clear();
                    //This determins whats happens if the key is Y or N
                    switch (play.Key)
                    {
                        // if key is y then it shall run this line of code but if it's N 
                        case ConsoleKey.Y:
                            DealHand();
                            Console.WriteLine("Please press a key to continue.");
                            Console.ReadKey(true);
                            //validation = true;
                            continue;

                        case ConsoleKey.N:
                            Console.WriteLine("Press any key to exit.");
                            Console.ReadKey(true);
                            Console.WriteLine("Thanks for playing.");

                            Thread.Sleep(3000);
                            return;
                    }

                } while (true);

            }

            static void DealHand()
            {
                if (deck.GetAmountOfRemainingCrads() < 35)
                {
                    deck.Initialize();
                    deck.Shuffle();
                }


                userHand = new List<Card>
            {
                deck.DrawACard(),
                deck.DrawACard()
            };

                foreach (Card card in userHand)
                {
                    if (card.Face == Face.Ace)
                    {
                        card.Value += 10;
                        break;
                    }
                }

                Console.WriteLine("Remaining Cards: {0}", deck.GetAmountOfRemainingCrads());


                dealerHand = new List<Card>
            {
                deck.DrawACard(),
                deck.DrawACard()
            };

                foreach (Card card in dealerHand)
                {
                    if (card.Face == Face.Ace)
                    {
                        card.Value += 10;
                        break;
                    }
                }

                //Outputs dealers hand
                Console.WriteLine("[Dealer]");
                Console.WriteLine("Card 1: {0} of {1}", dealerHand[0].Face, dealerHand[0].Suit);
                Console.WriteLine("Card 2: [Hidden]");
                Console.WriteLine("Total: {0}\n", dealerHand[0].Value);

                //Outputs users hand
                Console.WriteLine("[You]");
                Console.WriteLine("Card 1: {0} of {1}", userHand[0].Face, userHand[0].Suit);
                Console.WriteLine("Card 2: {0} of {1}", userHand[1].Face, userHand[1].Suit);
                Console.WriteLine("Total: {0}\n", userHand[0].Value + userHand[1].Value);

                if (userHand[0].Value + userHand[1].Value == 21)

                {
                    if (dealerHand[0].Value + dealerHand[1].Value == 21)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("The dealer and you both got a blackjack, its a draw!");
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Blackjack, You Won!");
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    return;
                }

                do
                {
                    Console.WriteLine("Would you like to (S)tick or (H)it?");
                    ConsoleKeyInfo userOption = Console.ReadKey(true);
                    while (userOption.Key != ConsoleKey.H && userOption.Key != ConsoleKey.S)
                    {
                        Console.WriteLine("Please choose an option s or h: ");
                        userOption = Console.ReadKey(true);
                    }
                    Thread.Sleep(200);
                    Console.Clear();
                    switch (userOption.Key)
                    {
                        case ConsoleKey.H:
                            userHand.Add(deck.DrawACard());
                            Console.WriteLine("Hitted {0} of {1}", userHand[userHand.Count - 1].Face, userHand[userHand.Count - 1].Suit);
                            int totalCardsValue = 0;

                            foreach (Card card in userHand)
                            {
                                totalCardsValue += card.Value;
                            }

                            Console.WriteLine("Total cards value now: {0}\n", totalCardsValue);

                            if (totalCardsValue > 21)
                            {

                                Console.WriteLine("Bust!");
                                Console.WriteLine("You lost better luck next time.");

                                return;
                            }

                            else
                            {
                                continue;
                            }


                        case ConsoleKey.S:
                            Console.WriteLine("[Dealer]");
                            Console.WriteLine("Card 1: {0} of {1}", dealerHand[0].Face, dealerHand[0].Suit);
                            Console.WriteLine("Card 2: {0} of {1}", dealerHand[1].Face, dealerHand[1].Suit);

                            int dealerCardsValue = 0;
                            foreach (Card card in dealerHand)
                            {
                                dealerCardsValue += card.Value;
                            }
                            int playerCardValue = 0;
                            foreach (Card card in userHand)
                            {
                                playerCardValue += card.Value;
                            }


                            while (dealerCardsValue <= playerCardValue)
                            {
                                dealerHand.Add(deck.DrawACard());
                                dealerCardsValue = 0;

                                foreach (Card card in dealerHand)
                                {
                                    dealerCardsValue += card.Value;
                                }

                                Console.WriteLine("Card {0}: {1} of {2}", dealerHand.Count, dealerHand[dealerHand.Count - 1].Face, dealerHand[dealerHand.Count - 1].Suit);
                            }

                            dealerCardsValue = 0;
                            foreach (Card card in dealerHand)
                            {
                                dealerCardsValue += card.Value;
                            }

                            Console.WriteLine("Total: {0}\n", dealerCardsValue);

                            if (dealerCardsValue > 21)
                            {

                                Console.WriteLine("Dealer bust! You win! ");

                                return;
                            }

                            else
                            {


                                if (dealerCardsValue > playerCardValue)
                                {

                                    Console.WriteLine("The dealers value {0} and your value is {1}, dealer wins!", dealerCardsValue, playerCardValue);
                                    Console.WriteLine("Better luck next time!!!");

                                    return;
                                }

                                else if (dealerCardsValue == playerCardValue)
                                {

                                    Console.WriteLine("The dealers value is {0} and your value is {1}, its a draw!", dealerCardsValue, playerCardValue);

                                    return;
                                }

                                else
                                {

                                    Console.WriteLine("Your value is {0} and dealers value is {1}, you win!", playerCardValue, dealerCardsValue);

                                    return;
                                }
                            }
                    }
                    Console.ReadLine();
                }
                while (true);
            }

        }
    }
}
