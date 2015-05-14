using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B15_Ex02_1
{
    class Utils
    {
        public static void MakeMove(Board i_Board, Player i_Player, int i_NewX, int i_NewY)
        {
            i_Board[i_NewX, i_NewY] = i_Player.ShapeCoin;

            // find coins to flip
            int positionOfNextSameCoin = i_NewX + 1;
            for (int i = 1; i < i_Board.Size - i_NewX; i++)
            {
                bool isSameCoin = i_Board[i_NewX, i_NewY] == i_Board[i_NewX + i, i_NewY];
                positionOfNextSameCoin++;
                if (isSameCoin)
                {
                    break;
                }

                bool isEndOfBoard = i + i_NewX == i_Board.Size;
                if (!isEndOfBoard)
                {
                    bool isInXaxis = true;
                    //flip(i_Board, i_NewX, positionOfNextSameCoin, i_NewY, isInXaxis, i_Player);
                }
            }

            // update valid moves for player
        }

        private Coin opponentCoin(Player i_Player)
        {
            if (i_Player.ShapeCoin == Coin.O)
            {
                return Coin.X;
            }
            return Coin.O;
        }

        public static int ParseValuesToInt(char i_InputValue)
        {
            const int v_Unicode = 65;
            int inputAsInt = (int)i_InputValue;
            return (inputAsInt - v_Unicode);
        }
    }
}
