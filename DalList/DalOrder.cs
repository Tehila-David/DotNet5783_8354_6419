
using DalApi;
using DO;
using System;
using System.Runtime.CompilerServices;

namespace Dal;

public class DalOrder : IOrder
{
    DataSource _dataSource = DataSource.s_instance;
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order? GetById(Func<Order?, bool>? predicate)
    {
        return _dataSource.OrderList?.FirstOrDefault(predicate)
            ?? throw new NotExists("Sorry ,this order does not exist in the List ");
    }
    /// <summary>
    /// This function adds an order to list
    /// </summary>
    /// 
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Order myOrder)
    {
        if (myOrder.ID == 0)
            myOrder.ID = DataSource.Config.NextOrderID;
        if (_dataSource.OrderList.Exists(p => p?.ID == myOrder.ID))
             throw new AlreadyExists("The order already exists");
        _dataSource.OrderList.Add(myOrder);
        return myOrder.ID;
    }


    /// <summary>
    /// This function returns the details of an order based on an id
    /// </summary>
   [MethodImpl(MethodImplOptions.Synchronized)]
    public Order GetById(int id)
    {
        return _dataSource.OrderList?.FirstOrDefault(s => s?.ID == id)
            ?? throw new NotExists("Sorry ,this order does not exist in the List ");
        
        ///If the order  was not found in the array
       
    }

    /// <summary>
    /// This function returns the array with all of the order items
    /// </summary>
    /// <returns></returns> array of orders
   [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? predicate = null)
    {
        ///looking for all of the products that have their details filed in and returning them
        if (predicate == null) { return _dataSource.OrderList.AsEnumerable(); }
        return _dataSource.OrderList.Where(predicate)?? _dataSource.OrderList.AsEnumerable();
    }


    /// <summary>
    /// This function receives an id of an order and deletes the order witn the same id
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        
        var order = (from item in _dataSource.OrderList
                     where item?.ID == id
                     select item).FirstOrDefault();
        if (order != null)
        {
            _dataSource.OrderList.Remove(order);
            return;
        }
        /// If the order was not found in the list
        throw new NotExists("Sorry ,this Order does not exist in the List ");

    }


    /// <summary>
    /// This function receives an order and updates the order in the array that has the same id
    /// </summary>
    /// 
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order myOrder)
    {
       
        var order= (from item in _dataSource.OrderList
                        where item?.ID == myOrder.ID
                        select item).FirstOrDefault();
        if (order != null)
        {
            _dataSource.OrderList.Remove(order);
            _dataSource.OrderList.Add(myOrder);
            return;
        }
        ///if the id of the requested order  is not found in the array
        throw new NotExists("Order to be updated does not exist");
    }
    

}
