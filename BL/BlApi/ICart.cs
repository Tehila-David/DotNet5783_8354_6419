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
        /// <summary>
        /// Update amount of product in cart
        /// </summary>
        /// <param name="myCart"></param>
        /// <param name="productId"></param>
        /// <param name="newAmount"></param>
        /// <returns></returns>
        public BO.Cart UpdateProductAmount(BO.Cart myCart, int productId, int newAmount);
        /// <summary>
        /// Add product to cart
        /// </summary>
        /// <param name="myCart"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public BO.Cart AddProduct(BO.Cart myCart, int productId);
        /// <summary>
        /// Confirmation of cart
        /// </summary>
        /// <param name="myCart"></param>
        public void CartConfirmation(BO.Cart myCart);
    }
}
