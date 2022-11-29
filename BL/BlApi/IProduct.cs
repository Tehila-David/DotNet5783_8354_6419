using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DO;

namespace BlApi;

public interface IProduct
{
    //public int Add(BO.Product myProduct);
    //public BO.Product GetById(int id);
    //public IEnumerable<BO.Product> GetAll();
    //public void Delete(int ID);
    //public void Update(BO.Product myProduct);
    public IEnumerable<BO.Product> GetListedProducts();
    public BO.Product GetById(int id);
    IEnumerable<ProductItem1> GetProducts();

}
