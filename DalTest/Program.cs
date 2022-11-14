// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System.Drawing;
using System.Security.Cryptography.X509Certificates;

//public enum CHOICE { EXIT,PRODUCT, ORDER,ORDER_ITEM};
class Program
{
    static void Main(string[] args)
    {
        int choose;
        Console.WriteLine(" Input : O - Exit , 1 - Product , 2 - Order, 3 - Order Item");
        choose = Console.Read();
        while (choose != 0)
        {

            switch (choose)
            {
                case 0:
                    return;
                case 1:
                    subProuduct();
                    break;
                case 2:
                    subOrder(); break;
                case 3:
                    subOrderItem();
                    break;

                default:
                    Console.WriteLine("ERROR");
                    break;

            }
            Console.WriteLine(" Input : O - Exit , 1 - Product , 2 - Order, 3 - Order Item");
            choose = Console.Read();
        }
    }
    public static void subProuduct()
    {
        Console.WriteLine(" Input : a - Add an object, b - Show an object by ID, c -  Show an array of object, d- Update an object , e - Delete an object");
        char choose;
        choose = (char)Console.Read();
        switch (choose)
        {
            case 'a':

                return;
            case 'b':
                subProuduct();
                break;
            case 'c':
                subOrder(); break;
            case 'd':
                subOrderItem();
                break;
            case 'e':
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }
        public static void subOrder()
        {

        }
        public static void subOrderItem()
        {

        }
    }