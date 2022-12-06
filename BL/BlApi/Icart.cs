using BlImplementation;
using DO;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface ICart
    {
     
        public BO.Cart UpdateProductAmount(BO.Cart myCart, int productId, int newAmount);
        public BO.Cart AddProduct(BO.Cart myCart, int productId);
        public void UpdateTotalSum(BO.Cart myCart);
    }
}
