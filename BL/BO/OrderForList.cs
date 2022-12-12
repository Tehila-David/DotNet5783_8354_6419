using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderForList
    {
        /// <summary>
        /// ID of order
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The name of customer
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// Status of order
        /// </summary>
        public OrderStatus? Status { get; set; }
        /// <summary>
        /// Amount of total products
        /// </summary>
        public int AmountOfItems { get; set; }
        /// <summary>
        /// Amount of final price on order
        /// </summary>
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
        ID:{ID},
        Costumer Name: {CustomerName},
        Status: {Status},
        AmountOfItems: {AmountOfItems},
        TotalPrice: {TotalPrice},
        ";

    }
}
