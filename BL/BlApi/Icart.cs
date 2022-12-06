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
     
        public void UpdateTotalSum(BO.Cart myCart);
        public BO.Cart AddProduct(BO.Cart myCart, int productId);

    }
}
