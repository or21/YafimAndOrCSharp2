using System;

namespace B15_Ex02_1
{
    /// <summary>
    /// Initializes a new game with given parameters.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Controls and manages the running game
        /// </summary>
        private GameManager gameManager;

        /// <summary>
        /// Initializes a new instance of the Game class.
        /// </summary>
        public Game()
        {
            // first player always human.
            string playerOneName = setName();
            int numberOfPlayers = setNumberOfPlayers();
            string playerTwoName = (numberOfPlayers == 2) ? setName() : "Comp";
            int size = setSize();
            bool runGame = true;
            while (runGame)
            {
                gameManager = new GameManager(size, numberOfPlayers, playerOneName, playerTwoName, runGame);
                gameManager.RunGame();
                runGame = playAgain();
            }

            Console.WriteLine("Thank You for playing...");
        }

        /// <summary>
        /// Get valid board game from the user.
        /// </summary>
        /// <returns>8 or 6</returns>
        private static int setSize()
        {
            // Default size
            int size = 6;

            // Some flags for input validation
            bool isValidInput = false;
            bool isValidSize = false;

            Console.Write("Please insert size of the board[6/8]: ");

            while (!isValidInput)
            {
                string inputFromUser = Console.ReadLine();
                bool inputIsNumber = int.TryParse(inputFromUser, out size);

                if (inputIsNumber)
                {
                    isValidSize = size == 8 || size == 6;
                }

                if (!inputIsNumber || !isValidSize)
                {
                    Console.WriteLine("Invalid Input! try again...");
                }
                else
                {
                    isValidInput = true;
                }
            }

            return size;
        }

        /// <summary>
        /// Get player's name
        /// </summary>
        /// <returns>Valid name</returns>
        private static string setName()
        {
            string name = null;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.Write("Please insert your name: ");
                string nameFromUser = Console.ReadLine();
                bool containsLetters = nameFromUser != null && stringContainsLetterOrDigits(nameFromUser);
                if (!containsLetters)
                {
                    Console.WriteLine("Invalid Name. try again");
                }
                else
                {
                    name = nameFromUser;
                    isValidInput = true;
                }
            }

            return name;
        }

        /// <summary>
        /// Name must contain only letters
        /// </summary>
        /// <param name="i_InputString">String to check</param>
        /// <returns>true if only letter Otherwise false</returns>
        private static bool stringContainsLetterOrDigits(string i_InputString)
        {
            foreach (char c in i_InputString)
            {
                if (!char.IsWhiteSpace(c))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// How many players will be playing.
        /// </summary>
        /// <returns>1 or 2</returns>
        private static int setNumberOfPlayers()
        {
            // Default
            int numberOfPlayers = 1;

            // Some flags for input validation
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.Write("Please insert number of players[1/2]: ");
                string inputFromUser = Console.ReadLine();
                bool inputIsNumber  = int.TryParse(inputFromUser, out numberOfPlayers);
                isValidInput = isValidNumberOfPlayers(inputIsNumber, numberOfPlayers);
            }

            if (numberOfPlayers == 2)
            {
                Console.WriteLine("Hello Player2! ");
            }

            return numberOfPlayers;
        }

        private static bool isValidNumberOfPlayers(bool i_InputIsNumber, int i_NumberOfPlayers)
        {
            bool isValidInput = false;
            bool isValidNumberOfPlayers = false;
            if (i_InputIsNumber)
            {
                isValidNumberOfPlayers = i_NumberOfPlayers == 2 || i_NumberOfPlayers == 1;
            }

            if (!i_InputIsNumber || !isValidNumberOfPlayers)
            {
                Console.WriteLine("Invalid Input! Only one or two players allowed");
            }
            else
            {
                isValidInput = true;
            }
            return isValidInput;
        }

        /// <summary>
        /// Ask the user if he wants play this game again
        /// </summary>
        /// <returns>true for play again. false for end game.</returns>
        private static bool playAgain()
        {
            Console.Write("Would you like to play again? [Y/N]: ");

            bool runGame = false;
            bool isValidInput = false;
            while (!isValidInput)
            {
                string playAgain = Console.ReadLine();
                
                // Valid answer
                bool isValidAnswer = playAgain != null && (playAgain.ToUpper().Equals("N") || playAgain.ToUpper().Equals("Y"));

                if (!isValidAnswer)
                {
                    Console.WriteLine("Invalid Input! Try again... ");
                }
                else
                {
                    runGame = playAgain.ToUpper().Equals("Y");
                    isValidInput = true;
                }
            }

            return runGame;
        }
    }
}
