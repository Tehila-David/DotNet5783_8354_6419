

using DO;

namespace Dal;

public class DalOrderItem

{
    public static int addOrderItem(OrderItem myOrderItem)
    {
        if(DataSource.arrayOrderItem.Length == DataSource.Config.IndexOrdersItem)
        {
            throw new Exception("The Order Item array is full");
        }
        for(int i = 0; i < DataSource.Config.IndexOrdersItem; i++)
        {
            if(myOrderItem.ID == DataSource.arrayOrderItem[i].ID)
            {
                throw new Exception(" The order item already exists");
            }
        }
        DataSource.arrayOrderItem[DataSource.Config.IndexOrdersItem++] = new OrderItem()
        {ID = myOrderItem.ID, ProductID = myOrderItem.ProductID, OrderID = myOrderItem.OrderID, Amount = myOrderItem.Amount, Price = myOrderItem.Price};
        
        return myOrderItem.ID;
    }
    public static OrderItem getSingleOrderItem(int id)
    {
        for (int i = 0; i < DataSource.Config.IndexOrdersItem; i++)
        {
            if (id == DataSource.arrayOrderItem[i].ID)
            {
                OrderItem singleOrderItem = new OrderItem()
                {
                    ID = DataSource.arrayOrderItem[i].ID,
                    ProductID = DataSource.arrayOrderItem[i].ProductID,
                    OrderID = DataSource.arrayOrderItem[i].OrderID,
                    Amount = DataSource.arrayOrderItem[i].Amount,
                    Price = DataSource.arrayOrderItem[i].Price
                };
                return singleOrderItem;
            }
        }
        throw new Exception("Sorry ,this item does not exist in the array ");
    }
    public static OrderItem[] getArrayOfOrderItem()
    {
        int index = DataSource.Config.IndexOrdersItem;
        OrderItem[] newOrderItemList = new OrderItem[index];

        for (int i = 0; i < index; i++)
        {
            DataSource.arrayOrderItem[i].ToString();
        }
        return DataSource.arrayOrderItem;
    }
    public static void deleteOrderItem(int id)
    {
        int nextIndex = DataSource.Config.IndexOrdersItem;
        for (int i = 0; i < nextIndex; i++)
        {
            if (DataSource.arrayOrderItem[i].ID == id)
            {
                for (int j = i; j < nextIndex - 1; j++)
                {
                    DataSource.arrayOrderItem[j] = DataSource.arrayOrderItem[j + 1];
                }
                DataSource.Config.IndexOrdersItem--;
                break;
            }
        }
        throw new Exception("Sorry ,this item does not exist in the array ");

    }

    public static void updateOrderItem(OrderItem myOrderItem)
    {
        for(int i = 0; i < DataSource.Config.IndexOrdersItem; i++)
        {
            if (DataSource.arrayOrderItem[i].ID == myOrderItem.ID)
            {
                DataSource.arrayOrderItem[i] = myOrderItem;
                break;
            }
        }
        throw new Exception("Order Item to be updated does not exist");
    }
}
  
