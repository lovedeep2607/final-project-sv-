using ConnectFourGame;
using System;

namespace ConnectFourGame
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Connect Four!");
                Console.WriteLine("Choose a mode:");
                Console.WriteLine("1. 2-Player Mode");
                Console.WriteLine("2. 1-Player Mode");
                Console.WriteLine("3. Exit");

                char choice = GetMenuChoice();

                switch (choice)
                {
                    case '1':
                        IGameMode twoPlayerMode = new TwoPlayerMode();
                        twoPlayerMode.PlayGame();
                        break;
                    case '2':
                        IGameMode onePlayerMode = new OnePlayerMode();
                        onePlayerMode.PlayGame();
                        break;
                    case '3':
                        return;
                }
            }
        }

        static char GetMenuChoice()
        {
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine();
            return choice;
        }
    }
}