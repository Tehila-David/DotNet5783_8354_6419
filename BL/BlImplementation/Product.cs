﻿using BlApi;
using BO;
using DalApi;
using DO;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    DalApi.IDal? dal = DalApi.Factory.Get();


    [MethodImpl(MethodImplOptions.Synchronized)]
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
            }).OrderBy(item => item.ID);
        }
        else//if Predicate !=null
        {
            return from item in dal.Product.GetAll(predicate)
                   orderby item?.Name //מיון לפי אב
                   select new BO.ProductForList
                   {
                       ID = item?.ID ?? throw new NullReferenceException("Missing ID"),
                       Name = item?.Name,
                       Price = item?.Price ?? 0d,
                       Category = (BO.Category)item?.Category
                   };
        }

    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductItem?> GetListProductItem( BO.Cart myCart , Func<DO.Product?, bool>? predicate = null  )
    {

        if (predicate == null)
        {
            // return  IEnumerable
            return dal.Product.GetAll().Select(product => new BO.ProductItem
            {

                ID = product?.ID ?? throw new NullReferenceException("Missing ID"),
                Name = product?.Name,
                Price = product?.Price ?? 0d,
                Category = (BO.Category)product?.Category,
                Amount = myCart.Items == null ? 0 : myCart.Items.FindAll(orderItem => orderItem.ProductID == product?.ID).Sum(item=>item.Amount),
                IsAvailable = (product?.InStock > 0) ? true : false
            }).OrderBy(item => item.ID);//sort by ID
        }
        else //if Predicate !=null
        {
            return dal.Product.GetAll(predicate).Select(product => new BO.ProductItem
            {

                ID = product?.ID ?? throw new NullReferenceException("Missing ID"),
                Name = product?.Name,
                Price = product?.Price ?? 0d,
                Category = (BO.Category)product?.Category,
                Amount = myCart.Items == null ? 0 : myCart.Items.FindAll(orderItem => orderItem.ProductID == product?.ID).Sum(item => item.Amount),
                IsAvailable = (product?.InStock > 0) ? true : false
            }).OrderBy(item => item.ID);//sort by ID
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
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
        catch (DO.NotExists ex)// not exists
        {
            throw new BO.InternalProblem("Sorry ,this product does not exists in the List ", ex);
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.ProductItem GetById1(int id, BO.Cart myCart)
    {
        try
        {
            if (id < 0) { throw new BO.InternalProblem("ID not positive"); }
            DO.Product product = dal?.Product.GetById(id) ?? throw new DO.NotExists("Sorry ,this product does not exist in the List");
            int amount;
            amount = myCart.Items.FindAll(orderItem => orderItem.ProductID == id).Count();

            BO.ProductItem productItem = new BO.ProductItem()
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Category = (BO.Category)(product.Category), 
                Amount = myCart.Items == null ? 0 : myCart.Items.FindAll(orderItem => orderItem.ProductID == id).Sum(item => item.Amount),
                IsAvailable = (product.InStock > 0) ? true : false,
            };
            return productItem;
        }
        catch (DO.NotExists ex)// not exists
        {
            throw new BO.InternalProblem("Sorry ,this product does not exist in the List ", ex);
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
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
        catch (DO.AlreadyExists ex)//order exists
        {
            throw new BO.InternalProblem("The order already exists", ex);
        }
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
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
        catch (DO.NotExists ex) // not exists
        {
            throw new BO.InternalProblem("Sorry ,this product does not exist in the List ", ex);
        }

    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        try
        {
            DO.Product product = dal?.Product.GetById(id) ?? throw new DO.NotExists("Sorry ,this product does not exist in the List of products");
           
            foreach(DO.Order? order in dal?.Order.GetAll())       
            {
                if(dal.OrderItem.GetAll(item=> item?.OrderID == order?.ID).Any(orderItem=>orderItem?.ProductID==id))
                {
                    throw new BO.InternalProblem("The product already ordered");
                }
            }
            
            dal.Product.Delete(id);
            return;
        }
        catch (DO.NotExists ex) // not exists
        {
            throw new BO.InternalProblem("Sorry ,this product does not exist in the List ", ex);
        }
    }
}

