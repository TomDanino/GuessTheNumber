using System;
using System.Collections.Generic;

namespace COBWEBS
{
    public class PlayerFactory
    {
        private IReadOnlyDictionary<PlayerType, Func<string, GameData, IPlayer>> playersCreators;

        public PlayerFactory()
        {
            playersCreators = new Dictionary<PlayerType, Func<string, GameData, IPlayer>>
            {
                [PlayerType.RandomPlayer] = (name, gameData) => new RandomPlayer(name, gameData),
                [PlayerType.MemoryPlayer] = (name, gameData) => new MemoryPlayer(name, gameData),
                [PlayerType.Cheaterplayer] = (name, gameData) => new CheaterPlayer(name, gameData),
                [PlayerType.ThoroughPlayer] = (name, gameData) => new ThoroughPlayer(name, gameData),
                [PlayerType.ThoroughCheaterPlayer] = (name, gameData) => new ThoroughCheaterPlayer(name, gameData)
            };
        }

        public IPlayer CreatePlayer(string name, PlayerType playerType, GameData gameData)
        {
            return playersCreators[playerType](name, gameData);
        }
    }
}
