
using DO;

namespace Dal;

internal static class DataSource
{
    
  //public static readonly
   static  DataSource()
    {
        s_Initialize();
    }

    internal static Product[] arrayProducts = new Product[50];
    internal static OrderItem[] arrayOrderItem = new OrderItem[100];
    internal static Order[] arrayOrder = new Order[200];

    static int NumProducts { get; set; }
    static int NumOrdersItem { get; set; }
    static int NumOrders { get; set ; }
  
    internal static class Config
    {
        internal const int s_startOrderNumber = 100000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => ++ s_nextOrderNumber; }
    }
 static void s_Initialize()// מאתחל את המערכים
    {
        createProducts();// 10
        createOrders();//20
        createOrderItem();//40
    }
    static void createProducts()
    {
        
    }
    static void createOrders()
    {

    }
    static void createOrderItem()
    {

    }
}