using System;

namespace Blackjack
{
    class Game
    {
        public static void Play(Player player, Player dealer)
        {
            string choice = "y";
            int moneyBetSum;

            player.score = 0;
            dealer.score = 0;

            Console.WriteLine("\nLet's play!\n");

            moneyBetSum = MoneyBet(player, dealer);


            Random rnd = new Random();
            dealer.score = rnd.Next(16, 21);

            while (player.score <= 21 || choice != "n")
            {
                Console.WriteLine("\nDealing your card...");
                Card pickedCard = new Card();
                CardPick(pickedCard);
                Console.WriteLine("Your card is: {0} {1} (+{2})", pickedCard.suit, pickedCard.face, CardValue(player, pickedCard));

                player.score += CardValue(player, pickedCard);
                Console.WriteLine("Your current score is: {0}", player.score);
                if (player.score > 21)
                {
                    break; 
                }
                else
                {
                    Console.WriteLine("Keep dealing? (y/n)");
                    choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "y":
                            continue;
                        case "n":
                            Console.WriteLine("Ending the game.");
                            break;
                        default:
                            while (choice != "y" || choice != "n")
                            {
                                Console.WriteLine("Enter a valid option! (y/n)");
                                choice = Console.ReadLine();
                                if (choice == "y" || choice == "n")
                                {
                                    break;
                                }
                            }
                            continue;
                    }
                   
                    if (choice == "n")
                    {
                        break;
                    }
                }
            }
            GameScore(player, dealer);
            MoneyGiver(player, dealer, moneyBetSum);

            Console.WriteLine("You have won {0} rounds out of {1}.", player.roundsWon, player.roundsPlayed);
        }

        static void CardPick(Card card)
        {

            Random rnd = new Random();
            card.faceIndex = rnd.Next(1, Enum.GetNames(typeof(Faces)).Length);
            int suitIndex = rnd.Next(1, Enum.GetNames(typeof(Suits)).Length);

            card.face = Enum.GetName(typeof(Faces), card.faceIndex);
            card.suit = Enum.GetName(typeof(Suits), suitIndex);
        }

        static int CardValue(Player player, Card card)
        {

            if (card.faceIndex >= 11 && card.faceIndex <= 13)
            {
                card.value = 10;
            }

            else if (card.faceIndex == 1)
            {
                if (player.score >= 11)
                {
                    card.value = 1;
                }
                else if (player.score < 11)
                {
                    card.value = 11;
                }
            }

            else
            {
                card.value = card.faceIndex;
            }
            return card.value;
        }

        static void GameScore(Player player, Player dealer)
        {
            Console.WriteLine("You scored {0}. The dealer scored {1}.", player.score, dealer.score);
            if (player.score <= 21 && player.score > dealer.score)
            {
                Console.WriteLine("You win!");
                player.roundsWon += 1;
            }
            else if (player.score > 21 || player.score < dealer.score)
            {
                Console.WriteLine("You lose...");
            }
            else
            {
                Console.WriteLine("It's a draw.");
            }
            player.roundsPlayed += 1;
        }

        static int MoneyBet(Player player, Player dealer)
        {

            int sum = 0;
            string input;
            Console.WriteLine("You have ${0} while the dealer has ${1}.", player.money, dealer.money);
            do
            {
                Console.WriteLine("How much would you like to bet?");

                do
                {
                    input = Console.ReadLine();
                    if (!int.TryParse(input, out player.bet))
                    {
                        Console.WriteLine("Please enter numbers!");
                    }
                } while (!int.TryParse(input, out player.bet));
                player.bet = Convert.ToInt32(input);
                if (player.bet > player.money)
                {
                    Console.WriteLine("You cannot bet more than what you have!");
                }
                else if (player.bet <= 0)
                {

                    Console.WriteLine("You cannot bet nothing at all!");
                }
                else if (player.bet > dealer.money)
                {
                    Console.WriteLine("The dealer doesn't have enough money to match your bet. Please lower your bet.");
                }
                continue;
            } while (player.bet > player.money || player.bet <= 0 || player.bet > dealer.money);
            Console.WriteLine("You have put in ${0}", player.bet);
            dealer.bet = player.bet;
            sum = player.bet + dealer.bet;
            player.money -= player.bet;
            dealer.money -= dealer.bet;
            return sum;
        }

        static void MoneyGiver(Player player, Player dealer, int betSum)
        {
            if (player.score > dealer.score && player.score <= 21)
            {
                player.money += betSum;
                Console.WriteLine("You have won ${0}.", betSum);
            }
            else if (player.score < dealer.score || player.score > 21)
            {
                dealer.money += betSum;
                Console.WriteLine("You have lost ${0}.", betSum / 2);
            }
            else if (player.score == dealer.score)
            {
                player.money += (betSum / 2);
                dealer.money += (betSum / 2);
                Console.WriteLine("Your balance remains unchanged.");
            }
            Console.WriteLine("Current amount of money: ${0}", player.money);
        }
    }
}