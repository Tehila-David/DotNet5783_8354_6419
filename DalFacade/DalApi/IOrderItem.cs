using DO;

namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    List<OrderItem> getListOfOrderItemsBasedOnOrderID(int id);
    List<OrderItem> getOrderItemBasedOnProducIDAndOrderID(int idOrder, int idProduct);
}
