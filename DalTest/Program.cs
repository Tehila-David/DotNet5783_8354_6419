using DO;
using System;

namespace Dal;


class Program
{
  static DalProduct dalProduct = new DalProduct(); // variable to access dalProduct
  static DalOrder dalOrder = new DalOrder(); // variable to access dalOrder
  static DalOrderItem dalOrderItem = new DalOrderItem(); // variable to access dalOrderItem

    static void Main()
    {

         int choice;
        Console.WriteLine(" Input: O - Exit , 1 - Product , 2 - Order, 3 - Order Item");
        int.TryParse(Console.ReadLine(),out choice);
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
                        subOrderItem();
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

            Console.WriteLine(" Input : O - Exit , 1 - Product , 2 - Order, 3 - Order Item");
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
        Console.WriteLine(" Input : a - Add a product, b - Show a product by ID, c -  Show an array of products, d- Update a product , e - Delete a product");
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
                Category.TryParse(Console.ReadLine(), out NewCategory);
                Console.WriteLine("Enter product Price:");
                double.TryParse(Console.ReadLine(), out NewPrice);
                Console.WriteLine("Enter product instock:");
                int.TryParse(Console.ReadLine(),out NewInstock);

                Product product = new Product ///Creating the new product with the dtails taht the user entered
                {
                    
                    Name = NewName,
                    ID= NewID,  
                    Price = NewPrice,
                    Category = NewCategory,
                    InStock = NewInstock
                };
                int id;
                id= dalProduct.addProduct(product); //Adding the product to the array
                break;

            case 'b': ///show single product
                Console.WriteLine("Enter product ID:");
                int idForItem;
                int.TryParse(Console.ReadLine(),out idForItem);
                Product singleProduct;
                singleProduct = dalProduct.getSingleProduct(idForItem); // sending the requested id to the function so that it can locate the product
                Console.WriteLine(singleProduct);
                break;

            case 'c':// print  array
                ///going through the array and printing the products
                foreach (var item in dalProduct.getListOfProducts())
                {
                    Console.WriteLine(item);
                }
                break;

            case 'd':// update product
                ///asking the user to enter the details of the product to be updated
                Console.WriteLine("Enter product name:");
                NewName = Console.ReadLine();
                Console.WriteLine("Enter product ID:");
                int.TryParse(Console.ReadLine(), out NewID);
                Console.WriteLine("Enter product Category:");
                Category.TryParse(Console.ReadLine(), out NewCategory);
                Console.WriteLine("Enter product Price:");
                double.TryParse(Console.ReadLine(), out NewPrice);
                Console.WriteLine("Enter product instock:");
                int.TryParse(Console.ReadLine(), out NewInstock);

                ///creating the product using the details that the user has entered
                Product product2 = new Product
                {
                    Name = NewName,
                    ID = NewID,
                    Price = NewPrice,
                    Category = NewCategory,
                    InStock = NewInstock
                };
                dalProduct.updateProduct(product2); ///updating the product
                break;

            case 'e'://delete product
                int delID;
                Console.WriteLine("Enter product ID:");
                int.TryParse(Console.ReadLine(), out delID);
                dalProduct.deleteProduct(delID); ///deleting the product with the ID the user entered
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
        int orderID;
        Console.WriteLine(" Input : a - Add a order, b - Show a order by ID, c -  Show an array of orders, d- Update a order , e - Delete a order");
        char choice;
        char.TryParse(Console.ReadLine(), out choice);
        switch (choice)
        {
            case 'a': ///add order
                Order order = new Order();
                /// Asking the user to enter the details for the new order
                Console.WriteLine("Enter Customer Name:");
                order.CustomerName = Console.ReadLine();
                Console.WriteLine("Enter Customer Email:");
                order.CustomerEmail = Console.ReadLine();
                Console.WriteLine("Enter Customer Address:");
                order.CustomerAddress= Console.ReadLine();
                order.OrderDate = DateTime.MinValue;
                order.ShipDate= DateTime.MinValue;
                order.DeliveryDate= DateTime.MinValue;
                dalOrder.addOrder(order); //adding the new order to the array of orders
                break;

            case 'b': ///show single order
                Console.WriteLine("Enter order ID:");
                int.TryParse(Console.ReadLine(),out orderID);
                Order singleOrder;
                singleOrder = dalOrder.getSingleOrder(orderID);
                /// sending the requested id to the function so that it can locate the product
                Console.WriteLine(singleOrder);
                break;

            case 'c': /// Printing the array of orders
                ///going through the array and printing the orders
                foreach (var item in dalOrder.getArrayOfOrders())
                {
                    Console.WriteLine(item);
                }
                break;
            case 'd': ///Updating an order
                Order order2 = new Order(); //Creating a new order variable
                ///Asking the user to enter the details for the order to be updated
                Console.WriteLine("Enter order ID:");
                int.TryParse(Console.ReadLine(), out orderID);
                order2.ID = orderID;
                Console.WriteLine("Enter Customer Name:");
                order2.CustomerName = Console.ReadLine();
                Console.WriteLine("Enter Customer Email:");
                order2.CustomerEmail = Console.ReadLine();
                Console.WriteLine("Enter Customer Address:");
                order2.CustomerAddress = Console.ReadLine();
                order2.OrderDate = DateTime.MinValue;
                order2.ShipDate = DateTime.MinValue;
                order2.DeliveryDate = DateTime.MinValue;
                dalOrder.updateOrder(order2); ///Updating the order
                break;
            case 'e': ///deleting an order
                Console.WriteLine("Enter order ID:");
                int.TryParse(Console.ReadLine(), out orderID);
                dalOrder.deleteOrder( orderID);
                break;
            default: //Wrong input
                Console.WriteLine("ERROR");
                break;
        }
    }
    /// <summary>
    /// Function that implements CRUD for order item
    /// </summary>
    public static void subOrderItem()
    {
        int newID;
        int newProductID;
        int newOrderID1;
        double newPrice1;
        int newAmount;
        Console.WriteLine(" Input : a - Add a orderItem, b - Show a orderItem by ID, c -  Show an array of orderItems, d- Update a orderItem , e - Delete a orderItem");
        char choice;
        char.TryParse(Console.ReadLine(), out choice);
        switch (choice)
        {
            case 'a': ///add order item
                ///Asking the user to enter the details of the order item to be added
                Console.WriteLine("Enter  ID:");
                int.TryParse(Console.ReadLine(), out newID);
                Console.WriteLine("Enter Product ID:");
                int.TryParse(Console.ReadLine(),out newProductID);
                Console.WriteLine("Enter Order ID:");
                int.TryParse(Console.ReadLine(),out newOrderID1);
                Console.WriteLine("Enter  Price:");
                double.TryParse(Console.ReadLine(), out newPrice1);
                Console.WriteLine("Enter  amount:");
                int.TryParse(Console.ReadLine(),out newAmount);

                OrderItem orderItem = new OrderItem ///Creating the new order item based on the user input
                {
                    ID = newID,
                    ProductID= newProductID,
                    OrderID = newOrderID1,
                    Price = newPrice1,
                    Amount = newAmount,
                };
                int  id = dalOrderItem.addOrderItem(orderItem); 
                ///Adding the order item to the Rray of order items
                break;

            case 'b': ///show single order item
                Console.WriteLine("Enter  ID:");
                int.TryParse(Console.ReadLine(), out newID);
                ///finding the order item based on the id that the user entered
                OrderItem singleOrderItem = dalOrderItem.getSingleOrderItem(newID);
                Console.WriteLine(singleOrderItem);
                break;

            case 'c':
                ///going through the array and printing the order items
                foreach (var item in dalOrderItem.getArrayOfOrderItem())
                {
                    Console.WriteLine(item);
                }
                break;

            case 'd': ///updating an order item
                Console.WriteLine("Enter  ID:");
                int.TryParse(Console.ReadLine(), out newID);
                Console.WriteLine("Enter Product ID:");
                int.TryParse(Console.ReadLine(), out newProductID);
                Console.WriteLine("Enter Order ID:");
                int.TryParse(Console.ReadLine(), out newOrderID1);
                Console.WriteLine("Enter  Price:");
                double.TryParse(Console.ReadLine(), out newPrice1);
                Console.WriteLine("Enter  amount:");
                int.TryParse(Console.ReadLine(), out newAmount);
                ///Creating the order item that the user wnts to ipdate
                OrderItem orderItem2 = new OrderItem
                {
                    ID = newID,
                    ProductID = newProductID,
                    OrderID = newOrderID1,
                    Price = newPrice1,
                    Amount = newAmount,
                };
                dalOrderItem.updateOrderItem(orderItem2); ///updating the requested order  item
                break;

            case 'e': ///deleting an order item
                Console.WriteLine("Enter  ID:");
                int.TryParse(Console.ReadLine(), out newID);
                dalOrderItem.deleteOrderItem(newID);
                break;
            default:   ///Wrong input
                Console.WriteLine("ERROR");
                break;
        }
    }
}
