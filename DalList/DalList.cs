using DalApi;
namespace Dal;

sealed internal class DalList: IDal
{

    private DalList() 
    {
        Order = new DalOrder();
        Product=new DalProduct();
        OrderItem = new DalOrderItem();
    }
    public static IDal Instance { get; } = new DalList();
    public IOrder Order { get; }
    public IProduct Product { get; }
    public IOrderItem OrderItem { get; }
}
