

namespace DalApi
{
    internal interface IDal
    {
        IOrder Order { get; }
        IOrderItem OrderItem { get; }
        IProduct Product { get; }   
    }
}
