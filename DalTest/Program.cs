// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System.Drawing;
using System.Security.Cryptography.X509Certificates;

//public enum OPTIONS { EXIT = '0', PRODUCT = '1', ORDER = '2', ORDERITEM = '3' };

class Program
{
    //public enum OPTIONS {EXIT = '0', PRODUCT = '1', ORDER = '2', ORDERITEM = '3' };
    static void Main(string[] args)
    {
        int choice;
        Console.WriteLine(" Input: O - Exit , 1 - Product , 2 - Order, 3 - Order Item");
        choice = Console.Read();
        while (choice != 0)
        {
            try
            {
                switch (choice)
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
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex);
                Console.WriteLine(" Input : O - Exit , 1 - Product , 2 - Order, 3 - Order Item");
                choice = Console.Read();
            }
        }
    }
    public static void subProuduct()
    {
        Console.WriteLine(" Input : a - Add a product, b - Show a product by ID, c -  Show an array of products, d- Update a product , e - Delete a product");
        char choice;
        choice = (char)Console.Read();
            switch (choice)
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