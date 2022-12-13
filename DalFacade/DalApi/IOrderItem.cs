using DO;

namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    IEnumerable<OrderItem?> getListOrderItems(int idOrder);
    OrderItem getOrderItem(int idOrder, int idProduct);
}
