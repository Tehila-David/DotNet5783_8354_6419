﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Product
    {
        /// <summary>
        /// Unique ID of Prudct
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Name of product
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Price of  Product
        /// </summary>
        public double? Price { get; set; }
        /// <summary>
        /// Category of product
        /// </summary>
        public Category Category { get; set; }
        /// <summary>
        /// Instock of product
        /// </summary>
        public int InStock { get; set; }


        public override string ToString() => $@"
        Product ID: {ID}
        Name: {Name} 
        category: {Category}
        Price: {Price}
        Amount in stock: {InStock}";
    }
}
