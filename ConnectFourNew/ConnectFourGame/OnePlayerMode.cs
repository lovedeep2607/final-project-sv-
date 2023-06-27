using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectFourGame;

namespace ConnectFourGame
{

    class OnePlayerMode : GameMode
    {
        private readonly HumanPlayer humanPlayer;
        private readonly ComputerPlayer computerPlayer;

        public OnePlayerMode()
        {
            humanPlayer = new HumanPlayer('X');
            computerPlayer = new ComputerPlayer('O', board);
        }

        public override void PlayGame()
        {
            InitializeBoard();
            currentPlayer = humanPlayer.Symbol;
            isGameOver = false;

            while (!isGameOver)
            {
                Console.Clear();
                PrintBoard();

                if (currentPlayer == humanPlayer.Symbol)
                {
                    Console.WriteLine("It is your turn:");
                    int column = humanPlayer.GetMove();

                    if (IsValidMove(column))
                    {
                        MakeMove(column);

                        if (CheckForWin())
                        {
                            Console.Clear();
                            PrintBoard();
                            Console.WriteLine("You win!");
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
                else
                {
                    Console.WriteLine("Computer is thinking...");
                    System.Threading.Thread.Sleep(1000); // Pause to simulate computer thinking

                    int column = computerPlayer.GetMove();

                    MakeMove(column);

                    if (CheckForWin())
                    {
                        Console.Clear();
                        PrintBoard();
                        Console.WriteLine("Computer wins!");
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

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("Press any key to return to the start screen...");
            Console.ReadKey();
        }
    }
}