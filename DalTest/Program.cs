// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System.Drawing;
using System.Security.Cryptography.X509Certificates;


class Program
{
    static void Main()
    {
        int choose;
        Console.WriteLine(" Input : O - Exit , 1 - Product , 2 - Order, 3 - Order Item");
        choose = Console.Read();
        while (choose != 0)
        {
            try
            {
                switch (choose)
                {
                    case 0:
                        return;
                    case 1:
                        subProuduct();
                        break;
                    case 2:
                        subOrder();
                        break;
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
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex);
            }

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

                break;
            case 'c':
                 break;
            case 'd':
                
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
        Console.WriteLine(" Input : a - Add an object, b - Show an object by ID, c -  Show an array of object, d- Update an object , e - Delete an object");
        char choose;
        choose = (char)Console.Read();
        switch (choose)
        {
            case 'a':

                return;
            case 'b':

                break;
            case 'c':
                 break;
            case 'd':
               
                break;
            case 'e':
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }
    public static void subOrderItem()
    {
        Console.WriteLine(" Input : a - Add an object, b - Show an object by ID, c -  Show an array of object, d- Update an object , e - Delete an object");
        char choose;
        choose = (char)Console.Read();
        switch (choose)
        {
            case 'a':

                return;
            case 'b':

                break;
            case 'c':
                break;
            case 'd':
               
                break;
            case 'e':
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }

}