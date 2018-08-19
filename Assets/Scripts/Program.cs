using System;

namespace CheckersLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckersLogic.GameManager game = CheckersLogic.GameManager.getInstance();
            //game.move(Move.fromString(""))
            Console.WriteLine(game.ToString());
        }
    }
}