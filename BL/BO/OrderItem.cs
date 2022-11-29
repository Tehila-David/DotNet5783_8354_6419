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
        /// 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Name { get; set; }
        public int ProductID { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int TotalPrice { get; set; }



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