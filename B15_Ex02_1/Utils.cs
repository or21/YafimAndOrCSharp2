using System;
using System.Collections.Generic;

namespace B15_Ex02_1
{
    /// <summary>
    /// This class holds Othello game logic
    /// </summary>
    internal class Utils
    {
        /// <summary>
        ///  8 directions for possible player movements
        /// </summary>
        private static readonly int[,] sr_DirectionsArrayForMakeMove = { { 0, -1 }, { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 } };

        /// <summary>
        /// Clone instance of GameManager
        /// </summary>
        /// <param name="i_GameManagerToClone">Instance to clone</param>
        /// <param name="i_Player">Current player</param>
        /// <returns>Clone of io_GameManagerToClone</returns>
        private static GameManager cloneGameManager(GameManager i_GameManagerToClone, Player i_Player)
        {
            GameManager clonedGameManager = new GameManager(i_GameManagerToClone.Size, 1, i_Player.Name, "Comp", false);
            for (int i = 0; i < i_GameManagerToClone.Size; i++)
            {
                for (int j = 0; j < i_GameManagerToClone.Size; j++)
                {
                    clonedGameManager[i, j] = i_GameManagerToClone[i, j];
                }
            }

            return clonedGameManager;
        }

        /// <summary>
        /// Clone instance of Player
        /// </summary>
        /// <param name="i_PlayerToClone">Instance to clone</param>
        /// <returns>Clone of io_PlayerToClone</returns>
        private static Player clonePlayer(Player i_PlayerToClone)
        {
            int size = i_PlayerToClone.BoardSize;

            Player clonedPlayer = new Player(true, i_PlayerToClone.ShapeCoin, i_PlayerToClone.Name, size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    clonedPlayer[i, j] = i_PlayerToClone[i, j];
                }
            }

            foreach (Coord c in i_PlayerToClone.PossibleMovesCoordinates)
            {
                clonedPlayer.PossibleMovesCoordinates.Add(c);
            }

            return clonedPlayer;
        }

        /// <summary>
        /// Place coin in given coordinate, flip the relevant opponent coins and update the relevant valid moves for current player
        /// </summary>
        /// <param name="io_GameManager">Current state of the game</param>
        /// <param name="i_Player">Current player</param>
        /// <param name="i_NewX">X coordinate</param>
        /// <param name="i_NewY">Y coordinate</param>
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

            // Update valid moves for player
            UpadteAvailableMoves(io_GameManager, ref i_Player);
        }

        /// <summary>
        /// Get move for the computer based on best result after checking all possible moves available.
        /// </summary>
        /// <param name="i_CurrentGameState">Current game state</param>
        /// <param name="i_Player">Current player</param>
        /// <param name="o_X">x Coordinate</param>
        /// <param name="o_Y">y Coordinate</param>
        public static void GetAiMove(GameManager i_CurrentGameState, Player i_Player, out int o_X, out int o_Y)
        {
            Player tempPlayer = clonePlayer(i_Player);
            GameManager tempGameManager = cloneGameManager(i_CurrentGameState, i_Player);

            int maxMovesSoFar = 0;
            int availableMovesForCurrentStep;

            List<Coord> bestResultsArray = new List<Coord>();

            foreach (Coord coordinate in i_Player.PossibleMovesCoordinates)
            {
                int tempX = coordinate.x;
                int tempY = coordinate.y;

                MakeMove(ref tempGameManager, tempPlayer, tempX, tempY);
                availableMovesForCurrentStep = tempPlayer.PossibleMovesCoordinates.Count;

                if (availableMovesForCurrentStep == maxMovesSoFar)
                {
                    bestResultsArray.Add(coordinate);
                }

                else if (availableMovesForCurrentStep > maxMovesSoFar)
                {
                    maxMovesSoFar = availableMovesForCurrentStep;
                    bestResultsArray.Clear();
                    bestResultsArray.Add(coordinate);
                }

                tempPlayer = clonePlayer(i_Player);
                tempGameManager = cloneGameManager(i_CurrentGameState, i_Player);
            }

            getRandomCoord(bestResultsArray, out o_X, out o_Y);
        }
        
        /// <summary>
        /// Pick random coordinate from List
        /// </summary>
        /// <param name="i_CoordinateArray">Array of coordinates</param>
        /// <param name="o_X">x Coordinate</param>
        /// <param name="o_Y">y Coordinate</param>
        private static void getRandomCoord(List<Coord> i_CoordinateArray, out int o_X, out int o_Y)
        {
            
            Random rnd = new Random();
            int i = rnd.Next(i_CoordinateArray.Count);
            o_X = i_CoordinateArray[i].x;
            o_Y = i_CoordinateArray[i].y;

            i_CoordinateArray.Clear();
        }

        /// <summary>
        /// Gets opponent coin shape
        /// </summary>
        /// <param name="i_Player">Player to check</param>
        /// <returns>Coin shape</returns>
        private static Coin getOpponentCoin(Player i_Player)
        {
            Coin opponentCoin = Coin.O;
            if (i_Player.ShapeCoin == Coin.O)
            {
                opponentCoin = Coin.X;
            }

            return opponentCoin;
        }

        /// <summary>
        /// Parse alphabetical value to integer
        /// </summary>
        /// <param name="i_InputValue">Value to parse</param>
        /// <returns>Parsed value</returns>
        public static int ParseValuesToInt(char i_InputValue)
        {
            const int v_Unicode = 65;
            int inputAsInt = i_InputValue;
            return inputAsInt - v_Unicode;
        }

        /// <summary>
        /// If the player make a move that requires coin flipping - make the flipping. Check for all 8 directions.
        /// </summary>
        /// <param name="io_GameManager"></param>
        /// <param name="i_OrigX"></param>
        /// <param name="i_OrigY"></param>
        /// <param name="i_DirectionX"></param>
        /// <param name="i_DirectionY"></param>
        /// <param name="i_CurrentPlayer"></param>
        private static void checkMove(ref GameManager io_GameManager, int i_OrigX, int i_OrigY, int i_DirectionX, int i_DirectionY, Player i_CurrentPlayer)
        {
            // check how much squares in current direction until the end of the game board.
            int numberOfIterations = numOfSquaresToCheckInDirection(i_OrigX, i_OrigY, i_DirectionX, i_DirectionY, io_GameManager);
            Coin opponentCoin = getOpponentCoin(i_CurrentPlayer);
            int numberOfCoinsToFlip = 0;

            for (int i = 1; i < numberOfIterations + 1; i++)
            {
                // if found player coin - flip coins from new position to it. Otherwise - count the number of coins to flip.
                bool isOpponentCoin = io_GameManager[i_OrigX + (i_DirectionX * i), i_OrigY + (i_DirectionY * i)] == opponentCoin;
                if (!isOpponentCoin)
                {
                    // checks that the coin is not Null. If not - make the flipping.
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

        /// <summary>
        /// Flip coins in the given direction.
        /// </summary>
        /// <param name="i_OrigX"></param>
        /// <param name="i_OrigY"></param>
        /// <param name="i_DirectionX"></param>
        /// <param name="i_DirectionY"></param>
        /// <param name="io_GameManager"></param>
        /// <param name="i_CurrentPlayer"></param>
        /// <param name="i_NumberOfCoinsToFlip"></param>
        private static void flipCoinsInRange(int i_OrigX, int i_OrigY, int i_DirectionX, int i_DirectionY, ref GameManager io_GameManager, Player i_CurrentPlayer, int i_NumberOfCoinsToFlip)
        {
            for (int i = 1; i < i_NumberOfCoinsToFlip + 1; i++)
            {
                io_GameManager[i_OrigX + (i_DirectionX * i), i_OrigY + (i_DirectionY * i)] = i_CurrentPlayer.ShapeCoin;
            }
        }

        /// <summary>
        /// Calculates the number of squares until the end of the game board in a given direction.
        /// </summary>
        /// <param name="i_OrigX"></param>
        /// <param name="i_OrigY"></param>
        /// <param name="i_OffsetX"></param>
        /// <param name="i_OffsetY"></param>
        /// <param name="i_GameManager"></param>
        /// <returns></returns>
        private static int numOfSquaresToCheckInDirection(int i_OrigX, int i_OrigY, int i_OffsetX, int i_OffsetY, GameManager i_GameManager)
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
                    numberOfIterations = Math.Min(i_OrigX, i_GameManager.Size - i_OrigY - 1);
                    break;
                case eDirection.Right:
                    numberOfIterations = i_GameManager.Size - i_OrigY - 1;
                    break;
                case eDirection.DownRight:
                    numberOfIterations = Math.Min(i_GameManager.Size - i_OrigX - 1, i_GameManager.Size - i_OrigY - 1);
                    break;
                case eDirection.Down:
                    numberOfIterations = i_GameManager.Size - i_OrigX - 1;
                    break;
                case eDirection.DownLeft:
                    numberOfIterations = Math.Min(i_GameManager.Size - i_OrigX - 1, i_OrigY);
                    break;
            }

            return numberOfIterations;
        }

        /// <summary>
        /// Returns the direction of the move to a given offsets.
        /// </summary>
        /// <param name="i_OffsetX"></param>
        /// <param name="i_OffsetY"></param>
        /// <returns></returns>
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

            // if direction[i] is true - returns the represnted enum.
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

        /// <summary>
        /// Updates available moves for a given player according to current state of game manager.
        /// </summary>
        /// <param name="i_GameManager"></param>
        /// <param name="io_Player"></param>
        public static void UpadteAvailableMoves(GameManager i_GameManager, ref Player io_Player)
        {
            Coin opponentCoin = getOpponentCoin(io_Player);
            io_Player.AvailableMoves = 0;

            io_Player.PossibleMovesCoordinates.Clear();
            clearPlayerAvailableMoves(i_GameManager, ref io_Player);

            // for each squre - if there's opponent coin there, check for available moves around it.
            for (int i = 0; i < i_GameManager.Size; i++)
            {
                for (int j = 0; j < i_GameManager.Size; j++)
                {
                    bool sqareWithOpponentCoin = i_GameManager[i, j] == opponentCoin;
                    if (sqareWithOpponentCoin)
                    {
                        // For each direction where there is Null coin check the opposit direction for a possible move.
                        bool[] directions = createDirectionArray(i_GameManager, i, j, Coin.Null);
                        checkAllDirections(i, j, directions, i_GameManager, ref io_Player);
                    }
                }
            }
        }

        /// <summary>
        /// Clear the player available moves matrix.
        /// </summary>
        /// <param name="i_GameManager"></param>
        /// <param name="io_Player"></param>
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

        /// <summary>
        /// For each direction check if it's available(not edge of the game board) and if there is i_Coin coin.
        /// </summary>
        /// <param name="i_GameManager"></param>
        /// <param name="i_StartX"></param>
        /// <param name="i_StartJ"></param>
        /// <param name="i_Coin"></param>
        /// <returns></returns>
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

        /// <summary>
        /// For each direction check if it's availabe (true in the given input) and if so, 
        /// runs the method that check if it can be a move for current player.
        /// In the end, update the available moves matrix and also the amount of available moves and the list of them.
        /// </summary>
        /// <param name="i_StartX"></param>
        /// <param name="i_StartY"></param>
        /// <param name="i_Directions"></param>
        /// <param name="i_GameManager"></param>
        /// <param name="io_Player"></param>
        private static void checkAllDirections(int i_StartX, int i_StartY, bool[] i_Directions, GameManager i_GameManager, ref Player io_Player)
        {
            for (int i = 0; i < 8; i++)
            {
                // if valid direction:
                if (i_Directions[i])
                {
                    // check if this can be a move for the player.
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

        /// <summary>
        /// Recives a direction to check and a player. check if somwhere in this direction there is a player coin.
        /// If so - return true - this is a valid direction that can be a move.
        /// If not - return false.
        /// </summary>
        /// <param name="i_StartX"></param>
        /// <param name="i_StartY"></param>
        /// <param name="i_DirectionX"></param>
        /// <param name="i_DirectionY"></param>
        /// <param name="i_GameManager"></param>
        /// <param name="i_CurrentPlayer"></param>
        /// <returns></returns>
        private static bool checkIfMyCoinInEnd(int i_StartX, int i_StartY, int i_DirectionX, int i_DirectionY, GameManager i_GameManager, Player i_CurrentPlayer)
        {
            Coin opponentCoin = getOpponentCoin(i_CurrentPlayer);
            int numberOfIterations = numOfSquaresToCheckInDirection(i_StartX, i_StartY, i_DirectionX, i_DirectionY, i_GameManager);
            bool isMyCoinInEnd = false;

            // numberOfIterations - number of squares in this direction until the edge of the game board.
            for (int i = 0; i < numberOfIterations + 1; i++)
            {
                Coin squareCoin = i_GameManager[i_StartX + (i_DirectionX * i), i_StartY + (i_DirectionY * i)];
                bool isMyCoin = squareCoin == i_CurrentPlayer.ShapeCoin;
                bool isOppCoin = squareCoin == opponentCoin;

                // if found a player coin - finish. 
                if (isMyCoin)
                {
                    isMyCoinInEnd = true;
                    break;
                }

                // if found Null coin - finish. this is not a valid move.
                if (!isOppCoin)
                {
                    break;
                }
            }

            return isMyCoinInEnd;
        }

        /// <summary>
        /// Counts the points in the game for the given players.
        /// </summary>
        /// <param name="i_GameManager">Current state of the game</param>
        /// <param name="io_PlayerA">Player One</param>
        /// <param name="io_PlayerB">Player Two</param>
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

        /// <summary>
        /// Holds the possible directions for a squre starts from the left side.
        /// </summary>
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
