﻿using DalApi;
using Dal;
using DO;

//namespace Dal;


public class Program

{
    static DalApi.IDal dal = DalApi.Factory.Get()!;    //static IDal dal = new DalList();


    static void Main(string[] arg)
    {
        int choice;
        Console.WriteLine(" Input: 0 - Exit , 1 - Product , 2 - Order, 3 - Order Item");
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
                Category.TryParse(Console.ReadLine().ToUpper(), out NewCategory);
                Console.WriteLine("Enter product Price:");
                double.TryParse(Console.ReadLine(), out NewPrice);
                Console.WriteLine("Enter product instock:");
                int.TryParse(Console.ReadLine(), out NewInstock);

                Product product = new Product ///Creating the new product with the dtails taht the user entered
                {

                    Name = NewName,
                    ID = NewID,
                    Price = NewPrice,
                    Category = NewCategory,
                    InStock = NewInstock
                };
                int id;
                id = dal.Product.Add(product); //Adding the product to the array
                Console.WriteLine(id);
                break;

            case 'b': ///show single product
                Console.WriteLine("Enter product ID:");
                int idForItem;
                int.TryParse(Console.ReadLine(), out idForItem);
                Product singleProduct1 = dal.Product.GetById(idForItem);
                // sending the requested id to the function so that it can locate the product
                Console.WriteLine(singleProduct1);
                break;

            case 'c':// print  array
                ///going through the array and printing the products
                foreach (var item in dal.Product.GetAll())
                {
                    Console.WriteLine(item);
                }
                break;

            case 'd':// update product
                ///asking the user to enter the details of the product to be updated
                Console.WriteLine("Enter product ID:");
                int.TryParse(Console.ReadLine(), out NewID);
                Product singleP = dal.Product.GetById(NewID);
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
                    Name = NewName,
                    ID = NewID,
                    Price = NewPrice,
                    Category = NewCategory,
                    InStock = NewInstock
                };
                dal.Product.Update(product2); ///updating the product
                break;

            case 'e'://delete product
                int delID;
                Console.WriteLine("Enter product ID:");
                int.TryParse(Console.ReadLine(), out delID);
                dal.Product.Delete(delID); ///deleting the product with the ID the user entered
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
        string CustomerName1;
        string CustomerEmail1;
        string CustomerAddress1;


        Console.WriteLine(" Input : a - Add a order, b - Show a order by ID, c -  Show an array of orders, d- Update a order , e - Delete a order");
        char choice;
        char.TryParse(Console.ReadLine(), out choice);
        switch (choice)
        {
            case 'a': ///add order

                      /// Asking the user to enter the details for the new order
                Console.WriteLine("Enter Customer ID:");
                int.TryParse(Console.ReadLine(), out orderID1);
                Console.WriteLine("Enter Customer Name:");
                CustomerName1 = Console.ReadLine();
                Console.WriteLine("Enter Customer Email:");
                CustomerEmail1 = Console.ReadLine();
                Console.WriteLine("Enter Customer Address:");
                CustomerAddress1 = Console.ReadLine();

                Order order = new Order
                {
                    ID = orderID1,
                    CustomerName = CustomerName1,
                    CustomerEmail = CustomerEmail1,
                    CustomerAddress = CustomerAddress1,
                    OrderDate = DateTime.MinValue,
                    ShipDate = DateTime.MinValue,
                    DeliveryDate = DateTime.MinValue
                };
                orderID1 = dal.Order.Add(order); //adding the new order to the array of orders
                Console.WriteLine(orderID1);
                break;

            case 'b': ///show single order
                Console.WriteLine("Enter order ID:");
                int.TryParse(Console.ReadLine(), out orderID1);
                Order singleOrder;
                singleOrder = dal.Order.GetById(orderID1);
                /// sending the requested id to the function so that it can locate the order
                Console.WriteLine(singleOrder);
                break;

            case 'c': /// Printing the array of orders
                      ///going through the array and printing the orders
                foreach (var item in dal.Order.GetAll())
                {
                    Console.WriteLine(item);
                }
                break;

            case 'd': ///Updating an order

                      ///Asking the user to enter the details for the order to be updated
                Console.WriteLine("Enter order ID:");
                /// sending the requested id to the function so that it can locate the order
                int.TryParse(Console.ReadLine(), out orderID1);
                Order singleOrder1 = dal.Order.GetById(orderID1);

                Console.WriteLine(singleOrder1);
                Console.WriteLine("Enter Customer Name:");
                CustomerName1 = Console.ReadLine();
                Console.WriteLine("Enter Customer Email:");
                CustomerEmail1 = Console.ReadLine();
                Console.WriteLine("Enter Customer Address:");
                CustomerAddress1 = Console.ReadLine();
                Order order3 = new Order//Creating a new order variable
                {
                    ID = orderID1,
                    CustomerName = CustomerName1,
                    CustomerEmail = CustomerEmail1,
                    CustomerAddress = CustomerAddress1,
                    OrderDate = DateTime.MinValue,
                    ShipDate = DateTime.MinValue,
                    DeliveryDate = DateTime.MinValue
                };
                dal.Order.Update(order3); ///Updating the order
                break;

            case 'e': ///deleting an order
                Console.WriteLine("Enter order ID:");
                int.TryParse(Console.ReadLine(), out orderID1);
                dal.Order.Delete(orderID1);
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
        Console.WriteLine(" Input : a - Add a orderItem, b - Show a orderItem by ID, c -  Show an array of orderItems, d- Update a orderItem , e - Delete a orderItem, f - get order item based on order and product, g - get array of order items based on order");
        char choice;
        char.TryParse(Console.ReadLine(), out choice);
        switch (choice)
        {
            case 'a': ///add order item
                      ///Asking the user to enter the details of the order item to be added
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

                OrderItem orderItem = new OrderItem ///Creating the new order item based on the user input
                {
                    ID = newID,
                    ProductID = newProductID,
                    OrderID = newOrderID1,
                    Price = newPrice1,
                    Amount = newAmount,
                };
                int id = dal.OrderItem.Add(orderItem);
                ///Adding the order item to the Rray of order items
                Console.WriteLine(id);
                break;

            case 'b': ///show single order item
                Console.WriteLine("Enter  ID:");
                int.TryParse(Console.ReadLine(), out newID);
                ///finding the order item based on the id that the user entered
                OrderItem singleOrderItem = dal.OrderItem.GetById(newID);
                Console.WriteLine(singleOrderItem);
                break;

            case 'c':
                ///going through the array and printing the order items
                foreach (var item in dal.OrderItem.GetAll())
                {
                    Console.WriteLine(item);
                }
                break;

            case 'd': ///updating an order item
                Console.WriteLine("Enter  ID:");
                int.TryParse(Console.ReadLine(), out newID);
                OrderItem orderItem1 = dal.OrderItem.GetById(newID);
                Console.WriteLine(orderItem1);

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
                dal.OrderItem.Update(orderItem2); ///updating the requested order  item
                break;

            case 'e': ///deleting an order item
                Console.WriteLine("Enter  ID:");
                int.TryParse(Console.ReadLine(), out newID);
                dal.OrderItem.Delete(newID);
                break;
            case 'f': /// returning order item based on product and order id
                Console.WriteLine("Enter order ID:");
                int.TryParse(Console.ReadLine(), out newOrderID1);
                Console.WriteLine("Enter product ID:");
                int.TryParse(Console.ReadLine(), out newProductID);
                /// Finding the order item with the same order and product id as the one the user entered
                OrderItem? orderItemFound = dal.OrderItem.GetAll(item => item?.OrderID == newOrderID1 && item?.ProductID == newProductID).FirstOrDefault();
                Console.WriteLine(orderItemFound);
                break;
            case 'g': ///returning array with order items based on order id
                Console.WriteLine("Enter order ID:");
                int.TryParse(Console.ReadLine(), out newOrderID1);
                ///Printing the order items with the order id corresponding to the user input
                foreach (var item in dal.OrderItem.GetAll(item=> item?.OrderID==newOrderID1))
                {
                    Console.WriteLine(item);
                }
                break;
            default:   ///Wrong input
                Console.WriteLine("ERROR");
                break;
        }
    }
}
