using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourGame
{
    class HumanPlayer : Player
    {
        public HumanPlayer(char symbol) : base(symbol)
        {
        }

        public override int GetMove()
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
    }
}