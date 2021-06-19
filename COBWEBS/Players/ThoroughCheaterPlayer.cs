namespace COBWEBS
{
    public class ThoroughCheaterPlayer : Player
    {
        int nextNumberToGuess;
        public ThoroughCheaterPlayer(string name, GameData gameData) : base(name, gameData)
        {
            nextNumberToGuess = gameData.rangeFirstNum - 1;
        }

        public override int GuessNumber()
        {            
            do
            {
                nextNumberToGuess++;
            }
            while (GameData.guesses.ContainsKey(nextNumberToGuess));

            return nextNumberToGuess;
        }
    }
}