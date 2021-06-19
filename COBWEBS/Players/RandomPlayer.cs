using System;

namespace COBWEBS
{
    public class RandomPlayer : Player
    {
        private readonly Random random;
        public RandomPlayer(string name, GameData gameData) : base(name, gameData)
        {
            random = new Random();
        }
        public override int GuessNumber()
        {
            return random.Next(GameData.rangeFirstNum, GameData.rangeLastNum + 1);
        }
    }
}
