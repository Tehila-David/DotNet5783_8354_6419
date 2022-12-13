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
    public class Cart
    {

        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerAddress { get; set; }

        public List <OrderItem>? Items { get; set; }
        public double? TotalPrice { get; set; }

        public override string ToString()
        {
            string str = "";
            str += $"CustomerName: {CustomerName} \n";
            str += $"CustomerEmail: {CustomerEmail} \n";
            str += $"CustomerAddress: {CustomerAddress} \n";
            str += $"Items:  \n";
            foreach (var item in Items)
            {
                str += $"OrderItem: {item} \n";
            }
            str += $"CustomerName: {CustomerName} \n";
            str += $"TotalPrice: {TotalPrice} \n";

            return str;
        }
    }
}
