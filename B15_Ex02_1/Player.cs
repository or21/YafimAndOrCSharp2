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
        private int size;
        private string name;

        public Player(bool i_IsComputer, Coin i_Shape, string i_PlayerName, int i_Size)
        {
            this.name = i_PlayerName;
            this.isComputer = i_IsComputer;
            this.shape = i_Shape;
            this.size = i_Size;
            this.availableMoves = new bool[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    this.availableMoves[i, j] = false;
                }
            }
       //     firstAvailableMoves();
        }

        private void firstAvailableMoves()
        {
            /*
            int halfBoard = size / 2 - 1;
            if (shape == Coin.O)
            {
                availableMoves[halfBoard - 1, halfBoard + 1] = true;
                availableMoves[halfBoard, halfBoard + 2] = true;
                availableMoves[halfBoard + 2, halfBoard] = true;
                availableMoves[halfBoard + 1, halfBoard - 1] = true;
            }
            else
            {
                availableMoves[halfBoard - 1, halfBoard] = true;
                availableMoves[halfBoard + 1, halfBoard + 2] = true;
                availableMoves[halfBoard + 2, halfBoard + 1] = true;
                availableMoves[halfBoard, halfBoard - 1] = true;
            }
            */
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

        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }
    }
}
