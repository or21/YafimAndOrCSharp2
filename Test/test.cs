using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Test
    {
        public static void Main()
        {
            char c = 'C';
            int num = (int) MyEnum.B;
            Console.WriteLine(num);
            Console.ReadLine();
        }

        public enum MyEnum
        {
            A = 0,
            B,
            C,
            D
        }
    }
}
