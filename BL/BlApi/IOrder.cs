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
        public int Add(BO.Order myOrder);
        public BO.Order GetById(int id);
        public IEnumerable<BO.Order> GetAll();
        public void Delete(int ID);
        public void Update(BO.Order myOrdert);
    }
}
