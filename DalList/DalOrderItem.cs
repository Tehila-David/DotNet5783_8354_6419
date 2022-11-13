

using DO;

namespace Dal;

public class DalOrderItem

{
    public int addOrdrItem(OrderItem myOrderItem)
    {
        if(DataSource.arrayOrderItem.Length == DataSource.Config.IndexOrdersItem)
        {
            throw new Exception("The array is full");
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
    public void getSingleOrdrItem(OrderItem myOrderItem)
    {

    }
    public void getListOfOrdrItem(OrderItem myOrderItem)
    {

    }
    public void deleteOrdrItem(OrderItem myOrderItem)
    {

    }

    public void updateOrdrItem(OrderItem myOrderItem)
    {
        for(int i = 0; i < DataSource.Config.IndexOrdersItem; i++)
        {
            if (DataSource.arrayOrderItem[i].ID == myOrderItem.ID)
            {
                DataSource.arrayOrderItem[i] = myOrderItem;
                break;
            }
        }
        throw new Exception("Order Item to be replaced does not exist");
    }
}

/* static class Config
    {
        internal static int IndexProducts { get; set; } // Actual number of products in the array  - the ladt empty place
        internal static int IndexOrdersItem { get; set; } // number of items in an order
        internal static int IndexOrder { get; set; } // the last empty space of  orders

        internal const int s_startOrderID = 100000;
        private static int s_nextOrderID = s_startOrderID;
        internal static int NextOrderID { get => ++s_nextOrderID; }

        internal const int s_startOrderItemID = 100000;
        private static int s_nextOrderItemID = s_startOrderItemID;
        internal static int NextOrderItemID { get => ++s_nextOrderItemID; }
    
