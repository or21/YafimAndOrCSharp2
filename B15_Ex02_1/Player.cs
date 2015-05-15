using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace B15_Ex02_1
{
    public class Player
    {
        private bool isComputer;
        private Coin shape;
        private bool[,] availableMoves;
        private int boardSize;
        private string name;
        private int currentPoints;
        private int numberOfAvailableMoves;

        public Player(bool i_IsComputer, Coin i_Shape, string i_PlayerName, int i_BoardSize)
        {
            this.name = i_PlayerName;
            this.isComputer = i_IsComputer;
            this.shape = i_Shape;
            this.boardSize = i_BoardSize;
            this.availableMoves = new bool[boardSize, boardSize];
            this.currentPoints = 0;
            this.numberOfAvailableMoves = 0;

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    this.availableMoves[i, j] = false;
                }
            }
        }

        public bool this[int i_I, int i_J]
        {
            get { return availableMoves[i_I, i_J]; }
            set { this.availableMoves[i_I, i_J] = value; }
        }

        public bool IsComp
        {
            get { return isComputer; }
        }

        public Coin ShapeCoin
        {
            get { return shape; }
        }

        public int Points
        {
            get { return currentPoints; }
            set { this.currentPoints = value; }
        }

        public int AvailableMoves
        {
            get { return numberOfAvailableMoves; }
            set { this.numberOfAvailableMoves = value; }
        }

        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }

        public int BoardSize
        {
            get { return boardSize; }
        }
    }
}
