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
    {
        public int ID { get; set; }
        public OrderStatus? Status { get; set; }
        public List<Tuple<DateTime, string>>? Tracking { get; set; }
        public override string ToString() => $@"
        Product ID: {ID}
        Status: {Status}
        Tracking: {Tracking}";
    }
}
