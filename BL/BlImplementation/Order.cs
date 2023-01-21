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
                       : BO.OrderStatus.Confirmed,
                       AmountOfItems = dal.OrderItem.GetAll(item => item?.OrderID == order?.ID).Sum(orderItem => orderItem?.Amount ?? 0),
                       TotalPrice = (dal.OrderItem.GetAll(item => item?.OrderID == order?.ID).Sum(orderItem => orderItem?.Price * orderItem?.Amount ?? 0))
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
                       : BO.OrderStatus.Confirmed,
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
            : BO.OrderStatus.Confirmed,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryDate = order.DeliveryDate,
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

        foreach (var item in dal.OrderItem.GetAll(item => item?.OrderID == id))
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
            : order.OrderDate != null ? BO.OrderStatus.Confirmed : null,
                Tracking = TrackingForHelp
            };
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Sorry ,this order does not exist in the List ", ex);
        }
    }
    public BO.Order UpdateItems(BO.Order Order, int productId, int amount, bool flag = true)
    {


        int firstAmount;
        int numberOrderItem;
        DO.Product product = new DO.Product();
        product = dal.Product.GetById(productId);
        //DO.OrderItem orderItem = new DO.OrderItem();
        if (flag != true)//ADD new item to Order
        {
            DO.OrderItem DOorderItem = new DO.OrderItem()
            {
                ID = 0,
                ProductID = productId,
                Price = product.Price,
                Amount = amount,
                OrderID = Order.ID
            };
            if (!Order.Items.Exists(item => item.ProductID == DOorderItem.ProductID))
            {
                try
                {
                    numberOrderItem = dal.OrderItem.Add(DOorderItem);
                }
                catch (DO.DalConfigException str)
                {
                    throw new BO.InternalProblem("Failed to add orderItem to data tier", str);
                }
                if (amount <= product.InStock)
                {
                    product.InStock -= amount;
                    dal.Product.Update(product);
                    BO.OrderItem orderItem = new BO.OrderItem
                    {
                        ID = numberOrderItem,
                        ProductID = productId,
                        Name = product.Name,
                        Price = product.Price,
                        Amount = amount,
                        TotalPrice = amount * product.Price
                    };
                    return Order;
                }
                throw new Exception("Not enough products in stock");
            }
            else
            {


                throw new Exception(" The item exsits in the Order");


            }

        }
        else
        {
            if (amount == 0)//delete item
            {
                DO.OrderItem DOorderItem = new DO.OrderItem();
                DOorderItem = dal.OrderItem.GetById(Order.Items.FirstOrDefault(item => item.ProductID == productId).ID);
                product.InStock += DOorderItem.Amount;
                dal.Product.Update(product);
                dal.OrderItem.Delete(DOorderItem.ID);
                BO.OrderItem orderItem = new BO.OrderItem
                {
                    ID = DOorderItem.ID,
                    ProductID = productId,
                    Name = product.Name,
                    Price = product.Price,
                    Amount = DOorderItem.Amount,
                    TotalPrice = DOorderItem.Price * DOorderItem.Amount
                };

                Order.Items.Remove(orderItem);
                return Order;

            }
            else// to change exsits item
            {
                DO.OrderItem DOorderItem = new DO.OrderItem();
                DOorderItem = dal.OrderItem.GetById(Order.Items.FirstOrDefault(item => item.ProductID == productId).ID);
                product.InStock += DOorderItem.Amount;
                DOorderItem.Amount = amount;
                if (amount <= product.InStock)
                {
                    product.InStock -= DOorderItem.Amount;
                    dal.Product.Update(product);
                    BO.OrderItem orderItem = new BO.OrderItem
                    {
                        ID = DOorderItem.ID,
                        ProductID = productId,
                        Name = product.Name,
                        Price = product.Price,
                        Amount = amount,
                        TotalPrice = amount * product.Price
                    };
                    var item = Order.Items.Where(item => item.ID == DOorderItem.ID).FirstOrDefault();
                    dal.OrderItem.Update(DOorderItem);
                    return Order;
                }
                throw new Exception("Not enough products in stock");

            }
        }


    }





    public int OrderForSimulator()// צריך להחזיר את ההזמנה עם הסטטוס הישן
    {


        var order = from item in dal.Order.GetAll(item => item?.ShipDate == null)
                    orderby item?.OrderDate
                    select item;
        while (order != null)
        {
            return order.First().Value.ID;

        }
        var order1 = from item in dal.Order.GetAll(item => item?.DeliveryDate == null)
                     orderby item?.DeliveryDate
                     select item;
        while (order1 != null)
        {
            return order1.First().Value.ID;
        }
        return 0;

    }


}