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
        /// for the manager, return 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrderForList> GetListedOrders();
       
        public IEnumerable<ProductItem> GetProducts();
    }
}
