using DalApi;
namespace Dal;

sealed internal class DalList: IDal
{
    public static IDal instance { get; } = new DalList();
    private DalList() { }
    public IOrder Order => new DalOrder();
    public IProduct Product => new DalProduct();
    public IOrderItem OrderItem => new DalOrderItem();


}
