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
        private int numberOfPlayers;

        /// <summary>
        /// Set the players
        /// </summary>
        private Player playerOne, playerTwo;

        /// <summary>
        /// Coin matrix holds the current state of the game
        /// </summary>
        private Coin[,] gameBoard;

        /// <summary>
        /// Size of the board
        /// </summary>
        private int m_size;
        
        /// <summary>
        /// Controls and manages the running game
        /// </summary>
        private GameManager gameManager;

        /// <summary>
        /// Initializes a new instance of the GameManager class.
        /// </summary>
        /// <param name="i_Size"> Size of the board</param>
        /// <param name="i_NumberOfPlayers"> Number of human players </param>
        /// <param name="i_PlayerOneName">Name of the first player</param>
        /// <param name="i_PlayerTwoName">Name of the second player ("Comp" if computer)</param>
        /// <param name="i_InitGame"></param>
        public GameManager(int i_Size, int i_NumberOfPlayers, string i_PlayerOneName, string i_PlayerTwoName, bool i_InitGame)
        {
            this.m_size = i_Size;
            this.numberOfPlayers = i_NumberOfPlayers;
            this.gameBoard = new Coin[i_Size, i_Size];

            this.playerOne = new Player(false, Coin.X, i_PlayerOneName, i_Size);
            //   this.m_playerOne = new Player(true, Coin.X, i_PlayerOneName, i_Size);
            this.playerTwo = (i_NumberOfPlayers == 2) ? new Player(false, Coin.O, i_PlayerTwoName, i_Size) : new Player(true, Coin.O, i_PlayerTwoName, i_Size);

            if (i_InitGame)
            {
                setNewGame();
            }

        }

        /// <summary>
        /// Initialize a new game.
        /// </summary>
        private void setNewGame()
        {
            initBoardForNewGame();

            // Place 4 coins in board
            int halfBoard = (m_size / 2) - 1;
            gameBoard[halfBoard + 1, halfBoard] = Coin.X;
            gameBoard[halfBoard, halfBoard + 1] = Coin.X;

            gameBoard[halfBoard, halfBoard] = Coin.O;
            gameBoard[halfBoard + 1, halfBoard + 1] = Coin.O;

            // Update availble moves for each player
            Utils.UpadteAvailableMoves(this, ref playerOne);
            Utils.UpadteAvailableMoves(this, ref playerTwo);
        }

        private void initBoardForNewGame()
        {
            // Set all board to null.
            for (int x = 0; x < m_size; x++)
            {
                for (int y = 0; y < m_size; y++)
                {
                    gameBoard[x, y] = Coin.Null;
                }
            }
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
            gameManager = this;

            // Players to play
            Player currentPlayer = playerOne;
            Player otherPlayer = playerTwo;

            while (!isGameOver)
            {
                // Check whose turn now
                currentPlayer = playerOneTurn ? playerOne : playerTwo;

                isGameOver = currentPlayerMove(currentPlayer, ref isGameOver, ref otherPlayer);

                // Switch turns for next move.
                if (!isGameOver)
                {
                    playerOneTurn = !playerOneTurn;
                    otherPlayer = currentPlayer;
                }
            }

            printResult(currentPlayer, otherPlayer);
        }

        private bool currentPlayerMove(Player i_CurrentPlayer, ref bool io_IsGameOver, ref Player io_OtherPlayer)
        {
            // Check if current player can move
            bool playerCanMove = i_CurrentPlayer.AvailableMoves != 0;

            if (playerCanMove)
            {
                io_IsGameOver = makePlayerMove(i_CurrentPlayer, io_IsGameOver, ref io_OtherPlayer);
            }
            else
            {
                // Other player can move, Otherwise No moves left so end current game.
                if (io_OtherPlayer.AvailableMoves == 0)
                {
                    Console.WriteLine("No move left for {0}!", i_CurrentPlayer.Name);
                }
                else
                {
                    io_IsGameOver = true;
                }
            }
            return io_IsGameOver;
        }

        private bool makePlayerMove(Player i_CurrentPlayer, bool i_IsGameOver, ref Player i_OtherPlayer)
        {
            int newX;
            int newY;
            // Computer's turn, Otherwise Human player's turn.
            if (i_CurrentPlayer.IsComp)
            {
                //      Utils.getAIMove(ref m_gameManager, currentPlayer, out o_X, out o_Y);
                Utils.GetAiMove(gameManager, i_CurrentPlayer, out newX, out newY);
            }
            else
            {
                getMove(i_CurrentPlayer, out newX, out newY, ref i_IsGameOver);
            }

            if (!i_IsGameOver)
            {
                Utils.MakeMove(ref gameManager, i_CurrentPlayer, newX, newY);
                Utils.UpadteAvailableMoves(this, ref i_OtherPlayer);
            }

            Ex02.ConsoleUtils.Screen.Clear();
            Drawer.DrawBoard(this);

            //TODO: DELETE --> DEBUG
            Console.WriteLine("{0} Av.Moves: {1}, {2} Av.Moves: {3}", playerOne.Name, playerOne.AvailableMoves,
                playerTwo.Name, playerTwo.AvailableMoves);
            return i_IsGameOver;
        }

        private void printResult(Player i_CurrentPlayer, Player i_OtherPlayer)
        {
            // Count points for each player
            Utils.CountPoints(gameManager, ref i_CurrentPlayer, ref i_OtherPlayer);
            Console.WriteLine("{0} Score: {1}, {2} Score: {3}", playerOne.Name, playerOne.Points, playerTwo.Name, playerTwo.Points);

            // The winner is the one with more coins
            Player winner = (i_CurrentPlayer.Points > i_OtherPlayer.Points) ? i_CurrentPlayer : i_OtherPlayer;
            Console.WriteLine("The Winner is {0}", winner.Name);
        }

        /// <summary>
        /// Get move from the user and check if valid move.
        /// </summary>
        /// <param name="i_Player">Current player</param>
        /// <param name="o_X">X Coordinate</param>
        /// <param name="o_Y">y Coordinate</param>
        /// <param name="io_IsGameOver">true if game over, Otherwise continue</param>
        private void getMove(Player i_Player, out int o_X, out int o_Y, ref bool io_IsGameOver)
        {
            // flag
            bool isValid = false;

            // Coordinates to be set
            o_X = 0;
            o_Y = 0;

            Console.WriteLine("{0}'s Turn: Please make a move ({1}): ", i_Player.Name, i_Player.ShapeCoin);

            while (!isValid)
            {
                string playerInput = Console.ReadLine();
                bool isValidInputSize = playerInput != null && playerInput.Length == 2;
                bool insertedExitCode = playerInput != null && playerInput.ToUpper().Equals("Q");
                if (isValidInputSize)
                {
                    isValid = areLettersValidMove(out o_X, out o_Y, playerInput, i_Player);
                }
                else if (insertedExitCode)
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

        private bool areLettersValidMove(out int o_X, out int o_Y, string i_PlayerInput, Player i_Player)
        {
            // Letter to Y, Number to X
            o_Y = char.ToUpper(i_PlayerInput[0]) - 64 - 1;
            o_X = i_PlayerInput[1] - '0' - 1;

            // Possible input should be within game borders.
            bool isPossible = o_X >= 0 && o_Y >= 0 && o_X < m_size && o_Y < m_size;

            if (!isPossible)
            {
                Console.WriteLine("Invalid Input! Please Try again...");
            }
            else
            {
                isPossible = i_Player[o_X, o_Y];
                if (!isPossible)
                {
                    Console.WriteLine("Can't Move here. Try again...");
                }
            }
            return isPossible;
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