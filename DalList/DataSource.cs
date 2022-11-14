
using DO;
using System.Drawing;
using System.Numerics;

namespace Dal;

internal static class DataSource
{

    public static readonly Random random = new Random();
    static DataSource()
    {
        s_Initialize();
    }

    internal static Product[] arrayProducts = new Product[50];
    internal static OrderItem[] arrayOrderItem = new OrderItem[100];
    internal static Order[] arrayOrder = new Order[200];


    internal static class Config
    {
        internal static int IndexProducts { get; set; } // Actual number of products in the array - the last empty place
        internal static int IndexOrdersItem { get; set; }
        internal static int IndexOrder { get; set; }

        internal const int s_startOrderID = 100000;
        private static int s_nextOrderID = s_startOrderID;
        internal static int NextOrderID { get => ++s_nextOrderID; }

        internal const int s_startOrderItemID = 100000;
        private static int s_nextOrderItemID = s_startOrderItemID;
        internal static int NextOrderItemID { get => ++s_nextOrderItemID; }
    }


    static void s_Initialize()// מאתחל את המערכים
    {
        createProducts();// 10
        createOrders();//20
        createOrderItem();//40
    }
    static void createProducts()
    {
        arrayProducts[0].ID = 222222; arrayProducts[0].Name = "IPHONE 14"; arrayProducts[0].Price = 4000; arrayProducts[0].InStock = 100; arrayProducts[0].Category = Category.PHONE;
        arrayProducts[1].ID = 222223; arrayProducts[1].Name = "IPHONE 13"; arrayProducts[1].Price = 2800; arrayProducts[1].InStock = 100; arrayProducts[1].Category = Category.PHONE;
        arrayProducts[2].ID = 222224; arrayProducts[2].Name = "IPHONE 12"; arrayProducts[2].Price = 2000; arrayProducts[2].InStock = 100; arrayProducts[2].Category = Category.PHONE;
        arrayProducts[3].ID = 222225; arrayProducts[3].Name = "Ipad Air 2022"; arrayProducts[3].Price = 2500; arrayProducts[3].InStock = 100; arrayProducts[3].Category = Category.TABLET;
        arrayProducts[4].ID = 222226; arrayProducts[4].Name = "Samsung smart TV"; arrayProducts[4].Price = 2800; arrayProducts[4].InStock = 100; arrayProducts[4].Category = Category.SCREEN;
        arrayProducts[5].ID = 222227; arrayProducts[5].Name = "Apple Watch 2022"; arrayProducts[5].Price = 4000; arrayProducts[5].InStock = 100; arrayProducts[5].Category = Category.SMART_WATCH;
        arrayProducts[6].ID = 222228; arrayProducts[6].Name = "HP laptop i7"; arrayProducts[6].Price = 4000; arrayProducts[6].InStock = 100; arrayProducts[6].Category = Category.LAPTOP;
        arrayProducts[7].ID = 222229; arrayProducts[7].Name = "Macbook air"; arrayProducts[7].Price = 4000; arrayProducts[7].InStock = 150; arrayProducts[7].Category = Category.LAPTOP;
        arrayProducts[8].ID = 222230; arrayProducts[8].Name = "LG screen"; arrayProducts[8].Price = 2000; arrayProducts[8].InStock = 150; arrayProducts[8].Category = Category.SCREEN;
        arrayProducts[9].ID = 222231; arrayProducts[9].Name = "Air pods"; arrayProducts[9].Price = 400; arrayProducts[9].InStock = 0; arrayProducts[9].Category = Category.PHONE;
        Config.IndexProducts = 10;
    }
    static void createOrders()
    {

        for (int i = 0; i < 12; i++)//80% ShipDatev+ 60% DeliveryDate =12
        {
            arrayOrder[i].ID = Config.NextOrderID;
            arrayOrder[i].CustomerName = " Coustumer_" + (char)i;
            arrayOrder[i].CustomerEmail = (char)i + "@gmail.com";
            arrayOrder[i].CustomerAddress = "address_" + (char)(i + 2);
            arrayOrder[i].OrderDate = DateTime.Now - new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));
            arrayOrder[i].ShipDate = DateTime.Now + new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));// check!!!!
            arrayOrder[i].DeliveryDate = DateTime.Now + new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));//check!!!!
        }
        for (int i = 0; i < 4; i++)// only DeliveryDate = 4
        {
            arrayOrder[i].ID = Config.NextOrderID;
            arrayOrder[i].CustomerName = " Coustumer_" + (char)i;
            arrayOrder[i].CustomerEmail = (char)i + "@gmail.com";
            arrayOrder[i].CustomerAddress = "address_" + (char)(i + 2);
            arrayOrder[i].OrderDate = DateTime.Now - new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L)); //check!!!!
            arrayOrder[i].ShipDate = DateTime.MinValue;
            arrayOrder[i].DeliveryDate = DateTime.Now + new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));//check!!!!
        }
        for (int i = 0; i < 4; i++)// without DeliveryDate and ShipDate = 4
        {
            arrayOrder[i].ID = Config.NextOrderID;
            arrayOrder[i].CustomerName = " Coustumer_" + (char)i;
            arrayOrder[i].CustomerEmail = (char)i + "@gmail.com";
            arrayOrder[i].CustomerAddress = "address_" + (char)(i + 2);
            arrayOrder[i].OrderDate = DateTime.Now - new TimeSpan(random.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));//check!!!!
            arrayOrder[i].ShipDate = DateTime.MinValue;
            arrayOrder[i].DeliveryDate = DateTime.MinValue;
        }
        Config.IndexOrder = 20;
    }
    static void createOrderItem()
    {
        for (int i = 0; i < 40; i++)
        {
            arrayOrderItem[i].ID = Config.NextOrderID;
            arrayOrderItem[i].ProductID = random.Next(222221, 222232);
            arrayOrderItem[i].OrderID = random.Next(99999, Config.NextOrderItemID);
            arrayOrderItem[i].Amount = random.Next(0, 4);
            var product = Array.Find(arrayProducts, element => element.ID == arrayOrderItem[i].ProductID);
            arrayOrderItem[i].Price = product.Price * arrayOrderItem[i].Amount;

        }
        Config.IndexOrdersItem = 40;
    }
}