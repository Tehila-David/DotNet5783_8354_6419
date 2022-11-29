using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IOrder
    {
        public int Add(Order myOrder);
        public Order GetById(int id);
        public IEnumerable<Order> GetAll();
        public void Delete(int ID);
        public void Update(Order myOrdert);
    }
}
