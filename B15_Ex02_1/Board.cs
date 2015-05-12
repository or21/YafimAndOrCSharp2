using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B15_Ex02_1
{
    class Board
    {
        Player playerOne, playerTwo;
        Coin[,] gameBoard;

        private int m_size;
        private int totalMovesLeft;

        // Constructor
        public Board(int i_Size, int i_NumberOfPlayers)
        {
            gameBoard = new Coin[i_Size, i_Size];
            playerOne = new Player(false, Coin.X);
            playerTwo = (i_NumberOfPlayers == 2 ) ? new Player(false, Coin.O) : new Player(true, Coin.O);
            
            this.m_size = i_Size;
            this.totalMovesLeft = (i_Size * 2 ) - 4;

            setNewGame();
            
        }

        public int size
        {
            get { return this.m_size; }
        }

        private void setNewGame()
        {
            // Set all board to null.
            for (int x = 0; x < m_size; x++)
            {
                for (int y = 0; y < m_size; y++)
                {
                    gameBoard[x, y] = Coin.Null;
                }
            }

            // Place 4 coins in board
            int halfBoard = m_size/2;
            gameBoard[halfBoard + 1, halfBoard] = Coin.X;
            gameBoard[halfBoard, halfBoard + 1] = Coin.X;

            gameBoard[halfBoard, halfBoard] = Coin.O;
            gameBoard[halfBoard + 1, halfBoard + 1] = Coin.O;


    }

        public string print()
        {

            return "";
        }


    }


    public enum Coin
    {
        X, 
        O, 
        Null
    };
}
