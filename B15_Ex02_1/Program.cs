using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B15_Ex02_1
{
    class Program
    {
        public static void Main()
        {
            int size = 0;
            int players = 0;
            Console.WriteLine("Insert number of squares:");
            string name = Console.ReadLine();
            if (name != null)
            {
                size = int.Parse(name);
            }
            Console.WriteLine("Insert number of players(1/2):");
            string number = Console.ReadLine();
            if (name != null)
            {
                players = int.Parse(name);
            }

            Board b = new Board(size, players);

            Console.ReadLine();

        }
    }
}
