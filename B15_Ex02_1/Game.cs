using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B15_Ex02_1
{
    class Game
    {
        public Game()
        {
            // first player always human
            string playerOneName = setName();

            int numberOfPlayers = setNumberOfPlayers();

            string playerTwoName = (numberOfPlayers == 2) ? setName() : "Comp";

            int size = setSize();

            Board b = new Board(size, numberOfPlayers, playerOneName, playerTwoName);

            Drawer.drawBoard(b);
            Console.ReadLine();
        }

        private static int setSize()
        {
            // default
            int size = 6;
            bool flag = true;
            while (flag)
            {
                Console.Write("Please insert size of the board: ");
                size = int.Parse(Console.ReadLine());
                bool isValidSize = size == 8 || size == 6;
                if (!isValidSize)
                {
                    Console.WriteLine("Invalid Input! try again...");
                }
                else
                {
                    flag = false;
                }
            }

            return size;
        }

        /// <summary>
        /// Set player's name
        /// </summary>
        private static string setName()
        {
            string name = " ";
            bool flag = true;

            while (flag)
            {
                Console.Write("Please insert your name: ");
                if (Console.ReadLine().Equals(name))
                {
                    Console.WriteLine("Invalid Name. try again");
                }
                else
                {
                    flag = false;
                }
            }

            return name;
        }

        private static int setNumberOfPlayers()
        {
            Console.Write("Please insert number of players: ");
            bool flag = true;

            // default
            int numberOfPlayers = 1;

            while (flag)
            {
                numberOfPlayers = int.Parse(Console.ReadLine());
                bool isValidInput = numberOfPlayers == 2 || numberOfPlayers == 1;

                if (!isValidInput)
                {
                    Console.WriteLine("Invalid Input! Only one or two players allowed");
                }
                else
                {
                    flag = false;
                }
            }
            if (numberOfPlayers == 2)
            {
                Console.WriteLine("Hello Player2 ");
            }
            return numberOfPlayers;
        }
    }
}
