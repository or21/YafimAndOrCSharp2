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

        GameManager gm;

        /// <summary>
        /// Initializes game with user properties
        /// </summary>
        public Game()
        {
            // first player always human.
            string playerOneName = "yafim"; //setName();
            int numberOfPlayers = 1; //setNumberOfPlayers();
       //     string playerTwoName = "Or"; //(numberOfPlayers == 2) ? setName() : "Comp";
            int size = 6; //setSize();
            bool runGame = true;
            while (runGame)
            {
                gm = new GameManager(size, numberOfPlayers, playerOneName, "Comp");

                Drawer.DrawBoard(gm);

                gm.RunGame();

                runGame = playAgain();
            }

            Console.WriteLine("Thank You for playing...");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static int setSize()
        {
            // default
            int size = 6;
            bool isValidInput = false;
            bool isValidSize = false;
            while (!isValidInput)
            {
                Console.Write("Please insert size of the board[8/6]: ");
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
        /// Set player's name
        /// </summary>
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

        private static int setNumberOfPlayers()
        {
            Console.Write("Please insert number of players[1/2]: ");
            bool isValidInput = false;
            bool isValidNumberOfPlayers = false;

            // default
            int numberOfPlayers = 1;

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

        private bool playAgain()
        {
            Console.Write("Would you like to play again? [Y/N]: ");

            bool runGame = false;
            bool flag = true;
            while (flag)
            {
                string playAgain = Console.ReadLine();

                if (playAgain.Length != 1 && playAgain[0] != 'N' && playAgain[0] != 'Y')
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
