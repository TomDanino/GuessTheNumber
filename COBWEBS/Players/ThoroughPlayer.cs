namespace COBWEBS
{
    public class ThoroughPlayer : Player
    {
        private int guess;

        public ThoroughPlayer(string name, GameData gameData) : base(name, gameData)
        {
            guess = gameData.rangeFirstNum;
        }

        public override int GuessNumber()
        {
            return(guess++);           
        }
    }
}