using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B15_Ex02_1
{
    /// <summary>
    /// Manage entire game
    /// </summary>
    public class GameManager
    {
        /// <summary>
        /// Number of human players
        /// </summary>
        private int m_NumberOfPlayers;
        
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
        /// <param name="i_PlayerOneName"></param>
        /// <param name="i_PlayerTwoName"></param>
        public GameManager(int i_Size, int i_NumberOfPlayers, string i_PlayerOneName, string i_PlayerTwoName)
        {
            this.m_Size = i_Size;
            m_NumberOfPlayers = i_NumberOfPlayers;
            gameBoard = new Coin[i_Size, i_Size];

            m_PlayerOne = new Player(false, Coin.X, i_PlayerOneName, i_Size);
            m_PlayerTwo = (i_NumberOfPlayers == 2) ? new Player(false, Coin.O, i_PlayerTwoName, i_Size) : new Player(true, Coin.O, i_PlayerTwoName, i_Size);
        
            this.m_TotalMovesLeft = (i_Size * 2) - 4;



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

            //update availble moves for each player
            Utils.UpadteAvailableMoves(this, ref m_PlayerOne);
            Utils.UpadteAvailableMoves(this, ref m_PlayerTwo);
        }

        public int Size
        {
            get { return m_Size; }
        }

        public Coin this[int i_I, int i_J]
        {
            get { return gameBoard[i_I, i_J]; }
            set { gameBoard[i_I, i_J] = value; }
        }

        // manage the game

        // runs the game
        public void RunGame()
        {
            bool playerOneTurn = true;
            bool isGameOver = false;
            while (!isGameOver)
            {
                if (playerOneTurn)
                {
                    getMove(m_PlayerOne);
                    playerOneTurn = false;
                }
                else
                {
                    getMove(m_PlayerTwo);
                    playerOneTurn = true;
                }
            }
        }

        /// <summary>
        /// Get move from the user
        /// </summary>

        private void getMove(Player player)
        {
            bool isValidInput = false;
            
            Console.WriteLine("{0}'s Turn: Please make a move: ", player.Name);

            while (!isValidInput)
            {
                string playerInput = Console.ReadLine();
                if (playerInput.Length == 2)
                {
                    int x = char.ToUpper(playerInput[0]) - 64 - 1;
                    int y = playerInput[1] - '0' - 1;
                    bool isValid = x >= 0 && y >= 0 && x < m_Size && y < m_Size;

                    if (!isValid)
                    {
                        Console.WriteLine("Invalid Input! Please Try again...");
                    }
                    else
                    {
                        isValidInput = player[x, y];

                        if (!isValidInput)
                        {
                            Console.WriteLine("Can't Move here. Try again...");
                        }
                    }

                }
            }
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