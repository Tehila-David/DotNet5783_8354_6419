﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BlImplementation;

internal class Bl: IBl
{
    public Bl() { }
    public IOrder Order { get; set; } = new Order();
    public IProduct Product { get; set; } = new Product();
    public ICart Cart { get; set; } = new Cart();

}
