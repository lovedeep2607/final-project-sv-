using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourGame
{
    class ComputerPlayer : Player
    {
        private readonly Random random;
        private readonly char[,] board;

        public ComputerPlayer(char symbol, char[,] gameBoard) : base(symbol)
        {
            random = new Random();
            board = gameBoard;
        }

        public override int GetMove()
        {
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

        private bool IsValidMove(int column)
        {
            // Check if the column is valid and not full
            for (int row = 0; row < 6; row++)
            {
                if (board[row, column] == ' ')
                {
                    return true;
                }
            }

            return false;
        }
    }
}