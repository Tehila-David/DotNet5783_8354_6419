using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage0
{
    partial class Program
    {
        static int Main (string[]args)
        {
            Welcome6419();
            Welcome8354();

        Console.ReadKey();
            return 0;
        }
         static partial void Welcome8354();
        private static void Welcome6419()
        {
            Console.WriteLine("Enter your name:");
            string username = Console.ReadLine();
            Console.WriteLine($"{username}, welcome to my first Console application");
        }
    }
}
