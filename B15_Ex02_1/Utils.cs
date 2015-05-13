using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B15_Ex02_1
{
    class Utils
    {
        public static void MakeMove(Coin[,] i_Board, Player i_Player, int i_NewX, int i_NewY)
        {
            //
        }

        public static int ParseValuesToInt(char i_InputValue)
        {
            const int v_Unicode = 65;
            int[] charsAsInts = {0, 1, 2, 3, 4, 5, 6, 7, 8};
            int inputAsInt = (int) i_InputValue;
            return charsAsInts[(inputAsInt - v_Unicode)];
        }


    }
}
