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
        private "COIN" shape;
        private bool[,] availableMoves;

        public Player(bool i_IsComputer, "COIN" shape)
        {
            this.isComputer = i_IsComputer;
            this.shape = shape;
            this.availableMoves = new bool[Board.size,Board.size];
            for (int i = 0; i < Board.size; i++)
            {
                for (int j = 0; j < Board.size; j++)
                {
                    this.availableMoves[i, j] = false;
                }
            }
        }

        public bool IsComp
        {
            get { return isComputer; }
        }

    }
}
