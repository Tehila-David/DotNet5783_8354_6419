using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IBl
{
    public IOrder Order { get; }
    public ICart OrderItem { get; }
    public IProduct Product { get; }
    //public IOrder Order { get; }
    //public IOrder Order { get; }
   // public IOrder Order { get; }
    
}
