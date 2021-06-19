using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace COBWEBS
{
    public class Game
    {
        private int basketWeight;
        private readonly IList<IPlayer> players;
        private GameData gameData;
        private bool gameFinished;
        private readonly PlayerFactory playerFactory;
        private readonly Random random;
        private readonly Stopwatch sw;      
        private readonly object stopLock;
        private Logging log;

        public bool GameFinished
        {
            get
            {
                lock (stopLock)
                {
                    return gameFinished;
                }
            }

            set
            {
                lock (stopLock)
                {
                    gameFinished = value;
                }
            }
        }

        public Game()
        {
            sw = new Stopwatch();
            stopLock = new object();            
            playerFactory = new PlayerFactory();
            players = new List<IPlayer>();
            basketWeight = 0;
            random = new Random();
            GameFinished = false;
        }

        public void InitGame()
        {
            log = new Logging();
            int rangeFirstNum = int.Parse(ConfigurationManager.AppSettings[Consts.RANGE_FIRST_NUM]);
            int rangeLastNum = int.Parse(ConfigurationManager.AppSettings[Consts.RANGE_LAST_NUM]);
            log.DeleteFile();
            log.WriteToLog("Initialize game");
            basketWeight = random.Next(rangeFirstNum, rangeLastNum + 1);
            log.WriteToLog($"Basket weight is: '{basketWeight}'");

            InputOutput.WelcomeMessage(ConfigurationManager.AppSettings[Consts.GAME_NAME]);

            int numberOfPlayers = InputOutput.GetNumberOfPlayers(int.Parse(ConfigurationManager.AppSettings[Consts.MIN_NUM_OF_PLAYERS]), int.Parse(ConfigurationManager.AppSettings[Consts.MAX_NUM_OF_PLAYERS]));

            gameData = new GameData(numberOfPlayers, rangeFirstNum, rangeLastNum);

            for (int i = 1; i <= numberOfPlayers; i++)
            {
                string playerName = InputOutput.GetPlayersName(i);
                while (players.Any(x=>x.Name==playerName))
                {
                    InputOutput.PlayerNameExist(playerName);
                    playerName = InputOutput.GetPlayersName(i);
                }

                int playerType = InputOutput.GetPlayersType(i);
                var player = playerFactory.CreatePlayer(playerName, (PlayerType)playerType, gameData);
                players.Add(player);
            }
        }        

        public void StartGame()
        {
            var tasks = new List<Task>();
            sw.Start();
            players.AsParallel().ForAll(p =>
            {
                tasks.Add(new Task(() =>
                {
                    while (!GameFinished)
                    {
                        int result = p.GuessNumber();
                        Interlocked.Increment(ref gameData.totalAttempts);
                        log.WriteToLog($"{sw.ElapsedMilliseconds}-> Player '{p.Name}' Guessed: '{result}'");
                        gameData.guesses.TryAdd(result, new Guess(p.Name, result, DateTime.Now));
                        if (result == basketWeight || gameData.totalAttempts == 100 || sw.ElapsedMilliseconds > 1500)
                        {
                            GameFinished = true;
                            sw.Stop();
                            return;
                        }
                        log.WriteToLog($"{sw.ElapsedMilliseconds}->Player '{p.Name}' guessed wrong and will wait for : '{Math.Abs(result - basketWeight)}' milliseconds");
                        Thread.Sleep(Math.Abs(result - basketWeight));
                    }
                }));
            });

            foreach (var task in tasks)
            {
                task.Start();
            }

            Task.WaitAny(tasks.ToArray());

            InputOutput.PrintBasketWeight(basketWeight);

            Guess winnerGuess = null;
            winnerGuess = GetWinner(winnerGuess);

            if (gameData.guesses.ContainsKey(basketWeight))
            {
                InputOutput.PrintWinner(winnerGuess.PlayerName, gameData.totalAttempts);
            }
            else
            {
                if (gameData.totalAttempts >= 100)
                {
                    InputOutput.GameEndTriggerTotalGuesses(gameData.totalAttempts);
                }
                else
                {
                    InputOutput.GameEndTriggerMilliseconds(sw.ElapsedMilliseconds);
                }

                InputOutput.PrintClosestWinner(winnerGuess.PlayerName, winnerGuess.Number);
            }

            log.CreateLogFile();
            Console.WriteLine("Thank you for playing! Press any key to quit");
        }

        private Guess GetWinner(Guess winnerGuess)
        {
            if (gameData.guesses.ContainsKey(basketWeight))
            {
                winnerGuess = gameData.guesses[basketWeight];
            }
            else
            {
                int i = 1;
                while (winnerGuess == null)
                {
                    Guess left = null;
                    Guess right = null;
                    if (gameData.guesses.TryGetValue(basketWeight - i, out left) && gameData.guesses.TryGetValue(basketWeight + i, out right))
                    {
                        winnerGuess = left.Date < right.Date ? left : right;
                    }
                    else if (left != null)
                        winnerGuess = left;
                    else if (right != null)
                        winnerGuess = right;

                    i++;
                }
            }

            return winnerGuess;
        }
    }
}
