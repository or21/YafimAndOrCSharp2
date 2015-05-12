using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace B15_Ex02_1
{
    class Player
    {
        private bool isComputer;
        private Coin shape;
        private bool[,] availableMoves;
        private int size;

        public Player(bool i_IsComputer, Coin i_Shape, int i_Size)
        {
            this.isComputer = i_IsComputer;
            this.shape = i_Shape;
            this.size = i_Size;
            this.availableMoves = new bool[size,size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    this.availableMoves[i, j] = false;
                }
            }
            firstAvailableMoves();
        }

        private void firstAvailableMoves()
        {
            if (shape == Coin.O)
            {
                availableMoves[(size / 2) - 1, (size / 2) + 1] = true;
                availableMoves[(size / 2), (size / 2) + 2] = true;
                availableMoves[(size / 2) + 2, (size / 2)] = true;
                availableMoves[(size / 2) + 1, (size / 2) - 1] = true;
            }
            else
            {
                availableMoves[(size / 2) - 1, (size / 2)] = true;
                availableMoves[(size / 2) + 1, (size / 2) + 2] = true;
                availableMoves[(size / 2) + 2, (size / 2) + 1] = true;
                availableMoves[(size / 2), (size / 2) - 1] = true;
            }
        }

        public bool this[int i_I, int i_J]
        {
            get  { return availableMoves[i_I,i_J]; }
            set { this.availableMoves[i_I,i_J] = value; }
        }

        public bool IsComp
        {
            get { return isComputer; }
        }

    }
}
