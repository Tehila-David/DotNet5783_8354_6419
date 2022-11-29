
using DalApi;
using DO;


namespace Dal;

internal class DalOrder : IOrder
{
    DataSource _dataSource = DataSource.s_instance;

    /// <summary>
    /// This function adds an order to list
    /// </summary>
    public int Add(Order myOrder)
    {

        if (_dataSource.OrderList.Exists(p => p.ID == myOrder.ID))
             throw new AlreadyExists("The order already exists");
        _dataSource.OrderList.Add(myOrder);
        return myOrder.ID;
    }

    /// <summary>
    /// This function returns the details of an order based on an id
    /// </summary>
    public Order GetById(int id)
    {
        foreach (var item in _dataSource.OrderList)
        {
            if (id == item.ID)
            {
                return item;
            }
        }
        ///If the order  was not found in the array
        throw new NotExists("Sorry ,this Order does not exist in the List ");
    }

    /// <summary>
    /// This function returns the array with all of the order items
    /// </summary>
    /// <returns></returns> array of orders
    public IEnumerable<Order> GetAll()

    {  ///looking for all of the order items that have their details filed in and returning them
        return _dataSource.OrderList.ToList();

    }
    /// <summary>
    /// This function receives an id of an order and deletes the order witn the same id
    /// </summary>
    public void Delete(int id)
    {
        foreach (var item in _dataSource.OrderList)
        {
            if (item.ID == id)
            {
                _dataSource.OrderList.Remove(item);
                return;

            }
        }
        /// If the order was not found in the list
        throw new NotExists("Sorry ,this Order does not exist in the List ");

    }


    /// <summary>
    /// This function receives an order and updates the order in the array that has the same id
    /// </summary>
    public void Update(Order myOrder)
    {
        int index = 0;
        foreach (Order item in _dataSource.OrderList)
        {
            if (item.ID == myOrder.ID) ///updating the order
            {
                _dataSource.OrderList.RemoveAt(index);
                _dataSource.OrderList.Insert(index, myOrder);
                return;
            }
            index++;
        }
        ///if the id of the requested order  is not found in the array
        throw new NotExists("Order to be updated does not exist");
    }
    

}
