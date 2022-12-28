﻿using BlApi;
using BO;
using DalApi;
using DO;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
DalApi.IDal? dal = DalApi.Factory.Get();
    public IEnumerable<BO.ProductForList> GetListedProducts(Func<DO.Product?, bool>? predicate = null)
    {
        if (predicate == null)
        {
            // return  IEnumerable
            return dal.Product.GetAll().Select(product => new BO.ProductForList
            {

                ID = product?.ID ?? throw new NullReferenceException("Missing ID"),
                Name = product?.Name,
                Price = product?.Price ?? 0d,
                Category = (BO.Category)product?.Category,
            });
        }
        else
        {
            return from item in dal.Product.GetAll(predicate)
                   orderby item?.Name
                   select new BO.ProductForList
                   {
                       ID = item?.ID ?? throw new NullReferenceException("Missing ID"),
                       Name = item?.Name,
                       Price = item?.Price ?? 0d,
                       Category = (BO.Category)item?.Category
                   };
        }
        //מיון לפי אב

        
    }
    public BO.Product GetById(int id)
    {
        try
        {
            if (id < 0) { throw new BO.InternalProblem("ID not positive"); }
            DO.Product product = dal?.Product.GetById(id) ?? throw new DO.NotExists("Sorry ,this product does not exist in the List");
            return new BO.Product()
            {
                ID = product.ID,
                Category = (BO.Category)(product.Category), 
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
            DO.Product product = dal?.Product.GetById(id) ?? throw new DO.NotExists("Sorry ,this product does not exist in the List");
            int amount;
            //if in the cart product amount=0 
            //if(myCart.Items.FindAll(orderItem => orderItem.ProductID == id).Count() == 0)
            //{
            //    amount = 0;
            //}
            //else
            
            amount = myCart.Items.FindAll(orderItem => orderItem.ProductID == id).Count();
            
            BO.ProductItem productItem = new BO.ProductItem()
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Category = (BO.Category)(product.Category), /*?? throw new BO.InternalProblemException("Missing product category")),*/
                Amount = myCart.Items == null ? 0 : myCart.Items.FindAll(orderItem => orderItem.ProductID == id).Count(),
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
            //add product in data
            dal.Product.Add(new DO.Product
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
            //update from data
            dal.Product.Update(new DO.Product
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
        DO.Product product = dal?.Product.GetById(id) ?? throw new DO.NotExists("Sorry ,this product does not exist in the List of products");

        //foreach (DO.Order order in dal.Order.GetAll())
        //{
        //    if (dal.OrderItem.getListOrderItems(order.ID).Any(orderItem => orderItem?.ProductID == id))
        //    {
        //        throw new BO.InternalProblem("The product already exists");
        //    }
        //}
        var product1 =from order in dal.Order.GetAll()
                      from orderItem in dal.OrderItem.GetAll(item => item?.OrderID == order.Value.ID)
                      where  orderItem?.ProductID == id
                      select orderItem;
       if(product1 != null) { throw new BO.InternalProblem("The product already exists"); }
        //delete from data
        dal.Product.Delete(id);
    }
}
