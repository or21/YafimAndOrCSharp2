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
            this.m_size = i_Size;

            gameBoard = new Coin[i_Size, i_Size];
            playerOne = new Player(false, Coin.X, m_size);
            playerTwo = (i_NumberOfPlayers == 2 ) ? new Player(false, Coin.O, m_size) : new Player(true, Coin.O, m_size);
            

            this.totalMovesLeft = (i_Size * 2 ) - 4;

            setNewGame();
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
            int halfBoard = m_size/2 - 1;
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

        public void print()
        {
            // 'A'
            int unicode = 65;
            char asciiValue;
            string separator = drawSeparator();

            for (int x = -1; x < m_size; x++)
            {
                for (int y = -1; y < m_size; y++)
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

        private string drawSeparator()
        {
            string separator = "  ";

            for (int i = 0; i < (4 * m_size + 1); i++)
            {
                separator += '=';
            }

            return (Environment.NewLine + separator);
        }
    }


    public enum Coin
    {
        X, 
        O, 
        Null
    };
}
