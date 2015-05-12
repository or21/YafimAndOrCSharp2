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
        public Board(int i_Width, int i_Height, int i_NumberOfPlayers)
        {
            gameBoard = new Coin[i_Width, i_Height];
            playerOne = new Player(false, Coin.X);
            playerTwo = (i_NumberOfPlayers == 2 ) ? new Player(false, Coin.O) : new Player(true, Coin.O);
            
        }
        
    }


    public enum Coin
    {
        X, 
        O, 
        Null
    };
}
