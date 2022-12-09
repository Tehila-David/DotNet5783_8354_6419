﻿using BlApi;

using Dal;
using DalApi;
using System.Diagnostics;
using System.Xml.Linq;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    //private static readonly IDal? dal = Factory.Get();
   IDal Dal = new DalList();
    public IEnumerable<BO.ProductForList> GetListedProducts()
    {
        return Dal.Product.GetAll().Select(product => new BO.ProductForList
        {

            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = (BO.Category)product.Category,
        });
    }


    public BO.Product GetById(int id)
    {
        try
        {
            if (id < 0) { throw new BO.InternalProblem("ID not positive"); }
            DO.Product product = Dal?.Product.GetById(id) ?? throw new DO.NotExists("");
            return new BO.Product()
            {
                ID = product.ID,
                Category = (BO.Category)(product.Category), /*?? throw new BO.InternalProblemException("Missing product category")),*/
                Price = product.Price,
                Name = product.Name,
                InStock = product.InStock
            };
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Sorry ,this product does not exist in the List ", ex);
        }

    }
    public BO.ProductItem GetById1(int id , BO.Cart myCart)
    {
        try
        {
            if (id < 0) { throw new BO.InternalProblem("ID not positive"); }
            DO.Product product = Dal?.Product.GetById(id) ?? throw new DO.NotExists("Sorry ,this product does not exist in the List");
            int amount;
            if (myCart.Items == null) { amount = 0; }
            else { amount = myCart.Items.FindAll(orderItem => orderItem.ProductID == id).Count(); }
            BO.ProductItem productItem = new BO.ProductItem()
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Category = (BO.Category)(product.Category), /*?? throw new BO.InternalProblemException("Missing product category")),*/
                Amount = amount/*myCart.Items.Count() == 0 ? 0: myCart.Items.FindAll(orderItem => orderItem.ProductID == id).Count()*/,
                IsAvailable = (product.InStock > 0) ? true : false,
            };
            return productItem;
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Sorry ,this product does not exist in the List ", ex);
        }
    }

    public void Add(BO.Product product)
    {
        try
        {
            if (product.ID < 0) { throw new BO.InternalProblem("ID not positive"); }
            if (String.IsNullOrEmpty(product.Name)) { throw new BO.InternalProblem("The String is empty"); }
            if (product.Price < 0) { throw new BO.InternalProblem("The Price is negative"); }
            if (product.InStock < 0) { throw new BO.InternalProblem("The Amount is negative"); }
            Dal.Product.Add(new DO.Product
            {

                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Category = (DO.Category)(product.Category),
                InStock = product.InStock
            });
        }
        catch (DO.AlreadyExists ex)
        {
            throw new BO.InternalProblem("The product already exists", ex);
        }
    }

    public void Update(BO.Product product)
    {
        try
        {
            if (product.ID < 0) { throw new BO.InternalProblem("ID not positive"); }
            if (product.Name == "") { throw new BO.InternalProblem("The String is empty"); }
            if (product.Price < 0) { throw new BO.InternalProblem("The Price is negative"); }
            if (product.InStock < 0) { throw new BO.InternalProblem("The Amount is negative"); }
            Dal.Product.Update(new DO.Product
            {

                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Category = (DO.Category)(product.Category),
                InStock = product.InStock
            });
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Sorry ,this product does not exist in the List ", ex);
        }

    }

    public void Delete(int id)
    {
        DO.Product product = Dal?.Product.GetById(id) ?? throw new DO.NotExists("Sorry ,this product does not exist in the List of products");

        foreach (DO.Order order in Dal.Order.GetAll())
        {
                if (Dal.OrderItem.getListOrderItems(order.ID).Any(orderItem => orderItem.ProductID == id))
                {
                    throw new BO.InternalProblem("The product already exists");
                }
        }
        Dal.Product.Delete(id);
    }
}
