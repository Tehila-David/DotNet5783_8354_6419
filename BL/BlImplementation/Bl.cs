﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BlImplementation;

internal class Bl: IBl
{
    internal Bl() { }
    public IOrder Order { get;} = new Order();
    public IProduct Product { get;} = new Product();
    public ICart Cart { get;} = new Cart();

}
