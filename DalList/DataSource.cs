
using DO;

namespace Dal;

internal sealed class DataSource
{


    internal static DataSource s_instance { get; } //This variable can contain an object of the class - dataSource
    private static readonly Random random = new Random(); //Variable to generate random numbers
    static DataSource() => s_instance = new DataSource(); //This function returns the created object
    private DataSource() => s_Initialize(); //constructor of an object of dataSource

    internal List<Product?> ProductList { get; } = new List<Product?>();
    internal List<Order?> OrderList { get; } = new List<Order?>();
    internal List<OrderItem?> OrderItemList { get; } = new List<OrderItem?>();

    internal static class Config
    {
        internal const int s_startOrderID = 100000;
        private static int s_nextOrderID = s_startOrderID;
        internal static int NextOrderID { get => ++s_nextOrderID; }

        internal const int s_startOrderItemID = 100000;
        private static int s_nextOrderItemID = s_startOrderItemID;
        internal static int NextOrderItemID { get => ++s_nextOrderItemID; }
    }


    private void s_Initialize()
    {
        createProducts();
        createOrders();
        createOrderItem();
    }
    private void createProducts()
    {
        ProductList.Add(new Product() { ID = 222222, Name = "IPHONE 14", Price = 4000, InStock = 100, Category = Category.PHONE });
        ProductList.Add(new Product() { ID = 222223, Name = "IPHONE 13", Price = 2800, InStock = 100, Category = Category.PHONE });
        ProductList.Add(new Product() { ID = 222224, Name = "IPHONE 12", Price = 2000, InStock = 100, Category = Category.PHONE });
        ProductList.Add(new Product() { ID = 222225, Name = "Ipad Air 2022", Price = 2500, InStock = 100, Category = Category.TABLET });
        ProductList.Add(new Product() { ID = 222226, Name = "Samsung smart TV", Price = 2800, InStock = 100, Category = Category.SCREEN });
        ProductList.Add(new Product() { ID = 222227, Name = "Apple Watch 2022", Price = 4000, InStock = 100, Category = Category.SMART_WATCH });
        ProductList.Add(new Product() { ID = 222228, Name = "HP laptop i7", Price = 4000, InStock = 100, Category = Category.LAPTOP });
        ProductList.Add(new Product() { ID = 222229, Name = "Macbook air", Price = 4000, InStock = 150, Category = Category.LAPTOP });
        ProductList.Add(new Product() { ID = 222230, Name = "LG screen", Price = 2000, InStock = 150, Category = Category.SCREEN });
        ProductList.Add(new Product() { ID = 222231, Name = "Air pods", Price = 400, InStock = 0, Category = Category.PHONE });
    }
    private void createOrders()
    {
        int j = 1;
        for (int i = 0; i < 16; i++)//80% ShipDatev => 60% DeliveryDate 
        {
            var order = new Order()
            {
                ID = Config.NextOrderID,
                CustomerName = "Customer_" + i,
                CustomerEmail = i + "@gmail.com",
                CustomerAddress = "address_" + (i + 2),
                OrderDate = DateTime.Now - new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L)),
                ShipDate = DateTime.Now + new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L)),
                DeliveryDate = null
            };
            order.DeliveryDate = (j<10) ? (order.ShipDate + new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L))) : DateTime.MinValue;
            j++;
            OrderList.Add(order);
        }
       
    
        for (int i = 0; i< 4; i++)// without DeliveryDate and ShipDate = 4
        {
            var order = new Order()
            { 
            ID = Config.NextOrderID,
            CustomerName = " Coustumer_" + i,
            CustomerEmail = i + "@gmail.com",
            CustomerAddress = "address_" + (i + 2),
            OrderDate = DateTime.Now - new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L)),
            ShipDate = null,
            DeliveryDate = null
            };
            OrderList.Add(order);
        }
    }
    private void createOrderItem()
    {
        for (int i = 0; i < 40; i++)
        {
            var orderItem = new OrderItem()
            {
                ID = Config.NextOrderID,
                ProductID = random.Next(222221, 222232),
                OrderID = random.Next(100000, Config.NextOrderItemID),
                Amount = random.Next(1, 3)
            };
            
            foreach(var item in ProductList)
            {
                if(item?.ID == orderItem.ProductID)
                {
                    orderItem.Price = (double)(item?.Price * orderItem.Amount);
                     break;
                }
            }
            OrderItemList.Add(orderItem);
        }
    }
}