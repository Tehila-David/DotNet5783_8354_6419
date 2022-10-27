// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome6419();
            welcome8354();
            Console.ReadKey();
        }
        static partial void welcome8354(); 
        private static void welcome6419()
        {
            Console.WriteLine("Enter your Name: ");
            string username = Console.ReadLine();
            Console.WriteLine("{0} Welcome to my first console application", username);
        }
    }
}