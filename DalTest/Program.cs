using DO;
namespace Dal;


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
                Console.WriteLine(ex.ToString());
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
                product.Category = Console.ReadLine();
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
                Product singleProduct;
                singleProduct = DalProduct.getSingleProduct(idForItem);
                Console.WriteLine(singleProduct);
                break;

            case 'c':
                foreach(Product product1 in DalProduct.getArrayOfProducts())
                {
                    Console.WriteLine(product1);
                }
                break;
            case 'd':
                Product product2 = new Product();
                Console.WriteLine("Enter ID: ");
                product2.ID= Console.Read();
                Console.WriteLine("Enter product name:");
                product2.Name = Console.ReadLine();
                Console.WriteLine("Enter product Category:");
                product2.Category = Console.ReadLine();
                //product2.Category = Enum.TryParse(Console.ReadLine());
                Console.WriteLine("Enter product Price:");
                product2.Price = Console.Read();
                Console.WriteLine("Enter product amount:");
                product2.InStock = Console.Read();

                DalProduct.updateProduct(product2);
                break;
            case 'e':
                Console.WriteLine("Enter ID: ");
                int delID= Console.Read();
                DalProduct.deleteProduct(delID);
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
            case 'a': ///add product
                Product product = new Product();
                Console.WriteLine("Enter product name:");
                product.Name = Console.ReadLine();
                Console.WriteLine("Enter product Category:");
                product.Category = (Category)Console.Read();
                Console.WriteLine("Enter product Price:");
                product.Price = Console.Read();
                Console.WriteLine("Enter product amount:");
                product.InStock = Console.Read();
                int id;
                id = DalProduct.addProduct(product);
                break;

            case 'b': ///show single product
                Console.WriteLine("Enter product ID:");
                int idForItem = Console.Read();
                Product singleProduct;
                singleProduct = DalProduct.getSingleProduct(idForItem);
                Console.WriteLine(singleProduct);
                break;

            case 'c':
                foreach (Product product1 in DalProduct.getArrayOfProducts())
                {
                    Console.WriteLine(product1);
                }
                break;
            case 'd':
                Product product2 = new Product();
                Console.WriteLine("Enter ID: ");
                product2.ID = Console.Read();
                Console.WriteLine("Enter product name:");
                product2.Name = Console.ReadLine();
                Console.WriteLine("Enter product Category:");
                product2.Category = (Category)Console.Read();
                Console.WriteLine("Enter product Price:");
                product2.Price = Console.Read();
                Console.WriteLine("Enter product amount:");
                product2.InStock = Console.Read();

                DalProduct.updateProduct(product2);
                break;
            case 'e':
                Console.WriteLine("Enter ID: ");
                int delID = Console.Read();
                DalProduct.deleteProduct(delID);
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }
    public static void subOrderItem()
    {

    }
}
