using DO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO
{
    public class OrderTracking
    {   /// <summary>
        /// ID of order
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// status of order
        /// </summary>
        public OrderStatus? Status { get; set; }
        /// <summary>
        /// Tuple list of date and Status
        /// </summary>
        public List<Tuple<DateTime, string>>? Tracking { get; set; }
       
        public override string ToString()
        {
            string str = "";
            str += $"Product ID: {ID} \n";
            str += $"Status: {Status} \n";
            str += $"Tracking:  \n";
            foreach (var tuple in Tracking)
            {
                str += $" {tuple.Item1} - {tuple.Item2}\n ";
            }
            return str;
        }
       
    }
}
