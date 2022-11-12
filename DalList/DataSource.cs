
using DO;

namespace Dal;

internal static class DataSource
{
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
    static void AddArrayProducts(Product myProduct)
    {
        if (NumProducts < 50)
        {
            NumProducts++;
            arrayProducts[NumProducts] = myProduct;
        }
    }
    static void AddArrayOrderItem(OrderItem myOrderItem)
    {
        if(NumOrdersItem < 100)
        {
            NumOrdersItem++;
            arrayOrderItem[NumOrdersItem] = myOrderItem;
        }
    }
    static void AddArrayOrder(Order myOrder)
    {
        if(NumOrders < 200)
        {
            NumOrders++;
            arrayOrder[NumOrders] = myOrder;
        }
    }
    internal static class Config
    {
        internal const int s_startOrderNumber = 1000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => ++ s_nextOrderNumber; }
    }
 static void s_Initialize()/// מאתחל את המערכים
    {
        createProducts();/// 10
        createOrders();///20
        createOrderItem();///40
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