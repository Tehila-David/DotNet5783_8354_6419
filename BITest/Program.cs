
using BlApi;
using BO;

using BlImplementation;

public class Program

{
   static IBl bl = new Bl();
    static void Main(string[] arg)
    {
        int choice;
        Console.WriteLine("Input: 0 - Exit , 1 - Product , 2 - Order, 3 - Cart");
        int.TryParse(Console.ReadLine(), out choice);
        while (choice != 0) ///checking if the user does not want to exit
        {
            try
            {
                switch (choice)
                {
                    case 0: /// If the user chooses to exit
                        return;
                    case 1: /// If the user chooses product
                        subProuduct();
                        break;
                    case 2: /// If the user chooses order
                        subOrder();
                        break;
                    case 3: /// If the user chooses order item
                        subCart();
                        break;

                    default: /// If bthe user chooses any other option
                        Console.WriteLine("ERROR");
                        break;

                }
            }
            catch (Exception ex) // Catching errors found in the functions
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Input : O - Exit , 1 - Product , 2 - Order, 3 - Cart");
            int.TryParse(Console.ReadLine(), out choice);
        }
        return;
    }
    /// <summary>
    /// Fuction that implements CRUD for product
    /// </summary>
    public static void subProuduct()
    {
        string NewName;
        double NewPrice;
        int NewID;
        Category NewCategory;
        int NewInstock;
        Console.WriteLine($@"Input : 
        a - Add a product 
        b - Show a product by ID for manager
        c - Show a product by ID for client 
        d - Show a list of products
        e - Update a product 
        f - Delete a product");
        char choice;
        char.TryParse(Console.ReadLine(), out choice);
        switch (choice)
        {
            case 'a': ///add new product
                Console.WriteLine("Enter product name:");
                NewName = Console.ReadLine();
                Console.WriteLine("Enter product ID:");
                int.TryParse(Console.ReadLine(), out NewID);
                Console.WriteLine("Enter product Category:");
                Category.TryParse(Console.ReadLine().ToUpper(), out NewCategory);
                Console.WriteLine("Enter product Price:");
                double.TryParse(Console.ReadLine(), out NewPrice);
                Console.WriteLine("Enter product instock:");
                int.TryParse(Console.ReadLine(), out NewInstock);

                Product product = new Product ///Creating the new product with the dtails taht the user entered
                {
                    ID = NewID,
                    Name = NewName,
                    Price = NewPrice,
                    Category = NewCategory,
                    InStock = NewInstock
                };
                bl.Product.Add(product);;//Adding the product 
                break;

            case 'b': ///show single product for manager
                Console.WriteLine("Enter product ID:");
                int idForItem;
                int.TryParse(Console.ReadLine(), out idForItem);
                Product singleProduct1 = bl.Product.GetById(idForItem);
                // sending the requested id to the function so that it can locate the product
                Console.WriteLine(singleProduct1);
                break;
            case 'c': ///show single product for client
                Cart myCart = new Cart();
                Console.WriteLine("Enter product ID:");
                int idForItem2;
                int.TryParse(Console.ReadLine(), out idForItem2);
                ProductItem singleProduct2 = bl.Product.GetById1(idForItem2, myCart);
                // sending the requested id to the function so that it can locate the product
                Console.WriteLine(singleProduct2);
                break;
            case 'd':// print  list of products
                ///going through the array and printing the products
                foreach (var item in bl.Product.GetListedProducts())
                {
                    Console.WriteLine(item);
                }
                break;

            case 'e':// update product
                ///asking the user to enter the details of the product to be updated
                Console.WriteLine("Enter product ID:");
                int.TryParse(Console.ReadLine(), out NewID);
                Product singleP = bl.Product.GetById(NewID);
                Console.WriteLine(singleP);

                Console.WriteLine("Enter product name:");
                NewName = Console.ReadLine();
                Console.WriteLine("Enter product Category:");
                Category.TryParse(Console.ReadLine(), out NewCategory);
                Console.WriteLine("Enter product Price:");
                double.TryParse(Console.ReadLine(), out NewPrice);
                Console.WriteLine("Enter product instock:");
                int.TryParse(Console.ReadLine(), out NewInstock);

                ///creating the product using the details that the user has entered
                Product product2 = new Product
                {
                    ID = NewID,
                    Name = NewName,
                    Price = NewPrice,
                    Category = NewCategory,
                    InStock = NewInstock
                };
                bl.Product.Update(product2); ///updating the product
                break;

            case 'f'://delete product
                int delID;
                Console.WriteLine("Enter product ID:");
                int.TryParse(Console.ReadLine(), out delID);
                bl.Product.Delete(delID); ///deleting the product with the ID the user entered
                break;
            default: ///wrong input
                Console.WriteLine("ERROR");
                break;
        }
    }
    /// <summary>
    /// Function that implements CRUD for order
    /// </summary>
    public static void subOrder()
    {
        int orderID1;
       
        Console.WriteLine($@"Input : 
        a - Show a order by ID 
        b - Show a List of orders
        c - Order shipping update
        d - Order delivery update
        e - Order tracking");
        char choice;
        char.TryParse(Console.ReadLine(), out choice);
        switch (choice)
        {
            case 'a': ///Show a order by ID
                Console.WriteLine("Enter Order ID:");
                int.TryParse(Console.ReadLine(), out orderID1);
                Order singleOrder2 = bl.Order.GetByID(orderID1);
                Console.WriteLine(singleOrder2);
                break;

            case 'b': ///Show an List of orders
                foreach (var item in bl.Order.GetListedOrders())
                {
                    Console.WriteLine(item);
                }
                break;
            case 'c': /// Update Order shipping
                Console.WriteLine("Enter order ID:");
                int.TryParse(Console.ReadLine(), out orderID1);
                Order orderArrival = bl.Order.UpdateShipDate(orderID1);
                Console.WriteLine(orderArrival);
                break;

            case 'd': ///Update Order delivery 
                Console.WriteLine("Enter order ID:");
                int.TryParse(Console.ReadLine(), out orderID1);
                Order orderDelivery = bl.Order.UpdateDelivery(orderID1);
                Console.WriteLine(orderDelivery);
                break;

            case 'e': ///Order tracking
                Console.WriteLine("Enter order ID:");
                int.TryParse(Console.ReadLine(), out orderID1);
                OrderTracking orderTracking = bl.Order.followOrder(orderID1);
                Console.WriteLine(orderTracking);
                break;

            default: //Wrong input
                Console.WriteLine("ERROR");
                break;
        }
    }
    /// <summary>
    /// Function that implements CRUD for order item
    /// </summary>
    public static void subCart()
    {
        int newID;
        int newProductID;
        int newOrderID1;
        double newPrice1;
        int newAmount;
        Console.WriteLine($@"Input : 
        a - Add a product to cart 
        b - Update product amount in cart 
        c - Cart confirmation");
        char choice;
        char.TryParse(Console.ReadLine(), out choice);
        switch (choice)
        {
            case 'a': ///Add a product to cart
                Console.WriteLine("Enter  ID:");
                int.TryParse(Console.ReadLine(), out newID);
                Cart myCart = new Cart();
                myCart = bl.Cart.AddProduct(myCart, newID);
                Console.WriteLine(myCart);
                break;

            case 'b': ///Update product amount in cart
                Console.WriteLine("Enter  product ID:");
                int.TryParse(Console.ReadLine(), out newID);
                Console.WriteLine("Enter new product amount:");
                int.TryParse(Console.ReadLine(), out newAmount);
                Cart myCart1 = new Cart();
                myCart1 = bl.Cart.UpdateProductAmount(myCart1, newID, newAmount);
                Console.WriteLine(myCart1);
                break;

            case 'c': ///Cart confirmation
                string customerName, customerEmail, customerAddress;
                Console.WriteLine("Enter  the customer name:");
                customerName = Console.ReadLine();
                Console.WriteLine("Enter the customer email address:");
                customerEmail = Console.ReadLine();
                Console.WriteLine("Enter the customer address:");
                customerAddress = Console.ReadLine();
                Cart myCart2 = new Cart()
                {
                    CustomerName = customerName,
                    CustomerEmail = customerEmail,
                    CustomerAddress = customerAddress
                };
                bl.Cart.CartConfirmation(myCart2);

                break;
            default:   ///Wrong input
                Console.WriteLine("ERROR");
                break;
        }
    }



}