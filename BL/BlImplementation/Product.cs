using BlApi;


using Dal;
using DalApi;

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
            if(id < 0) { throw new BO.InternalProblemException("ID not positive "); }
            DO.Product product = Dal?.Product.GetById(id) ?? throw new BO.InternalProblemException("Got null product from data");
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
            throw new BO.DoesNotExistException("Missing product", ex);
        }
    }
     public IEnumerable<BO.ProductItem> GetProducts()
    {
        return Dal.Product.GetAll().Select(product => new BO.ProductItem
        {

            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = (BO.Category)(product.Category),
            InStock=product.InStock
        });
    }
}
