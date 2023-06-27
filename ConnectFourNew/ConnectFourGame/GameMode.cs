using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectFourGame;

namespace ConnectFourGame
{

    abstract class GameMode : IGameMode
    {
        protected char[,] board;
        protected char currentPlayer;
        protected bool isGameOver;

        protected GameMode()
        {
            board = new char[6, 7];
        }

        public abstract void PlayGame();

        protected void InitializeBoard()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }

        protected void PrintBoard()
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

        protected bool IsValidMove(int column)
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

        protected void MakeMove(int column)
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

        protected bool CheckForWin()
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

        protected bool IsBoardFull()
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

        protected void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }
    }
}