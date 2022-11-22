using DO;

namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    List<OrderItem> getListOfOrderItemsBasedOnOrderID(int id);
    OrderItem getOrderItemBasedOnProducIDAndOrderID(int idOrder, int idProduct);
}
