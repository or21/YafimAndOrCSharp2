using System;
using System.Collections.Generic;

namespace B15_Ex02_1
{
    internal class Utils
    {
        private static readonly int[,] sr_DirectionsArrayForMakeMove = { { 0, -1 }, { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 } };

        public static void MakeMove(ref GameManager io_GameManager, Player i_Player, int i_NewX, int i_NewY)
        {
            io_GameManager[i_NewX, i_NewY] = i_Player.ShapeCoin;
            i_Player[i_NewX, i_NewY] = false;

            Coin opponentCoin = getOpponentCoin(i_Player);
            bool[] directions = createDirectionArray(io_GameManager, i_NewX, i_NewY, opponentCoin);
            for (int i = 0; i < 8; i++)
            {
                if (directions[i])
                {
                    checkMove(ref io_GameManager, i_NewX, i_NewY, sr_DirectionsArrayForMakeMove[i, 0], sr_DirectionsArrayForMakeMove[i, 1], i_Player);
                }
            }

            // update valid moves for each player
            UpadteAvailableMoves(io_GameManager, ref i_Player);
        }

        public static void getAIMove(ref GameManager i_GameManager, Player i_Player, out int x, out int y)
        {
            // Random choose!
            Random rnd = new Random();
            int i = rnd.Next(i_Player.PossibleMovesCoordinates.Count);
            x = i_Player.PossibleMovesCoordinates[i].x;
            y = i_Player.PossibleMovesCoordinates[i].y;
            Console.WriteLine("({0},{1})", x,y);

            /*
            Player tempPlayer = i_Player;
            GameManager tempGM = i_GameManager;
            int availableMovesForCurrentStep = 0;
            int maxProf = 0;

            int tempX, tempY;
            x = 0;
            y = 0;

            foreach(string coordinate in possibleMovesCoordinates)
            {
                tempX = coordinate[0] - '0';
                tempY = coordinate[1] - '0';

                MakeMove(ref tempGM, tempPlayer, tempX, tempY);
                availableMovesForCurrentStep = tempPlayer.AvailableMoves;

                if (availableMovesForCurrentStep > maxProf)
                {
                    x = tempX;
                    y = tempY;
                    maxProf = availableMovesForCurrentStep;
                }

                tempGM = i_GameManager;
                tempPlayer = i_Player;
            
            }
            */
        }

        private static Coin getOpponentCoin(Player i_Player)
        {
            Coin opponentCoin = Coin.O;
            if (i_Player.ShapeCoin == Coin.O)
            {
                opponentCoin = Coin.X;
            }

            return opponentCoin;
        }

        public static int ParseValuesToInt(char i_InputValue)
        {
            const int v_Unicode = 65;
            int inputAsInt = i_InputValue;
            return inputAsInt - v_Unicode;
        }

        private static void checkMove(ref GameManager io_GameManager, int i_OrigX, int i_OrigY, int i_DirectionX, int i_DirectionY, Player i_CurrentPlayer)
        {
            int numberOfIterations = numOfIterationsInDirection(i_OrigX, i_OrigY, i_DirectionX, i_DirectionY, io_GameManager);
            Coin opponentCoin = getOpponentCoin(i_CurrentPlayer);
            int numberOfCoinsToFlip = 0;

            for (int i = 1; i < numberOfIterations; i++)
            {
                bool isOpponentCoin = io_GameManager[i_OrigX + (i_DirectionX * i), i_OrigY + (i_DirectionY * i)] == opponentCoin;
                if (!isOpponentCoin)
                {
                    bool isCurrentPlayerCoin = io_GameManager[i_OrigX + (i_DirectionX * i), i_OrigY + (i_DirectionY * i)] == i_CurrentPlayer.ShapeCoin;
                    if (isCurrentPlayerCoin)
                    {
                        numberOfCoinsToFlip++;
                        flipCoinsInRange(i_OrigX, i_OrigY, i_DirectionX, i_DirectionY, ref io_GameManager, i_CurrentPlayer, numberOfCoinsToFlip);
                        break;
                    }
                }

                numberOfCoinsToFlip++;
            }
        }

        private static void flipCoinsInRange(int i_OrigX, int i_OrigY, int i_DirectionX, int i_DirectionY, ref GameManager io_GameManager, Player i_CurrentPlayer, int i_NumberOfCoinsToFlip)
        {
            for (int i = 1; i < i_NumberOfCoinsToFlip + 1; i++)
            {
                io_GameManager[i_OrigX + (i_DirectionX * i), i_OrigY + (i_DirectionY * i)] = i_CurrentPlayer.ShapeCoin;
            }
        }

        private static int numOfIterationsInDirection(int i_OrigX, int i_OrigY, int i_OffsetX, int i_OffsetY, GameManager i_GameManager)
        {
            int numberOfIterations = 0;
            eDirection direction = calcDirection(i_OffsetX, i_OffsetY);
            switch (direction)
            {
                case eDirection.Left:
                    numberOfIterations = i_OrigY;
                    break;
                case eDirection.UpLeft:
                    numberOfIterations = Math.Min(i_OrigX, i_OrigY);
                    break;
                case eDirection.Up:
                    numberOfIterations = i_OrigX;
                    break;
                case eDirection.UpRight:
                    numberOfIterations = Math.Min(i_OrigX, i_GameManager.Size - i_OrigY);
                    break;
                case eDirection.Right:
                    numberOfIterations = i_GameManager.Size - i_OrigY - 1;
                    break;
                case eDirection.DownRight:
                    numberOfIterations = Math.Min(i_GameManager.Size - i_OrigX, i_GameManager.Size - i_OrigY);
                    break;
                case eDirection.Down:
                    numberOfIterations = i_GameManager.Size - i_OrigX - 1;
                    break;
                case eDirection.DownLeft:
                    numberOfIterations = Math.Min(i_GameManager.Size - i_OrigX, i_OrigY);
                    break;
            }

            return numberOfIterations;
        }

        private static eDirection calcDirection(int i_OffsetX, int i_OffsetY)
        {
            eDirection direction = eDirection.Left;
            bool[] directions = new bool[8];
            directions[(int)eDirection.Left] = i_OffsetX == 0 && i_OffsetY < 0;
            directions[(int)eDirection.UpLeft] = i_OffsetX < 0 && i_OffsetY < 0;
            directions[(int)eDirection.Up] = i_OffsetX < 0 && i_OffsetY == 0;
            directions[(int)eDirection.UpRight] = i_OffsetX < 0 && i_OffsetY > 0;
            directions[(int)eDirection.Right] = i_OffsetX == 0 && i_OffsetY > 0;
            directions[(int)eDirection.DownRight] = i_OffsetX > 0 && i_OffsetY > 0;
            directions[(int)eDirection.Down] = i_OffsetX > 0 && i_OffsetY == 0;
            directions[(int)eDirection.DownLeft] = i_OffsetX > 0 && i_OffsetY < 0;

            for (int i = 0; i < 8; i++)
            {
                if (directions[i])
                {
                    direction = (eDirection)i;
                    break;
                }
            }

            return direction;
        }

        public static void UpadteAvailableMoves(GameManager i_GameManager, ref Player io_Player)
        {
            Coin opponentCoin = getOpponentCoin(io_Player);
            io_Player.AvailableMoves = 0;

            io_Player.PossibleMovesCoordinates.Clear();

            clearPlayerAvailableMoves(i_GameManager, ref io_Player);

            for (int i = 0; i < i_GameManager.Size; i++)
            {
                for (int j = 0; j < i_GameManager.Size; j++)
                {
                    bool sqareWithOpponentCoin = i_GameManager[i, j] == opponentCoin;
                    if (sqareWithOpponentCoin)
                    {
                        bool[] directions = createDirectionArray(i_GameManager, i, j, Coin.Null);
                        checkAllDirections(i, j, directions, i_GameManager, ref io_Player);
                    }
                }
            }
        }

        private static void clearPlayerAvailableMoves(GameManager i_GameManager, ref Player io_Player)
        {
            for (int i = 0; i < i_GameManager.Size; i++)
            {
                for (int j = 0; j < i_GameManager.Size; j++)
                {
                    io_Player[i, j] = false;
                }
            }
        }

        private static bool[] createDirectionArray(GameManager i_GameManager, int i_StartX, int i_StartJ, Coin i_Coin)
        {
            bool[] directions = new bool[8];
            bool leftEdge = i_StartJ == 0;
            bool upEdge = i_StartX == 0;
            bool rightEdge = i_StartJ == i_GameManager.Size - 1;
            bool downEdge = i_StartX == i_GameManager.Size - 1;

            if (!leftEdge)
            {
                directions[(int)eDirection.Left] = i_GameManager[i_StartX, i_StartJ - 1] == i_Coin;
                if (!upEdge)
                {
                    directions[(int)eDirection.Up] = i_GameManager[i_StartX - 1, i_StartJ] == i_Coin;
                    directions[(int)eDirection.UpLeft] = i_GameManager[i_StartX - 1, i_StartJ - 1] == i_Coin;
                }

                if (!downEdge)
                {
                    directions[(int)eDirection.Down] = i_GameManager[i_StartX + 1, i_StartJ] == i_Coin;
                    directions[(int)eDirection.DownLeft] = i_GameManager[i_StartX + 1, i_StartJ - 1] == i_Coin;
                }
            }

            if (!rightEdge)
            {
                directions[(int)eDirection.Right] = i_GameManager[i_StartX, i_StartJ + 1] == i_Coin;
                if (!upEdge)
                {
                    directions[(int)eDirection.Up] = i_GameManager[i_StartX - 1, i_StartJ] == i_Coin;
                    directions[(int)eDirection.UpRight] = i_GameManager[i_StartX - 1, i_StartJ + 1] == i_Coin;
                }

                if (!downEdge)
                {
                    directions[(int)eDirection.Down] = i_GameManager[i_StartX + 1, i_StartJ] == i_Coin;
                    directions[(int)eDirection.DownRight] = i_GameManager[i_StartX + 1, i_StartJ + 1] == i_Coin;
                }
            }

            return directions;
        }

        private static void checkAllDirections(int i_StartX, int i_StartY, bool[] i_Directions, GameManager i_GameManager, ref Player io_Player)
        {
            for (int i = 0; i < 8; i++)
            {
                if (i_Directions[i])
                {
                    bool canBeMove = checkIfMyCoinInEnd(i_StartX, i_StartY, -1 * sr_DirectionsArrayForMakeMove[i, 0], -1 * sr_DirectionsArrayForMakeMove[i, 1], i_GameManager, io_Player);
                    if (canBeMove)
                    {
                        io_Player[i_StartX + sr_DirectionsArrayForMakeMove[i, 0], i_StartY + sr_DirectionsArrayForMakeMove[i, 1]] = true;
                        io_Player.AvailableMoves++;

                        Coord coord = new Coord();
                        coord.x = i_StartX + sr_DirectionsArrayForMakeMove[i, 0];
                        coord.y = i_StartY + sr_DirectionsArrayForMakeMove[i, 1];
                        io_Player.PossibleMovesCoordinates.Add(coord);
                    }
                }
            }
        }

        private static bool checkIfMyCoinInEnd(int i_StartX, int i_StartY, int i_DirectionX, int i_DirectionY, GameManager i_GameManager, Player i_CurrentPlayer)
        {
            Coin opponentCoin = getOpponentCoin(i_CurrentPlayer);
            int numberOfIterations = numOfIterationsInDirection(i_StartX, i_StartY, i_DirectionX, i_DirectionY, i_GameManager);
            bool isMyCoinInEnd = false;

            for (int i = 0; i < numberOfIterations; i++)
            {
                Coin squareCoin = i_GameManager[i_StartX + (i_DirectionX * i), i_StartY + (i_DirectionY * i)];
                bool isMyCoin = squareCoin == i_CurrentPlayer.ShapeCoin;
                bool isOppCoin = squareCoin == opponentCoin;

                if (isMyCoin)
                {
                    isMyCoinInEnd = true;
                    break;
                }

                if (!isOppCoin)
                {
                    break;
                }
            }

            return isMyCoinInEnd;
        }

        public static void CountPoints(GameManager i_GameManager, ref Player io_PlayerA, ref Player io_PlayerB)
        {
            io_PlayerA.Points = 0;
            io_PlayerB.Points = 0;
            for (int i = 0; i < i_GameManager.Size; i++)
            {
                for (int j = 0; j < i_GameManager.Size; j++)
                {
                    Coin squareCoin = i_GameManager[i, j];
                    bool squareIsO = Coin.O == squareCoin;
                    bool squareIsX = Coin.X == squareCoin;
                    if (squareIsX)
                    {
                        io_PlayerA.Points++;
                    }

                    if (squareIsO)
                    {
                        io_PlayerB.Points++;
                    }
                }
            }
        }

        private enum eDirection
        {
            Left = 0,
            UpLeft,
            Up,
            UpRight,
            Right,
            DownRight,
            Down,
            DownLeft
        }
    }
}
