using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;

using Dal;


namespace BlImplementation;

internal class Order: BlApi.IOrder
{
    IDal Dal = new DalList();


    public IEnumerable<BO.OrderForList> GetListedOrders()
    {
        return Dal.Order.GetAll().Select(order => new BO.OrderForList
        {

            ID = order.ID,
            CustomerName = order.CustomerName,
            Status = BO.OrderStatus.Confirmed,
            AmountOfItems = Dal.OrderItem.getListOrderItems(order.ID).Sum(orderItem => orderItem.Amount),
            TotalPrice = (Dal.OrderItem.getListOrderItems(order.ID).Sum(orderItem => orderItem.Price * orderItem.Amount))
        });
    }
    public BO.Order GetByID(int id)
    {

    }
    
    public BO.Order UpdateDelivery(int id)
    {

    }
    
    public BO.Order UpdateShipDate(int id);
   
    public BO.Order followOrder(int id);

}
