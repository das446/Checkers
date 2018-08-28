using System;

namespace CheckersLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            Checkers.GameManager game = Checkers.GameManager.getInstance();
            //game.move(Move.fromString(""))
            Console.WriteLine(game.ToString());
        }
    }
}