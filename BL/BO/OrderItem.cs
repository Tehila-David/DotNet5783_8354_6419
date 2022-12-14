using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderItem
    {
        /// <summary>
        /// ID of order item
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// name of product
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// ID of product
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// price of product
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// amount of products in order
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// total price of all products in order
        /// </summary>
        public double TotalPrice { get; set; }



        public override string ToString() => $@"
        ID:{ID},
        Name: {Name},
        Product Id: {ProductID},
        Price per product: {Price},
        Amount: {Amount}
        TotalPrice: {TotalPrice}
        ";
    }
}