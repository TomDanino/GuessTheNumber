using System;
using System.Collections.Generic;
using System.Linq;

namespace COBWEBS
{
    internal class CheaterPlayer : Player
    {
        private string name;
        public List<int> fullList;
        Random random;
        public CheaterPlayer(string name, GameData gameData) : base(name, gameData)
        {
            this.name = name;
            fullList = Enumerable.Range(GameData.rangeFirstNum, GameData.rangeLastNum-GameData.rangeFirstNum + 1).ToList();
            random = new Random();
        }

        public override int GuessNumber()        
        {
            List<int> availableGuesses = fullList.Except(GameData.guesses.Keys.ToList()).ToList();
            int index = random.Next(0, availableGuesses.Count);
            int res = availableGuesses[index];
            return res;
        }

    }
}