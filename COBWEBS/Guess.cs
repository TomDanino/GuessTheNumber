using System;

namespace COBWEBS
{
    public class Guess
    {
        public string PlayerName { get; }
        public int Number { get; }
        public DateTime Date { get; set; }

        public Guess(string playerName, int number, DateTime date)
        {
            PlayerName = playerName;
            Number = number;
            Date = date;
        }
    }
}