using DO;
namespace Dal;


class Program
{
    static void Main()
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
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine(" Input : O - Exit , 1 - Product , 2 - Order, 3 - Order Item");
            choice = Console.Read();
        }
        return;
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
                product.Category = (Category)Console.Read();
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
                product2.Category = (Category)Console.Read();
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
        Console.WriteLine(" Input : a - Add a order, b - Show a order by ID, c -  Show an array of orders, d- Update a order , e - Delete a order");
        char choice;
        choice = (char)Console.Read();
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
                int idForItem1 = Console.Read();
                Order singleOrder;
                singleOrder = DalOrder.getSingleOrder(idForItem1);
                Console.WriteLine(singleOrder);
                break;

            case 'c':
                foreach (Order order1 in DalOrder.getArrayOfOrders())
                {
                    Console.WriteLine(order1);
                }
                break;
            case 'd':
                Order order2 = new Order();
                Console.WriteLine("Enter ID: ");
                order2.ID = Console.Read();
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
                Console.WriteLine("Enter ID: ");
                int delID1 = Console.Read();
                DalOrder.deleteOrder(delID1);
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }
    public static void subOrderItem()
    {
        Console.WriteLine(" Input : a - Add a orderItem, b - Show a orderItem by ID, c -  Show an array of orderItems, d- Update a orderItem , e - Delete a orderItem");
        char choice;
        choice = (char)Console.Read();
        switch (choice)
        {
            case 'a': ///add product
                OrderItem orderItem = new OrderItem();
                Console.WriteLine("Enter Product ID:");
                orderItem.ProductID = Console.Read();
                Console.WriteLine("Enter Order ID:");
                orderItem.OrderID = Console.Read();
                Console.WriteLine("Enter  Price:");
                orderItem.Price = Console.Read();
                Console.WriteLine("Enter  amount:");
                orderItem.Amount = Console.Read();
                int id;
                id = DalOrderItem.addOrderItem(orderItem);
                break;

            case 'b': ///show single order item
                Console.WriteLine("Enter order item ID:");
                int idForItem3 = Console.Read();
                OrderItem singleOrderItem;
                singleOrderItem = DalOrderItem.getSingleOrderItem(idForItem3);
                Console.WriteLine(singleOrderItem);
                break;

            case 'c':
                foreach (OrderItem orderItem1 in DalOrderItem.getArrayOfOrderItem())
                {
                    Console.WriteLine(orderItem1);
                }
                break;
            case 'd':
               OrderItem orderItem2 = new OrderItem();
                Console.WriteLine("Enter ID: ");
                orderItem2.ID = Console.Read();
                Console.WriteLine("Enter Product ID:");
                orderItem2.ProductID = Console.Read();
                Console.WriteLine("Enter Order ID:");
                orderItem2.OrderID = Console.Read();
                Console.WriteLine("Enter  Price:");
                orderItem2.Price = Console.Read();
                Console.WriteLine("Enter  amount:");
                orderItem2.Amount = Console.Read();
                DalOrderItem.updateOrderItem(orderItem2);
                break;
            case 'e':
                Console.WriteLine("Enter ID: ");
                int delID3 = Console.Read();
                DalOrderItem.deleteOrderItem(delID3);
                break;
            default:
                Console.WriteLine("ERROR");
                break;
        }
    }
}
