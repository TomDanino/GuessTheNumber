using System;

namespace COBWEBS
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Game game = new Game();
                game.InitGame();
                game.StartGame();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error occured: {ex.Message}");
            }
            Console.Read();

        }
    }
}
