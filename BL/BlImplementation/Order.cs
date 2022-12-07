using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;

using Dal;



namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    IDal Dal = new DalList();


    public IEnumerable<BO.OrderForList> GetListedOrders()
    {
        return Dal.Order.GetAll().Select(order => new BO.OrderForList
        {

            ID = order.ID,
            CustomerName = order.CustomerName,
            Status = order.DeliveryDate != DateTime.MinValue ? BO.OrderStatus.Deliverded: order.ShipDate != DateTime.MinValue ? BO.OrderStatus.shipped 
            : order.OrderDate != DateTime.MinValue ? BO.OrderStatus.Confirmed : null,
            AmountOfItems = Dal.OrderItem.getListOrderItems(order.ID).Sum(orderItem => orderItem.Amount),
            TotalPrice = (Dal.OrderItem.getListOrderItems(order.ID).Sum(orderItem => orderItem.Price * orderItem.Amount))
        });
    }
    public BO.Order GetByID(int id)
    {
        try
        {
            DO.Order order = Dal?.Order.GetById(id) ?? throw new DO.NotExists("Got null order from data");
            //BO.OrderStatus status = OrderStatus.Confirmed;
            //if (order.ShipDate != DateTime.MinValue)
            //{
            //    status = BO.OrderStatus.shipped;
            //    if (order.DeliveryDate != DateTime.MinValue)
            //    {
            //        status = BO.OrderStatus.Deliverded;
            //    }
            //}

            if (id < 0) { throw new BO.InternalProblem("ID not positive"); }
            return new BO.Order()
            {
                ID = order.ID,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                CustomerAddress = order.CustomerAddress,
                Status = order.DeliveryDate != DateTime.MinValue ? BO.OrderStatus.Deliverded : order.ShipDate != DateTime.MinValue ? BO.OrderStatus.shipped
            :   order.OrderDate != DateTime.MinValue ? BO.OrderStatus.Confirmed : null,
                OrderDate = order.OrderDate,
            };
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Sorry ,this order does not exist in the List ", ex);
        }
    }

    public BO.Order UpdateShipDate(int id)
    {
        try
        {
            if (id < 0) { throw new BO.InternalProblem("ID not positive"); }
            DO.Order order = Dal?.Order.GetById(id) ?? throw new DO.NotExists("Got null order from data");
            if (order.OrderDate == DateTime.MinValue) { throw new BO.InternalProblem("The order not  confirmed"); }
            if (order.ShipDate != DateTime.MinValue) { throw new BO.InternalProblem("The order shipped"); }
            order.ShipDate = DateTime.Now;
            Dal.Order.Update(order);
            return new BO.Order()
            {
                ID = order.ID,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                CustomerAddress = order.CustomerAddress,
                Status = BO.OrderStatus.shipped,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryDate = order.DeliveryDate,
                TotalPrice = Dal.OrderItem.getListOrderItems(order.ID).Sum(orderItem => orderItem.Price * orderItem.Amount),
                Items = (List<BO.OrderItem>)Dal.OrderItem.getListOrderItems(order.ID)

            };
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Sorry ,this order does not exist in the List ", ex);
        }
    }

    public BO.Order UpdateDelivery (int id)
    {
        try
        {
            if (id < 0) { throw new BO.InternalProblem("ID not positive"); }
            DO.Order order = Dal?.Order.GetById(id) ?? throw new DO.NotExists("Got null order from data");
            if (order.OrderDate == DateTime.MinValue) { throw new BO.InternalProblem("The order not confiremed");}
            if (order.ShipDate != DateTime.MinValue) { throw new BO.InternalProblem("The order shipped"); }
            order.ShipDate = DateTime.Now;
            Dal.Order.Update(order);
            return new BO.Order()
            {
                ID = order.ID,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                CustomerAddress = order.CustomerAddress,
                Status = BO.OrderStatus.Deliverded,
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                ShipDate = order.ShipDate,
                TotalPrice = Dal.OrderItem.getListOrderItems(order.ID).Sum(orderItem => orderItem.Price * orderItem.Amount),
                Items = (List<BO.OrderItem>)Dal.OrderItem.getListOrderItems(order.ID),

            };
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Sorry ,this order does not exist in the List ", ex);
        }
    }

    public BO.OrderTracking followOrder(int id)
    {

        try
        {
            if (id < 0) { throw new BO.InternalProblem("ID not positive"); }
            DO.Order order = Dal?.Order.GetById(id) ?? throw new DO.NotExists("Got null order from data");
            List<Tuple<DateTime, string>>? TrackingForHelp = new List<Tuple<DateTime, string>>();
            //BO.OrderStatus status = OrderStatus.Confirmed;
            
            if(order.OrderDate != DateTime.MinValue)
            {
                TrackingForHelp.Add(new Tuple<DateTime, string>(order.OrderDate, "Confirmed"));
                if (order.ShipDate != DateTime.MinValue)
                {
                    TrackingForHelp.Add(new Tuple<DateTime, string>((DateTime)order.ShipDate, "Shipped"));
                    
                    if (order.DeliveryDate != DateTime.MinValue)
                    {
                        TrackingForHelp.Add(new Tuple<DateTime, string> ((DateTime)order.DeliveryDate, "Delivered"));
                    }
                }
            }
            
            return new BO.OrderTracking()
            {
                ID = order.ID,
                Status = order.DeliveryDate != DateTime.MinValue ? BO.OrderStatus.Deliverded : order.ShipDate != DateTime.MinValue ? BO.OrderStatus.shipped
            :   order.OrderDate != DateTime.MinValue ? BO.OrderStatus.Confirmed : null,
                Tracking = TrackingForHelp,

            };
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Sorry ,this order does not exist in the List ", ex);
        }
    }
}