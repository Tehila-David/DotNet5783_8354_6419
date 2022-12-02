using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

public interface IProduct
{
    public IEnumerable <BO.Product> GetListedProducts();
    public BO.Product GetById(int id);
    public IEnumerable<ProductItem> GetProducts();


}
