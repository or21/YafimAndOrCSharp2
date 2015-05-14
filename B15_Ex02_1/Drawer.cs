using System;
namespace B15_Ex02_1
{
    /// <summary>
    /// Draws current state of the board
    /// </summary>
    class Drawer
    {
        public static void DrawBoard(Board i_GameBoard)
        {
            // 'A'
            int unicode = 65;
            int size = i_GameBoard.Size;
            string separator = drawSeparator(size);

            for (int x = -1; x < size; x++)
            {
                for (int y = -1; y < size; y++)
                {
                    if (x == -1 && y != -1)
                    {
                        char asciiValue = (char)unicode++;
                        Console.Write(" {0}  ", asciiValue);
                    }
                    else if (x == -1 && y == -1)
                    {
                        Console.Write("   ");
                    }
                    else if (x != -1 && y == -1)
                    {
                        Console.Write("{0} |", x + 1);
                    }

                    if (x > -1 && y > -1)
                    {
                        if (i_GameBoard[x, y].Equals(Coin.Null))
                        {
                            Console.Write("   |");
                        }
                        else if (i_GameBoard[x, y].Equals(Coin.X))
                        {
                            Console.Write(" X |");
                        }
                        else if (i_GameBoard[x, y].Equals(Coin.O))
                        {
                            Console.Write(" O |");
                        }
                    }
                }

                Console.WriteLine(separator);
            }
        }

        /// <summary>
        ///  Draws bottom border of each cell
        /// </summary>
        /// <returns>valid separator</returns>
        private static string drawSeparator(int i_Size)
        {
            string separator = "  ";

            for (int i = 0; i < (4 * i_Size) + 1; i++)
            {
                separator += '=';
            }

            return Environment.NewLine + separator;
        }
    }
}
