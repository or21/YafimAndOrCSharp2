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
        private int m_numberOfPlayers;
        
        /// <summary>
        /// Set the players
        /// </summary>
        private Player m_playerOne, m_playerTwo;
        
        /// <summary>
        /// Coin matrix holds the current state of the game
        /// </summary>
        private Coin[,] gameBoard;
        
        /// <summary>
        /// Size of the board
        /// </summary>
        private int m_size;

        /// <summary>
        /// Number of moves left to end game
        /// </summary>
        private int m_totalMovesLeft;

        private GameManager m_gameManager;

        /// <summary>
        /// Initializes a new instance of the Board class.
        /// </summary>
        /// <param name="i_Size"> size of the board</param>
        /// <param name="i_NumberOfPlayers"> number of current human players </param>
        /// <param name="i_PlayerOneName"></param>
        /// <param name="i_PlayerTwoName"></param>
        public GameManager(int i_Size, int i_NumberOfPlayers, string i_PlayerOneName, string i_PlayerTwoName)
        {
            this.m_size = i_Size;
            m_numberOfPlayers = i_NumberOfPlayers;
            gameBoard = new Coin[i_Size, i_Size];

       //     m_playerOne = new Player(false, Coin.X, i_PlayerOneName, i_Size);
            m_playerOne = new Player(true, Coin.X, i_PlayerOneName, i_Size);
            m_playerTwo = (i_NumberOfPlayers == 2) ? new Player(false, Coin.O, i_PlayerTwoName, i_Size) : new Player(true, Coin.O, i_PlayerTwoName, i_Size);
        
            this.m_totalMovesLeft = (i_Size * 2) - 4;
            setNewGame();
        }

        /// <summary>
        /// Initialize a new game.
        /// </summary>
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
            int halfBoard = (m_size / 2) - 1;
            gameBoard[halfBoard + 1, halfBoard] = Coin.X;
            gameBoard[halfBoard, halfBoard + 1] = Coin.X;

            gameBoard[halfBoard, halfBoard] = Coin.O;
            gameBoard[halfBoard + 1, halfBoard + 1] = Coin.O;

            //update availble moves for each player
            Utils.UpadteAvailableMoves(this, ref m_playerOne);
            Utils.UpadteAvailableMoves(this, ref m_playerTwo);
        }

        public int Size
        {
            get { return m_size; }
        }

        public Coin this[int i_I, int i_J]
        {
            get { return gameBoard[i_I, i_J]; }
            set { gameBoard[i_I, i_J] = value; }
        }

        // runs the game
        public void RunGame()
        {
            bool playerOneTurn = true;
            bool isGameOver = false;
            int x, y;

            Player currentPlayer = m_playerOne;
            Player otherPlayer = m_playerTwo;

            while (!isGameOver)
            {
                currentPlayer = playerOneTurn ? m_playerOne : m_playerTwo;

                // Availble moves of the player
                bool canMove = currentPlayer.AvailableMoves != 0;

                if (canMove)
                {
                    if (currentPlayer.IsComp)
                    {
                        Utils.MakeAIMove(ref m_gameManager, currentPlayer, out x, out y);
                    }
                    else
                    {
                        getMove(currentPlayer, out  x, out y, ref isGameOver);
                    }

                    m_gameManager = this;

                    if (isGameOver)
                    {
                        break;
                    }

                    Utils.MakeMove(ref m_gameManager, currentPlayer, x, y);
                    Utils.UpadteAvailableMoves(this, ref otherPlayer);

//                    Ex02.ConsoleUtils.Screen.Clear();
                    
                    Drawer.DrawBoard(this);

                    //TODO: DELETE --> DEBUG
                    
                    Console.WriteLine("{0} Av.Moves: {1}, {2} Av.Moves: {3}", currentPlayer.Name, currentPlayer.AvailableMoves, otherPlayer.Name, otherPlayer.AvailableMoves);
                }
                else
                {
                    if (otherPlayer.AvailableMoves != 0)
                    {
                        Console.WriteLine("No move left for {0}!", currentPlayer.Name);
                    }
                    else
                    {
                        isGameOver = true;
                    }
                }
                if (!isGameOver)
                {
                    playerOneTurn = !playerOneTurn;
                    otherPlayer = currentPlayer;
                }
            }

            Utils.CountPoints(m_gameManager, ref currentPlayer, ref otherPlayer);
            Console.WriteLine("{0} Score: {1}, {2} Score: {3}", currentPlayer.Name, currentPlayer.Points, otherPlayer.Name, otherPlayer.Points);

            Player winner = (currentPlayer.Points > otherPlayer.Points) ? currentPlayer : otherPlayer;
            Console.WriteLine("The Winner is {0}", winner.Name);
        }


        /// <summary>
        /// Get move from the user
        /// </summary>
        private void getMove(Player i_Player, out int i_X, out int i_Y, ref bool io_IsGameOver)
        {
            bool isValidInput = false;
            i_X = 0;
            i_Y = 0;
            Console.WriteLine("{0}'s Turn: Please make a move ({1}): ", i_Player.Name, i_Player.ShapeCoin);

            while (!isValidInput)
            {
                string playerInput = Console.ReadLine();
                if (playerInput != null && playerInput.Length == 2)
                {
                    i_Y = char.ToUpper(playerInput[0]) - 64 - 1;
                    i_X = playerInput[1] - '0' - 1;
                    bool isValid = i_X >= 0 && i_Y >= 0 && i_X < m_size && i_Y < m_size;

                    if (!isValid)
                    {
                        Console.WriteLine("Invalid Input! Please Try again...");
                    }
                    else
                    {
                        isValidInput = i_Player[i_X, i_Y];

                        if (!isValidInput)
                        {
                            Console.WriteLine("Can't Move here. Try again...");
                        }
                    }
                }
                else if (playerInput != null && playerInput.ToUpper().Equals("Q"))
                {
                    isValidInput = true;
                    io_IsGameOver = true;
                }
                else
                {
                        Console.WriteLine("Invalid Input! Please Try again...");
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