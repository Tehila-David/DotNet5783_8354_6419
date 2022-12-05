using BlApi;


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

    public IEnumerable<BO.ProductItem> GetProducts()
    {
        return Dal.Product.GetAll().Select(product => new BO.ProductItem
        {

            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = (BO.Category)(product.Category),
            InStock = product.InStock
        });
    }

    public BO.Product GetById(int id)
    {
        try
        {
            if (id < 0) { throw new BO.InternalProblem("ID not positive"); }
            DO.Product product = Dal?.Product.GetById(id) ?? throw new DO.NotExists("Got null product from data");
            return new BO.Product()
            {
                ID = product.ID,
                Category = (BO.Category)(product.Category), /*?? throw new BO.InternalProblemException("Missing product category")),*/
                Price = product.Price,
                Name = product.Name,
                InStock = product.InStock,
            };
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Missing product", ex);
        }
        
    }
        public BO.Product GetById1(int id)
    {
        try
        {
            if (id < 0) { throw new BO.InternalProblem("ID not positive"); }
            DO.Product product = Dal?.Product.GetById(id) ?? throw new DO.NotExists("Got null product from data");
            return new BO.Product()
            {
                ID = product.ID,
                Category = (BO.Category)(product.Category), /*?? throw new BO.InternalProblemException("Missing product category")),*/
                Price = product.Price,
                Name = product.Name,
                InStock = product.InStock,
            };
        }
        catch (DO.NotExists ex)
        {
            throw new BO.InternalProblem("Missing product", ex);
        }
    }
   
    public void Add(BO.Product product)
    {
        if (product.ID < 0) { throw new BO.InternalProblem("ID not positive"); }
        if (product.Name == "") { throw new BO.InternalProblem("The String is empty"); }
        if (product.Price < 0) { throw new BO.InternalProblem("The Price is negative"); }
        if(product.InStock < 0) { throw new BO.InternalProblem("The Amount is negative"); }
        Dal.Product.Add(new DO.Product
        {

            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = (DO.Category)(product.Category),
            InStock = product.InStock
        });

    }
    
    public void Update(BO.Product product)
    {

    }
   
    public void Delete(BO.Product product)
    {

    }

}
