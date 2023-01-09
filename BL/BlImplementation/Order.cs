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
   
            return from item in dal?.Order.GetAll()
                   where item != null
                   orderby item?.ID
                   let order = item
                   select new BO.OrderForList
                   {
                       ID = order?.ID ?? throw new NullReferenceException("Missing ID"),
                       CustomerName = order?.CustomerName,
                       Status = order?.DeliveryDate != null ? BO.OrderStatus.Deliverded : order?.ShipDate != null ? BO.OrderStatus.shipped
                       :  BO.OrderStatus.Confirmed ,
                       AmountOfItems = dal.OrderItem.GetAll(item=> item?.OrderID == order?.ID).Sum(orderItem => orderItem?.Amount ?? 0),
                       TotalPrice = (dal.OrderItem.GetAll(item=> item?.OrderID == order?.ID).Sum(orderItem => orderItem?.Price * orderItem?.Amount ?? 0))
                   };
                   

        }
        else
        {
            return from item in dal.Order.GetAll(predicate)
                   where item != null
                   orderby item?.ID
                   let order = item
                   select new BO.OrderForList
                   {
                       ID = item?.ID ?? throw new NullReferenceException("Missing ID"),
                       CustomerName = item?.CustomerName,
                       Status = item?.DeliveryDate != null ? BO.OrderStatus.Deliverded : item?.ShipDate != null ? BO.OrderStatus.shipped
                       :  BO.OrderStatus.Confirmed ,
                       AmountOfItems = dal.OrderItem.GetAll(item => item?.OrderID == order?.ID).Sum(orderItem => orderItem?.Amount ?? 0),
                       TotalPrice = (dal.OrderItem.GetAll(item => item?.OrderID == order?.ID).Sum(orderItem => orderItem?.Price * orderItem?.Amount ?? 0))
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
                Status = order.DeliveryDate != null ? BO.OrderStatus.Deliverded : order.ShipDate != null ? BO.OrderStatus.shipped
            :    BO.OrderStatus.Confirmed,
                OrderDate = order.OrderDate,
                ShipDate= order.ShipDate,
                DeliveryDate= order.DeliveryDate,
                Items = getDoOrderItem(order.ID),
                TotalPrice = getDoOrderItem(order.ID).Sum(item => item?.TotalPrice ?? 0)
            };
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Sorry ,this order does not exist in the List ", ex);
        }
    }
    public List<BO.OrderItem?> getDoOrderItem(int id)
    {
        List<BO.OrderItem?> listForBo = new List<BO.OrderItem>();
        //it is creating new list of orderItem and adding orderItem

        foreach (var item in dal.OrderItem.GetAll(item => item?.OrderID==id))
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
                TotalPrice = getDoOrderItem(order.ID).Sum(item => item?.TotalPrice ?? 0),
                Items = getDoOrderItem(id)

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
            if (order.DeliveryDate != null) { throw new BO.InternalProblem("The order Delivered"); }

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
                TotalPrice = getDoOrderItem(id).Sum(orderItem => orderItem?.TotalPrice ?? 0),
                Items = getDoOrderItem(id),

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
            :   order.OrderDate != null ? BO.OrderStatus.Confirmed : null,
                Tracking = TrackingForHelp
            };
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Sorry ,this order does not exist in the List ", ex);
        }
    }
    public BO.OrderItem UpdateItems(BO.Order Order,int productId,int amount)
    {
        int firstAmount;
        DO.Product product = new DO.Product();
        product=dal.Product.GetById(productId);
        BO.OrderItem orderItem = new BO.OrderItem();
        if (product.InStock >= amount )
        {
            orderItem = Order.Items.Find(item => item.ProductID == productId);
            firstAmount = orderItem.Amount;
            Order.Items.RemoveAll(item => item.ProductID == productId);
            if(orderItem != null) //Order Item Exsits- עדכון פריט קיים
            {
                if (Order.Items.Any(item => item.ProductID == productId))
                {
                    orderItem.Amount = amount;
                    Order.Items.Add(orderItem);
                }
            }
            else//new Order Item- הוספת פריט חדש
            {
               /* orderItem.ID=dal.*//*NextOrderItemID*/ ///??  שלו  idביצירת פריט חדש איך יודעים מה ה
            }
           product.InStock -= amount- firstAmount;
           dal.Product.Update(product);
           
            return orderItem;
        }
        else 
        { throw new BO.InternalProblem("The amount of  products is not available"); }
       
    }
}