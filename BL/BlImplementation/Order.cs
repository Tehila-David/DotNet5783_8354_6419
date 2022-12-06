using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;

using Dal;
using BO;


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
        try
        {
            DO.Order order = Dal?.Order.GetById(id) ?? throw new DO.NotExists("Got null order from data");
            BO.OrderStatus status = OrderStatus.Confirmed;
            if (order.ShipDate != DateTime.MinValue) { status = BO.OrderStatus.shipped; }
            if (order.DeliveryDate != DateTime.MinValue) { status = BO.OrderStatus.Deliverded; }
            if (id < 0) { throw new BO.InternalProblem("ID not positive"); }
            return new BO.Order()
            {
                ID = order.ID,
                CustomerName = order.CustomerName,
                CustomerEmail= order.CustomerEmail,
                CustomerAddress= order.CustomerAddress,
                Status= BO.OrderStatus.Confirmed,///!!!!!!!!!!!!!!!
                OrderDate = order.OrderDate,
            };
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Sorry ,this order does not exist in the List ", ex);
        }
    }

    public BO.Order UpdateDelivery(int id)
    {
        try
        {
            if (id < 0) { throw new BO.InternalProblem("ID not positive"); }
            DO.Order order = Dal?.Order.GetById(id) ?? throw new DO.NotExists("Got null order from data");
            if (order.DeliveryDate != DateTime.MinValue) { throw new BO.InternalProblem("The order shipped"); }
            order.DeliveryDate = DateTime.Now;
            Dal.Order.Update(order);
            return new BO.Order()
            {
                ID = order.ID,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                CustomerAddress = order.CustomerAddress,
                Status = BO.OrderStatus.Confirmed,///!!!!!!!!!!!!!!!
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                ShipDate= order.ShipDate,
                TotalPrice=

            };
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Sorry ,this order does not exist in the List ", ex);
        }
    }

    public BO.Order UpdateShipDate(int id)
    {

    }
   
    public BO.Order followOrder(int id)
    {

    }

}
