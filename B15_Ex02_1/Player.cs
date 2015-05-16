//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="B15_Ex02">
// Yafim Vodkov 308973882 Or Brand 302521034
// </copyright>
//----------------------------------------------------------------------
using System.Collections.Generic;

namespace B15_Ex02_1
{
    /// <summary>
    /// This class hold player's properties.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// If player is computer
        /// </summary>
        private bool isComputer;

        /// <summary>
        /// Shape of the player
        /// </summary>
        private Coin shape;

        /// <summary>
        /// Possible moves
        /// </summary>
        private bool[,] availableMoves;

        /// <summary>
        /// Size of the board
        /// </summary>
        private int boardSize;

        /// <summary>
        /// The name of the player
        /// </summary>
        private string name;

        /// <summary>
        /// Current points of the player
        /// </summary>
        private int currentPoints;

        /// <summary>
        /// Number of available moves left
        /// </summary>
        private int numberOfAvailableMoves;

        /// <summary>
        /// The possible coordinates player can move
        /// </summary>
        private List<Coord> possibleMovesCoordinates;

        /// <summary>
        /// Initializes a new instance of the Player class.
        /// </summary>
        /// <param name="i_IsComputer">Player is computer</param>
        /// <param name="i_Shape">Coin shape of the player</param>
        /// <param name="i_PlayerName">Name of the player</param>
        /// <param name="i_BoardSize">Game board size</param>
        public Player(bool i_IsComputer, Coin i_Shape, string i_PlayerName, int i_BoardSize)
        {
            this.name = i_PlayerName;
            this.isComputer = i_IsComputer;
            this.shape = i_Shape;
            this.boardSize = i_BoardSize;
            this.availableMoves = new bool[boardSize, boardSize];
            this.currentPoints = 0;
            this.numberOfAvailableMoves = 0;
            this.possibleMovesCoordinates = new List<Coord>();

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    this.availableMoves[i, j] = false;
                }
            }
        }

        /// <summary>
        /// Get if (x,y) is possible move.
        /// </summary>
        /// <param name="i_X">x coordinate</param>
        /// <param name="i_Y">y coordinate</param>
        /// <returns>True if possible move</returns>
        public bool this[int i_X, int i_Y]
        {
            get { return availableMoves[i_X, i_Y]; }
            set { this.availableMoves[i_X, i_Y] = value; }
        }

        /// <summary>
        /// Gets a value indicating whether its a computer or not
        /// </summary>
        public bool IsComp
        {
            get { return isComputer; }
        }

        /// <summary>
        /// Gets player's coin share
        /// </summary>
        public Coin ShapeCoin
        {
            get { return shape; }
        }

        /// <summary>
        /// Gets or sets current points of the player
        /// </summary>
        public int Points
        {
            get { return currentPoints; }
            set { this.currentPoints = value; }
        }

        /// <summary>
        /// Gets or sets player's points.
        /// </summary>
        public int AvailableMoves
        {
            get { return numberOfAvailableMoves; }
            set { this.numberOfAvailableMoves = value; }
        }

        /// <summary>
        /// Gets or sets player's name.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets the size of the board.
        /// </summary>
        public int BoardSize
        {
            get { return boardSize; }
        }

        /// <summary>
        /// Gets or sets the possible coordinates player can go.
        /// </summary>
        public List<Coord> PossibleMovesCoordinates
        {
            get { return possibleMovesCoordinates; }
            set { this.possibleMovesCoordinates = value; }
        }
    }

    /// <summary>
    /// Struct holds coordinate (x,y)
    /// </summary>
    public struct Coord
    {
        /// <summary>
        /// X Coordinate
        /// </summary>
        public int m_X;

        /// <summary>
        /// Y Coordinate
        /// </summary>
        public int m_Y;
    }
}
