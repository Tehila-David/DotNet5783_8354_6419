using DO;
using System;

namespace Dal;


class Program
{
    static void Main()
    {

        //DalProduct product;;;;
        int choice;
        Console.WriteLine(" Input: O - Exit , 1 - Product , 2 - Order, 3 - Order Item");
        int.TryParse(Console.ReadLine(),out choice);
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
                        subOrder();
                        break;
                    case 3:
                        subOrderItem();
                        break;

                    default:
                        Console.WriteLine("ERROR");
                        break;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine(" Input : O - Exit , 1 - Product , 2 - Order, 3 - Order Item");
            int.TryParse(Console.ReadLine(), out choice);
        }
        return;
    }
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

                Product product = new Product
                {
                    
                    Name = NewName,
                    ID= NewID,  
                    Price = NewPrice,
                    Category = NewCategory,
                    InStock = NewInstock
                };
                int id;
                id= DalProduct.addProduct(product);
                break;

            case 'b': ///show single product
                Console.WriteLine("Enter product ID:");
                int idForItem;
                int.TryParse(Console.ReadLine(),out idForItem);
                Product singleProduct;
                singleProduct = DalProduct.getSingleProduct(idForItem);
                Console.WriteLine(singleProduct);
                break;

            case 'c':// print  array
                DalProduct.getArrayOfProducts();
                break;
            case 'd':// update product
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

                Product product2 = new Product
                {

                    Name = NewName,
                    ID = NewID,
                    Price = NewPrice,
                    Category = NewCategory,
                    InStock = NewInstock
                };

                DalProduct.updateProduct(product2);
                break;

            case 'e'://delete product
                int delID;
                Console.WriteLine("Enter product ID:");
                int.TryParse(Console.ReadLine(), out delID);
                DalProduct.deleteProduct(delID);
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }
    public static void subOrder()
    {
        int orderID;
        Console.WriteLine(" Input : a - Add a order, b - Show a order by ID, c -  Show an array of orders, d- Update a order , e - Delete a order");
        char choice;
        char.TryParse(Console.ReadLine(), out choice);
        switch (choice)
        {
            case 'a': ///add product
                Order order = new Order();
                Console.WriteLine("Enter Customer Name:");
                order.CustomerName = Console.ReadLine();
                Console.WriteLine("Enter Customer Email:");
                order.CustomerEmail = Console.ReadLine();
                Console.WriteLine("Enter Customer Address:");
                order.CustomerAddress= Console.ReadLine();
                order.OrderDate = DateTime.MinValue;
                order.ShipDate= DateTime.MinValue;
                order.DeliveryDate= DateTime.MinValue;
                DalOrder.addOrder(order);
                break;

            case 'b': ///show single order
                Console.WriteLine("Enter order ID:");
                int.TryParse(Console.ReadLine(),out orderID);
                Order singleOrder;
                singleOrder = DalOrder.getSingleOrder(orderID);
                Console.WriteLine(singleOrder);
                break;

            case 'c':
                DalOrder.getArrayOfOrders();
                break;
            case 'd':
                Order order2 = new Order();
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
                DalOrder.updateOrder(order2);
                break;
            case 'e':
                Console.WriteLine("Enter order ID:");
                int.TryParse(Console.ReadLine(), out orderID);
                DalOrder.deleteOrder( orderID);
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }
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
            case 'a': ///add product

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

                OrderItem orderItem = new OrderItem
                {
                    ID = newID,
                    ProductID= newProductID,
                    OrderID = newOrderID1,
                    Price = newPrice1,
                    Amount = newAmount,
                };
                int id;
                id = DalOrderItem.addOrderItem(orderItem);
                break;

            case 'b': ///show single order item
                Console.WriteLine("Enter  ID:");
                int.TryParse(Console.ReadLine(), out newID);
                OrderItem singleOrderItem;
                singleOrderItem = DalOrderItem.getSingleOrderItem(newID);
                Console.WriteLine(singleOrderItem);
                break;

            case 'c':
                DalOrderItem.getArrayOfOrderItem();
                break;
            case 'd':
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

                OrderItem orderItem2 = new OrderItem
                {
                    ID = newID,
                    ProductID = newProductID,
                    OrderID = newOrderID1,
                    Price = newPrice1,
                    Amount = newAmount,
                };
                DalOrderItem.updateOrderItem(orderItem2);
                break;

            case 'e':
                Console.WriteLine("Enter  ID:");
                int.TryParse(Console.ReadLine(), out newID);
                DalOrderItem.deleteOrderItem(newID);
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }
}
