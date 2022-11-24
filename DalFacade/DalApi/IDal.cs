

namespace DalApi
{
    public interface IDal
    {
        IOrder Order { get; }
        IOrderItem OrderItem { get; }
        IProduct Product { get; }   
    }
}
