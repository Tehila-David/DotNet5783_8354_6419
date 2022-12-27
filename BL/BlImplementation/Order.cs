using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;




namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    DalApi.IDal dal = DalApi.Factory.Get()!;

    public IEnumerable<BO.OrderForList> GetListedOrders(Func<DO.Order?, bool>? predicate = null)
    {
        if (predicate == null)
        {
            // return list of orders
            return dal.Order.GetAll().Select(order => new BO.OrderForList
            {
                ID = order?.ID ?? throw new NullReferenceException("Missing ID"),
                CustomerName = order?.CustomerName,
                Status = order?.DeliveryDate != DateTime.MinValue ? BO.OrderStatus.Deliverded : order?.ShipDate != DateTime.MinValue ? BO.OrderStatus.shipped
            : order?.OrderDate != DateTime.MinValue ? BO.OrderStatus.Confirmed : null,
                AmountOfItems = dal.OrderItem.getListOrderItems(order?.ID ?? 0).Sum(orderItem => orderItem?.Amount ?? 0),
                TotalPrice = (dal.OrderItem.getListOrderItems(order?.ID ?? 0).Sum(orderItem => orderItem?.Price * orderItem?.Amount ?? 0))
            });
        }
        else
        {
            return from item in dal.Order.GetAll(predicate)
                   select new BO.OrderForList
                   {
                       ID = item?.ID ?? throw new NullReferenceException("Missing ID"),
                       CustomerName = item?.CustomerName,
                       Status = item?.DeliveryDate != DateTime.MinValue ? BO.OrderStatus.Deliverded : item?.ShipDate != DateTime.MinValue ? BO.OrderStatus.shipped
                       : item?.OrderDate != DateTime.MinValue ? BO.OrderStatus.Confirmed : null,
                       AmountOfItems = dal.OrderItem.getListOrderItems(item?.ID ?? 0).Sum(orderItem => orderItem?.Amount ?? 0),
                       TotalPrice = (dal.OrderItem.getListOrderItems(item?.ID ?? 0).Sum(orderItem => orderItem?.Price * orderItem?.Amount ?? 0))
                   };
        }
    }
    public BO.Order GetByID(int id)
    {
        try
        {
            //if order exsits
            DO.Order order = dal?.Order.GetById(id) ?? throw new DO.NotExists("Got null order");

            if (id < 0) { throw new BO.InternalProblem("ID not positive"); }
            // return the order by ID
            return new BO.Order()
            {
                ID = order.ID,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                CustomerAddress = order.CustomerAddress,
                Status = order.DeliveryDate != DateTime.MinValue ? BO.OrderStatus.Deliverded : order.ShipDate != DateTime.MinValue ? BO.OrderStatus.shipped
            : order.OrderDate != DateTime.MinValue ? BO.OrderStatus.Confirmed : null,
                OrderDate = order.OrderDate,
                Items = getDoOrderItem(order.ID)
            };
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Sorry ,this order does not exist in the List ", ex);
        }
    }
    public List<BO.OrderItem> getDoOrderItem(int id)
    {
        List<BO.OrderItem> listForBo = new List<BO.OrderItem>();
        // it is creating new list of orderItem and adding orderItem
        foreach (var item in dal.OrderItem.getListOrderItems(id))
        {
            listForBo.Add(new BO.OrderItem
            {
                ID = item?.ID ?? 0,
                Name = dal.Product.GetById(item?.ProductID ?? 0).Name,
                ProductID = item?.ProductID ?? 0,
                Amount = item?.Amount ?? 0,
                Price = item?.Price ?? 0,
                TotalPrice = item?.Amount * item?.Price ?? 0
            });
        }
        return listForBo;
    }

    public BO.Order UpdateShipDate(int id)
    {
        try
        {
            if (id < 0) { throw new BO.InternalProblem("ID not positive"); }
            DO.Order order = dal?.Order.GetById(id) ?? throw new DO.NotExists("Got null order");
            //There is no order date
            if (order.OrderDate == null) { throw new BO.InternalProblem("The order not  confirmed"); }
            //The order shipped 
            if (order.ShipDate != null) { throw new BO.InternalProblem("The order shipped"); }
            order.ShipDate = DateTime.Now;
            dal.Order.Update(order);
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
                TotalPrice = dal.OrderItem.getListOrderItems(order.ID).Sum(orderItem => orderItem?.Price * orderItem?.Amount ?? 0),
                Items = getDoOrderItem(order.ID),

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
            DO.Order order = dal?.Order.GetById(id) ?? throw new DO.NotExists("Got null order ");
            //There is no order date
            if (order.OrderDate == null) { throw new BO.InternalProblem("The order not confiremed"); }
            //The order wasn't shipped
            if (order.ShipDate == null) { throw new BO.InternalProblem("The order not  shipped"); }
            order.DeliveryDate = DateTime.Now;
            dal.Order.Update(order);

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
                TotalPrice = dal.OrderItem.getListOrderItems(order.ID).Sum(orderItem => orderItem?.Price * orderItem?.Amount ?? 0),
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
            DO.Order order = dal?.Order.GetById(id) ?? throw new DO.NotExists("Got null order");
            List<Tuple<DateTime?, string?>>? TrackingForHelp = new List<Tuple<DateTime?, string?>>();
            //add status and dateTime to list-TrackingForHelp
            if (order.OrderDate != DateTime.MinValue)
            {
                TrackingForHelp.Add(new Tuple<DateTime?, string?>((DateTime)order.OrderDate, "Confirmed"));
                if (order.ShipDate != null)
                {
                    TrackingForHelp.Add(new Tuple<DateTime?, string?>((DateTime)order.ShipDate, "Shipped"));

                    if (order.DeliveryDate != null)
                    {
                        TrackingForHelp.Add(new Tuple<DateTime?, string?>((DateTime)order.DeliveryDate, "Delivered"));
                    }
                }
            }

            return new BO.OrderTracking()
            {
                ID = order.ID,
                Status = order.DeliveryDate != null ? BO.OrderStatus.Deliverded : order.ShipDate != DateTime.MinValue ? BO.OrderStatus.shipped
            : order.OrderDate != null ? BO.OrderStatus.Confirmed : null,
                Tracking = TrackingForHelp
            };
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Sorry ,this order does not exist in the List ", ex);
        }
    }
}