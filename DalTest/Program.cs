using DO;

using Dal;


class Program
{
    static void Main(string[] arg)
    {

        //DalProduct product;
        int choice;
        Console.WriteLine(" Input: O - Exit , 1 - Product , 2 - Order, 3 - Order Item");
        choice = Console.Read();
        while (choice != 0)
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
            Console.WriteLine(" Input : O - Exit , 1 - Product , 2 - Order, 3 - Order Item");
            choice = Console.Read();
        }
    }
    public static void subProuduct()
    {
        Console.WriteLine(" Input : a - Add a product, b - Show a product by ID, c -  Show an array of products, d- Update a product , e - Delete a product");
        char choice;
        choice = (char)Console.Read();
        switch (choice)
        {
            case 'a': ///add product
                Product product = new Product();
                Console.WriteLine("Enter product name:");
                product.Name = Console.ReadLine();
                Console.WriteLine("Enter product Category:");
                product.Category = Console.Read();
                Console.WriteLine("Enter product Price:");
                product.Price = Console.Read();
                Console.WriteLine("Enter product amount:");
                product.InStock = Console.Read();
                int id;
                id= DalProduct.addProduct(product);
                break;

            case 'b': ///show single product
                Console.WriteLine("Enter product ID:");
                int idForItem = Console.Read();
                Product singleProduct = getSingleProduct(idForItem);

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
