using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B15_Ex02_1
{
    /// <summary>
    /// This class holds the GUI and Initializes a new game
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Set the players
        /// </summary>
        private Player m_PlayerOne, m_PlayerTwo;
        
        /// <summary>
        /// Coin matrix holds the current state of the game
        /// </summary>
        private Coin[,] gameBoard;
        
        /// <summary>
        /// Size of the board
        /// </summary>
        private int m_Size;

        /// <summary>
        /// Number of moves left to end game
        /// </summary>
        private int m_TotalMovesLeft;

        /// <summary>
        /// Initializes a new instance of the Board class.
        /// </summary>
        /// <param name="i_Size"> size of the board</param>
        /// <param name="i_NumberOfPlayers"> number of current human players </param>
        public Board(int i_Size, int i_NumberOfPlayers)
        {
            this.m_Size = i_Size;

            gameBoard = new Coin[i_Size, i_Size];
            m_PlayerOne = new Player(false, Coin.X, m_Size);
            m_PlayerTwo = (i_NumberOfPlayers == 2 ) ? new Player(false, Coin.O, m_Size) : new Player(true, Coin.O, m_Size);
        
            this.m_TotalMovesLeft = (i_Size * 2 ) - 4;

            setNewGame();
        }

        /// <summary>
        /// Initialize a new game.
        /// </summary>
        private void setNewGame()
        {
            // Set all board to null.
            for (int x = 0; x < m_Size; x++)
            {
                for (int y = 0; y < m_Size; y++)
                {
                    gameBoard[x, y] = Coin.Null;
                }
            }

            // Place 4 coins in board
            int halfBoard = (m_Size / 2) - 1;
            gameBoard[halfBoard + 1, halfBoard] = Coin.X;
            gameBoard[halfBoard, halfBoard + 1] = Coin.X;

            gameBoard[halfBoard, halfBoard] = Coin.O;
            gameBoard[halfBoard + 1, halfBoard + 1] = Coin.O;

            /*
            //TODO: DELETE --> Debug
            System.Console.WriteLine("({0},{1}) = X", halfBoard + 1, halfBoard);
            System.Console.WriteLine("({0},{1}) = X", halfBoard, halfBoard+1);
            System.Console.WriteLine("({0},{1}) = O", halfBoard, halfBoard);
            System.Console.WriteLine("({0},{1}) = O", halfBoard + 1, halfBoard + 1);
            */
        }

        /// <summary>
        /// Draws current state of the board
        /// </summary>
        public void drawBoard()
        {
            // 'A'
            int unicode = 65;
            char asciiValue;
            string separator = drawSeparator();

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
        private string drawSeparator()
        {
            string separator = "  ";

            for (int i = 0; i < (4 * m_Size) + 1; i++)
            {
                separator += '=';
            }

            return Environment.NewLine + separator;
        }
    }

    /// <summary>
    /// Represents 3 coin states
    /// </summary>
    public enum Coin
    {
        /// <summary>
        /// Black coin
        /// </summary>
        X,
 
        /// <summary>
        /// White coin
        /// </summary>
        O, 

        /// <summary>
        /// No coin
        /// </summary>
        Null
    }
}