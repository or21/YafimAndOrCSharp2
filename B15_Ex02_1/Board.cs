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


            //TODO: DELTE.
            Drawer.drawBoard(this);
                    }

        public int Size
                    {
            get {return m_Size;}
                        }

        public Coin[,] GameBoard
            {
            get { return gameBoard; }
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