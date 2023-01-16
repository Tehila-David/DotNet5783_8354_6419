using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;


namespace Dal
{
    sealed internal class DalXml : IDal
    {
        private DalXml() { }
        public static IDal Instance { get; } = new DalXml();
        public IOrder Order { get; } = new Order();
        public IProduct Product { get; } = new Product();
        public IOrderItem OrderItem { get; } = new OrderItem();

    }
}