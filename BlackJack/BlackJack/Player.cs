namespace Blackjack
{
    class Player
    {
        public int money, score, bet, roundsPlayed, roundsWon;
        public Player(int playerMoney, int cardScore)
        {
            money = playerMoney;
            score = cardScore;
        }
    }
}