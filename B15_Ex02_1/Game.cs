using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private GameManager m_gameManager;

        /// <summary>
        /// Initializes a new instance of the Game class.
        /// </summary>
        public Game()
        {
            /// first player always human.
            string playerOneName = "yafim"; /// setName();
            int numberOfPlayers = 1; //setNumberOfPlayers();
            string playerTwoName = "Or"; //(numberOfPlayers == 2) ? setName() : "Comp";
            int size = 6;//setSize();
            bool runGame = true;
            while (runGame)
            {
                m_gameManager = new GameManager(size, numberOfPlayers, playerOneName, "Comp", runGame);

                Drawer.DrawBoard(m_gameManager);

                m_gameManager.RunGame();

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

            Console.Write("Please insert size of the board[8/6]: ");

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
            bool isValidNumberOfPlayers = false;

            Console.Write("Please insert number of players[1/2]: ");

            while (!isValidInput)
            {
                string inputFromUser = Console.ReadLine();
                bool inputIsNumber  = int.TryParse(inputFromUser, out numberOfPlayers);

                if (inputIsNumber)
                {
                    isValidNumberOfPlayers = numberOfPlayers == 2 || numberOfPlayers == 1;
                }
                
                if (!inputIsNumber || !isValidNumberOfPlayers)
                {
                    Console.WriteLine("Invalid Input! Only one or two players allowed");
                }
                else
                {
                    isValidInput = true;
                }
            }

            if (numberOfPlayers == 2)
            {
                Console.WriteLine("Hello Player2! ");
            }

            return numberOfPlayers;
        }

        /// <summary>
        /// Ask the user if he wants play this game again
        /// </summary>
        /// <returns>true for play again. false for end game.</returns>
        private bool playAgain()
        {
            Console.Write("Would you like to play again? [Y/N]: ");

            bool runGame = false;
            bool flag = true;
            while (flag)
            {
                string playAgain = Console.ReadLine();
                
                // Valid input
                bool isValid = playAgain.ToUpper().Equals("N") || playAgain.ToUpper().Equals("Y");

                if (!isValid)
                {
                    Console.WriteLine("Invalid Input! Try again... ");
                }
                else
                {
                    runGame = playAgain.ToUpper().Equals("Y") ? true : false;
                    flag = false;
                }
            }

            return runGame;
        }
    }
}
