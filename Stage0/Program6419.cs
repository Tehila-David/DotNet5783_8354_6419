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
            Console.WriteLine("Enter your name:");
            string  username = Console.ReadLine();
            Console.WriteLine($"{username}, welcome to my first Console application");
            Console.ReadKey();
            return 0;
        }
    }
}
