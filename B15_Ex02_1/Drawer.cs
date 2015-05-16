//-----------------------------------------------------------------------
// <copyright file="Drawer.cs" company="B15_Ex02">
// Yafim Vodkov 308973882 Or Brand 302521034
// </copyright>
//----------------------------------------------------------------------
using System;

namespace B15_Ex02_1
{
    /// <summary>
    /// Draws current state of the board
    /// </summary>
    public class Drawer
    {
        /// <summary>
        /// Draws current state of the board
        /// </summary>
        /// <param name="i_GameBoard">Current state of the game</param>
        public static void DrawBoard(GameManager i_GameBoard)
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

                    bool positiveCoordinates = x > -1 && y > -1;
                    
                    if (positiveCoordinates)
                    {
                        drawInnerCells(i_GameBoard, x, y);
                    }
                }

                Console.WriteLine(separator);
            }
        }

        /// <summary>
        /// Draws inside cells of the table
        /// </summary>
        /// <param name="i_GameBoard">Current state of the game</param>
        /// <param name="i_X">x Coordinate</param>
        /// <param name="i_Y">y Coordinate</param>
        private static void drawInnerCells(GameManager i_GameBoard, int i_X, int i_Y)
        {
            if (i_GameBoard[i_X, i_Y].Equals(Coin.Null))
            {
                Console.Write("   |");
            }
            else if (i_GameBoard[i_X, i_Y].Equals(Coin.X))
            {
                Console.Write(" X |");
            }
            else if (i_GameBoard[i_X, i_Y].Equals(Coin.O))
            {
                Console.Write(" O |");
            }
        }

        /// <summary>
        /// Draws bottom border of each cell
        /// </summary>
        /// <param name="i_Size">size of the board</param>
        /// <returns>Valid length separator</returns>
        private static string drawSeparator(int i_Size)
        {
            string separator = "  ";

            if (i_Size == 8)
            {
                separator += "=================================";
            }
            else
            {
                separator += "=========================";
            }
            
            return Environment.NewLine + separator;
        }
    }
}
