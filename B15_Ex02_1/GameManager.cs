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

        /// <summary>
        /// Controls and manages the running game
        /// </summary>
        private GameManager m_gameManager;

        /// <summary>
        /// Initializes a new instance of the GameManager class.
        /// </summary>
        /// <param name="i_Size"> Size of the board</param>
        /// <param name="i_NumberOfPlayers"> Number of human players </param>
        /// <param name="i_PlayerOneName">Name of the first player</param>
        /// <param name="i_PlayerTwoName">Name of the second player ("Comp" if computer)</param>
        public GameManager(int i_Size, int i_NumberOfPlayers, string i_PlayerOneName, string i_PlayerTwoName)
        {
            this.m_size = i_Size;
            this.m_numberOfPlayers = i_NumberOfPlayers;
            this.gameBoard = new Coin[i_Size, i_Size];

            this.m_playerOne = new Player(false, Coin.X, i_PlayerOneName, i_Size);
    //  this.m_playerOne = new Player(true, Coin.X, i_PlayerOneName, i_Size);
            this.m_playerTwo = (i_NumberOfPlayers == 2) ? new Player(false, Coin.O, i_PlayerTwoName, i_Size) : new Player(true, Coin.O, i_PlayerTwoName, i_Size);
        
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

            // Update availble moves for each player
            Utils.UpadteAvailableMoves(this, ref m_playerOne);
            Utils.UpadteAvailableMoves(this, ref m_playerTwo);
        }

        /// <summary>
        /// Gets size of the board
        /// </summary>
        public int Size
        {
            get { return m_size; }
        }

        /// <summary>
        /// Current Coin in specific cell
        /// </summary>
        /// <param name="i_I"> Coordinate i </param>
        /// <param name="i_J"> Coordinate j </param>
        /// <returns>Coin value</returns>
        public Coin this[int i_I, int i_J]
        {
            get { return gameBoard[i_I, i_J]; }
            set { gameBoard[i_I, i_J] = value; }
        }

        /// <summary>
        /// Runs the game
        /// </summary>
        public void RunGame()
        {
            // Some flags
            bool playerOneTurn = true;
            bool isGameOver = false;
            
            // Coordinates to make moves
            int i_X, i_Y;

            // Players to play
            Player currentPlayer = m_playerOne;
            Player otherPlayer = m_playerTwo;
            Player winner;

            while (!isGameOver)
            {
                // Check whose turn now
                currentPlayer = playerOneTurn ? m_playerOne : m_playerTwo;

                // Check if current player can move
                bool canMove = currentPlayer.AvailableMoves != 0;

                if (canMove)
                {
                    // Computer's turn, Otherwise Human player's turn.
                    if (currentPlayer.IsComp)
                    {
                  //      Utils.getAIMove(ref m_gameManager, currentPlayer, out i_X, out i_Y);
                        Utils.getAIMove(m_gameManager, currentPlayer, out i_X, out i_Y);
                    }
                    else
                    {
                        getMove(currentPlayer, out i_X, out i_Y, ref isGameOver);
                    }

                    m_gameManager = this;
                    
                    Utils.MakeMove(ref m_gameManager, currentPlayer, i_X, i_Y);
                    Utils.UpadteAvailableMoves(this, ref otherPlayer);

//                    Ex02.ConsoleUtils.Screen.Clear();
                    
                    Drawer.DrawBoard(this);

                    //TODO: DELETE --> DEBUG
                    Console.WriteLine("{0} Av.Moves: {1}, {2} Av.Moves: {3}", m_playerOne.Name, m_playerOne.AvailableMoves, m_playerTwo.Name, m_playerTwo.AvailableMoves);
                }
                else
                {
                    // Other player can move, Otherwise No moves left so end current game.
                    if (otherPlayer.AvailableMoves != 0)
                    {
                        Console.WriteLine("No move left for {0}!", currentPlayer.Name);
                    }
                    else
                    {
                        isGameOver = true;
                    }
                }
                
                // Switch turns for next move.
                if (!isGameOver)
                {
                    playerOneTurn = !playerOneTurn;
                    otherPlayer = currentPlayer;
                }
            }

            // Count points for each player
            Utils.CountPoints(m_gameManager, ref currentPlayer, ref otherPlayer);
            Console.WriteLine("{0} Score: {1}, {2} Score: {3}", m_playerOne.Name, m_playerOne.Points, m_playerTwo.Name, m_playerTwo.Points);

            // The winner is the one with more coins
            winner = (currentPlayer.Points > otherPlayer.Points) ? currentPlayer : otherPlayer;
            Console.WriteLine("The Winner is {0}", winner.Name);
        }

        /// <summary>
        /// Get move from the user and check if valid move.
        /// </summary>
        /// <param name="i_Player">Current player</param>
        /// <param name="i_X">X Coordinate</param>
        /// <param name="i_Y">y Coordinate</param>
        /// <param name="io_IsGameOver">true if game over, Otherwise continue</param>
        private void getMove(Player i_Player, out int i_X, out int i_Y, ref bool io_IsGameOver)
        {
            // flag
            bool isValid = false;
            
            // Coordinates to be set
            i_X = 0;
            i_Y = 0;

            Console.WriteLine("{0}'s Turn: Please make a move ({1}): ", i_Player.Name, i_Player.ShapeCoin);

            while (!isValid)
            {
                string playerInput = Console.ReadLine();
                if (playerInput != null && playerInput.Length == 2)
                {
                    // Leter to Y, Number to X
                    i_Y = char.ToUpper(playerInput[0]) - 64 - 1;
                    i_X = playerInput[1] - '0' - 1;
                    
                    // Possible input should be within game borders.
                    bool isPossible = i_X >= 0 && i_Y >= 0 && i_X < m_size && i_Y < m_size;

                    // if not a possible move try again, Otherwise Check if valid move.
                    if (!isPossible)
                    {
                        Console.WriteLine("Invalid Input! Please Try again...");
                    }
                    else
                    {
                        isValid = i_Player[i_X, i_Y];
                        
                        if (!isValid)
                        {
                            Console.WriteLine("Can't Move here. Try again...");
                        }
                    }
                }
                else if (playerInput != null && playerInput.ToUpper().Equals("Q"))
                {
                    isValid = true;
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