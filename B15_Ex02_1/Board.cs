using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B15_Ex02_1
{
    class Board
    {
        Player playerOne, playerTwo;
        Coin[,] gameBoard;

        // Constructor
        public Board(int width, int height, int numberOfPlayers)
        {
            gameBoard = new Coin[width, height];
            playerOne = new Player(false, Coin.X);
            playerTwo = (numberOfPlayers == 2 ) ? new Player(false, Coin.O) : new Player(true, Coin.O);
            
        }


    }


    public enum Coin
    {
        X = "X", O = "O", NULL
    };
}
