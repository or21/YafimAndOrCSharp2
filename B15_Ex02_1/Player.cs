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
        private bool m_isComputer;

        /// <summary>
        /// Shape of the player
        /// </summary>
        private Coin m_shape;

        /// <summary>
        /// Possible moves
        /// </summary>
        private bool[,] m_availableMoves;

        /// <summary>
        /// Size of the board
        /// </summary>
        private int m_boardSize;

        /// <summary>
        /// The name of the player
        /// </summary>
        private string m_name;

        /// <summary>
        /// Current points of the player
        /// </summary>
        private int m_currentPoints;

        /// <summary>
        /// Number of available moves left
        /// </summary>
        private int m_numberOfAvailableMoves;

        /// <summary>
        /// The possible coordinates player can move
        /// </summary>
        private List<Coord> m_possibleMovesCoordinates;

        /// <summary>
        /// Initializes a new instance of the Player class.
        /// </summary>
        /// <param name="i_IsComputer">Player is computer</param>
        /// <param name="i_Shape">Coin shape of the player</param>
        /// <param name="i_PlayerName">Name of the player</param>
        /// <param name="i_BoardSize">Game board size</param>
        public Player(bool i_IsComputer, Coin i_Shape, string i_PlayerName, int i_BoardSize)
        {
            this.m_name = i_PlayerName;
            this.m_isComputer = i_IsComputer;
            this.m_shape = i_Shape;
            this.m_boardSize = i_BoardSize;
            this.m_availableMoves = new bool[m_boardSize, m_boardSize];
            this.m_currentPoints = 0;
            this.m_numberOfAvailableMoves = 0;
            this.m_possibleMovesCoordinates = new List<Coord>();

            for (int i = 0; i < m_boardSize; i++)
            {
                for (int j = 0; j < m_boardSize; j++)
                {
                    this.m_availableMoves[i, j] = false;
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
            get { return m_availableMoves[i_X, i_Y]; }
            set { this.m_availableMoves[i_X, i_Y] = value; }
        }

        /// <summary>
        /// Gets a value indicating whether its a computer or not
        /// </summary>
        public bool IsComp
        {
            get { return m_isComputer; }
        }

        /// <summary>
        /// Gets player's coin share
        /// </summary>
        public Coin ShapeCoin
        {
            get { return m_shape; }
        }

        /// <summary>
        /// Gets or sets current points of the player
        /// </summary>
        public int Points
        {
            get { return m_currentPoints; }
            set { this.m_currentPoints = value; }
        }

        /// <summary>
        /// Gets or sets player's points.
        /// </summary>
        public int AvailableMoves
        {
            get { return m_numberOfAvailableMoves; }
            set { this.m_numberOfAvailableMoves = value; }
        }

        /// <summary>
        /// Gets or sets player's name.
        /// </summary>
        public string Name
        {
            get { return m_name; }
            set { this.m_name = value; }
        }

        /// <summary>
        /// Gets the size of the board.
        /// </summary>
        public int BoardSize
        {
            get { return m_boardSize; }
        }

        /// <summary>
        /// Gets or sets the possible coordinates player can go.
        /// </summary>
        public List<Coord> PossibleMovesCoordinates
        {
            get { return m_possibleMovesCoordinates; }
            set { this.m_possibleMovesCoordinates = value; }
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
