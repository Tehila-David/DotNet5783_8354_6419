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
        // return list of orders
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
            //if order exsits
            DO.Order order = Dal?.Order.GetById(id) ?? throw new DO.NotExists("Got null order");
            
            if (id < 0) { throw new BO.InternalProblem("ID not positive"); }
            // return the order by ID
            return new BO.Order()
            {
                ID = order.ID,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                CustomerAddress = order.CustomerAddress,
                Status = order.DeliveryDate != DateTime.MinValue ? BO.OrderStatus.Deliverded : order.ShipDate != DateTime.MinValue ? BO.OrderStatus.shipped
            :   order.OrderDate != DateTime.MinValue ? BO.OrderStatus.Confirmed : null,
                OrderDate = order.OrderDate,
                Items= getDoOrderItem(order.ID)
            };
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Sorry ,this order does not exist in the List ", ex);
        }
    }
    public List<BO.OrderItem?> getDoOrderItem(int id)
    {
        List<BO.OrderItem> listForBo=new List<BO.OrderItem>();
       // it is creating new list of orderItem and adding orderItem
        foreach(var item in Dal.OrderItem.getListOrderItems(id))
        {
            listForBo.Add(new BO.OrderItem
            {
                ID = item.ID,
                Name = Dal.Product.GetById(item.ProductID).Name,
                ProductID = item.ProductID,
                Amount = item.Amount,
                Price = item.Price,
                TotalPrice = item.Amount * item.Price
            });
        }
        return listForBo;
    }

    public BO.Order UpdateShipDate(int id)
    {
        try
        {
            if (id < 0) { throw new BO.InternalProblem("ID not positive"); }
            DO.Order order = Dal?.Order.GetById(id) ?? throw new DO.NotExists("Got null order");
            //There is no order date
            if (order.OrderDate == DateTime.MinValue) { throw new BO.InternalProblem("The order not  confirmed"); }
            //The order shipped 
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
                Items = getDoOrderItem(order.ID),

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
            DO.Order order = Dal?.Order.GetById(id) ?? throw new DO.NotExists("Got null order ");
            //There is no order date
            if (order.OrderDate == DateTime.MinValue) { throw new BO.InternalProblem("The order not confiremed");}
            //The order wasn't shipped
            if (order.ShipDate == DateTime.MinValue) { throw new BO.InternalProblem("The order not  shipped"); }
            order.DeliveryDate = DateTime.Now;
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
                Items = getDoOrderItem(order.ID),

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
            DO.Order order = Dal?.Order.GetById(id) ?? throw new DO.NotExists("Got null order");
            List<Tuple<DateTime, string>>? TrackingForHelp = new List<Tuple<DateTime, string>>();
            //add status and dateTime to list-TrackingForHelp
            if (order.OrderDate != DateTime.MinValue)
            {
                TrackingForHelp.Add(new Tuple<DateTime, string>((DateTime)order.OrderDate, "Confirmed"));
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
                Tracking = TrackingForHelp

            };
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Sorry ,this order does not exist in the List ", ex);
        }
    }
}