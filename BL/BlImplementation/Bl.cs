using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BlImplementation;

internal class Bl: IBl
{
    internal Bl() { }
    public IOrder Order { get;} = new BlImplementation. Order();
    public IProduct Product { get;} = new BlImplementation.Product();
    public ICart Cart { get;} = new BlImplementation.Cart();
}
