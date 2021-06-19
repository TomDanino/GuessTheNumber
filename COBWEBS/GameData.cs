using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COBWEBS
{
    public class GameData
    {
        public readonly ConcurrentDictionary<int, Guess> guesses;
        public int totalAttempts;
        public readonly int rangeFirstNum;
        public readonly int rangeLastNum;

        public GameData(int concurrencyLevel, int rangeFirstNum, int rangeLastNum)
        {
            totalAttempts = 0;
            guesses = new ConcurrentDictionary<int, Guess>(concurrencyLevel, rangeLastNum - rangeFirstNum);
            this.rangeFirstNum = rangeFirstNum;
            this.rangeLastNum = rangeLastNum;
        }

    }
}
