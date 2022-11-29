using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IOrderItem
    {
        public int Add(BO.OrderItem myOrderItem);
        public BO.OrderItem GetById(int id);
        public IEnumerable<BO.OrderItem> GetAll();
        public void Delete(int ID);
        public void Update(BO.OrderItem myOrderItem);
        public BO.OrderItem getOrderItemBasedOnProducIDAndOrderID(int idOrder, int idProduct);
        public IEnumerable<BO.OrderItem> getListOfOrderItemsBasedOnOrderID(int idOrder);

    }
}
