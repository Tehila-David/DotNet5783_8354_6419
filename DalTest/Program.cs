//using DO;
//using Dal;
//using DalProduct;
internal class Program
{
    static void Main(string[] args)
    {
        //DalProduct product;
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
            case 'a': ///add product
                Product product = new Product();
                Console.WriteLine("Enter product name:");
                product.Name = Console.ReadLine();
                Console.WriteLine("Enter product Category:");
                product.Category = Console.ReadLine();
                Console.WriteLine("Enter product Price:");
                product.Price = Console.ReadLine();
                Console.WriteLine("Enter product amount:");
                product.InStock = Console.ReadLine();
                int id = DalProduct.addProduct(product);
                break;

                case 'b': ///show single product
                Console.WriteLine("Enter product ID:");
                int idForItem = Console.ReadLine();
                Product singleProduct = getSingleProduct(idForItem);

                break;
                case 'c': ///show list of products
                     break;
                case 'd': ///update product
                    break; 
                case 'e': ///delete product
                    break;
                default:
                    Console.WriteLine("ERROR");
                    break;
            }
    }
        public static void subOrder()
        {
        Console.WriteLine(" Input : a - Add a product, b - Show a product by ID, c -  Show an array of products, d- Update a product , e - Delete a product");
        char choice;
        choice = (char)Console.Read();
        switch (choice)
        {
            case 'a': ///add order

                break;
            case 'b': ///show single order
                break;
            case 'c': ///show list of orders
                break;
            case 'd': ///update order
                break;
            case 'e': ///delete order
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }
        public static void subOrderItem()
        {
        Console.WriteLine(" Input : a - Add a product, b - Show a product by ID, c -  Show an array of products, d- Update a product , e - Delete a product");
        char choice;
        choice = (char)Console.Read();
        switch (choice)
        {
            case 'a': ///add order item

                break;
            case 'b': ///show single order item
                break;
            case 'c': ///show list of order items
                break;
            case 'd': ///update item
                break;
            case 'e': ///delete item
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }
    
}