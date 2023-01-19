using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;


namespace BlApi
{
    public interface IOrder
    {
        /// <summary>
        /// for the manager, return list of orders
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrderForList?> GetListedOrders(Func<DO.Order?, bool>? predicate = null);
        /// <summary>
        /// for the manager and customer , retrun details of Order
        /// </summary>
        public BO.Order GetByID(int id);
        /// <summary>
        /// func for help,get id of order and return list of BO.OrderItem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<BO.OrderItem?> getDoOrderItem(int id);
        /// <summary>
        /// for the manager , update DeliveryDate and return Order
        /// </summary>
        public BO.Order UpdateDelivery(int id);
        /// <summary>
        /// for the manager , update ShipDate and return Order
        /// </summary>
        public BO.Order UpdateShipDate(int id);
        /// <summary>
        /// for the manager, return Order tracking
        /// </summary>
        public BO.OrderTracking followOrder(int id);
        /// <summary>
        /// for help in PL 
        /// </summary>
        /// <param name="Order"></param>
        /// <param name="productId"></param>
        /// <param name="amount"></param>
        public BO.OrderItem UpdateItems(BO.Order Order, int productId, int amount);
        /// <summary>
        /// for simulator , return the oldest Order
        /// </summary>
        /// <returns></returns>
        public int? OrderForSimulator();

       

    }
}
