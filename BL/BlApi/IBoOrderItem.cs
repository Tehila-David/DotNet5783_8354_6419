using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IBoOrderItem
    {
        public int Add(OrderItem myOrderItem);
        public OrderItem GetById(int id);
        public IEnumerable<OrderItem> GetAll();
        public void Delete(int ID);
        public void Update(OrderItem myOrderItem);
        public OrderItem getOrderItemBasedOnProducIDAndOrderID(int idOrder, int idProduct);
        public IEnumerable<OrderItem> getListOfOrderItemsBasedOnOrderID(int idOrder);

    }
}
