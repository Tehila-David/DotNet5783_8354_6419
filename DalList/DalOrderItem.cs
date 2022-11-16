
using DO;

namespace Dal;

public class DalOrderItem

{
    /// This function adds an order item
    public  int addOrderItem(OrderItem myOrderItem) 
    {
        ///Checking if the order item array is full
        if(DataSource.arrayOrderItem.Length == DataSource.Config.IndexOrdersItem)
        {
            throw new Exception("The Order Item array is full");
        }
        ///Going through the array of order items
        for(int i = 0; i < DataSource.Config.IndexOrdersItem; i++)
        {
            ///checking if  the id of an order item in the array is equal to the id of an order item wanted to be added
            if(myOrderItem.ID == DataSource.arrayOrderItem[i].ID)
            {
                throw new Exception(" The order item already exists");
            }
        }
        ///Placing the new order item in thelast available spot in the array
        DataSource.arrayOrderItem[DataSource.Config.IndexOrdersItem++] = new OrderItem()
        {ID = myOrderItem.ID, ProductID = myOrderItem.ProductID, OrderID = myOrderItem.OrderID, Amount = myOrderItem.Amount, Price = myOrderItem.Price};
        
        return myOrderItem.ID;
    }
    /// This function returns the details of an lorder item based on an id
    public  OrderItem getSingleOrderItem(int id)
    {
        ///Going through the array of order items
        for (int i = 0; i < DataSource.Config.IndexOrdersItem; i++)
        {
            ///Checking if the requested id is equal to an id of an order item in the array
            if (id == DataSource.arrayOrderItem[i].ID)
            {
                ///Copying the details of the order item wirh the correct id to a new order item
                OrderItem singleOrderItem = new OrderItem()
                {
                    ID = DataSource.arrayOrderItem[i].ID,
                    ProductID = DataSource.arrayOrderItem[i].ProductID,
                    OrderID = DataSource.arrayOrderItem[i].OrderID,
                    Amount = DataSource.arrayOrderItem[i].Amount,
                    Price = DataSource.arrayOrderItem[i].Price
                };
                return singleOrderItem; ///returning the new order item
            }
        }
        ///If the order item was not found in the array
        throw new Exception("Sorry ,this item does not exist in the array ");
    }
    /// <summary>
    /// This function returns the array with all of the order items
    /// </summary>
    /// <returns></returns> array of order items
    public OrderItem[] getArrayOfOrderItem()
    {
        ///looking for all of the order items that have their details filed in and returning them
        return Array.FindAll(DataSource.arrayOrderItem, p => p.ID != 0);
    }
    /// This function receives an id of an ordre item and deletes the order item witn the same id
    public  void deleteOrderItem(int id)
    {
        int nextIndex = DataSource.Config.IndexOrdersItem; ///The size of the occupied places in the array 
        for (int i = 0; i < nextIndex; i++) ///Going through the array of order items
        {
            ///Checking if the requested id is equal to the id in the array
            if (DataSource.arrayOrderItem[i].ID == id)
            {
                /// Going through the array from  the point of the found id
                for (int j = i; j < nextIndex - 1; j++)
                {
                    ///moving each order item one space to the left
                    DataSource.arrayOrderItem[j] = DataSource.arrayOrderItem[j + 1];
                }
                DataSource.Config.IndexOrdersItem--; ///Decreasing the amount of order items by one
                break;
            }
        }
        /// If the order item was not found in the array
        throw new Exception("Sorry ,this item does not exist in the array ");

    }

    /// This function receives an order item and updates the order item in the array that has the same id
    public  void updateOrderItem(OrderItem myOrderItem)
    {
        ///Going through the order items array
        for(int i = 0; i < DataSource.Config.IndexOrdersItem; i++)
        {
            /// Checking if the id in the array is equal to the id of the requested order item to be updated
            if (DataSource.arrayOrderItem[i].ID == myOrderItem.ID)
            {
                ///updating the order item
                DataSource.arrayOrderItem[i] = myOrderItem;
                break;
            }
        }
        ///if the id of the requested order item is not found in the array
        throw new Exception("Order Item to be updated does not exist");
    }

    /// This function receives an order id and a product id and returns the matching order item
    public OrderItem getOrderItemBasedOnProducIDAndOrderID(int idOrder, int idProduct)
    {
        OrderItem orderItem = new OrderItem();
        int nextIndex = DataSource.Config.IndexOrdersItem; ///The size of the occupied places in the array 
        for (int i = 0; i < nextIndex; i++) //Going through the array of order items
        {
            ///checking if an orderitem has the same product and order id's as the on e the user entered 
            if(DataSource.arrayOrderItem[i].ProductID == idProduct && DataSource.arrayOrderItem[i].OrderID == idOrder)
            {
                ///The correct order item based on the user input
                orderItem = DataSource.arrayOrderItem[i];
                return orderItem;
            }
        }
        //If the order item being looked for is not found incthe array
        throw new Exception("Sorry ,this item does not exist in the array ");
    }

    /// This function returns an array of all of the order items with the order id the user entered
    public OrderItem[] getArrayOfOrderItemsBasedOnOrderID(int idOrder)
    {
        //creating an array with all of the order items that have the same order id as the one the user entered
        return Array.FindAll(DataSource.arrayOrderItem, p => p.OrderID == idOrder);
        //If no order items arec found with the order id the user entered
        throw new Exception("No order items found with the entered order id");
    }
}
  
