

using DO;

namespace Dal;

public class DalOrder
{
    public int addOrder(Order myOrder)
    {
        if (DataSource.arrayOrder.Length == DataSource.Config.IndexOrder)
        {
            throw new Exception("The Order array is full");
        }
        for (int i = 0; i < DataSource.Config.IndexOrder; i++)
        {
            if (myOrder.ID == DataSource.arrayOrder[i].ID)
            {
                throw new Exception(" The Order already exists");
            }
        }
        DataSource.arrayOrder[DataSource.Config.IndexOrder++] = new Order()
        { ID = myOrder.ID, CustomerName = myOrder.CustomerName, CustomerEmail = myOrder.CustomerEmail, CustomerAddress = myOrder.CustomerAddress,
         OrderDate = myOrder.OrderDate, ShipDate = myOrder.ShipDate, DeliveryDate = myOrder.DeliveryDate
        };

        return myOrder.ID;
    }
    public Order getSingleOrder(int id)
    {
        for (int i = 0; i < DataSource.Config.IndexOrder; i++)
        {
            if (id == DataSource.arrayOrder[i].ID)
            {
                Order singleOrder = new Order()
                {
                    ID = DataSource.arrayOrder[i].ID,
                    CustomerName = DataSource.arrayOrder[i].CustomerName,
                    CustomerEmail = DataSource.arrayOrder[i].CustomerEmail,
                    CustomerAddress = DataSource.arrayOrder[i].CustomerAddress,
                    OrderDate = DataSource.arrayOrder[i].OrderDate,
                    ShipDate = DataSource.arrayOrder[i].ShipDate,
                    DeliveryDate = DataSource.arrayOrder[i].DeliveryDate

                };
                return singleOrder;
            }
        }
        throw new Exception("Sorry ,this Order does not exist in the array ");
    }
    public Order[] getListOfOrders()
    {
        int index = DataSource.Config.IndexOrder;
        Order[] newOrderList = new Order[index];

        for (int i = 0; i < index; i++)
        {
            newOrderList[i] = new Order()
            {
                CustomerName = DataSource.arrayOrder[i].CustomerName,
                CustomerEmail = DataSource.arrayOrder[i].CustomerEmail,
                CustomerAddress = DataSource.arrayOrder[i].CustomerAddress,
                OrderDate = DataSource.arrayOrder[i].OrderDate,
                ShipDate = DataSource.arrayOrder[i].ShipDate,
                DeliveryDate = DataSource.arrayOrder[i].DeliveryDate
            };
        }
        return newOrderList;
    }
    public void deleteOrder(int id)
    {
        int nextIndex = DataSource.Config.IndexOrder;
        for (int i = 0; i < nextIndex; i++)
        {
            if (DataSource.arrayOrder[i].ID == id)
            {
                for (int j = i; j < nextIndex - 1; j++)
                {
                    DataSource.arrayOrder[j] = DataSource.arrayOrder[j + 1];
                }
                DataSource.Config.IndexOrder--;
                break;
            }
        }
        throw new Exception("Sorry ,this Order does not exist in the array ");

    }

    public void updateOrder(Order myOrder)
    {
        for (int i = 0; i < DataSource.Config.IndexOrder; i++)
        {
            if (DataSource.arrayOrder[i].ID == myOrder.ID)
            {
                DataSource.arrayOrder[i] = myOrder;
                break;
            }
        }
        throw new Exception("Order to be updated does not exist");
    }
}
