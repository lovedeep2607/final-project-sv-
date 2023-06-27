using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectFourGame;

namespace ConnectFourGame
{
    class TwoPlayerMode : GameMode
    {
        private readonly HumanPlayer player1;
        private readonly HumanPlayer player2;

        public TwoPlayerMode()
        {
            player1 = new HumanPlayer('X');
            player2 = new HumanPlayer('O');
        }

        public override void PlayGame()
        {
            InitializeBoard();
            currentPlayer = 'X';
            isGameOver = false;

            while (!isGameOver)
            {
                Console.Clear();
                PrintBoard();

                Console.WriteLine($"It is Player {currentPlayer}'s turn:");
                int column = currentPlayer == 'X' ? player1.GetMove() : player2.GetMove();

                if (IsValidMove(column))
                {
                    MakeMove(column);

                    if (CheckForWin())
                    {
                        Console.Clear();
                        PrintBoard();
                        Console.WriteLine($"Player {currentPlayer} wins!");
                        isGameOver = true;
                    }
                    else if (IsBoardFull())
                    {
                        Console.Clear();
                        PrintBoard();
                        Console.WriteLine("It's a draw!");
                        isGameOver = true;
                    }
                    else
                    {
                        SwitchPlayer();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid move! Press any key to continue...");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("Press any key to return to the start screen...");
            Console.ReadKey();
        }
    }
}