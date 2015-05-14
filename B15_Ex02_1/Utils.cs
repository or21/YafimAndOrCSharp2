using System;

namespace B15_Ex02_1
{
    internal class Utils
    {
        public static void MakeMove(Board i_Board, Player i_Player, int i_NewX, int i_NewY)
        {
            i_Board[i_NewX, i_NewY] = i_Player.ShapeCoin;

            Coin opponentCoin = getOpponentCoin(i_Player);
            bool left = i_Board[i_NewX, i_NewY - 1] == opponentCoin;
            bool leftUp = i_Board[i_NewX - 1, i_NewY - 1] == opponentCoin;
            bool up = i_Board[i_NewX - 1, i_NewY] == opponentCoin;
            bool upRight = i_Board[i_NewX - 1, i_NewY + 1] == opponentCoin;
            bool right = i_Board[i_NewX, i_NewY + 1] == opponentCoin;
            bool downRight = i_Board[i_NewX + 1, i_NewY + 1] == opponentCoin;
            bool down = i_Board[i_NewX + 1, i_NewY] == opponentCoin;
            bool downLeft = i_Board[i_NewX + 1, i_NewY - 1] == opponentCoin;

            if (left)
            {
                checkMove(i_Board, i_NewX, i_NewY, 0, -1, i_Player);
            }
            if (leftUp)
            {
                checkMove(i_Board, i_NewX, i_NewY, -1, -1, i_Player);
            }
            if (up)
            {
                checkMove(i_Board, i_NewX, i_NewY, -1, 0, i_Player);
            }
            if (upRight)
            {
                checkMove(i_Board, i_NewX, i_NewY, -1, 1, i_Player);
            }
            if (right)
            {
                checkMove(i_Board, i_NewX, i_NewY, 0, 1, i_Player);
            }
            if (downRight)
            {
                checkMove(i_Board, i_NewX, i_NewY, 1, 1, i_Player);
            }
            if (down)
            {
                checkMove(i_Board, i_NewX, i_NewY, 1, 0, i_Player);
            }
            if (downLeft)
            {
                checkMove(i_Board, i_NewX, i_NewY, 1, -1, i_Player);
            }

            // update valid moves for each player
            UpadteAvailableMoves(i_Board, ref i_Player);
        }

        private static Coin getOpponentCoin(Player i_Player)
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
            int inputAsInt = i_InputValue;
            return (inputAsInt - v_Unicode);
        }

        private static void checkMove(Board i_Board, int i_OrigX, int i_OrigY, int i_DirectionX, int i_DirectionY,
            Player i_CurrentPlayer)
        {
            int numberOfIterations = numOfIterationsInDirection(i_OrigX, i_OrigY, i_DirectionX, i_DirectionY, i_Board);
            Coin opponentCoin = getOpponentCoin(i_CurrentPlayer);
            int numberOfCoinsToFlip = 0;

            for (int i = 1; i < numberOfIterations; i++)
            {
                bool isOpponentCoin = i_Board[i_OrigX + i_DirectionX, i_OrigY + i_DirectionY] == opponentCoin;
                if (!isOpponentCoin)
                {
                    bool isCurrentPlayerCoin = i_Board[i_OrigX + i_DirectionX, i_OrigY + i_DirectionY] ==
                                               i_CurrentPlayer.ShapeCoin;
                    if (isCurrentPlayerCoin)
                    {
                        flipCoinsInRange(i_OrigX, i_OrigY, -i_DirectionX, -i_DirectionY, i_Board, i_CurrentPlayer,
                            numberOfCoinsToFlip);
                        break;
                    }
                    else
                    {
                        numberOfCoinsToFlip++;
                    }

                }

            }
        }

        private static void flipCoinsInRange(int i_OrigX, int i_OrigY, int i_DirectionX, int i_DirectionY, Board i_Board,
            Player i_CurrentPlayer, int i_NumberOfCoinsToFlip)
        {
            for (int i = 0; i < i_NumberOfCoinsToFlip; i++)
            {
                i_Board[i_OrigX + i_DirectionX, i_OrigY + i_DirectionY] = i_CurrentPlayer.ShapeCoin;
            }
        }


        private static int numOfIterationsInDirection(int i_OrigX, int i_OrigY, int i_OffsetX, int i_OffsetY, Board i_Board)
        {
            if (i_OffsetX == 0)
            {
                return (i_OffsetY > 0 ? i_Board.Size - i_OrigY - 1 : i_OrigY);
            }
            if (i_OffsetY == 0)
            {
                return (i_OffsetX > 0 ? i_Board.Size - i_OrigX - 1 : i_OrigX);
            }
            if (i_OffsetX > 0 && i_OffsetY > 0)
            {
                return Math.Min(i_Board.Size - i_OrigX, i_Board.Size - i_OrigY);
            }
            if (i_OffsetX < 0 && i_OffsetY > 0)
            {
                return Math.Min(i_OrigX, i_Board.Size - i_OrigY);
            }
            if (i_OffsetX > 0 && i_OffsetY < 0)
            {
                return Math.Min(i_Board.Size - i_OrigX, i_OrigY);
            }
            if (i_OffsetX < 0 && i_OffsetY < 0)
            {
                return Math.Min(i_OrigX, i_OrigY);
            }
            return 0;
        }

        public static void UpadteAvailableMoves(Board i_Board, ref Player i_Player)
        {
            Coin opponentCoin = getOpponentCoin(i_Player);
            bool[] directions = new bool[8];

            for (int i = 0; i < i_Board.Size; i++)
            {
                for (int j = 0; j < i_Board.Size; j++)
                {
                    bool sqareWithOpponentCoin = i_Board[i, j] == opponentCoin;
                    if (sqareWithOpponentCoin)
                    {
                        directions[0] = i_Board[i, j - 1] == Coin.Null;
                        directions[1] = i_Board[i - 1, j - 1] == Coin.Null;
                        directions[2] = i_Board[i - 1, j] == Coin.Null;
                        directions[3] = i_Board[i - 1, j + 1] == Coin.Null;
                        directions[4] = i_Board[i, j + 1] == Coin.Null;
                        directions[5] = i_Board[i + 1, j + 1] == Coin.Null;
                        directions[6] = i_Board[i + 1, j] == Coin.Null;
                        directions[7] = i_Board[i + 1, j - 1] == Coin.Null;

                        checkAllDirections(i, j, directions, i_Board, ref i_Player);
                    }
                }
            }
        }

        private static void checkAllDirections(int i_StartX, int i_StartY, bool[] i_Directions, Board i_Board, ref Player i_Player)
        {
            bool canBeMove;
            if (i_Directions[0])
            {
                canBeMove = checkIfMyCoinInEnd(i_StartX, i_StartY, 0, 1, i_Board, i_Player);
                if (canBeMove)
                {
                    i_Player[i_StartX, i_StartY - 1] = true;
                }
            }
            if (i_Directions[1])
            {
                canBeMove = checkIfMyCoinInEnd(i_StartX, i_StartY, 1, 1, i_Board, i_Player);
                if (canBeMove)
                {
                    i_Player[i_StartX - 1, i_StartY - 1] = true;
                }
            }
            if (i_Directions[2])
            {
                canBeMove = checkIfMyCoinInEnd(i_StartX, i_StartY, 1, 0, i_Board, i_Player);
                if (canBeMove)
                {
                    i_Player[i_StartX - 1, i_StartY] = true;
                }
            }
            if (i_Directions[3])
            {
                canBeMove = checkIfMyCoinInEnd(i_StartX, i_StartY, 1, -1, i_Board, i_Player);
                if (canBeMove)
                {
                    i_Player[i_StartX - 1, i_StartY + 1] = true;
                }
            }
            if (i_Directions[4])
            {
                canBeMove = checkIfMyCoinInEnd(i_StartX, i_StartY, 0, -1, i_Board, i_Player);
                if (canBeMove)
                {
                    i_Player[i_StartX, i_StartY - 1] = true;
                }
            }
            if (i_Directions[5])
            {
                canBeMove = checkIfMyCoinInEnd(i_StartX, i_StartY, -1, -1, i_Board, i_Player);
                if (canBeMove)
                {
                    i_Player[i_StartX + 1, i_StartY + 1] = true;
                }
            }
            if (i_Directions[6])
            {
                canBeMove = checkIfMyCoinInEnd(i_StartX, i_StartY, -1, 0, i_Board, i_Player);
                if (canBeMove)
                {
                    i_Player[i_StartX + 1, i_StartY] = true;
                }
            }
            if (i_Directions[7])
            {
                canBeMove = checkIfMyCoinInEnd(i_StartX, i_StartY, -1, 1, i_Board, i_Player);
                if (canBeMove)
                {
                    i_Player[i_StartX + 1, i_StartY - 1] = true;
                }
            }
        }

        private static bool checkIfMyCoinInEnd(int i_StartX, int i_StartY, int i_DirectionX, int i_DirectionY, Board i_Board,
        Player i_CurrentPlayer)
        {
            Coin opponentCoin = getOpponentCoin(i_CurrentPlayer);
            int numberOfIterations = numOfIterationsInDirection(i_StartX, i_StartY, i_DirectionX, i_DirectionY, i_Board);

            for (int i = 0; i < numberOfIterations; i++)
            {
                Coin squareCoin = i_Board[i_StartX + i_DirectionX, i_StartY + i_DirectionY];
                bool isMyCoin = squareCoin == i_CurrentPlayer.ShapeCoin;
                bool isOppCoin = squareCoin == opponentCoin;

                if (isMyCoin)
                {
                    return true;
                }
                if (!isOppCoin)
                {
                    return false;
                }
            }

            return false;
        }
    }
}
