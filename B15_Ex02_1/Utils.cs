using System;

namespace B15_Ex02_1
{
    internal class Utils
    {
        private const int Left = 0;
        private const int UpLeft = 1;
        private const int Up = 2;
        private const int UpRight = 3;
        private const int Right = 4;
        private const int DownRight = 5;
        private const int Down = 6;
        private const int DownLeft = 7;

        public static void MakeMove(GameManager i_GameManager, Player i_Player, int i_NewX, int i_NewY)
        {
            i_GameManager[i_NewX, i_NewY] = i_Player.ShapeCoin;

            Coin opponentCoin = getOpponentCoin(i_Player);
            bool[] directions = createDirectionArray(i_GameManager, i_NewX, i_NewY, opponentCoin);
            if (directions[Left])
            {
                checkMove(i_GameManager, i_NewX, i_NewY, 0, -1, i_Player);
            }

            if (directions[UpLeft])
            {
                checkMove(i_GameManager, i_NewX, i_NewY, -1, -1, i_Player);
            }

            if (directions[Up])
            {
                checkMove(i_GameManager, i_NewX, i_NewY, -1, 0, i_Player);
            }

            if (directions[UpRight])
            {
                checkMove(i_GameManager, i_NewX, i_NewY, -1, 1, i_Player);
            }

            if (directions[Right])
            {
                checkMove(i_GameManager, i_NewX, i_NewY, 0, 1, i_Player);
            }

            if (directions[DownRight])
            {
                checkMove(i_GameManager, i_NewX, i_NewY, 1, 1, i_Player);
            }

            if (directions[Down])
            {
                checkMove(i_GameManager, i_NewX, i_NewY, 1, 0, i_Player);
            }

            if (directions[DownLeft])
            {
                checkMove(i_GameManager, i_NewX, i_NewY, 1, -1, i_Player);
            }

            // update valid moves for each player
            UpadteAvailableMoves(i_GameManager, ref i_Player);
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
            return inputAsInt - v_Unicode;
        }

        private static void checkMove(GameManager i_GameManager, int i_OrigX, int i_OrigY, int i_DirectionX, int i_DirectionY, Player i_CurrentPlayer)
        {
            int numberOfIterations = numOfIterationsInDirection(i_OrigX, i_OrigY, i_DirectionX, i_DirectionY, i_GameManager);
            Coin opponentCoin = getOpponentCoin(i_CurrentPlayer);
            int numberOfCoinsToFlip = 0;

            for (int i = 1; i < numberOfIterations; i++)
            {
                bool isOpponentCoin = i_GameManager[i_OrigX + i_DirectionX, i_OrigY + i_DirectionY] == opponentCoin;
                if (!isOpponentCoin)
                {
                    bool isCurrentPlayerCoin = i_GameManager[i_OrigX + i_DirectionX, i_OrigY + i_DirectionY] == i_CurrentPlayer.ShapeCoin;
                    if (isCurrentPlayerCoin)
                    {
                        flipCoinsInRange(i_OrigX, i_OrigY, -i_DirectionX, -i_DirectionY, i_GameManager, i_CurrentPlayer, numberOfCoinsToFlip);
                        break;
                    }

                    numberOfCoinsToFlip++;
                }
            }
        }

        private static void flipCoinsInRange(int i_OrigX, int i_OrigY, int i_DirectionX, int i_DirectionY, GameManager i_GameManager, Player i_CurrentPlayer, int i_NumberOfCoinsToFlip)
        {
            for (int i = 0; i < i_NumberOfCoinsToFlip; i++)
            {
                i_GameManager[i_OrigX + i_DirectionX, i_OrigY + i_DirectionY] = i_CurrentPlayer.ShapeCoin;
            }
        }

        private static int numOfIterationsInDirection(int i_OrigX, int i_OrigY, int i_OffsetX, int i_OffsetY, GameManager i_GameManager)
        {
            if (i_OffsetX == 0)
            {
                return i_OffsetY > 0 ? i_GameManager.Size - i_OrigY - 1 : i_OrigY;
            }

            if (i_OffsetY == 0)
            {
                return i_OffsetX > 0 ? i_GameManager.Size - i_OrigX - 1 : i_OrigX;
            }

            if (i_OffsetX > 0 && i_OffsetY > 0)
            {
                return Math.Min(i_GameManager.Size - i_OrigX, i_GameManager.Size - i_OrigY);
            }

            if (i_OffsetX < 0 && i_OffsetY > 0)
            {
                return Math.Min(i_OrigX, i_GameManager.Size - i_OrigY);
            }

            if (i_OffsetX > 0 && i_OffsetY < 0)
            {
                return Math.Min(i_GameManager.Size - i_OrigX, i_OrigY);
            }

            if (i_OffsetX < 0 && i_OffsetY < 0)
            {
                return Math.Min(i_OrigX, i_OrigY);
            }

            return 0;
        }

        public static void UpadteAvailableMoves(GameManager i_GameManager, ref Player i_Player)
        {
            Coin opponentCoin = getOpponentCoin(i_Player);

            for (int i = 0; i < i_GameManager.Size; i++)
            {
                for (int j = 0; j < i_GameManager.Size; j++)
                {
                    bool sqareWithOpponentCoin = i_GameManager[i, j] == opponentCoin;
                    if (sqareWithOpponentCoin)
                    {
                        bool[] directions = createDirectionArray(i_GameManager, i, j, Coin.Null);
                        checkAllDirections(i, j, directions, i_GameManager, ref i_Player);
                    }
                }
            }
        }

        private static bool[] createDirectionArray(GameManager i_GameManager, int i_StartX, int i_StartJ, Coin i_Coin)
        {
            bool[] directions = new bool[8];
            bool leftEdge = i_StartX == 0;
            bool upEdge = i_StartJ == 0;
            bool rightEdge = i_StartX == i_GameManager.Size - 1;
            bool downEdge = i_StartJ == i_GameManager.Size - 1;

            if (!leftEdge)
            {
                directions[Left] = i_GameManager[i_StartX, i_StartJ - 1] == i_Coin;
                if (!upEdge)
                {
                    directions[Up] = i_GameManager[i_StartX - 1, i_StartJ] == i_Coin;
                    directions[UpLeft] = i_GameManager[i_StartX - 1, i_StartJ - 1] == i_Coin;
                }

                if (!downEdge)
                {
                    directions[Down] = i_GameManager[i_StartX + 1, i_StartJ] == i_Coin;
                    directions[DownLeft] = i_GameManager[i_StartX + 1, i_StartJ - 1] == i_Coin;
                }
            }

            if (!rightEdge)
            {
                directions[Right] = i_GameManager[i_StartX, i_StartJ + 1] == i_Coin;
                if (!upEdge)
                {
                    directions[Up] = i_GameManager[i_StartX - 1, i_StartJ] == i_Coin;
                    directions[UpRight] = i_GameManager[i_StartX - 1, i_StartJ + 1] == i_Coin;
                }

                if (!downEdge)
                {
                    directions[Down] = i_GameManager[i_StartX + 1, i_StartJ] == i_Coin;
                    directions[DownRight] = i_GameManager[i_StartX + 1, i_StartJ + 1] == i_Coin;
                }
            }
            
            return directions;
        }

        private static void checkAllDirections(int i_StartX, int i_StartY, bool[] i_Directions, GameManager i_GameManager, ref Player i_Player)
        {
            bool canBeMove;
            if (i_Directions[Left])
            {
                canBeMove = checkIfMyCoinInEnd(i_StartX, i_StartY, 0, 1, i_GameManager, i_Player);
                if (canBeMove)
                {
                    i_Player[i_StartX, i_StartY - 1] = true;
                }
            }

            if (i_Directions[UpLeft])
            {
                canBeMove = checkIfMyCoinInEnd(i_StartX, i_StartY, 1, 1, i_GameManager, i_Player);
                if (canBeMove)
                {
                    i_Player[i_StartX - 1, i_StartY - 1] = true;
                }
            }

            if (i_Directions[Up])
            {
                canBeMove = checkIfMyCoinInEnd(i_StartX, i_StartY, 1, 0, i_GameManager, i_Player);
                if (canBeMove)
                {
                    i_Player[i_StartX - 1, i_StartY] = true;
                }
            }

            if (i_Directions[UpRight])
            {
                canBeMove = checkIfMyCoinInEnd(i_StartX, i_StartY, 1, -1, i_GameManager, i_Player);
                if (canBeMove)
                {
                    i_Player[i_StartX - 1, i_StartY + 1] = true;
                }
            }

            if (i_Directions[Right])
            {
                canBeMove = checkIfMyCoinInEnd(i_StartX, i_StartY, 0, -1, i_GameManager, i_Player);
                if (canBeMove)
                {
                    i_Player[i_StartX, i_StartY - 1] = true;
                }
            }

            if (i_Directions[DownRight])
            {
                canBeMove = checkIfMyCoinInEnd(i_StartX, i_StartY, -1, -1, i_GameManager, i_Player);
                if (canBeMove)
                {
                    i_Player[i_StartX + 1, i_StartY + 1] = true;
                }
            }

            if (i_Directions[Down])
            {
                canBeMove = checkIfMyCoinInEnd(i_StartX, i_StartY, -1, 0, i_GameManager, i_Player);
                if (canBeMove)
                {
                    i_Player[i_StartX + 1, i_StartY] = true;
                }
            }

            if (i_Directions[DownLeft])
            {
                canBeMove = checkIfMyCoinInEnd(i_StartX, i_StartY, -1, 1, i_GameManager, i_Player);
                if (canBeMove)
                {
                    i_Player[i_StartX + 1, i_StartY - 1] = true;
                }
            }
        }

        private static bool checkIfMyCoinInEnd(int i_StartX, int i_StartY, int i_DirectionX, int i_DirectionY, GameManager i_GameManager, Player i_CurrentPlayer)
        {
            Coin opponentCoin = getOpponentCoin(i_CurrentPlayer);
            int numberOfIterations = numOfIterationsInDirection(i_StartX, i_StartY, i_DirectionX, i_DirectionY, i_GameManager);

            for (int i = 0; i < numberOfIterations; i++)
            {
                Coin squareCoin = i_GameManager[i_StartX + i_DirectionX, i_StartY + i_DirectionY];
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
