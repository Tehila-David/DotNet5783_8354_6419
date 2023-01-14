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
    internal sealed class DalXml : IDal
    {
        public static IDal Instance { get; } = new DalXml();
        public IOrder Order { get; }
        public IProduct Product { get; }
        public IOrderItem OrderItem { get; }
    }
}