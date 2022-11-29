using BlImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    public interface ICart
    {
     
        public void UpdateTotalSum(BO.Cart myCart);
        BO.Cart AddProduct(BO.Cart myCart, int productId);

    }
}
