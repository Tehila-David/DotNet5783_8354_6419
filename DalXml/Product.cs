﻿using System;
using DalApi;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using DO;
using System.Linq;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Text;
using System.Runtime.CompilerServices;

//implement IProduct with xelement
namespace Dal
{
    internal class Product : IProduct
    {
        string s_products = @"Products";

        /// <summary>
        /// Create the xelement of Product
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        static DO.Product? getProduct(XElement s) =>
        s.ToIntNullable("ID") is null ? null : new DO.Product()
        {
            ID = (int)s.ToIntNullable("ID")!,
            Name = (string?)s.Element("Name"),
            Price = (double)s.ToDoubleNullable("Price"),
            InStock = (int)s.ToIntNullable("InStock"),
            Category = s.ToEnumNullable<DO.Category>("Category"),
        };




#pragma warning restore CS8604 // Possible null reference argument.
        /// <summary>
        /// Create the List xelement of Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        static IEnumerable<XElement> createProductElement(DO.Product product)
        {
            yield return new XElement("ID", product.ID);
            yield return new XElement("Price", product.Price);
            yield return new XElement("InStock", product.InStock);
            if (product.Name is not null)
                yield return new XElement("Name", product.Name);          
            if (product.Category is not null)
                yield return new XElement("Category", product.Category);
            
                
        }



        /// <summary>
        /// Return list of products by Filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? filter = null) =>
            filter is null
            ? XMLTools.LoadListFromXMLElement(s_products).Elements().Select(s => getProduct(s))
            : XMLTools.LoadListFromXMLElement(s_products).Elements().Select(s => getProduct(s)).Where(filter);



        /// <summary>
        /// Return product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Product GetById( int id) =>
            (DO.Product)getProduct(XMLTools.LoadListFromXMLElement(s_products)?.Elements()
            .FirstOrDefault(st => st.ToIntNullable("ID") == id)
            // fix to: throw new DalMissingIdException(id);
            ?? throw new Exception("missing id"))!;



        /// <summary>
        /// Return product by Predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DO.Product? GetById(Func<DO.Product?, bool>? predicate)
        {
            var productsList = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products)!;
            if (productsList.FirstOrDefault(predicate) == null)
            {
                throw new Exception("Product not Exsits");
            }
            return productsList.FirstOrDefault(predicate);
        }



        /// <summary>
        /// Add product to list
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(DO.Product product)
        {
            XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);

            if (XMLTools.LoadListFromXMLElement(s_products)?.Elements()
                .FirstOrDefault(st => st.ToIntNullable("ID") == product.ID) is not null)
                // fix to: throw new DalMissingIdException(id);;
                throw new Exception("id already exist");

            productsRootElem.Add(new XElement("Product", createProductElement(product)));
            XMLTools.SaveListToXMLElement(productsRootElem, s_products);

            return product.ID; ;
        }



        /// <summary>
        /// Delete  product from list
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int id)
        {
            XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);

            (productsRootElem.Elements()
                // fix to: throw new DalMissingIdException(id);
                .FirstOrDefault(st => (int?)st.Element("ID") == id) ?? throw new Exception("missing id"))
                .Remove();

            XMLTools.SaveListToXMLElement(productsRootElem, s_products);
        }



        /// <summary>
        ///  Update product from List
        /// </summary>
        /// <param name="product"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(DO.Product product)
        {

            Delete(product.ID);
            Add(product);
        }

    }
}
