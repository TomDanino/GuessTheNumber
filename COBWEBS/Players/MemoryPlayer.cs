using System;
using System.Linq;

namespace COBWEBS
{
    public class MemoryPlayer : Player
    {
        private readonly Random random;
        private readonly int[] availableGuesses;
        private readonly int diff;
        private int attempts;
        public MemoryPlayer(string name, GameData gameData) : base(name, gameData)
        {
            random = new Random();
            diff = GameData.rangeLastNum - GameData.rangeFirstNum;
            availableGuesses = Enumerable.Range(GameData.rangeFirstNum, diff+1).ToArray(); ;
            attempts = 0;
        }

        public override int GuessNumber()
        {
            //Always replace the guessed number with the number at the last available index, and next time choose random index up to him(excluded).
            int index = random.Next(0, diff - attempts);
            int res = availableGuesses[index];
            availableGuesses[index] = availableGuesses[diff - 1 - attempts];
            attempts++;
            return res;
        }
    }
}
