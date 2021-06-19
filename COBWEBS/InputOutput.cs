using System;

namespace COBWEBS
{
    public class InputOutput
    {

        public static void WelcomeMessage(string gameName)
        {
            Console.WriteLine($"Welcome to {gameName} game!");
        }

        public static int GetNumberOfPlayers(int minNumOfPlayers, int maxNumOfPlayers)
        {
            int numOfPlayers;           
            Console.WriteLine($"How many players are going to play today({minNumOfPlayers}-{maxNumOfPlayers})?");
            string numOfPlayersAsString = Console.ReadLine();
            while (!int.TryParse(numOfPlayersAsString, out numOfPlayers) || numOfPlayers < minNumOfPlayers || numOfPlayers > maxNumOfPlayers)
            {
                Console.WriteLine($"Invalid input. Please enter a number between {minNumOfPlayers} and {maxNumOfPlayers}.");
                numOfPlayersAsString = Console.ReadLine();
            }

            return numOfPlayers;
        }

        public static string GetPlayersName(int playerNumber)
        {
            Console.WriteLine($"Please enter a name for player number {playerNumber}");
            string playerName = Console.ReadLine();
            return playerName;
        }

        public static void PlayerNameExist(string playerName)
        {
            Console.WriteLine($"A player with the name '{playerName}' already exist. please choose a different name");
        }

        public static int GetPlayersType(int playerNumber)
        {
            int playerType;
            
            Console.WriteLine("Please choose the type of that player:\n1- Random Player\n2- Memory Player\n3- Thorough Player\n4- Cheater player\n5- Thorough Cheater Player");
            string playerTypeAsString = Console.ReadLine();
            while (!int.TryParse(playerTypeAsString, out playerType) || playerType < 1 || playerType > 5)
            {
                Console.WriteLine("Invalid input. Please enter a valid number between 1 and 5.");
                playerTypeAsString = Console.ReadLine();
            }

            return playerType;
        }

        public static void PrintBasketWeight(int basketWeight)
        {
            Console.WriteLine($"Basket weight was: {basketWeight}");
        }

        public static void PrintWinner(string playerName, int totalAttempts)
        {
            Console.WriteLine($"'{playerName}' guessed the number correctly and he is the winner!");
            Console.WriteLine($"Total guesses in the game was: {totalAttempts}");
        }

        public static void PrintClosestWinner(string playerName, int guess)
        {            
            Console.WriteLine($"'{playerName}' guessed the number '{guess}' which is the closest one and he is the winner!");
        }

        public static void GameEndTriggerTotalGuesses(int totalGuesses)
        {
            Console.WriteLine($"'Game ended after reaching {totalGuesses} total guesses.");
        }

        public static void GameEndTriggerMilliseconds(long milliseconds)
        {
            Console.WriteLine($"'Game ended after {milliseconds} milliseconds passed.");
        }
    }
}
