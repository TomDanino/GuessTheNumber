namespace COBWEBS
{
    abstract public class Player : IPlayer
    {
        public string Name { get; }
        public GameData GameData { get; }

        public Player(string name, GameData gameData)
        {
            Name = name;
            GameData = gameData;
        }

        public abstract int GuessNumber();
    }
}
