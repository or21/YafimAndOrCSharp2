using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B15_Ex02_1
{
    /// <summary>
    /// Draws current state of the board
    /// </summary>
    class Drawer
    {

        public static void drawBoard(Board gameBoard)
        {
            int m_Size = gameBoard.Size;

            // 'A'
            int unicode = 65;
            char asciiValue;
            string separator = drawSeparator(m_Size);

            for (int x = -1; x < m_Size; x++)
            {
                for (int y = -1; y < m_Size; y++)
                {
                    if (x == -1 && y != -1)
                    {
                        asciiValue = (char)unicode++;
                        System.Console.Write(" {0}  ", asciiValue.ToString());
                    }
                    else if (x == -1 && y == -1)
                    {
                        System.Console.Write("   ");
                    }
                    else if (x != -1 && y == -1)
                    {
                        System.Console.Write("{0} |", x + 1);
                    }

                    if (x > -1 && y > -1)
                    {
                        if (gameBoard[x, y].Equals(Coin.Null))
                        {
                            System.Console.Write("   |");
                        }
                        else if (gameBoard[x, y].Equals(Coin.X))
                        {
                            System.Console.Write(" X |");
                        }
                        else if (gameBoard[x, y].Equals(Coin.O))
                        {
                            System.Console.Write(" O |");
                        }
                    }
                }

                System.Console.WriteLine(separator);
            }
        }

        /// <summary>
        ///  Draws bottom border of each cell
        /// </summary>
        /// <returns>valid separator</returns>
        private static string drawSeparator(int m_Size)
        {
            string separator = "  ";

            for (int i = 0; i < (4 * m_Size) + 1; i++)
            {
                separator += '=';
            }

            return Environment.NewLine + separator;
        }
    }
}
