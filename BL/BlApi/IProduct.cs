using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

public interface IProduct
{
    /// <summary>
    ///  for the manager , return list of ProductForList
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ProductForList> GetListedProducts();
    /// <summary>
    ///for the customer, return list of productItem
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IEnumerable<ProductItem> GetProducts();
    /// <summary>
    /// for the manager , return details of product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Product GetById(int id);
    /// <summary>
    /// for the customer , return details of product
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Product GetById1(int id);
    /// <summary>
    /// for the manager, add product to list
    /// </summary>
    /// <param name="product"></param>
    public void Add (BO.Product product);
    /// <summary>
    /// for the manager, update product from list
    /// </summary>
    /// <param name="product"></param>
    public void Update (BO.Product product);
    /// <summary>
    /// for the manager ,delete product from list
    /// </summary>
    /// <param name="product"></param>
    public void Delete (int id);

}
