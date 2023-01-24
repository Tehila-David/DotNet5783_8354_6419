
using DO;
using DalApi;
using System.Reflection.Metadata.Ecma335;
using System;
using System.Runtime.CompilerServices;

namespace Dal;

public class DalProduct : IProduct
{
    DataSource _dataSource = DataSource.s_instance;
   // [MethodImpl(MethodImplOptions.Synchronized)]
    public Product? GetById(Func<Product?, bool>? predicate)
    {
        return _dataSource.ProductList?.FirstOrDefault(predicate)
            ?? throw new NotExists("Sorry ,this product does not exist in the List ");
    }
    /// <summary>
    /// This function adds an Product
    /// </summary>
    /// 
   // [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Product myProduct)
    {
        if(_dataSource.ProductList.Exists(p => p?.ID== myProduct.ID))
             throw new AlreadyExists("The product already exists");
        _dataSource.ProductList.Add(myProduct);
        return myProduct.ID;
    }

    /// <summary>
    /// This function returns a product based on the useer input id
    /// </summary>
    /// 
   // [MethodImpl(MethodImplOptions.Synchronized)]
    public Product GetById(int id)
    {
        
       return _dataSource.ProductList?.FirstOrDefault(s=>s?.ID==id)
            ?? throw new NotExists("Sorry ,this product does not exist in the List ");
        
    }

    /// <summary>
    /// This function returns all of the products
    /// </summary>
    /// 
   // [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? predicate=null )
    {
        ///looking for all of the products that have their details filed in and returning them
        if (predicate == null) { return _dataSource.ProductList.AsEnumerable(); }
        return _dataSource.ProductList.Where(predicate);/*?? _dataSource.ProductList.AsEnumerable();*/ 
    }

    /// <summary>
    /// This function deletes a product from the list of products
    /// </summary>
    /// 
    //[MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int ID)
    {

        var product = (from item in _dataSource.ProductList
                       where item?.ID == ID
                       select item).FirstOrDefault();
        if (product != null)
        {
            _dataSource.ProductList.Remove(product);
            return;
        }
        // If the id the user entered was not found in the list 
        throw new NotExists("Sorry ,this product does not exist in the list ");

    }

    /// <summary>
    /// This function receives a product and updates an existing product with it
    /// </summary>
    /// 
    //[MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Product myProduct)
    {
        
        var product = (from item in _dataSource.ProductList
                       where item?.ID == myProduct.ID
                       select item).FirstOrDefault();
        if (product != null)
        {
            _dataSource.ProductList.Remove(product);
            _dataSource.ProductList.Add(myProduct);
            return;
        }

        //If the id of the product the user entered to be updated is not found in the list
        throw new NotExists("Product to be updated does not exist");
    }
}
