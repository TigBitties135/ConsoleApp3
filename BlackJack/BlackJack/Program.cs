using System;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string choice;
            Console.WriteLine("===========================================");
            Console.WriteLine("$                BLACKJACK                $");
            Console.WriteLine("===========================================");

            Console.Write("Would you like to play? (y/n) ");

            do
            {
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "y":
                        Player player = new Player(1000, 0);
                        Player dealer = new Player(1000, 0);

                        while (choice != "n")
                        {
                            Game.Play(player, dealer);

                            if (MoneyCheckWinner(player, dealer) == true)
                            {
                                break;
                            }
                            else
                            {
                                Console.Write("\nWould you like to go for another round? (y/n) ");
                                choice = Console.ReadLine();
                                if (choice == "n")
                                {
                                    break;
                                }
                                else if (choice != "y")
                                {
                                    while (choice != "y" || choice != "n")
                                    {
                                        Console.Write("Enter a valid option! (y/n) ");
                                        choice = Console.ReadLine();
                                        if (choice == "y" || choice == "n")
                                        {
                                            break;
                                        }
                                    }
                                    continue;
                                }
                            }

                        }
                        Console.WriteLine("\nYou have finished playing the game.");
                        break;
                    case "n":
                        Console.WriteLine("\nYou have opted to not play the game.\n");
                        break;
                    default:
                        Console.Write("Enter a valid option! (y/n) ");
                        continue;
                }
                break;
            } while (choice != "n");
            Console.WriteLine("Goodbye! See you again.");
        }

        static bool MoneyCheckWinner(Player player, Player dealer)
        {
            if (player.money <= 0 || dealer.money <= 0)
            {
                Console.WriteLine("One of the players has no money left to play. The game is over.");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}