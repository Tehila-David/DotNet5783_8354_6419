
using DO;
using DalApi;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Dal;

public class DalOrderItem : IOrderItem

{
    DataSource _dataSource = DataSource.s_instance;
  [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem? GetById(Func<OrderItem?, bool>? predicate)
    {
        return _dataSource.OrderItemList?.FirstOrDefault(predicate)
            ?? throw new NotExists("Sorry ,this item does not exist in the List ");
    }
    /// <summary>
    /// This function adds an order item
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(OrderItem myOrderItem)
    {
        if (myOrderItem.ID == 0)
            myOrderItem.ID = DataSource.Config.NextOrderItemID;
        if (_dataSource.OrderItemList.Exists(p => p?.ID == myOrderItem.ID))
            throw new AlreadyExists("The order item already exists");
        _dataSource.OrderItemList.Add(myOrderItem);
        return myOrderItem.ID;
    }

    /// <summary>
    /// This function returns the details of an order item based on an id
    /// </summary>
   [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem GetById(int id)
    {

        return _dataSource.OrderItemList?.FirstOrDefault(s => s?.ID == id)
        ?? throw new NotExists("Sorry ,this item does not exist in the List ");

    }

    /// <summary>
    /// This function returns the list with all of the order items
    /// </summary>
   [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? predicate = null)
    {
        ///looking for all of the products that have their details filed in and returning them
        if (predicate == null) { return _dataSource.OrderItemList.AsEnumerable(); }
        return _dataSource.OrderItemList.Where(predicate) ?? _dataSource.OrderItemList.AsEnumerable();
    }

    /// <summary>
    /// This function receives an id of an ordre item and deletes the order item witn the same id
    /// </summary>
    /// 
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {

       
        var orderItem = (from item in _dataSource.OrderItemList
                         where item?.ID == id
                         select item).FirstOrDefault();
        if (orderItem != null)
        {
            _dataSource.OrderItemList.Remove(orderItem);
            return;
        }
        /// If the order item was not found in the list
        throw new NotExists("Sorry ,this item does not exist in the list ");

    }

    /// <summary>
    /// This function receives an order item and updates the order item in the list that has the same id
    /// </summary>
    /// 
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(OrderItem myOrderItem)
    {
        var orderItem = (from item in _dataSource.OrderItemList
                         where item?.ID == myOrderItem.ID
                         select item).FirstOrDefault();
        if (orderItem != null)
        {
            _dataSource.OrderItemList.Remove(orderItem);
            _dataSource.OrderItemList.Add(myOrderItem);
            return;
        }
        ///if the id of the requested order item is not found in the list
        throw new NotExists("Order Item to be updated does not exist");
    }


}

 


