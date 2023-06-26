// See https://aka.ms/new-console-template for more information

using System;

namespace ConnectFourGame
{
    class Program
    {
        static char[,] board = new char[6, 7]; // Game board

        static char currentPlayer; // Current player ('X' or 'O')
        static bool isGameOver; // Flag to indicate if the game is over

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
                        PlayTwoPlayerMode();
                        break;
                    case '2':
                        PlayOnePlayerMode();
                        break;
                    case '3':
                        return;
                }
            }
        }

        static void PlayTwoPlayerMode()
        {
            InitializeBoard();
            currentPlayer = 'X';
            isGameOver = false;

            while (!isGameOver)
            {
                Console.Clear();
                PrintBoard();

                Console.WriteLine($"It is Player {currentPlayer}'s turn:");
                int column = GetPlayerMove();

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

        static void PlayOnePlayerMode()
        {
            InitializeBoard();
            Random random = new Random();
            currentPlayer = random.Next(0, 2) == 0 ? 'X' : 'O'; // Randomly select the starting player
            isGameOver = false;

            while (!isGameOver)
            {
                Console.Clear();
                PrintBoard();

                if (currentPlayer == 'X')
                {
                    Console.WriteLine("It is your turn:");
                    int column = GetPlayerMove();

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

                    int column = GetComputerMove();

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

        static void InitializeBoard()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }

        static void PrintBoard()
        {
            for (int row = 5; row >= 0; row--)
            {
                Console.Write("|");
                for (int col = 0; col < 7; col++)
                {
                    Console.Write(board[row, col] + "|");
                }
                Console.WriteLine();
            }
            Console.WriteLine(" 1 2 3 4 5 6 7 ");
        }

        static char GetMenuChoice()
        {
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine();
            return choice;
        }

        static int GetPlayerMove()
        {
            int column;
            while (true)
            {
                if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out column))
                {
                    if (column >= 1 && column <= 7)
                    {
                        return column - 1; // Convert to 0-based index
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Invalid input! Please enter a number between 1 and 7:");
            }
        }

        static int GetComputerMove()
        {
            Random random = new Random();
            int column;
            while (true)
            {
                column = random.Next(0, 7);
                if (IsValidMove(column))
                {
                    return column;
                }
            }
        }

        static bool IsValidMove(int column)
        {
            if (column < 0 || column >= 7)
            {
                return false;
            }

            for (int row = 0; row < 6; row++)
            {
                if (board[row, column] == ' ')
                {
                    return true;
                }
            }

            return false;
        }

        static void MakeMove(int column)
        {
            for (int row = 0; row < 6; row++)
            {
                if (board[row, column] == ' ')
                {
                    board[row, column] = currentPlayer;
                    break;
                }
            }
        }

        static bool CheckForWin()
        {
            // Check rows
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row, col] != ' ' &&
                        board[row, col] == board[row, col + 1] &&
                        board[row, col] == board[row, col + 2] &&
                        board[row, col] == board[row, col + 3])
                    {
                        return true;
                    }
                }
            }

            // Check columns
            for (int col = 0; col < 7; col++)
            {
                for (int row = 0; row < 3; row++)
                {
                    if (board[row, col] != ' ' &&
                        board[row, col] == board[row + 1, col] &&
                        board[row, col] == board[row + 2, col] &&
                        board[row, col] == board[row + 3, col])
                    {
                        return true;
                    }
                }
            }

            // Check diagonals (positive slope)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (board[row, col] != ' ' &&
                        board[row, col] == board[row + 1, col + 1] &&
                        board[row, col] == board[row + 2, col + 2] &&
                        board[row, col] == board[row + 3, col + 3])
                    {
                        return true;
                    }
                }
            }

            // Check diagonals (negative slope)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 3; col < 7; col++)
                {
                    if (board[row, col] != ' ' &&
                        board[row, col] == board[row + 1, col - 1] &&
                        board[row, col] == board[row + 2, col - 2] &&
                        board[row, col] == board[row + 3, col - 3])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static bool IsBoardFull()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (board[row, col] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        static void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }
    }
}
