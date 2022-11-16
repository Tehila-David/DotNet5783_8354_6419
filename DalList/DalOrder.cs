

using DO;

namespace Dal;

public class DalOrder
{
    /// This function adds an order to array
    public int addOrder(Order myOrder)
    {
        if (DataSource.arrayOrder.Length == DataSource.Config.IndexOrder)//check if array is full
        {
            throw new Exception("The Order array is full");
        }
        for (int i = 0; i < DataSource.Config.IndexOrder; i++)// check if myOrder is exists
        {
            if (myOrder.ID == DataSource.arrayOrder[i].ID)
            {
                throw new Exception(" The Order already exists");
            }
        }
        //Placing the new order  in thelast available spot in the array
        DataSource.arrayOrder[DataSource.Config.IndexOrder++] = new Order()
        { ID = myOrder.ID, CustomerName = myOrder.CustomerName, CustomerEmail = myOrder.CustomerEmail, CustomerAddress = myOrder.CustomerAddress,
         OrderDate = myOrder.OrderDate, ShipDate = myOrder.ShipDate, DeliveryDate = myOrder.DeliveryDate
        };

        return myOrder.ID;
    }

    /// This function returns the details of an order based on an id
    public Order getSingleOrder(int id)
    {
        ///Going through the array of order 
        for (int i = 0; i < DataSource.arrayOrder.Length; i++)
        {
            ///Checking if the requested id is equal to an id of an orders in the array
            if (id == DataSource.arrayOrder[i].ID)
            {
                ///Copying the details of the orders with the correct id to a new order
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
        ///If the order  was not found in the array
        throw new Exception("Sorry ,this Order does not exist in the array ");
    }

    /// <summary>
    /// This function returns the array with all of the order items
    /// </summary>
    /// <returns></returns> array of orders
    
    public Order[] getArrayOfOrders()

    {  ///looking for all of the order items that have their details filed in and returning them
        return Array.FindAll(DataSource.arrayOrder, p => p.ID != 0);
    }
    /// This function receives an id of an order and deletes the order witn the same id
    public void deleteOrder(int id)
    {
        //int nextIndex = DataSource.Config.IndexOrder;///The size of the occupied places in the array
        for (int i = 0; i < DataSource.arrayOrder.Length; i++)
        {
            if (DataSource.arrayOrder[i].ID == id)
            {
                for (int j = i; j < DataSource.arrayOrder.Length - 1; j++)
                {   //moving each order one space to the left
                    DataSource.arrayOrder[j] = DataSource.arrayOrder[j + 1];
                }
                DataSource.Config.IndexOrder--;///Decreasing the amount of order items by one
                break;
            }
        }
        /// If the order was not found in the array
        throw new Exception("Sorry ,this Order does not exist in the array ");

    }

    /// This function receives an order and updates the order in the array that has the same id
    public void updateOrder(Order myOrder)
    {
        for (int i = 0; i < DataSource.arrayOrder.Length; i++)
        {
            if (DataSource.arrayOrder[i].ID == myOrder.ID)
            {
                ///updating the order 
                DataSource.arrayOrder[i] = myOrder;
                break;
            }
        }
        ///if the id of the requested order  is not found in the array
        throw new Exception("Order to be updated does not exist");
    }
}
